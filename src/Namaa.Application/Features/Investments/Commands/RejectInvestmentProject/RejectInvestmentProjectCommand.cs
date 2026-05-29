using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.RejectInvestmentProject;
public sealed record RejectInvestmentProjectCommand(Guid ProjectId):IRequest<Result<Updated>>;