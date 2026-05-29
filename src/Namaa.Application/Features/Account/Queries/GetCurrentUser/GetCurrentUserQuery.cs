using MediatR;
using Namaa.Application.Features.Account.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Account.Queries.GetCurrentUser;
public sealed record GetCurrentUserQuery(string? UserId):IRequest<Result<AppUserDto>>;