using MediatR;
using Namaa.Application.Common.Models;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Admin.Queries.GetAllUsers;
public sealed record GetAllUsersQuery(
    int PageNumber,
    int PageSize,
    UserStatus? Status,
    string? Search
):IRequest<Result<PaginatedList<UserLookupModel>>>;