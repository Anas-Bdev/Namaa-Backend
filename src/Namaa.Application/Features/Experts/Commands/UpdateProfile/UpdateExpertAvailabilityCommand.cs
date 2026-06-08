using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Experts.Commands.UpdateProfile;
public sealed record UpdateExpertAvailabilityCommand(DayOfWeek Day,TimeSpan StartTime,TimeSpan EndTime):IRequest<Result<Updated>>;