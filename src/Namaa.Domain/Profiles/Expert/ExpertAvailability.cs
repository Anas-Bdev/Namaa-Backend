using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Profiles.Expert;
public sealed class ExpertAvailability:AuditableEntity
{
 public Guid ExpertProfileId {get;}
 public ExpertProfile? Expert {get;private set;}
 public DayOfWeek Day {get;private set;}
 public TimeSpan StartTime {get;private set;}
 public TimeSpan EndTime {get;private set;}

 private ExpertAvailability () {}

 private ExpertAvailability(Guid id,DayOfWeek day,TimeSpan startTime,TimeSpan endTime)
 : base(id)
    {
        Day=day;
        StartTime=startTime;
        EndTime=endTime;
    }

    public static Result<ExpertAvailability> Create(
        Guid id, 
        DayOfWeek day, 
        TimeSpan startTime, 
        TimeSpan endTime)
    {
        if (id == Guid.Empty)
        return ExpertErrors.AvailabilityIdRequired;


         if (startTime >= endTime)
         return ExpertErrors.InvalidTimeRange;

         return new ExpertAvailability(id, day, startTime, endTime);
    }
}
