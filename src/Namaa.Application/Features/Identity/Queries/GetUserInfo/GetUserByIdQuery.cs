using MediatR;
using Namaa.Application.Features.Identity.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Queries.GetUserInfo;
public sealed record GetUserByIdQuery(string? UserId):IRequest<Result<AppUserDto>>;