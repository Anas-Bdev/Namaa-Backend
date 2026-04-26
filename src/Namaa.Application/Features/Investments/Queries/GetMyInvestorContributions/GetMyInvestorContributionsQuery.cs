using MediatR;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Queries.GetMyInvestorContributions;
public sealed record GetMyInvestorContributionsQuery(Guid InvestorId):IRequest<Result<List<InvestorContributionListItemDto>>>;