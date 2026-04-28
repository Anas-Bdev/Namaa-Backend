using MediatR;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Experts.Commands.UpdateProfile;
public sealed record UpdateExpertProfileCommand(
    Guid UserId,
    ExpertSpecialization Specialization,
    int YearsOfExperience,
    int CityId,
    string AddressDetail,
    bool CanVisitOnSite,
    List<UpdateExpertAvailabilityCommand> Availabilities
) : IRequest<Result<Updated>>;