using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.WithdrawInvestorContribution;
public sealed record WithdrawInvestorContributionCommand(
Guid ProjectId, 
Guid ContributionId, 
Guid InvestorId) : IRequest<Result<Updated>>;