using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Profiles.Expert;
public sealed class ExpertAvailability:AuditableEntity
{
<<<<<<< HEAD
 public Guid ExpertProfileId {get;}
=======
 public Guid ExpertProfileId {get;private set;}
>>>>>>> dev-alaa
 public ExpertProfile? Expert {get;private set;}
 public DayOfWeek Day {get;private set;}
 public TimeSpan StartTime {get;private set;}
 public TimeSpan EndTime {get;private set;}

 private ExpertAvailability () {}

<<<<<<< HEAD
 private ExpertAvailability(Guid id,DayOfWeek day,TimeSpan startTime,TimeSpan endTime)
 : base(id)
    {
=======
 private ExpertAvailability(Guid id,Guid expertProfileId,DayOfWeek day,TimeSpan startTime,TimeSpan endTime)
 : base(id)
    {
        ExpertProfileId=expertProfileId;
>>>>>>> dev-alaa
        Day=day;
        StartTime=startTime;
        EndTime=endTime;
    }

    public static Result<ExpertAvailability> Create(
        Guid id, 
<<<<<<< HEAD
=======
        Guid expertProfileId, 
>>>>>>> dev-alaa
        DayOfWeek day, 
        TimeSpan startTime, 
        TimeSpan endTime)
    {
        if (id == Guid.Empty)
        return ExpertErrors.AvailabilityIdRequired;

<<<<<<< HEAD
=======
        if (expertProfileId == Guid.Empty)
         return ExpertErrors.UserIdRequired;    
>>>>>>> dev-alaa

         if (startTime >= endTime)
         return ExpertErrors.InvalidTimeRange;

<<<<<<< HEAD
         return new ExpertAvailability(id, day, startTime, endTime);
=======
         return new ExpertAvailability(id, expertProfileId, day, startTime, endTime);
>>>>>>> dev-alaa
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
