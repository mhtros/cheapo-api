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

        // Scoped - lifetime services are created once per request
        services.AddScoped<IApplicationInternalErrorRepository, ApplicationInternalErrorRepository>();

        // Singleton - lifetime services are created the first time they are requested
        // and then every subsequent request will use the same instance
        var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
        services.AddSingleton(emailSettings);
    }
}