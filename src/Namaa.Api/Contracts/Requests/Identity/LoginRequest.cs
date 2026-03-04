using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.Identity.Commands.Login;

namespace Namaa.Api.Contracts.Requests.Identity;
public class LoginRequest
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]

    public string Email {get;init;}=default!;
    [Required(ErrorMessage = "Password is required.")]
    public string Password {get;init;}=default!;
    public LoginCommand ToCommand()
    {
        return new(Email,Password);
    }
}