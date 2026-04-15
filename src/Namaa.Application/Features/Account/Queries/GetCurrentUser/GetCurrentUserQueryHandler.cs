using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Account.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Account.Queries.GetCurrentUser;
public class GetCurrentUserQueryHandler(IIdentityService identityService) : IRequestHandler<GetCurrentUserQuery, Result<AppUserDto>>
{
    public async Task<Result<AppUserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
    var getUserResult=await identityService.GetUserByIdAsync(request.UserId!);
    if(getUserResult.IsError)
    return getUserResult.Errors;
    return getUserResult.Value;  
    }
}