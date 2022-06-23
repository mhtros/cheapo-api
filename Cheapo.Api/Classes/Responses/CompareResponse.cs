namespace Cheapo.Api.Classes.Responses;

public class CompareResponse
{
    public string Category { get; set; } = "";
    public decimal Date1TotalAmount { get; set; }
    public decimal Date2TotalAmount { get; set; }
    public bool IsExpense { get; set; }
}