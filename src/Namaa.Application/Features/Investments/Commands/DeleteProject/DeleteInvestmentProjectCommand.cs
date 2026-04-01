using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.DeleteProject;

public sealed record DeleteInvestmentProjectCommand(
    Guid ProjectId,
    Guid UserId
) : IRequest<Result<Deleted>>;