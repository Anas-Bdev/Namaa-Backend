using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.RejectInvestorContribution;
public sealed record RejectInvestorContributionCommand(Guid ContributionId):IRequest<Result<Updated>>;