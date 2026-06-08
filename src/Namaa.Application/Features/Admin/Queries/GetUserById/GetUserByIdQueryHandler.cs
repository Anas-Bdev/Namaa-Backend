using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Admin.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUserReadRepository userReadRepository) : IRequestHandler<GetUserByIdQuery, Result<UserLookupModel>>
{
    public async Task<Result<UserLookupModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userReadRepository.GetByIdAsync(request.UserId,cancellationToken);
        if(user is null)
        return ApplicationErrors.UserNotFound;
        return user;
    }
}