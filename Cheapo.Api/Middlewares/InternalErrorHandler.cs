using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text.Json;
using Cheapo.Api.Entities;
using Cheapo.Api.Extensions;
using Cheapo.Api.Interfaces.Repositories;
using Cheapo.Api.Interfaces.Services;

namespace Cheapo.Api.Middlewares;

/// <summary>
///     Catches server internal errors (500) globally and store the data
///     as file in computer or inside the database (or both).
///     On Developer mode shows additional information in the http response.
/// </summary>
public class InternalErrorHandler
{
    private readonly ISaveToFile _fileService;
    private readonly bool _isDevelopment;
    private readonly ILogger<InternalErrorHandler> _logger;
    private readonly RequestDelegate _next;
    private readonly bool _storeToDb;
    private readonly bool _storeToFile;
    private IApplicationInternalErrorRepository _applicationInternalErrors = null!;
    private HttpContext _context = null!;

    private ApplicationInternalError _error = new();

    public InternalErrorHandler(RequestDelegate next, ILogger<InternalErrorHandler> logger,
        IHostEnvironment environment, IConfiguration configuration, ISaveToFile fileService)
    {
        _isDevelopment = environment.IsDevelopment();
        _fileService = fileService;
        _logger = logger;
        _next = next;
        _storeToDb = configuration.GetSection("StoreErrorsToDb").Get<bool>();
        _storeToFile = configuration.GetSection("StoreErrorsToFile").Get<bool>();
    }

    public async Task InvokeAsync(HttpContext context, IApplicationInternalErrorRepository applicationInternalErrors)
    {
        _context = context;
        _applicationInternalErrors = applicationInternalErrors;

        try
        {
            // Ensure the request body can be read multiple times.
            _context.Request.EnableBuffering();

            // Call the next method in the middleware pipeline.
            await _next(_context);
        }
        catch (Exception ex)
        {
            _error = new ApplicationInternalError
            {
                Id = Activity.Current?.Id ?? _context.TraceIdentifier,
                ErrorMessage = ex.Message,
                StatusCode = _context.Response.StatusCode,
                OccurrenceDate = DateTime.UtcNow,
                RequestHeaders = _context.ExtractHeaders(),
                RequestBody = await _context.ExtractBodyAsync(),
                ControllerName = _context.Request.Path.ToString(),
                MethodType = _context.Request.Method,
                ExceptionType = ex.GetType().ToString(),
                StackTrace = ex.StackTrace
            };

            await StoreInternalErrorAsync();

            // The error message will be logged from the ex object.
            // The message param is intended to be a format string, you can check that in the method summary. 
            _logger.LogError(ex, "");

            await CreateResponseAsync();
        }
    }

    private async Task StoreInternalErrorAsync()
    {
        if (!_storeToDb || !_storeToFile) return;

        if (_storeToDb)
            await _applicationInternalErrors.SaveErrorAsync(_error);

        if (_storeToFile)
        {
            var content = CreateFileContent();

            var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var path = Path.Combine(userProfile, "cheapo_logs");
            var filename = $"log_{DateTime.UtcNow:yy_MM_dd}";

            await _fileService.SaveToFileAsync(path, filename, content);
        }
    }

    private string CreateFileContent()
    {
        return $"Trace Id: {_error.Id}"
               + $"\nDate: {_error.OccurrenceDate.ToString(CultureInfo.InvariantCulture)}"
               + $"\nException Type: {_error.ExceptionType}"
               + $"\nMethod Type: {_error.MethodType}"
               + $"\nControl Name: {_error.ControllerName}"
               + $"\nRequest Body: {_error.RequestBody}"
               + $"\nRequest Headers: {_error.RequestHeaders}"
               + $"\nStack Trace: {_error.StackTrace}"
               + "\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";
    }

    private async Task CreateResponseAsync()
    {
        _context.Response.ContentType = "application/json";
        _context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            StatusCodes = _error.StatusCode,
            TraceId = _error.Id,
            Message = _isDevelopment ? "Internal Server Error" : _error.ErrorMessage,
            StackTrace = _isDevelopment ? string.Empty : _error.StackTrace
        };

        var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var json = JsonSerializer.Serialize(response, option);

        await _context.Response.WriteAsync(json);
    }
}