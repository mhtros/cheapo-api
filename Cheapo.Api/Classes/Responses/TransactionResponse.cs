namespace Cheapo.Api.Classes.Responses;

public class TransactionResponse
{
    public string? Id { get; set; }

    public string Description { get; set; }

    public DateTime TransactionDate { get; set; }

    public decimal Amount { get; set; }

    public string? Comments { get; set; }

    public bool IsExpense { get; set; }

    public TransactionCategoriesResponse Category { get; set; }

    public string? UserId { get; set; }
}