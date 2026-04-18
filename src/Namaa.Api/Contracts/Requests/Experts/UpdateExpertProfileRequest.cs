using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.Experts.Commands.UpdateProfile;
using Namaa.Domain.Enums;

namespace Namaa.Api.Contracts.Requests.Experts;
public class UpdateExpertProfileRequest
{
    [Required(ErrorMessage = "Specialization is required.")]
    public ExpertSpecialization? Specialization { get; init; }

    [Required(ErrorMessage = "Years of experience is required.")]
    [Range(0, 70, ErrorMessage = "Years of experience cannot be negative or exceed 70.")]
    public int? YearsOfExperience { get; init; }

    [Required(ErrorMessage = "City ID is required.")]
    public int? CityId { get; init; }
    
    [Required(ErrorMessage = "Address details are required.")]
    public string AddressDetail { get; init; } = default!;
    
    [Required(ErrorMessage = "Please specify if on-site visits are possible.")]
    public bool? CanVisitOnSite { get; init; }
    public List<UpdateExpertAvailabilityRequest> Availabilities {get;init;}=[];

    public UpdateExpertProfileCommand ToCommand(Guid userId)
{
    return new UpdateExpertProfileCommand(
        userId,
        Specialization!.Value,
        YearsOfExperience!.Value,
        CityId!.Value,
        AddressDetail,
        CanVisitOnSite!.Value,
        Availabilities.ConvertAll(a => new UpdateExpertAvailabilityCommand(a.Day!.Value, a.StartTime!.Value, a.EndTime!.Value))
    );
}

  
}