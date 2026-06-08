using MediatR;
using Namaa.Application.Common.Models;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Admin.Queries.GetAllUsers;
public sealed record GetAllUsersQuery(
    int PageNumber,
    int PageSize
):IRequest<Result<PaginatedList<UserLookupModel>>>;