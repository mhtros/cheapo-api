using System.Security.Claims;
using Cheapo.Api.Classes;
using Cheapo.Api.Classes.Attributes;
using Cheapo.Api.Classes.Models;
using Cheapo.Api.Classes.Responses;
using Cheapo.Api.Entities;
using Cheapo.Api.Interfaces.Repositories;
using Cheapo.Api.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cheapo.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserController : ControllerBase
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<UserController> _logger;
    private readonly IThumbnailGenerator _thumbnailGenerator;
    private readonly IApplicationTransactionCategoriesRepository _transactionCategories;
    private readonly IApplicationTransactionRepository _transactions;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(UserManager<ApplicationUser> userManager, IThumbnailGenerator thumbnailGenerator,
        IEmailSender emailSender, ILogger<UserController> logger,
        IApplicationTransactionCategoriesRepository transactionCategories,
        IApplicationTransactionRepository transactions)
    {
        _userManager = userManager;
        _thumbnailGenerator = thumbnailGenerator;
        _emailSender = emailSender;
        _logger = logger;
        _transactionCategories = transactionCategories;
        _transactions = transactions;
    }

    /// <summary>
    ///     Retrieves user personal data.
    /// </summary>
    /// <response code="200">Successfully retrieves user personal data.</response>
    /// <response code="401">If the user credentials is wrong.</response>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet]
    public async Task<IActionResult> GetUserData()
    {
        var user = await FindUserFromAccessTokenAsync();
        if (user == null) return Unauthorized();

        var personalData = new PersonalDataResponse
        {
            Email = user.Email,
            Id = user.Id,
            Image = user.Image,
            CreatedAt = user.CreatedAt,
            EmailConfirmed = user.EmailConfirmed,
            UserName = user.UserName,
            TwoFactorEnabled = user.TwoFactorEnabled
        };

        return Ok(new DataResponse<PersonalDataResponse>(personalData));
    }

    /// <summary>
    ///     Sends a deletion token to user email.
    /// </summary>
    /// <response code="204">Successfully sends deletion token email.</response>
    /// <response code="401">If the user credentials is wrong.</response>
    /// <response code="502">If the email proxy doesn't sends the email.</response>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    [HttpGet("delete-token")]
    public async Task<IActionResult> GetAccountDeleteToken()
    {
        var user = await FindUserFromAccessTokenAsync();
        if (user == null) return Unauthorized();

        try
        {
            await SendDeleteTokenEmailAsync(user);
        }
        catch (Exception e)
        {
            _logger.LogWarning("Message: {Message}\nStack Trace: {StackTrade}", e.Message, e.StackTrace);
            return Ok(new ErrorResponse(new[] { Errors.EmailNotSend }));
        }

        return NoContent();
    }

    /// <summary>
    ///     Delete user account.
    /// </summary>
    /// <response code="204">Successfully deletes account.</response>
    /// <response code="401">If the user credentials is wrong.</response>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpDelete("{token}")]
    public async Task<IActionResult> DeleteAccount(string token)
    {
        var user = await FindUserFromAccessTokenAsync();
        if (user == null) return Unauthorized();

        var isValidCode = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", token);

        if (!isValidCode)
            return BadRequest(new ErrorResponse(new[] { Errors.NotValidTwoFactorToken }));

        _transactions.UserRemoveAll(user.Id);
        _transactionCategories.UserRemoveAll(user.Id);
        // The SaveAsync is called inside DeleteAsync so we don't need
        // to call it manually on _transactions or _transactionCategories
        await _userManager.DeleteAsync(user);

        return NoContent();
    }

    /// <summary>
    ///     Change the current user profile image.
    /// </summary>
    /// <response code="204">Successfully changes the user image.</response>
    /// <response code="401">If the user credentials is wrong.</response>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPut("image")]
    public async Task<IActionResult> UpdateImage(UpdateImageModel model)
    {
        var user = await FindUserFromAccessTokenAsync();
        if (user == null) return Unauthorized();

        user.Image = _thumbnailGenerator.Generate(model.Image);

        await _userManager.UpdateAsync(user);

        return NoContent();
    }

    // HELPING METHODS

    private async Task<ApplicationUser?> FindUserFromAccessTokenAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return await _userManager.FindByIdAsync(userId);
    }

    private async Task SendDeleteTokenEmailAsync(ApplicationUser user)
    {
        var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

        const string subject = "Security alert - Account Deletion";
        var content = $"Delete token: {token}";

        var message = new EmailMessage(new[] { user.Email }, subject, content);

        await _emailSender.SendEmailAsync(message);
    }
}