using Cheapo.Api.Extensions.Services;
using Cheapo.Api.Middlewares;

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

app.UseCors(policyBuilder =>
{
    policyBuilder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .WithOrigins(configuration.GetSection("ClientUri").Get<string>());
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();