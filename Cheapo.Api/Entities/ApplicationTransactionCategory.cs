using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cheapo.Api.Interfaces;

namespace Cheapo.Api.Entities;

[Table("ApplicationTransactionCategories")]
public class ApplicationTransactionCategory : IDistinctable<string>
{
    [Required] public string Name { get; set; }

    public string? UserId { get; set; }

    public ApplicationUser? User { get; set; }

    [Required] public string? Id { get; set; }
}