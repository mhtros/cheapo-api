using Cheapo.Api.Data;
using Cheapo.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace Cheapo.Api.Extensions.Services;

public static class IdentityRegistrator
{
    /// <summary>
    ///     Registers the identity services in the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection" />.</param>
    public static void AddIdentityService(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                // SignIn settings
                options.SignIn.RequireConfirmedEmail = true;
            })
            // Adds the default Stores: UserStore and RoleStore.
            // Which add helping methods for managing an IdentityUser instance e.g. SetUserNameAsync
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
}