using System.ComponentModel.DataAnnotations;

namespace Cheapo.Api.Classes.Models;

public class EnableTwoFactorModel
{
    [Required] [Display(Name = "Token")] public string? Token { get; set; }
}