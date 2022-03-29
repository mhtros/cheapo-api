using Cheapo.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Cheapo.Api.Extensions.Services;

public static class DbContextRegistrator
{
    /// <summary>
    ///     Registers the database context as a service in the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection" />.</param>
    /// <param name="configuration"><see cref="IConfiguration" />.</param>
    public static void AddDbContextService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString);
        });
    }
}