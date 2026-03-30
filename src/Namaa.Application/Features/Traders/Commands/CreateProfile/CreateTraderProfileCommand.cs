using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Traders.Commands.CreateProfile;

public sealed record CreateTraderProfileCommand(Guid UserId) : IRequest<Result<Created>>;