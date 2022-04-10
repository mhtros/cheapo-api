using Cheapo.Api.Interfaces;

namespace Cheapo.Api.Classes;

/// <summary>
///     Contains the most important parameters of a jwt that is necessary for security reasons.
/// </summary>
public class JwtParameters : IJwtParameters
{  
    /// <summary>
    ///     The string that will be used as the key to create the token signature.
    ///     Used as argument in the hash function.
    /// </summary>
    public string? IssuerSigningKey { get; set; }

    /// <summary>
    ///     Τhe server that issued the token.
    /// </summary>
    public string? Issuer { get; set; }

    /// <summary>
    ///     Which server can accept the token.
    /// </summary>
    public string? Audience { get; set; }

    /// <summary>
    ///     If <b>true</b> the issuer will be validated during token validation.
    /// </summary>
    public bool ValidateIssuer { get; set; }

    /// <summary>
    ///     If <b>true</b> the audience will be validated during token validation.
    /// </summary>
    public bool ValidateAudience { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether tokens must have an 'expiration' value.
    /// </summary>
    public bool RequireExpirationTime { get; set; }

    /// <summary>
    ///     If <b>true</b> the lifetime of the token (is it expired or valid)
    ///     will be validated during token validation.
    /// </summary>
    public bool ValidateLifetime { get; set; }

    /// <summary>
    ///     If <b>true</b> the issuer signin key will be validated during token validation.
    /// </summary>
    public bool ValidateIssuerSigningKey { get; set; }
}