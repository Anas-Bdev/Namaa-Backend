using MediatR;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.CreateInvestorContribution;
public sealed record CreateInvestorContributionCommand(Guid InvestmentProjectId,Guid InvestorId,decimal Amount):IRequest<Result<InvestorContributionDto>>;