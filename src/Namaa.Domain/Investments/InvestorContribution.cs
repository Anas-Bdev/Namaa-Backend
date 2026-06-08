using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
namespace Namaa.Domain.Investments;
public sealed class InvestorContribution:AuditableEntity
{
    public Guid InvestmentProjectId {get;}
    public Guid InvestorId {get;}
    public InvestmentProject? InvestmentProject {get;set;}
    public decimal Amount {get;private set;}
    public decimal? ProfitAmount {get;private set;}
    public ContributionStatus  Status {get;private set;}
    private InvestorContribution () {}
     private InvestorContribution(
        Guid id,
        Guid investmentProjectId,
        Guid investorId,
        decimal amount) : base(id)
    {
        InvestmentProjectId = investmentProjectId;
        InvestorId = investorId;
        Amount = amount;
        Status = ContributionStatus.Pending;
    }

    public static Result<InvestorContribution> Create(
        Guid id,
        Guid investmentProjectId,
        Guid investorId,
        decimal amount)
    {
        if (id == Guid.Empty)
            return InvestorContributionErrors.IdRequired;

        if (investmentProjectId == Guid.Empty)
            return InvestorContributionErrors.InvestmentProjectIdRequired;

        if (investorId == Guid.Empty)
            return InvestorContributionErrors.InvestorIdRequired;

        if (amount <= 0)
            return InvestorContributionErrors.InvalidAmount;

        return new InvestorContribution(
            id,
            investmentProjectId,
            investorId,
            amount);
    }

    public Result<Updated> Approve()
    {
        if (Status != ContributionStatus.Pending)
        return InvestorContributionErrors.InvalidStatusTransition;

        Status = ContributionStatus.Approved;
        return Result.Updated;
    }

    public Result<Updated> Reject()
    {
        if (Status != ContributionStatus.Pending)
            return InvestorContributionErrors.InvalidStatusTransition;

        Status = ContributionStatus.Rejected;
        return Result.Updated;
    }


    public Result<Updated> SetProfitAmount(decimal profitAmount)
    {
        if (Status != ContributionStatus.Approved)
            return InvestorContributionErrors.InvalidStatusTransition;

        if (profitAmount < 0)
            return InvestorContributionErrors.InvalidProfitAmount;

        ProfitAmount = profitAmount;
        return Result.Updated;
    }

}