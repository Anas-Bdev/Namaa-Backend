using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.Experts.Commands.RegisterExpert;
using Namaa.Application.Features.Identity.Commands.RefreshTokens;

namespace Namaa.Api.Contracts.Requests.Experts;
public class RegisterExpertRequest
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

     [Required(ErrorMessage = "Please upload your CV.")]
    public IFormFile CvFile {get;init;}=default!;

    public RegisterExpertCommand ToCommand()
    {
        return new(Email,Password,FirstName,LastName,PhoneNumber,CvFile);
    }

}