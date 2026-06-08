using System.ComponentModel.DataAnnotations;

namespace Namaa.Api.Contracts.Requests.Experts;
public class RejectExpertRequest
{
   [Required(ErrorMessage = "Rejection reason is  required.")]
    public string Reason {get;init;}=default!;
}