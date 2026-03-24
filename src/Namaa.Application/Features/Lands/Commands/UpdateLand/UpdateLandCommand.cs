using MediatR;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Lands.Commands.UpdateLand;
public sealed record UpdateLandCommand(
    Guid LandId,
    string Name,
    double AreaDonum,
    int CityId,
    int SoilId,
    WaterSourceType WaterSourceType,
    WaterAvailability WaterAvailability,
    EnvironmentType EnvironmentType
) : IRequest<Result<Updated>>;