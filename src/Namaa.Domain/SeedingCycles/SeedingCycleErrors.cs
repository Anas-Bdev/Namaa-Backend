using Namaa.Domain.Common.Results;

namespace Namaa.Domain.SeedingCycles;
public static class SeedingCycleErrors
{
    public static readonly Error InvalidLandId = 
            Error.Validation("SeedingCycle_LandId_Invalid", "The selected land identifier is empty or invalid.");

        public static readonly Error InvalidCropId = 
            Error.Validation("SeedingCycle_CropId_Invalid", "The selected crop identifier must be greater than zero.");

        public static readonly Error PlannedCycleMustStartInFuture = 
            Error.Validation("SeedingCycle_StartDate_InvalidPlanned", "A planned seeding cycle cannot start in the past or today.");

        public static readonly Error ActiveCycleCannotStartInFuture = 
            Error.Validation("SeedingCycle_StartDate_InvalidActive", "An active seeding cycle cannot have a start date in the future.");

        public static readonly Error InvalidDates = 
            Error.Validation("SeedingCycle_Dates_Invalid", "The estimated harvest date must be strictly after the start date.");

        public static readonly Error InvalidInitialStatus = 
            Error.Validation("SeedingCycle_Status_Invalid", "A brand new seeding cycle can only begin with a 'Planned' or 'Active' status.");

        public static readonly Error InvalidSeedQuantity = 
            Error.Validation("SeedingCycle_SeedQuantity_Invalid", "The seed quantity must be greater than zero.");

        public static readonly Error InvalidSeedingArea = 
            Error.Validation("SeedingCycle_SeedingArea_Invalid", "The seeding area must be greater than zero.");

        public static readonly Error InvalidExpectedYield = 
            Error.Validation("SeedingCycle_ExpectedYield_Invalid", "The expected yield must be greater than zero.");
        public static readonly Error IdRequired = 
            Error.Validation("SeedingCycle_Id_Required", "SeedingCycle ID is required.");
        public static readonly Error CycleIsLocked = 
            Error.Validation("SeedingCycle_Status_Locked", "Cannot update a cycle that is already harvested, failed, or cancelled.");
        public static readonly Error CycleMustBeActiveToHarvest = 
            Error.Validation("SeedingCycle_Status_NotActiveToHarvest", "Only active seeding cycles can be harvested.");

        public static readonly Error HarvestDateCannotBeBeforeStart = 
            Error.Validation("SeedingCycle_HarvestDate_Invalid", "The actual harvest date cannot be earlier than the planting date.");
    
        public static readonly Error InvalidActualYield = 
            Error.Validation("SeedingCycle_ActualYield_Invalid", "The actual yield cannot be a negative value.");
        public static readonly Error CycleCannotBeCancelled = 
           Error.Validation("SeedingCycle_Status_CannotCancel", "Only planned cycles can be cancelled.");

        public static readonly Error CycleCannotFail = 
          Error.Validation("SeedingCycle_Status_CannotFail", "Only active cycles can be marked as failed.");

        public static readonly Error CycleMustBePlannedToActivate = 
        Error.Validation("SeedingCycle.InvalidState", "A seeding cycle must be in 'Planned' status to be activated.");    

    }
