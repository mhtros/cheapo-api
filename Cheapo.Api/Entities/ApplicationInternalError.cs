using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cheapo.Api.Entities;

[Table("ApplicationInternalErrors")]
public class ApplicationInternalError
{
    [Required] public string? Id { get; set; }
    
    public string? ErrorMessage { get; set; }

    public DateTime OccurrenceDate { get; set; }

    public string? RequestBody { get; set; }

    public string? RequestHeaders { get; set; }

    public string? ControllerName { get; set; }

    public string? ExceptionType { get; set; }

    public string? MethodType { get; set; }

    public string? StackTrace { get; set; }
}