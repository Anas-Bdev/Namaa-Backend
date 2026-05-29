using System.Net.Sockets;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Application.Features.Investments.Mappers;
using Namaa.Application.Features.Traders.Commands.CreateProfile;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.Investments;

namespace Namaa.Application.Features.Investments.Commands.CreateInvestorContribution;

public class CreateInvestorContributionCommandHandler(IAppDbContext context) : IRequestHandler<CreateInvestorContributionCommand, Result<InvestorContributionDto>>
{
    public async Task<Result<InvestorContributionDto>> Handle(CreateInvestorContributionCommand request, CancellationToken cancellationToken)
    {
        var investmentProject=await context.InvestmentProjects.Include(x => x.Contributions).FirstOrDefaultAsync(x => x.Id==request.InvestmentProjectId,cancellationToken);
        if(investmentProject is null)
        return ApplicationErrors.InvestmentProjectNotFound;

        var createResult=InvestorContribution.Create(Guid.NewGuid(),request.InvestmentProjectId,request.InvestorId,request.Amount);
        if(createResult.IsError)
        return createResult.Errors;
        var addResult=investmentProject.AddContribution(createResult.Value);
        if(addResult.IsError)
        return addResult.Errors;
        await context.SaveChangesAsync(cancellationToken);
        return createResult.Value.ToDto();
    }
}