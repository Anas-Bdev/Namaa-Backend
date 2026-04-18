using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.SeedingCycles.Commands.HarvestSeedingCycle;

namespace Namaa.Api.Contracts.Requests.SeedingCycles;
public class HarvestSeedingCycleRequest
{
    [Required(ErrorMessage = "Actual harvest date is required.")]
    public DateTime ActualHarvestDate { get; init; }

    [Required(ErrorMessage = "Actual yield is required.")]
    [Range(0.0, 100000.0, ErrorMessage = "Actual yield cannot be negative.")] 
    public double ActualYield { get; init; }

    public HarvestSeedingCycleCommand ToCommand(Guid id)
    {
        return new HarvestSeedingCycleCommand(
            id,
            ActualHarvestDate,
            ActualYield
        );
    }
}