using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Experts.Commands.RejectExpert;
public sealed record RejectExpertCommand(Guid ExpertId,string Reason):IRequest<Result<Updated>>;