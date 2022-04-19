using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Cheapo.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Cheapo.Api.Classes.Attributes;

/// <summary>
///     Custom Attribute to grant request access only to authorize users.
///     It will check if the header contains the key value "Authorization" if not the user is not authorized.
///     If the "Authorization" key exists then the second step is to check if the date of the token is expired.
///     If it's expired then the user in not authorized.
/// </summary>
public class JwtAuthenticationAttribute : TypeFilterAttribute
{
    public JwtAuthenticationAttribute() : base(typeof(AuthorizationFilter))
    {
    }

    private class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly IJwtParameters _jwtParams;

        public AuthorizationFilter(IJwtParameters jwtParams)
        {
            _jwtParams = jwtParams;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"]
                .FirstOrDefault()?
                .Split(" ")
                .Last();

            if (string.IsNullOrWhiteSpace(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                ValidateToken(token);
            }
            catch (Exception e)
            {
                context.Result = e is SecurityTokenExpiredException
                    ? new UnauthorizedObjectResult(new ErrorResponse(new[] {Errors.ExpiredToken}))
                    : new UnauthorizedResult();
            }
        }

        private void ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _jwtParams.IssuerSigningKey ?? string.Empty;

            var parameters = new TokenValidationParameters
            {
                ValidateLifetime = _jwtParams.ValidateLifetime,
                ValidateAudience = _jwtParams.ValidateAudience,
                ValidateIssuer = _jwtParams.ValidateIssuer,
                ValidIssuer = _jwtParams.Issuer,
                ValidAudience = _jwtParams.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };

            tokenHandler.ValidateToken(authToken, parameters, out _);
        }
    }
}