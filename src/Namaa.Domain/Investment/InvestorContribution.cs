using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Domain.Investment;

public sealed class InvestorContribution : AuditableEntity
{
    public Guid ProjectId { get; private set; }
    public InvestmentProject? Project { get; private set; }
    public Guid ContributorId { get; private set; }
    public decimal Amount { get; private set; }
    public ContributionStatus Status { get; private set; } = ContributionStatus.Pending;

    private InvestorContribution() { }

    private InvestorContribution(
        Guid id,
        Guid projectId,
        Guid contributorId,
        decimal amount) : base(id)
    {
        ProjectId = projectId;
        ContributorId = contributorId;
        Amount = amount;
    }

    public static Result<InvestorContribution> Create(
        Guid id,
        Guid projectId,
        Guid contributorId,
        decimal amount)
    {
        if (id == Guid.Empty)
            return InvestorContributionErrors.ContributionIdRequired;
        if (amount <= 0)
            return InvestorContributionErrors.InvalidAmount;

        return new InvestorContribution(id, projectId, contributorId, amount);
    }
    
    public Result<Updated> Approve()
    {
        if (Status != ContributionStatus.Pending)
            return InvestorContributionErrors.AlreadyResponded;

        Status = ContributionStatus.Approved;
        return Result.Updated;
    }

    public Result<Updated> Reject()
    {
        if (Status != ContributionStatus.Pending)
            return InvestorContributionErrors.AlreadyResponded;

        Status = ContributionStatus.Rejected;
        return Result.Updated;
    }
}