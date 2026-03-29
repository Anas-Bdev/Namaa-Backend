using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.Lands.Commands.UpdateLand;
using Namaa.Domain.Enums;

namespace Namaa.Api.Contracts.Requests.Lands;

public class UpdateLandRequest
{
    [Required(ErrorMessage = "Land name is required.")]
    [MinLength(3, ErrorMessage = "Land name must be at least 3 characters.")]
    public string Name { get; init; } = default!;

    [Required(ErrorMessage = "Area in donums is required.")]
    [Range(0.1, 100000.0, ErrorMessage = "Area must be greater than 0.")]
    public double AreaDonum { get; init; }

    [Required(ErrorMessage = "City is required.")]
    public int CityId { get; init; }

    [Required(ErrorMessage = "Soil type is required.")]
    public int SoilId { get; init; }

    [Required(ErrorMessage = "Water source type is required.")]
    public WaterSourceType? WaterSourceType { get; init; }

    [Required(ErrorMessage ="Irrigation Method is required.")]
    public IrrigationMethod? IrrigationMethod {get;init;}

    [Required(ErrorMessage = "Water availability is required.")]
    public WaterAvailability? WaterAvailability { get; init; }

    [Required(ErrorMessage = "Environment type is required.")]
    public EnvironmentType? EnvironmentType { get; init; }

    public UpdateLandCommand ToCommand(Guid landId,Guid farmerId)
    {
        return new UpdateLandCommand(
            farmerId,
            landId,
            Name,
            AreaDonum,
            CityId,
            SoilId,
            IrrigationMethod!.Value,
            WaterSourceType!.Value,
            WaterAvailability!.Value,
            EnvironmentType!.Value
        );
    }
}