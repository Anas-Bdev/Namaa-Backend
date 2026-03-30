using MediatR;
using Namaa.Application.Features.Farmers.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Farmers.Commands.UpdateProfile;

public sealed record UpdateFarmerProfileCommand(
    Guid UserId,
    string? Description,
    int CityId,
    string? AddressDetail,
    string? ExperienceLevel
) : IRequest<Result<FarmerProfileDto>>;
