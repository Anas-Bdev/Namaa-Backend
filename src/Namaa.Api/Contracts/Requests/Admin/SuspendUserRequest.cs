using System.ComponentModel.DataAnnotations;

namespace Namaa.Api.Contracts.Requests.Admin;
public class SuspendUserRequest
{
    [Required(ErrorMessage = "A reason for suspension is required.")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "The suspension reason must be between 5 and 500 characters.")]
    public string Reason { get; set; } = default!;
}