using System.ComponentModel.DataAnnotations;

namespace Cheapo.Api.Classes.Models;

public class ResetPasswordModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 8)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password")]
    [Display(Name = "Confirm password")]
    public string? ConfirmPassword { get; set; }

    public string? Code { get; set; }
}