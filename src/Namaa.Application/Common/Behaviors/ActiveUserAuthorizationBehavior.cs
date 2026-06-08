using System.Threading;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Enums;

namespace Namaa.Application.Common.Behaviors;

public class ActiveUserAuthorizationBehavior<TRequest, TResponse>(
    IUser currentUser,
    IUserReadRepository userReadRepository,
    HybridCache cache)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequiresActiveUser
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var userId=currentUser.Id;
        string cacheKey=$"UserStatus_{userId}";
        var status=await cache.GetOrCreateAsync(
            cacheKey,
            async cancelToken =>
            {
                var user=await userReadRepository.GetByIdAsync(Guid.Parse(userId!),cancelToken);

                if(user is null)
                throw new UnauthorizedAccessException("User profile could not be found.");

                return user.Status;
            },
            cancellationToken:cancellationToken
        );

        if(status!=UserStatus.Active)
        throw new Exception("Your account must be active to perform this action.");

        return await next();
    }
}