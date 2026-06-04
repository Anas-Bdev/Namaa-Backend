using MediatR;

namespace Namaa.Application.Features.Account.Queries.IsProfileCompleted;
public sealed record IsProfileCompletedQuery(Guid UserId,string Role):IRequest<bool?>;