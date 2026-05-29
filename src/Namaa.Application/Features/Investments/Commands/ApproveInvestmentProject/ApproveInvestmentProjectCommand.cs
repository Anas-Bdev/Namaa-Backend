using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.ApproveInvestmentProject;
public sealed record ApproveInvestmentProjectCommand(Guid ProjectId):IRequest<Result<Updated>>;