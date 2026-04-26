using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.StartInvestmentProject;
public sealed record StartInvestmentProjectCommand(Guid ProjectId):IRequest<Result<Updated>>;