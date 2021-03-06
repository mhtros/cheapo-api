using System.Text;
using Cheapo.Api.Classes;
using Cheapo.Api.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Cheapo.Api.Extensions.Services;

public static class JwtAuthenticationRegistrator
{
    /// <summary>
    ///     Adds JWT authentication.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection" />.</param>
    /// <param name="configuration"><see cref="IConfiguration" />.</param>
    public static void AddJwtAuthenticationService(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtParameters = new JwtParameters
        {
            Audience = configuration.GetSection("Audience").Get<string>(),
            Issuer = configuration.GetSection("Issuer").Get<string>(),
            ValidateAudience = configuration.GetSection("ValidateAudience").Get<bool>(),
            ValidateIssuer = configuration.GetSection("ValidateIssuer").Get<bool>(),
            ValidateLifetime = configuration.GetSection("ValidateLifetime").Get<bool>(),
            IssuerSigningKey = configuration.GetSection("IssuerSigningKey").Get<string>(),
            RequireExpirationTime = configuration.GetSection("RequireExpirationTime").Get<bool>(),
            ValidateIssuerSigningKey = configuration.GetSection("ValidateIssuerSigningKey").Get<bool>(),
        };

        services.AddScoped<IJwtParameters, JwtParameters>(_ => jwtParameters);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var key = Encoding.UTF8.GetBytes(jwtParameters.IssuerSigningKey!);

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidIssuer = jwtParameters.Issuer,
                    ValidAudience = jwtParameters.Audience,
                    RequireExpirationTime = jwtParameters.RequireExpirationTime,
                    ValidateIssuerSigningKey = jwtParameters.ValidateIssuerSigningKey,
                    ValidateIssuer = jwtParameters.ValidateIssuer,
                    ValidateAudience = jwtParameters.ValidateAudience,
                    ValidateLifetime = jwtParameters.ValidateLifetime
                };
            });
    }
}