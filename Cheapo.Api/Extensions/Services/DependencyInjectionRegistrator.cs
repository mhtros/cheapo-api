using Cheapo.Api.Classes.Repositories;
using Cheapo.Api.Classes.Services;
using Cheapo.Api.Interfaces.Repositories;
using Cheapo.Api.Interfaces.Services;

namespace Cheapo.Api.Extensions.Services;

public static class DependencyInjectionRegistrator
{
    /// <summary>
    /// Add additional services as dependency injection in the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    public static void AddDependencyInjectionService(this IServiceCollection services)
    {
        // Transient - lifetime services are created each time they are requested
        services.AddTransient<ISaveToFile, SaveToFile>();

        // Scoped - lifetime services are created once per request
        services.AddScoped<IApplicationInternalErrorRepository, ApplicationInternalErrorRepository>();

        // Singleton - lifetime services are created the first time they are requested
        // and then every subsequent request will use the same instance
    }
}