using System.ComponentModel.DataAnnotations;

namespace Cheapo.Api.Classes.Models;

public class ChangePasswordModel
{
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Current password")]
    public string? CurrentPassword { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 8)]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    public string? NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Compare("NewPassword")]
    [Display(Name = "Confirm password")]
    public string? ConfirmPassword { get; set; }
}