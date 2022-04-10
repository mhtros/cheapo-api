using System.ComponentModel.DataAnnotations;

namespace Cheapo.Api.Classes.Models;

public class SignupModel
{
    [Required]
    [MaxLength(50)]
    [Display(Name = "Username")]
    public string? Username { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(100)]
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

    [Display(Name = "Image")] public string? Image { get; set; }
}