using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.Identity.Commands.RefreshTokens;

namespace Namaa.Api.Contracts.Requests.Identity;
public class RefreshTokenRequest
{
    [Required(ErrorMessage = "The expired access token is required.")]
    public string ExpiredToken {get;init;}=default!;
    [Required(ErrorMessage = "The refresh token is required.")]
    public string RefreshToken {get;init;}=default!;
    public RefreshTokenCommand ToCommand()
    {
        return new(RefreshToken,ExpiredToken);
    }
}