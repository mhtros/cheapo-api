using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Cheapo.Api.Classes;
using Cheapo.Api.Classes.Attributes;
using Cheapo.Api.Classes.Models;
using Cheapo.Api.Classes.Responses;
using Cheapo.Api.Entities;
using Cheapo.Api.Extensions;
using Cheapo.Api.Interfaces;
using Cheapo.Api.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cheapo.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IEmailSender _emailSender;
    private readonly IJwtParameters _jwtParameters;
    private readonly ILogger<AuthenticationController> _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IThumbnailGenerator _thumbnailGenerator;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthenticationController(UserManager<ApplicationUser> userManager, IThumbnailGenerator thumbnailGenerator,
        IEmailSender emailSender, ILogger<AuthenticationController> logger, IJwtParameters jwtParameters,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _thumbnailGenerator = thumbnailGenerator;
        _emailSender = emailSender;
        _logger = logger;
        _jwtParameters = jwtParameters;
        _signInManager = signInManager;
    }

    /// <summary>Registers a new user into the application.</summary>
    /// <response code="200">If the user has successfully created.</response>
    /// <response code="422">If the user already exists.</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [HttpPost("signup")]
    public async Task<IActionResult> Signup(SignupModel model)
    {
        var exists = !string.IsNullOrWhiteSpace((await _userManager.FindByNameAsync(model.Username))?.Id);
        if (exists) return UnprocessableEntity(new ErrorResponse(new[] { Errors.UserAlreadyExists }));

        ApplicationUser user = new()
        {
            Email = model.Email,
            UserName = model.Username,
            Image = _thumbnailGenerator.Generate(model.Image)
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(new ErrorResponse(result.Errors.Select(e => e.Description.ToUnderscore())));

        try
        {
            await SendConfirmationEmailAsync(user);
        }
        catch (Exception e)
        {
            _logger.LogWarning("Message: {Message}\nStack Trace: {StackTrade}", e.Message, e.StackTrace);
            return Ok(new ErrorResponse(new[] { Errors.EmailNotSend }));
        }

        return Ok();
    }

    /// <summary>Confirms the user email.</summary>
    /// <response code="200">If the email has been successfully confirmed.</response>
    /// <response code="400">If the request url parameters are invalid.</response>
    /// <response code="401">If the user doesn't exists.</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string code)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            return BadRequest();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Unauthorized();

        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (!result.Succeeded)
            return BadRequest(new ErrorResponse(result.Errors.Select(e => e.Description.ToUnderscore())));

        return new ContentResult
        {
            ContentType = "text/html",
            Content = @"<html><h1 style='color: red;'>The Email has confirmed successfully</h1></html>"
        };
    }

    /// <summary>
    ///     Signs in the user to the application.
    /// </summary>
    /// <response code="200">
    ///     If the user has successfully signed in returns two JSON Web Token (Access, Refresh).
    /// </response>
    /// <response code="401">If the user credentials is wrong.</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("signin")]
    public async Task<IActionResult> Signin(SigninModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        var canSignIn = await _signInManager.CanSignInAsync(user);
        if (!canSignIn) return Unauthorized(new ErrorResponse(new[] { Errors.AccountNotVerified }));

        var result = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!result) return Unauthorized();

        var tokens = new TokenModel
        {
            AccessToken = user.GenerateToken(_jwtParameters),
            RefreshToken = GenerateRefreshToken()
        };

        user.RefreshToken = tokens.RefreshToken;
        user.RefreshTokenValidUntil = DateTime.UtcNow.AddDays(1);

        // Update refresh token
        await _userManager.UpdateAsync(user);

        return Ok(new DataResponse<TokenModel>(tokens));
    }

    /// <summary>
    ///     Refresh access token using a refresh token.
    /// </summary>
    /// <response code="200">
    ///     If the refresh token is valid generate a new access token.
    /// </response>
    /// <response code="400">
    ///     If the refresh or access tokens are invalid.
    /// </response>
    /// <response code="401">
    ///     If the refresh token is expired.
    /// </response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenModel model)
    {
        JwtSecurityToken? token;
        var handler = new JwtSecurityTokenHandler();

        try
        {
            token = handler.ReadToken(model.AccessToken) as JwtSecurityToken;
        }
        catch (Exception)
        {
            return BadRequest(new ErrorResponse(new[] { Errors.InvalidToken }));
        }

        if (token == null)
            return BadRequest(new ErrorResponse(new[] { Errors.InvalidToken }));

        var userId = token.Subject;
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || user.RefreshToken != model.RefreshToken)
            return BadRequest(new ErrorResponse(new[] { Errors.InvalidToken }));

        if (user.RefreshTokenValidUntil <= DateTime.UtcNow)
            return Unauthorized(new ErrorResponse(new[] { Errors.ExpiredRefreshToken }));

        // Ιf we wanted to keep the user constantly connected we would also renew
        // the refresh token and update the database (see the Signin method)

        model.AccessToken = user.GenerateToken(_jwtParameters);

        return Ok(new DataResponse<TokenModel>(model));
    }

    /// <summary>
    ///     Revokes the refresh token of a given user.
    /// </summary>
    /// <response code="204">
    ///     Successfully removes the refresh token from the database.
    /// </response>
    /// <response code="400">If the user credentials is wrong.</response>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return BadRequest();

        if (user.RefreshToken == null || user.RefreshTokenValidUntil == null)
            return NoContent();

        user.RefreshToken = null;
        user.RefreshTokenValidUntil = null;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }

    /// <summary>Resend user confirmation email.</summary>
    /// <response code="204">If the confirmation email has been successfully send.</response>
    /// <response code="401">If the user doesn't exists.</response>
    /// <response code="502">If the email proxy doesn't sends the email.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    [HttpPost("resend-confirmation-email")]
    public async Task<IActionResult> ResendConfirmationEmail(ConfirmEmailModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Email))
            return Unauthorized();

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null) return Unauthorized();

        try
        {
            await SendConfirmationEmailAsync(user);
        }
        catch (Exception e)
        {
            _logger.LogWarning("Message: {Message}\nStack Trace: {StackTrade}", e.Message, e.StackTrace);
            return StatusCode(StatusCodes.Status502BadGateway, new ErrorResponse(new[] { Errors.EmailNotSend }));
        }

        return NoContent();
    }

    /// <summary>Sends email with a reset code.</summary>
    /// <response code="401">If the user doesn't exists.</response>
    /// <response code="502">If the email proxy doesn't sends the email.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null) return Unauthorized();

        var code = await _userManager.GeneratePasswordResetTokenAsync(user);

        const string subject = "Security alert - Reset password";
        var content = $"Confirmation code: {code}";

        var message = new EmailMessage(new[] { model.Email }, subject, content);

        try
        {
            await _emailSender.SendEmailAsync(message);
        }
        catch (Exception e)
        {
            _logger.LogWarning("Message: {Message}\nStack Trace: {StackTrade}", e.Message, e.StackTrace);
            return StatusCode(StatusCodes.Status502BadGateway, new ErrorResponse(new[] { Errors.EmailNotSend }));
        }

        return NoContent();
    }

    /// <summary>Reset the user password.</summary>
    /// <response code="200">If the password has been successfully reset.</response>
    /// <response code="400">If reset code is invalid.</response>
    /// <response code="401">If the user doesn't exists.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null) return Unauthorized();

        var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
        if (!result.Succeeded) return BadRequest();

        return new ContentResult
        {
            ContentType = "text/html",
            Content = @"<html><h1 style='color: red;'>The Password has successfully changed!</h1></html>"
        };
    }

    /// <summary>
    ///     Change the current password.
    /// </summary>
    /// <response code="204">
    ///     Successfully changes the password with the new.
    /// </response>
    /// <response code="400">If code generation logic fails.</response>
    /// <response code="401">If the user credentials is wrong.</response>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Unauthorized();

        var authorized = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
        if (!authorized) return Unauthorized();

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
        if (!result.Succeeded) return BadRequest();

        return NoContent();
    }

    /// <summary>
    ///     Generate a new authenticator key.
    /// </summary>
    /// <response code="200">
    ///     Successfully generates authenticator key.
    /// </response>
    /// <response code="401">If the user credentials is wrong.</response>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("generate-authenticator-key")]
    public async Task<IActionResult> GenerateAuthenticatorKey()
    {
        var user = await FindUserFromAccessTokenAsync();
        if (user == null) return Unauthorized();

        await _userManager.ResetAuthenticatorKeyAsync(user);

        var response = new AuthenticatorTokenResponse
        {
            Token = await _userManager.GetAuthenticatorKeyAsync(user)
        };

        return Ok(new DataResponse<AuthenticatorTokenResponse>(response));
    }

    // HELPING METHODS

    private async Task<ApplicationUser?> FindUserFromAccessTokenAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return await _userManager.FindByIdAsync(userId);
    }

    private async Task SendConfirmationEmailAsync(ApplicationUser user)
    {
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var callbackUrl = Url.Action(
            "ConfirmEmail",
            "Authentication",
            new { userId = user.Id, code },
            HttpContext.Request.Scheme);

        const string subject = "Security alert - Email confirmation";
        var content = $"Click the link to confirm your email: {callbackUrl}";

        var message = new EmailMessage(new[] { user.Email }, subject, content);

        await _emailSender.SendEmailAsync(message);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}