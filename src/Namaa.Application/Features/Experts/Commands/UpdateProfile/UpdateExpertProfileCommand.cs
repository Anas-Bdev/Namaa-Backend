using MediatR;
using Namaa.Application.Features.Experts.Dtos;
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
<<<<<<< HEAD
    List<UpdateExpertAvailabilityCommand> Availabilities
) : IRequest<Result<Updated>>;
=======
    List<CreateExpertAvailabilityCommand> Availabilities
) : IRequest<Result<ExpertProfileDto>>;
>>>>>>> dev-alaa
