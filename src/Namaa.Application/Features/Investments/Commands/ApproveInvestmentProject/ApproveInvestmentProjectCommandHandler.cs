using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.ApproveInvestmentProject;

public class ApproveInvestmentProjectCommandHandler(IAppDbContext context) : IRequestHandler<ApproveInvestmentProjectCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(ApproveInvestmentProjectCommand request, CancellationToken cancellationToken)
    {
      var investmentProject=await context.InvestmentProjects.FindAsync([request.ProjectId],cancellationToken);
      if(investmentProject is null)
      return ApplicationErrors.InvestmentProjectNotFound;
      var approveResult=investmentProject.Approve();
      if(approveResult.IsError)
      return approveResult.Errors;
      await context.SaveChangesAsync(cancellationToken);
      return Result.Updated;
    }
}