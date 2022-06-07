using System.ComponentModel.DataAnnotations;

namespace Cheapo.Api.Classes.Models;

public class TransactionUpdateModel
{
    [Required]
    [MaxLength(200)]
    [Display(Name = "Description")]
    public string Description { get; set; }

    [Required]
    [Display(Name = "Category Id")]
    public string CategoryId { get; set; }

    [Required]
    [Range(0.01d, 9999999999999999.99)]
    [Display(Name = "Amount")]
    public decimal Amount { get; set; }

    [MaxLength(2000)]
    [Display(Name = "Comments")]
    public string? Comments { get; set; }
}