using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Domain.Profiles.Expert;
public sealed class ExpertProfile:AuditableEntity
{
    public string? CvUrl {get;private set;}
    public int? YearsOfExperience {get;private set;}
    public int? CityId {get;private set;}
    public string? AddressDetail {get;private set;}
    public ExpertSpecialization? Specialization {get;private set;}
    public bool? CanVisitOnSite {get;private set;}
    private readonly List<ExpertAvailability> _availabilities=[];
    public IReadOnlyCollection<ExpertAvailability> Availabilities => _availabilities.AsReadOnly();
    
    private ExpertProfile() {}
    private ExpertProfile(Guid id,string cvUrl) : base(id)
    {
        this.CvUrl=cvUrl;
    }

   public static Result<ExpertProfile> Create(Guid id,string cvUrl)
    {
        if (id == Guid.Empty)
            return ExpertErrors.UserIdRequired;

        if (string.IsNullOrWhiteSpace(cvUrl))
            return ExpertErrors.CvRequired;

            return new ExpertProfile(id,cvUrl);
    }


    public Result<Updated> UpdateProfile(
    ExpertSpecialization specialization,
    int yearsOfExperience,
    int cityId,
    string addressDetail,
    bool canVisitOnSite) 
{
    if (yearsOfExperience < 0) return ExpertErrors.InvalidExperience;
    if (cityId <= 0) return ExpertErrors.CityRequired;

    Specialization = specialization;
    YearsOfExperience = yearsOfExperience;
    CityId = cityId;
    AddressDetail = addressDetail;
    CanVisitOnSite = canVisitOnSite;

    return Result.Updated;
}
    public Result<Updated> UpdateCvUrl(string newCvUrl)
    {
        if (string.IsNullOrWhiteSpace(newCvUrl))
        {
            return ExpertErrors.CvUrlRequired;
        }

        CvUrl = newCvUrl;
        return Result.Updated;
    }
    
  public Result<Updated> UpdatedAvailability(List<ExpertAvailability> incomingSlots)
    {
        _availabilities.RemoveAll(existing => !incomingSlots.Any(incoming => incoming.Id==existing.Id));

        foreach(var incoming in incomingSlots)
        {
            var existing=_availabilities.FirstOrDefault( v => v.Id==incoming.Id);
            
            if(existing is null){
            _availabilities.Add(incoming);
            }
            
            else{

          var UpdatedTimeSlotResult=existing.Update(incoming.Day,incoming.StartTime,incoming.EndTime);
                if (UpdatedTimeSlotResult.IsError)
                {
                    return UpdatedTimeSlotResult.Errors;
                }
          
            }
        }
        return Result.Updated;
    }
        
    }