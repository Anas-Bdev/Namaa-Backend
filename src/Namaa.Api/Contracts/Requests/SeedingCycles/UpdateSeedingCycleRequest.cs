using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.SeedingCycles.Commands.UpdateSeedingCycle;
using Namaa.Domain.Enums;

namespace Namaa.Api.Contracts.Requests.SeedingCycles;
public class UpdateSeedingCycleRequest
{
    [Required(ErrorMessage = "Start date is required.")]
    public DateTime StartDate { get; init; }

    [Required(ErrorMessage = "Estimated harvest date is required.")]
    public DateTime EstimatedHarvestDate { get; init; }

    [Required(ErrorMessage = "Seed quantity is required.")]
    [Range(0.01, 100000.0, ErrorMessage = "Seed quantity must be greater than 0.")]
    public double SeedQuantity { get; init; }

    [Required(ErrorMessage = "Seeding area in donums is required.")]
    [Range(0.01, 100000.0, ErrorMessage = "Seeding area must be greater than 0.")]
    public double SeedingArea { get; init; }

    [Required(ErrorMessage = "Expected yield is required.")]
    [Range(0.01, 100000.0, ErrorMessage = "Expected yield must be greater than 0.")]
    public double ExpectedYield { get; init; }

    [Required(ErrorMessage = "Crop name is required.")]
    public string CropName {get;init;}=default!;

     
    [Required(ErrorMessage ="Environment type is required")]
    public EnvironmentType EnvironmentType {get;init;}

    public UpdateSeedingCycleCommand ToCommand(Guid id)
    {
        return new UpdateSeedingCycleCommand(
            id,
            CropName,
            StartDate,
            EstimatedHarvestDate,
            SeedQuantity,
            SeedingArea,
            ExpectedYield,
            EnvironmentType
        );
    }
}