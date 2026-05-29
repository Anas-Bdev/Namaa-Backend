using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.LogOut;
public sealed record LogOutCommand(string? UserId,string RefreshToken):IRequest<Result<Success>>;