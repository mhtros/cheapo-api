using System.Security.Claims;
using Cheapo.Api.Classes.Attributes;
using Cheapo.Api.Classes.Models;
using Cheapo.Api.Entities;
using Cheapo.Api.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cheapo.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserController : ControllerBase
{
    private readonly IThumbnailGenerator _thumbnailGenerator;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(UserManager<ApplicationUser> userManager, IThumbnailGenerator thumbnailGenerator)
    {
        _userManager = userManager;
        _thumbnailGenerator = thumbnailGenerator;
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Unauthorized();

        user.Image = _thumbnailGenerator.Generate(model.Image);

        await _userManager.UpdateAsync(user);

        return NoContent();
    }
}