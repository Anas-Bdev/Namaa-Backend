using MediatR;
<<<<<<< HEAD
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Experts.Commands.UpdateProfile;
public sealed record UpdateExpertAvailabilityCommand(DayOfWeek Day,TimeSpan StartTime,TimeSpan EndTime):IRequest<Result<Updated>>;
=======
using Namaa.Application.Features.Experts.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Experts.Commands.UpdateProfile;
public sealed record CreateExpertAvailabilityCommand(DayOfWeek Day,TimeSpan StartTime,TimeSpan EndTime):IRequest<Result<ExpertAvailabilityDto>>;
>>>>>>> dev-alaa
