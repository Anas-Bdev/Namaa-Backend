using MediatR;
using Namaa.Application.Features.Experts.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Experts.Commands.UpdateProfile;
public sealed record CreateExpertAvailabilityCommand(DayOfWeek Day,TimeSpan StartTime,TimeSpan EndTime):IRequest<Result<ExpertAvailabilityDto>>;