using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.Identity.Commands.ResendConfirmationEmail;

namespace Namaa.Api.Contracts.Requests.Identity;
public class ResendConfirmationEmailRequest
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    public string Email {get;init;}=default!;
    public ResendConfirmationEmailCommand ToCommand()
    {
        return new(Email);
    }
}