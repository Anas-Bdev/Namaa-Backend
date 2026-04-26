using MediatR;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Queries.GetPendingInvestmentProjects;
public sealed record GetPendingInvestmentProjectsQuery():IRequest<Result<List<InvestmentProjectListItemDto>>>;