using MediatR;
using Namaa.Application.Common.Models;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Admin.Queries.GetUserById;
public sealed record GetUserByIdQuery(Guid UserId):IRequest<Result<UserLookupModel>>;