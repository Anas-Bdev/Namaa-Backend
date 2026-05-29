using MediatR;
using Namaa.Application.Features.Farmers.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Farmers.Commands.CreateProfile;
public sealed record CreateFarmerProfileCommand(Guid UserId,int GovernorateId,string? Description,string? AddressDetail) : IRequest<Result<FarmerProfileDto>>;