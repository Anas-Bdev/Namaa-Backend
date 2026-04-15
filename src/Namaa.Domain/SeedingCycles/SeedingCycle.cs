using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Lands;

namespace Namaa.Domain.SeedingCycles;
public sealed class SeedingCycle : AuditableEntity
{
    public Guid LandId {get;} 
    public int CropId {get;}
    public DateTime StartDate {get;private set;}
    public DateTime EstimatedHarvestDate {get;private set;}
    public DateTime? ActualHarvestDate {get;private set;}
    public CycleStatus Status {get;private set;}
    public double SeedQuantity {get;private set;}
    public double SeedingArea {get;private set;}
    public double ExpectedYield {get;private set;}
    public double? ActualYield {get;private set;}
    public Land? Land {get;private set;}

    private SeedingCycle () {}
    private SeedingCycle(
    Guid id, 
    Guid landId, 
    int cropId, 
    DateTime startDate, 
    DateTime estimatedHarvestDate, 
    CycleStatus status, 
    double seedQuantity, 
    double seedingArea, 
    double expectedYield) :base(id)
{ 
    LandId = landId;
    CropId = cropId;
    StartDate = startDate;
    EstimatedHarvestDate = estimatedHarvestDate;
    Status = status;
    SeedQuantity = seedQuantity;
    SeedingArea = seedingArea;
    ExpectedYield = expectedYield;
}

     
    public static Result<SeedingCycle> Create(Guid id,Guid landId,int cropId,DateTime startDate,DateTime estimatedHarvestDate,CycleStatus initialStatus,double seedQuantity,double seedingArea,double expectedYield)
    {
        if(id==Guid.Empty)
        return SeedingCycleErrors.IdRequired;

        if(landId==Guid.Empty)
        return SeedingCycleErrors.InvalidLandId;

        if(cropId<=0)
        return SeedingCycleErrors.InvalidCropId;

        var today=DateTime.UtcNow.Date;

        if(initialStatus==CycleStatus.Planned && startDate.Date <= today)
        return SeedingCycleErrors.PlannedCycleMustStartInFuture;
        
        if(initialStatus==CycleStatus.Active && startDate.Date>today)
        return SeedingCycleErrors.ActiveCycleCannotStartInFuture;

        if(estimatedHarvestDate.Date <=startDate.Date)
        return SeedingCycleErrors.InvalidDates;

        if (initialStatus != CycleStatus.Planned && initialStatus != CycleStatus.Active)
        return SeedingCycleErrors.InvalidInitialStatus;

        if (seedQuantity <= 0)
                return SeedingCycleErrors.InvalidSeedQuantity;
                
        if (seedingArea <= 0)
                return SeedingCycleErrors.InvalidSeedingArea;

        if (expectedYield <= 0)
                return SeedingCycleErrors.InvalidExpectedYield;

     return new SeedingCycle(id,landId,cropId,startDate,estimatedHarvestDate,initialStatus,seedQuantity,seedingArea,expectedYield);
        
    }
   
   public Result<Updated> Update(DateTime startDate,DateTime estimatedHarvestDate,double seedQuantity,double seedingArea,double expectedYield)
    {
        if(Status!=CycleStatus.Planned && Status!=CycleStatus.Active)
        return SeedingCycleErrors.CycleIsLocked;

        var today=DateTime.UtcNow.Date;

        if(Status==CycleStatus.Planned && startDate.Date<=today)
        return SeedingCycleErrors.PlannedCycleMustStartInFuture;

        if(Status==CycleStatus.Active && startDate.Date>today)
        return SeedingCycleErrors.ActiveCycleCannotStartInFuture;

        if(estimatedHarvestDate.Date<=startDate.Date)
        return SeedingCycleErrors.InvalidDates;

        if (seedQuantity <= 0)
        return SeedingCycleErrors.InvalidSeedQuantity;
            
        if (seedingArea <= 0)
        return SeedingCycleErrors.InvalidSeedingArea;

        if (expectedYield <= 0)
        return SeedingCycleErrors.InvalidExpectedYield;

        StartDate=startDate;
        EstimatedHarvestDate=estimatedHarvestDate;
        SeedQuantity=seedQuantity;
        SeedingArea=seedingArea;
        ExpectedYield=expectedYield;
        return Result.Updated;
    }

    public Result<Updated> Harvest(DateTime actualHarvestDate,double actualYield)
    {
        if(Status!=CycleStatus.Active)
        return SeedingCycleErrors.CycleMustBeActiveToHarvest;

        if(actualHarvestDate.Date<StartDate.Date)
        return SeedingCycleErrors.HarvestDateCannotBeBeforeStart;

        if(actualYield<0)
        return SeedingCycleErrors.InvalidActualYield;

        ActualHarvestDate=actualHarvestDate;
        ActualYield=actualYield;
        Status=CycleStatus.Completed;

        return Result.Updated;
    }

    public Result<Updated> Cancel()
    {
        if(Status!=CycleStatus.Planned)
        return SeedingCycleErrors.CycleCannotBeCancelled;

        Status=CycleStatus.Cancelled;

        return Result.Updated;
    }
    
    public Result<Updated> Activate()
    {
        if(Status!=CycleStatus.Planned)
        return SeedingCycleErrors.CycleMustBePlannedToActivate;

        if(StartDate.Date>DateTime.UtcNow.Date)
        return SeedingCycleErrors.ActiveCycleCannotStartInFuture;

        Status=CycleStatus.Active;
        return Result.Updated;
    }
    public Result<Updated> Fail()
    {
        if(Status!=CycleStatus.Active)
        return SeedingCycleErrors.CycleCannotFail;

        Status=CycleStatus.Failed;
        ActualYield=0;
        return Result.Updated;
    }
}