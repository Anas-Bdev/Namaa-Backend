using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.Identity.Commands.ForgotPassword;

namespace Namaa.Api.Contracts.Requests.Identity;
public class ForgotPasswordRequest
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    public string Email {get;init;}=default!;
    public ForgotPasswordCommand ToCommand()
    {
        return new(Email);
    }
}