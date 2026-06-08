using MediatR;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Queries.GetMyInvestmentProjects;
public sealed record GetMyInvestmentProjectsQuery(Guid FarmerId):IRequest<Result<List<InvestmentProjectListItemDto>>>;