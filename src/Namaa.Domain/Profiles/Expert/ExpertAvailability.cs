using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Profiles.Expert;
public sealed class ExpertAvailability:AuditableEntity
{
 public Guid ExpertProfileId {get;private set;}
 public ExpertProfile? Expert {get;private set;}
 public DayOfWeek Day {get;private set;}
 public TimeSpan StartTime {get;private set;}
 public TimeSpan EndTime {get;private set;}

 private ExpertAvailability () {}

 private ExpertAvailability(Guid id,Guid expertProfileId,DayOfWeek day,TimeSpan startTime,TimeSpan endTime)
 : base(id)
    {
        ExpertProfileId=expertProfileId;
        Day=day;
        StartTime=startTime;
        EndTime=endTime;
    }

    public static Result<ExpertAvailability> Create(
        Guid id, 
        Guid expertProfileId, 
        DayOfWeek day, 
        TimeSpan startTime, 
        TimeSpan endTime)
    {
        if (id == Guid.Empty)
        return ExpertErrors.AvailabilityIdRequired;

        if (expertProfileId == Guid.Empty)
         return ExpertErrors.UserIdRequired;    

         if (startTime >= endTime)
         return ExpertErrors.InvalidTimeRange;

         return new ExpertAvailability(id, expertProfileId, day, startTime, endTime);
    }

    public Result<Updated> Update(DayOfWeek day, TimeSpan startTime, TimeSpan endTime)
    {
        if (startTime >= endTime)
            return ExpertErrors.InvalidTimeRange;

        Day = day;
        StartTime = startTime;
        EndTime = endTime;

        return Result.Updated;
    }
}
