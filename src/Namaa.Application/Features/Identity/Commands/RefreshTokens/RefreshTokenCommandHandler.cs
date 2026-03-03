using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Identity.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.RefreshTokens;

public class RefreshTokenCommandHandler(IAppDbContext dbContext,ITokenProvider tokenProvider,IIdentityService identityService) : IRequestHandler<RefreshTokenCommand, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principal= tokenProvider.GetPrincipalFromExpiredToken(request.ExpiredAccessToken);
        if(principal is null)
        return ApplicationErrors.ExpiredAccessTokenInvalid;
        var userId=principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userId is null)
        return ApplicationErrors.UserIdClaimInvalid;
        if(!Guid.TryParse(userId, out var parsedUserId))
        {
            return Error.Validation("Auth.InvalidId", "The provided user ID format is invalid.");
        }
        var getUserResult=await identityService.GetUserByIdAsync(userId);
        if(getUserResult.IsError)
        return getUserResult.Errors;
        var refreshToken=await dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token==request.RefreshToken && t.UserId==parsedUserId,cancellationToken);
        if(refreshToken is null || refreshToken.ExpiresOnUtc<DateTime.UtcNow)
        return ApplicationErrors.RefreshTokenExpired;
        var generateTokenResult = await tokenProvider.GenerateJwtTokenAsync(getUserResult.Value, cancellationToken);

        if (generateTokenResult.IsError)
        {

            return generateTokenResult.Errors;
        }

        return generateTokenResult.Value;
    }
}