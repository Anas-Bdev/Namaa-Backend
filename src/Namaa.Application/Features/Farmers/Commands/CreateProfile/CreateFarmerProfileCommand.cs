using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Farmers.Commands.CreateProfile;

public sealed record CreateFarmerProfileCommand(Guid UserId) : IRequest<Result<Created>>;