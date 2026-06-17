using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.Lands;

namespace Namaa.Domain.SeedingCycles;
public sealed class SeedingCycle : AuditableEntity
{
    public Guid LandId { get; } 
    public string CropName { get; private set; } 
    public DateTime StartDate { get; private set; }
    public DateTime EstimatedHarvestDate { get; private set; }
    public DateTime? ActualHarvestDate { get; private set; }
    public CycleStatus Status { get; private set; }
    public double SeedQuantityKg { get; private set; }
    public double SeedingAreaDunums { get; private set; }
    public double ExpectedYieldKg { get; private set; }
    public EnvironmentType EnvironmentType { get; private set; }
    public double? ActualYieldKg { get; private set; }
    public Land? Land { get; private set; }
     
    #pragma warning disable CS8618

    private SeedingCycle() {}
    #pragma warning restore CS8618


    private SeedingCycle(
        Guid id, 
        Guid landId, 
        string cropName, 
        DateTime startDate, 
        DateTime estimatedHarvestDate, 
        CycleStatus status, 
        double seedQuantity, 
        double seedingArea, 
        double expectedYield,
        EnvironmentType environmentType) : base(id)
    { 
        LandId = landId;
        CropName = cropName; 
        StartDate = startDate;
        EstimatedHarvestDate = estimatedHarvestDate;
        Status = status;
        SeedQuantityKg = seedQuantity;
        SeedingAreaDunums = seedingArea;
        ExpectedYieldKg = expectedYield;
        EnvironmentType = environmentType;
    }

    public static Result<SeedingCycle> Create(
        Guid id, 
        Guid landId, 
        string cropName, 
        DateTime startDate, 
        DateTime estimatedHarvestDate, 
        CycleStatus initialStatus, 
        double seedQuantity, 
        double seedingArea, 
        double expectedYield, 
        EnvironmentType environmentType)
    {
        if(id == Guid.Empty)
            return SeedingCycleErrors.IdRequired;

        if(landId == Guid.Empty)
            return SeedingCycleErrors.InvalidLandId;

        if(string.IsNullOrWhiteSpace(cropName))
            return SeedingCycleErrors.CropNameRequired;

        var today = DateTime.UtcNow.Date;

        if(initialStatus == CycleStatus.Planned && startDate.Date <= today)
            return SeedingCycleErrors.PlannedCycleMustStartInFuture;
        
        if(initialStatus == CycleStatus.Active && startDate.Date > today)
            return SeedingCycleErrors.ActiveCycleCannotStartInFuture;

        if(estimatedHarvestDate.Date <= startDate.Date)
            return SeedingCycleErrors.InvalidDates;

        if (initialStatus != CycleStatus.Planned && initialStatus != CycleStatus.Active)
            return SeedingCycleErrors.InvalidInitialStatus;

        if (seedQuantity <= 0)
            return SeedingCycleErrors.InvalidSeedQuantity;
                
        if (seedingArea <= 0)
            return SeedingCycleErrors.InvalidSeedingArea;

        if (expectedYield <= 0)
            return SeedingCycleErrors.InvalidExpectedYield;

        return new SeedingCycle(id, landId, cropName, startDate, estimatedHarvestDate, initialStatus, seedQuantity, seedingArea, expectedYield, environmentType);
    }
   
    public Result<Updated> Update(
        string cropName, 
        DateTime startDate, 
        DateTime estimatedHarvestDate, 
        double seedQuantity, 
        double seedingArea, 
        double expectedYield, 
        EnvironmentType environmentType)
    {
        if(Status != CycleStatus.Planned && Status != CycleStatus.Active)
            return SeedingCycleErrors.CycleIsLocked;

        if(string.IsNullOrWhiteSpace(cropName))
            return SeedingCycleErrors.CropNameRequired;

        var today = DateTime.UtcNow.Date;

        if(Status == CycleStatus.Planned && startDate.Date <= today)
            return SeedingCycleErrors.PlannedCycleMustStartInFuture;

        if(Status == CycleStatus.Active && startDate.Date > today)
            return SeedingCycleErrors.ActiveCycleCannotStartInFuture;

        if(estimatedHarvestDate.Date <= startDate.Date)
            return SeedingCycleErrors.InvalidDates;

        if (seedQuantity <= 0)
            return SeedingCycleErrors.InvalidSeedQuantity;
            
        if (seedingArea <= 0)
            return SeedingCycleErrors.InvalidSeedingArea;

        if (expectedYield <= 0)
            return SeedingCycleErrors.InvalidExpectedYield;

        CropName = cropName; 
        StartDate = startDate;
        EstimatedHarvestDate = estimatedHarvestDate;
        SeedQuantityKg = seedQuantity;
        SeedingAreaDunums = seedingArea;
        ExpectedYieldKg = expectedYield;
        EnvironmentType = environmentType;
        
        return Result.Updated;
    }

    public Result<Updated> Harvest(DateTime actualHarvestDate, double actualYield)
    {
        if(Status != CycleStatus.Active)
            return SeedingCycleErrors.CycleMustBeActiveToHarvest;

        if(actualHarvestDate.Date < StartDate.Date)
            return SeedingCycleErrors.HarvestDateCannotBeBeforeStart;

        if(actualYield < 0)
            return SeedingCycleErrors.InvalidActualYield;

        ActualHarvestDate = actualHarvestDate;
        ActualYieldKg = actualYield;
        Status = CycleStatus.Completed;

        return Result.Updated;
    }

    public Result<Updated> Cancel()
    {
        if(Status != CycleStatus.Planned)
            return SeedingCycleErrors.CycleCannotBeCancelled;

        Status = CycleStatus.Cancelled;

        return Result.Updated;
    }
    
    public Result<Updated> Activate()
    {
        if(Status != CycleStatus.Planned)
            return SeedingCycleErrors.CycleMustBePlannedToActivate;

        if(StartDate.Date > DateTime.UtcNow.Date)
            return SeedingCycleErrors.ActiveCycleCannotStartInFuture;

        Status = CycleStatus.Active;
        return Result.Updated;
    }

    public Result<Updated> Fail()
    {
        if(Status != CycleStatus.Active)
            return SeedingCycleErrors.CycleCannotFail;

        Status = CycleStatus.Failed;
        ActualYieldKg = 0;
        return Result.Updated;
    }
}