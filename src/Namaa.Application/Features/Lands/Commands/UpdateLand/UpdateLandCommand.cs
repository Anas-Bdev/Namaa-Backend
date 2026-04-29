using MediatR;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Lands.Commands.UpdateLand;
public sealed record UpdateLandCommand(
    Guid FarmerId,
    string AddressDetail,
    Guid LandId,
    string Name,
    double AreaDonum,
    int CityId,
    int SoilId,
    IrrigationMethod IrrigationMethod,
    WaterSourceType WaterSourceType,
    WaterAvailability WaterAvailability,
    EnvironmentType EnvironmentType
) : IRequest<Result<Updated>>;