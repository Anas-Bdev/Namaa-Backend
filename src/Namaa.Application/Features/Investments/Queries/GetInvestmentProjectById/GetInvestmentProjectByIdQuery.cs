using MediatR;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Queries.GetInvestmentProjectById;
public sealed record GetInvestmentProjectByIdQuery(Guid ProjectId):IRequest<Result<InvestmentProjectDto>>;