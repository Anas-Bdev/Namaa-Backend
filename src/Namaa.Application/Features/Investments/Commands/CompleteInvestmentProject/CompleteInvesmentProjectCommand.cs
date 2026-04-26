using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.CompleteInvestmentProject;
public sealed record CompleteInvestmentProjectCommand(Guid ProjectId,decimal ActualRevenue,decimal ActualCost):IRequest<Result<Updated>>;