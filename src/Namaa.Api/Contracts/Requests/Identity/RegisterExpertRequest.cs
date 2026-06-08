using System.ComponentModel.DataAnnotations;

namespace Namaa.Api.Contracts.Requests.Identity;
public class RegisterExpertRequest
{
      [Required(ErrorMessage = "First name is required.")]
    public string FirstName {get;init;}=default!;
    public string? LastName {get;init;}

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    public string Email {get;init;}=default!;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; init; }=default!;
    [Phone]
    public string? PhoneNumber {get;init;}

     [Required(ErrorMessage = "Please upload your CV.")]
    public IFormFile CvFile {get;init;}=default!;
}