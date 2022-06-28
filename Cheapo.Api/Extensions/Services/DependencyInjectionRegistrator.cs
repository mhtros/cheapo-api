using Cheapo.Api.Classes;
using Cheapo.Api.Classes.Repositories;
using Cheapo.Api.Classes.Services;
using Cheapo.Api.Interfaces.Repositories;
using Cheapo.Api.Interfaces.Services;

namespace Cheapo.Api.Extensions.Services;

public static class DependencyInjectionRegistrator
{
    /// <summary>
    ///     Add additional services as dependency injection in the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection" />.</param>
    /// <param name="configuration"><see cref="IConfiguration" />.</param>
    public static void AddDependencyInjectionService(this IServiceCollection services, IConfiguration configuration)
    {
        // Transient - lifetime services are created each time they are requested
        services.AddTransient<ISaveToFile, SaveToFile>();
        services.AddTransient<IThumbnailGenerator, ThumbnailGenerator>();
        services.AddTransient<IEmailSender, EmailSender>();

        // Scoped - lifetime services are created once per request
        services.AddScoped<IApplicationInternalErrorRepository, ApplicationInternalErrorRepository>();
        services.AddScoped<IApplicationTransactionCategoriesRepository, ApplicationTransactionCategoriesRepository>();
        services.AddScoped<IApplicationTransactionRepository, ApplicationTransactionRepository>();

        // Singleton - lifetime services are created the first time they are requested
        // and then every subsequent request will use the same instance.
        var emailSettings = new EmailSettings
        {
            From = configuration.GetSection("From").Get<string>(),
            Password = configuration.GetSection("Password").Get<string>(),
            Port = configuration.GetSection("Port").Get<int>(),
            Username = configuration.GetSection("Username").Get<string>(),
            SmtpServer = configuration.GetSection("SmtpServer").Get<string>()
        };

        services.AddSingleton(emailSettings);
    }
}