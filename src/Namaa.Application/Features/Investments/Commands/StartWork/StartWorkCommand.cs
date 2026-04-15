using MediatR;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.StartWork;

public sealed record StartWorkCommand(
    Guid ProjectId,
    Guid UserId
) : IRequest<Result<InvestmentProjectDto>>;