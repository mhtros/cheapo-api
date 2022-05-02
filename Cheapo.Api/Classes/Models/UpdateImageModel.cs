using System.ComponentModel.DataAnnotations;

namespace Cheapo.Api.Classes.Models;

public class UpdateImageModel
{
    [Required]
    [Display(Name = "Image")] public string? Image { get; set; }
}