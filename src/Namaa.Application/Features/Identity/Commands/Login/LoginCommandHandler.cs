using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Identity.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.Login;

public class LoginCommandHandler(
    IIdentityService identityService, 
    ITokenProvider tokenProvider) 
    : IRequestHandler<LoginCommand, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var authResult = await identityService.AuthenticateAsync(request.Email, request.Password);
        
        if (!authResult.IsSuccess)
        {
            return authResult.Errors;
        }

        var tokenResult = await tokenProvider.GenerateJwtTokenAsync(authResult.Value);
        
        if (!tokenResult.IsSuccess)
        {
            return tokenResult.Errors;
        }

        return tokenResult.Value;
    }
}