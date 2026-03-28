using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.Identity.Commands.Register;

namespace Namaa.Api.Contracts.Requests.Identity;
public class RegisterRequest
{
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName {get;init;}=default!;
    public string? LastName {get;init;}

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    public string Email {get;init;}=default!;

    [Required(ErrorMessage = "Password is required.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", 
    ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number.")]

    public string Password { get; init; }=default!;
    [Phone]
    public string? PhoneNumber {get;init;}

    [Required(ErrorMessage = "Role is required.")]
    public string Role {get;init;}=default!;
    public RegisterCommand ToCommand()
    {
        return new (Email,Password,Role,FirstName,LastName,PhoneNumber);
    }
}