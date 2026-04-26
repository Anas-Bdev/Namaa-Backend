using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.CancelInvestmentProject;
public sealed record CancelInvestmentProjectCommand(Guid ProjectId):IRequest<Result<Updated>>;