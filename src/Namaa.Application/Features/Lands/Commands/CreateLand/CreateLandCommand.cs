using MediatR;
using Namaa.Application.Features.Lands.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Lands.Commands.CreateLand;

public sealed record CreateLandCommand(
    Guid FarmerId,
    string AddressDetail,
    string Name,
    double AreaDonum,
    int CityId,
    int SoilId,
    WaterSourceType WaterSourceType,
    WaterAvailability WaterAvailability
) : IRequest<Result<LandDto>>;