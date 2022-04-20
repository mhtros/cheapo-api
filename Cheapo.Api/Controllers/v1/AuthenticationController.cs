﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Cheapo.Api.Classes;
using Cheapo.Api.Classes.Attributes;
using Cheapo.Api.Classes.Models;
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
    private readonly IThumbnailGenerator _thumbnailGenerator;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthenticationController(UserManager<ApplicationUser> userManager, IThumbnailGenerator thumbnailGenerator,
        IEmailSender emailSender, ILogger<AuthenticationController> logger, IJwtParameters jwtParameters)
    {
        _userManager = userManager;
        _thumbnailGenerator = thumbnailGenerator;
        _emailSender = emailSender;
        _logger = logger;
        _jwtParameters = jwtParameters;
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
        if (exists) return UnprocessableEntity(new ErrorResponse(new[] {Errors.UserAlreadyExists}));

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
            return Ok(new ErrorResponse(new[] {Errors.EmailNotSend}));
        }

        return Ok();
    }

    /// <summary>Confirms the user email.</summary>
    /// <response code="204">If the email has been successfully confirmed.</response>
    /// <response code="400">If the request url parameters are invalid.</response>
    /// <response code="401">If the user doesn't exists.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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

        return NoContent();
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
            return BadRequest(new ErrorResponse(new[] {Errors.InvalidToken}));
        }

        if (token == null)
            return BadRequest(new ErrorResponse(new[] {Errors.InvalidToken}));

        var userId = token.Subject;
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || user.RefreshToken != model.RefreshToken)
            return BadRequest(new ErrorResponse(new[] {Errors.InvalidToken}));

        if (user.RefreshTokenValidUntil <= DateTime.UtcNow)
            return Unauthorized(new ErrorResponse(new[] {Errors.ExpiredRefreshToken}));

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
    [HttpPost("revoke/{userId}")]
    public async Task<IActionResult> Revoke(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return BadRequest();

        if (user.RefreshToken == null || user.RefreshTokenValidUntil == null)
            return NoContent();

        user.RefreshToken = null;
        user.RefreshTokenValidUntil = null;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }

    // HELPING METHODS

    private async Task SendConfirmationEmailAsync(ApplicationUser user)
    {
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var callbackUrl = Url.Action(
            "ConfirmEmail",
            "Authentication",
            new {userId = user.Id, code},
            HttpContext.Request.Scheme);

        const string subject = "Security alert - Email confirmation";
        var content = $"Click the link to confirm your email: {callbackUrl}";

        var message = new EmailMessage(new[] {user.Email}, subject, content);

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