namespace Cheapo.Api.Classes.Responses;

public class PersonalDataResponse
{
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? Id { get; set; }

    public string? Image { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public string? UserName { get; set; }
}