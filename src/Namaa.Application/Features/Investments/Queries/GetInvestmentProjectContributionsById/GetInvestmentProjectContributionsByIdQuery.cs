using MediatR;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Queries.GetInvestmentProjectContributionsByIdj;
public sealed record GetInvestmentProjectContributionsByIdQuery(Guid ProjectId):IRequest<Result<List<InvestorContributionListItemDto>>>;