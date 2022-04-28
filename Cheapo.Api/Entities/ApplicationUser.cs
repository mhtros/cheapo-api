using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cheapo.Api.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Cheapo.Api.Entities;

public class ApplicationUser : IdentityUser
{
    /// <summary>
    ///     User's profile image.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    ///     User's creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     Unique credential string used to issue new id tokens.
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    ///     Refresh token validity date.
    ///     After this time has passed the refresh token expires.
    /// </summary>
    public DateTime? RefreshTokenValidUntil { get; set; }

    public string GenerateToken(IJwtParameters jwtParameters)
    {
        var claims = new List<Claim>
        {
            new("username", UserName),
            new("sub", Id),
            new("email", Email),
            new("image", Image ?? string.Empty)
        };

        var issuer = jwtParameters?.Issuer;
        var audience = jwtParameters?.Audience;
        var notBefore = DateTime.UtcNow;
        var expires = DateTime.UtcNow.AddMinutes(15);

        var key = Encoding.ASCII.GetBytes(jwtParameters?.IssuerSigningKey ?? string.Empty);
        var symmetricKey = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(issuer, audience, claims, notBefore, expires, signingCredentials);
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}