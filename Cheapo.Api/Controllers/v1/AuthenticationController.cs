using Cheapo.Api.Classes;
using Cheapo.Api.Classes.Models;
using Cheapo.Api.Entities;
using Cheapo.Api.Extensions;
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
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IThumbnailGenerator _thumbnailGenerator;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthenticationController(UserManager<ApplicationUser> userManager, IThumbnailGenerator thumbnailGenerator,
        IEmailSender emailSender, ILogger<AuthenticationController> logger)
    {
        _userManager = userManager;
        _thumbnailGenerator = thumbnailGenerator;
        _emailSender = emailSender;
        _logger = logger;
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
        if (exists) return UnprocessableEntity(new ErrorResponse {Errors = new[] {Errors.UserAlreadyExists}});

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
}