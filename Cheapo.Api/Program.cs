using Cheapo.Api.Data;
using Cheapo.Api.Extensions.Services;
using Cheapo.Api.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerService();

builder.Services.AddDbContextService(configuration);

builder.Services.AddIdentityService();

builder.Services.AddDependencyInjectionService(configuration);

builder.Services.AddJwtAuthenticationService(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<InternalErrorHandler>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

// app.UseCors(policyBuilder =>
// {
//     policyBuilder
//         .AllowAnyHeader()
//         .AllowAnyMethod()
//         .AllowCredentials()
//         .WithOrigins(configuration.GetSection("ClientUri").Get<string>());
// });

app.UseAuthentication();

app.UseAuthorization();

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllers();
    endpoint.MapFallbackToController("Index", "Fallback");
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();