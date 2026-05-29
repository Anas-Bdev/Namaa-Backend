using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.Identity.Commands.ConfirmEmail;
using Namaa.Domain.Common.Results;

namespace Namaa.Api.Contracts.Requests.Identity;
public class ConfirmEmailRequest
{
    [Required(ErrorMessage ="User ID is required.")]
    public string UserId {get;init;}=default!;
    [Required(ErrorMessage ="Confirmation token is required.")]
    public string Token {get;init;}=default!;
    public ConfirmEmailCommand ToCommand()
    {
         return new (UserId,Token);
    }
}