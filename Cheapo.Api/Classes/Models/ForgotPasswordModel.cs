using System.ComponentModel.DataAnnotations;

namespace Cheapo.Api.Classes.Models;

public class ForgotPasswordModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }
}