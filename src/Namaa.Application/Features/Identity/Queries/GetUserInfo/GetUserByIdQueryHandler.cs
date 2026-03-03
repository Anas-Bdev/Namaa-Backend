using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Identity.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Queries.GetUserInfo;

public class GetUserByIdQueryHandler(IIdentityService identityService) : IRequestHandler<GetUserByIdQuery, Result<AppUserDto>>
{
    public async Task<Result<AppUserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
    var getUserResult=await identityService.GetUserByIdAsync(request.UserId!);
    if(getUserResult.IsError)
    return getUserResult.Errors;
    return getUserResult.Value;
    }
}