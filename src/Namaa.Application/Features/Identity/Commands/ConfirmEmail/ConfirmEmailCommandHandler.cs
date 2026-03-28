using System.Runtime.InteropServices;
using System.Security.Principal;
using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Identity.Dtos;
using Namaa.Domain.Common.Results;
namespace Namaa.Application.Features.Identity.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler(IIdentityService service, ITokenProvider tokenProvider) : IRequestHandler<ConfirmEmailCommand, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
      var confirmResult = await service.ConfirmEmailAsync(request.UserId, request.Token);
    if (!confirmResult.IsSuccess)
        return confirmResult.Errors;

    var userResult = await service.GetUserByIdAsync(request.UserId);
    if (!userResult.IsSuccess)
        return userResult.Errors;

    var tokenResponse = await tokenProvider.GenerateJwtTokenAsync(userResult.Value,ct:cancellationToken);
    if (!tokenResponse.IsSuccess)
        return tokenResponse.Errors;

    return tokenResponse.Value;
        }

    }
