using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Experts.Commands.ApproveExpert;
public sealed record ApproveExpertCommand(Guid ExpertId):IRequest<Result<Updated>>;