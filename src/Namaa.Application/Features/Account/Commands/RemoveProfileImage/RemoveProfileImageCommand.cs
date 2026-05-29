using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Account.Commands.RemvoeProfileImage;
public sealed record RemoveProfileImageCommand(string? UserId):IRequest<Result<Updated>>;