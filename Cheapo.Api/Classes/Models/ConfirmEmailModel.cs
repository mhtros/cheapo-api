using System.ComponentModel.DataAnnotations;

namespace Cheapo.Api.Classes.Models;

public class ConfirmEmailModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }
}