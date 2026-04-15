using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.Identity.Commands.ResetPassword;

namespace Namaa.Api.Contracts.Requests.Identity;
public class ResetPasswordRequest
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    public string Email {get;init;}=default!;
    [Required(ErrorMessage ="ResetCode is required.")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "The reset code must be 6 digits.")]
    public string ResetCode {get;init;}=default!;
    [Required(ErrorMessage ="NewPassword is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    public string NewPassword {get;init;}=default!;
    public ResetPasswordCommand ToCommand()
    {
        return new(Email,ResetCode,NewPassword);
    }
}