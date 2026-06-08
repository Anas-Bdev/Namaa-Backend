using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.ApproveInvestorContribution;
public sealed record ApproveInvestorContributionCommand(Guid ContributionId):IRequest<Result<Updated>>;