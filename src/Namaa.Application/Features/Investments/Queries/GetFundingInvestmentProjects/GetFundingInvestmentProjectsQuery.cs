using MediatR;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Queries.GetFundingInvestmentProjects;
public sealed record GetFundingInvestmentProjectsQuery():IRequest<Result<List<InvestmentProjectListItemDto>>>;