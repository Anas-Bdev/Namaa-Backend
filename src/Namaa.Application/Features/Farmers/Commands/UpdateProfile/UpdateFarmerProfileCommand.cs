using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Farmers.Commands.UpdateProfile;
public sealed record UpdateFarmerProfileCommand(Guid UserId,int GovernorateId,string? Description,string? AddressDetail) : IRequest<Result<Updated>>;