using System.ComponentModel.DataAnnotations;

namespace Cheapo.Api.Classes.Models;

public class SigninModel
{
    [Required] [Display(Name = "Email")] public string? Email { get; set; }

    [Required]
    [Display(Name = "Password")]
    public string? Password { get; set; }
}