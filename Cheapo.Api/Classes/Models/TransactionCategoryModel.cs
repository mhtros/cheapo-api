using System.ComponentModel.DataAnnotations;

namespace Cheapo.Api.Classes.Models;

public class TransactionCategoryModel
{
    [Required] public string Name { get; set; }
}