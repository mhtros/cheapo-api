namespace Cheapo.Api.Classes.Responses;

public class TransactionResponse
{
    public string? Id { get; set; }

    public string Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public decimal? Amount { get; set; }

    public string? Comments { get; set; }

    public bool? IsExpense { get; set; }

    public string CategoryId { get; set; }

    public string? UserId { get; set; }
}