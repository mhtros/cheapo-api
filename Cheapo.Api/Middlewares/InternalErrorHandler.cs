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
        _applicationInternalErrors = applicationInternalErrors;

        try
        {
            // Ensure the request body can be read multiple times.
            context.Request.EnableBuffering();

            // Call the next method in the middleware pipeline.
            await _next(context);
        }
        catch (Exception ex)
        {
            _error = new ApplicationInternalError
            {
                Id = Activity.Current?.Id ?? context.TraceIdentifier,
                ErrorMessage = ex.Message,
                OccurrenceDate = DateTime.UtcNow,
                RequestHeaders = context.ExtractHeaders(),
                RequestBody = await context.ExtractBodyAsync(),
                ControllerName = context.Request.Path.ToString(),
                MethodType = context.Request.Method,
                ExceptionType = ex.GetType().ToString(),
                StackTrace = ex.StackTrace
            };

            await StoreInternalErrorAsync();

            // The error message will be logged from the ex object.
            // The message param is intended to be a format string, you can check that in the method summary. 
            _logger.LogError(ex, "");

            await CreateResponseAsync(context);
        }
    }

    private async Task StoreInternalErrorAsync()
    {
        if (_storeToDb)
        {
            await _applicationInternalErrors.AddAsync(_error);
            await _applicationInternalErrors.SaveAsync();
        }

        if (_storeToFile)
        {
            var content = CreateFileContent();

            var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var path = Path.Combine(userProfile, "cheapo_logs");
            var filename = $"log_{DateTime.UtcNow:yy_MM_dd}.txt";

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

    private async Task CreateResponseAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            StatusCodes = context.Response.StatusCode,
            Message = _isDevelopment ? "Internal Server Error" : _error.ErrorMessage,
            StackTrace = _isDevelopment ? string.Empty : _error.StackTrace
        };

        var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var json = JsonSerializer.Serialize(response, option);

        await context.Response.WriteAsync(json);
    }
}