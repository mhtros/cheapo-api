using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cheapo.Api.Entities;

[Table("ApplicationTransactions")]
public class ApplicationTransaction
{
    [Required] public string? Id { get; set; }

    public string Description { get; set; }

    public DateTime CreatedAt { get; set; }

    [Required] public decimal? Amount { get; set; }

    public string? Comments { get; set; }

    [Required] public bool? IsExpense { get; set; }

    [Required] public string CategoryId { get; set; }

    public ApplicationTransactionCategory? Category { get; set; }

    [Required] public string? UserId { get; set; }

    public ApplicationUser? User { get; set; }

    public bool BelongTo(string userId)
    {
        return UserId == userId;
    }
}