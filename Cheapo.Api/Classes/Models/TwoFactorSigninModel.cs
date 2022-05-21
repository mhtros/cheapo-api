using System.ComponentModel.DataAnnotations;

namespace Cheapo.Api.Classes.Models;

public class TwoFactorSigninModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Required] [Display(Name = "Token")] public string? Token { get; set; }

    [Required]
    [Display(Name = "Is Recovery Token")]
    public bool IsRecoveryToken { get; set; }
}