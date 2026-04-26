using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Application.Features.Investments.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Investments;

namespace Namaa.Application.Features.Investments.Commands.CreateInvestmentProject;

public class CreateInvestmentProjectCommandHandler(IAppDbContext context) : IRequestHandler<CreateInvestmentProjectCommand, Result<InvestmentProjectDto>>
{
    public async Task<Result<InvestmentProjectDto>> Handle(CreateInvestmentProjectCommand request, CancellationToken cancellationToken)
    {
        var result=InvestmentProject.Create(
            Guid.NewGuid(),
            request.FarmerId,
            request.LandId,
            request.Title,
            request.Description,
            request.CoverImageUrl,
            request.RequiredAmount,
            request.MinimumInvestment,
            request.FundingDeadline,
            request.ExpectedRevenue,
            request.ExpectedCost,
            request.InvestorProfitSharePercentage,
            request.DurationInMonths,
            request.ExpectedStartDate,
            request.ExpectedEndDate
        );
        
        if(result.IsError)
        return result.Errors;

        context.InvestmentProjects.Add(result.Value);
        await context.SaveChangesAsync(cancellationToken);
        return result.Value.ToDto();
    }
}