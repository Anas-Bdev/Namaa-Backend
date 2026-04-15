using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.Identity.Commands.LogOut;

namespace Namaa.Api.Contracts.Requests.Identity;
public class LogOutRequest
{
    [Required(ErrorMessage = "The refresh token is required to log out.")]
    public string RefreshToken {get;init;}=default!;
    public LogOutCommand ToCommand(string userId)
    {
        return new(userId,RefreshToken);
    }
}