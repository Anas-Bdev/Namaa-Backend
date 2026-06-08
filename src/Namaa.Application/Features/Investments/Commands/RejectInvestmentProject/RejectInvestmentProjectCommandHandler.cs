using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.RejectInvestmentProject;

public class RejectInvestmentProjectCommandHandler(IAppDbContext context) : IRequestHandler<RejectInvestmentProjectCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(RejectInvestmentProjectCommand request, CancellationToken cancellationToken)
    {
       var investmentProject=await context.InvestmentProjects.FindAsync([request.ProjectId],cancellationToken);
       if(investmentProject is null)
       return ApplicationErrors.InvestmentProjectNotFound;
       var rejectResult=investmentProject.Reject();
       if(rejectResult.IsError)
       return rejectResult.Errors;
       await context.SaveChangesAsync(cancellationToken);
       return Result.Updated;
    }
}