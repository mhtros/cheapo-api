using System.ComponentModel.DataAnnotations;

namespace Cheapo.Api.Classes.Models;

public class TransactionModel
{
    [Required]
    [MaxLength(200)]
    [Display(Name = "Description")]
    public decimal? Description { get; set; }

    [Required]
    [Display(Name = "Category Id")]
    public string? CategoryId { get; set; }

    [Required]
    [Range(typeof(decimal), "0.0001", "1,7976931348623157E+308")]
    [Display(Name = "Amount")]
    public decimal? Amount { get; set; }

    [MaxLength(2000)]
    [Display(Name = "Comments")]
    public string? Comments { get; set; }

    [Required]
    [Display(Name = "Is Expense")]
    public bool? IsExpense { get; set; }
}