using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Application.Features.Investments.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Investment;

namespace Namaa.Application.Features.Investments.Commands.CreateProject;

public class CreateInvestmentProjectCommandHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<CreateInvestmentProjectCommand, Result<InvestmentProjectDto>>
{
    public async Task<Result<InvestmentProjectDto>> Handle(
        CreateInvestmentProjectCommand request,
        CancellationToken cancellationToken)
    {
        var result = InvestmentProject.Create(
            Guid.NewGuid(),
            request.CreatorId,
            request.CreatorRole,
            request.Title,
            request.Description,
            request.RequiredAmount,
            request.ExpectedProfit,
            request.SharePercentage
        );

        if (result.IsError)
            return result.Errors;

        context.InvestmentProjects.Add(result.Value);
        await context.SaveChangesAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var creatorName = users.FirstOrDefault(u => u.Id == request.CreatorId)?.FullName ?? string.Empty;

        return result.Value.ToDto(creatorName, new Dictionary<Guid, string>());
    }
}