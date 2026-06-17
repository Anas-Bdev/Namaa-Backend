namespace Namaa.Api.Contracts.Requests.SeedingCycles;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using Namaa.Application.Features.SeedingCycles.Commands.CreateSeedingCycle;
using Namaa.Domain.Enums;
using Namaa.Domain.SeedingCycles;

public class CreateSeedingCycleRequest
{
    [Required(ErrorMessage = "Land is required.")]
    public Guid LandId { get; init; }
     
     [Required(ErrorMessage ="Cycle status is required")]
     [AllowedValues(CycleStatus.Planned, CycleStatus.Active, ErrorMessage = "A new cycle must be Planned or Active.")]
    public CycleStatus? InitialStatus {get;init;}

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

    public CreateSeedingCycleCommand ToCommand()
    {
        return new CreateSeedingCycleCommand(
            LandId,
            CropName,
            StartDate,
            EstimatedHarvestDate,
            InitialStatus!.Value,
            SeedQuantity,
            SeedingArea,
            ExpectedYield,
            EnvironmentType
        );
    }
}