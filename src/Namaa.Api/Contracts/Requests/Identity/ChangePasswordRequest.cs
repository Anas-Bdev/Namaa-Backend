using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.Identity.Commands.ChangePassword;

namespace Namaa.Api.Contracts.Requests.Identity;
public class ChangePasswordRequest
{
    [Required(ErrorMessage = "Current password is required.")]
    public string CurrentPassword {get;init;}=default!;

    [Required(ErrorMessage = "New password is required.")]
    [MinLength(8, ErrorMessage = "New password must be at least 8 characters.")]
    public string NewPassword {get;init;}=default!;
    
    public ChangePasswordCommand ToCommand(string userId)
    {
        return new(userId,CurrentPassword,NewPassword);
    }
}