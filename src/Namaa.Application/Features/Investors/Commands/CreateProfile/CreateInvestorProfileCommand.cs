using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investors.Commands.CreateProfile;

public sealed record CreateInvestorProfileCommand(Guid UserId) : IRequest<Result<Created>>;