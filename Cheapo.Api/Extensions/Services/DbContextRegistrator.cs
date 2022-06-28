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
            string connectionString;
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (env == "Development")
            {
                connectionString = configuration.GetConnectionString("DefaultConnection");
            }
            else
            {
                // Use connection string provided at runtime by Heroku.
                var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                // Parse connection URL to connection string for Npgsql
                connectionUrl = connectionUrl?.Replace("postgres://", string.Empty);
                var pgUserPass = connectionUrl?.Split("@")[0];
                var pgHostPortDb = connectionUrl?.Split("@")[1];
                var pgHostPort = pgHostPortDb?.Split("/")[0];
                var pgDb = pgHostPortDb?.Split("/")[1];
                var pgUser = pgUserPass?.Split(":")[0];
                var pgPass = pgUserPass?.Split(":")[1];
                var pgHost = pgHostPort?.Split(":")[0];
                var pgPort = pgHostPort?.Split(":")[1];

                connectionString =
                    $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;TrustServerCertificate=True";
            }

            options.UseNpgsql(connectionString);
        });
    }
}