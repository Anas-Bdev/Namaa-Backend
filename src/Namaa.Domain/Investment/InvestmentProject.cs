using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Domain.Investment;

public sealed class InvestmentProject : AuditableEntity
{
    public Guid CreatorId { get;  }
    public ProjectCreatorRole CreatorRole { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public decimal RequiredAmount { get; private set; }
    public decimal AmountCollected { get; private set; }
    public decimal? ExpectedProfit { get; private set; }
    public decimal? SharePercentage { get; private set; }
    public decimal? ActualRevenue { get; private set; }
    public decimal? ActualCost { get; private set; }
    public decimal? ActualProfit { get; private set; }
    public ProjectStatus Status { get; private set; } = ProjectStatus.Pending;

    private readonly List<InvestorContribution> _contributions = [];
    public IReadOnlyCollection<InvestorContribution> Contributions => _contributions.AsReadOnly();

    private InvestmentProject() { }

    private InvestmentProject(
        Guid id,
        Guid creatorId,
        ProjectCreatorRole creatorRole,
        string title,
        string? description,
        decimal requiredAmount,
        decimal? expectedProfit,
        decimal? sharePercentage) : base(id)
    {
        CreatorId = creatorId;
        CreatorRole = creatorRole;
        Title = title;
        Description = description;
        RequiredAmount = requiredAmount;
        ExpectedProfit = expectedProfit;
        SharePercentage = sharePercentage;
    }

    public static Result<InvestmentProject> Create(
        Guid id,
        Guid creatorId,
        ProjectCreatorRole creatorRole,
        string title,
        string? description,
        decimal requiredAmount,
        decimal? expectedProfit,
        decimal? sharePercentage)
    {
        if (id == Guid.Empty)
            return InvestmentProjectErrors.ProjectIdRequired;
        if (creatorId == Guid.Empty)
            return InvestmentProjectErrors.CreatorIdRequired;
        if (string.IsNullOrWhiteSpace(title))
            return InvestmentProjectErrors.TitleRequired;
        if (requiredAmount <= 0)
            return InvestmentProjectErrors.InvalidRequiredAmount;

        return new InvestmentProject(
            id, creatorId, creatorRole, title,
            description, requiredAmount, expectedProfit, sharePercentage);
    }

    public Result<Updated> Update(
        string title,
        string? description,
        decimal requiredAmount,
        decimal? expectedProfit,
        decimal? sharePercentage)
    {
        if (Status != ProjectStatus.Pending && Status != ProjectStatus.Funding)
            return InvestmentProjectErrors.CannotUpdateProject;
        if (string.IsNullOrWhiteSpace(title))
            return InvestmentProjectErrors.TitleRequired;
        if (requiredAmount <= 0)
            return InvestmentProjectErrors.InvalidRequiredAmount;

        Title = title;
        Description = description;
        RequiredAmount = requiredAmount;
        ExpectedProfit = expectedProfit;
        SharePercentage = sharePercentage;

        return Result.Updated;
    }

    public Result<Updated> StartFunding()
    {
        if (Status != ProjectStatus.Pending)
            return InvestmentProjectErrors.InvalidStatusTransition;
        Status = ProjectStatus.Funding;
        return Result.Updated;
    }

    public Result<Updated> StartWork()
    {
        if (Status != ProjectStatus.Funded)
            return InvestmentProjectErrors.InvalidStatusTransition;
        Status = ProjectStatus.InProgress;
        return Result.Updated;
    }

    public Result<Updated> Cancel()
    {
        if (Status == ProjectStatus.Completed)
            return InvestmentProjectErrors.InvalidStatusTransition;
        Status = ProjectStatus.Cancelled;
        return Result.Updated;
    }

    public Result<Updated> ApproveContribution(InvestorContribution contribution)
    {
        if (Status != ProjectStatus.Funding)
            return InvestmentProjectErrors.ProjectNotOpen;

        var approveResult = contribution.Approve();
        if (approveResult.IsError)
            return approveResult.Errors;

        AmountCollected += contribution.Amount;

        if (AmountCollected >= RequiredAmount)
            Status = ProjectStatus.Funded;

        return Result.Updated;
    }

    public Result<Updated> RejectContribution(InvestorContribution contribution)
    {
        var rejectResult = contribution.Reject();
        if (rejectResult.IsError)
            return rejectResult.Errors;
        return Result.Updated;
    }

    public Result<Updated> SetActualResults(decimal actualRevenue, decimal actualCost)
    {
        if (Status != ProjectStatus.InProgress)
            return InvestmentProjectErrors.InvalidStatusTransition;
        if (actualRevenue < 0)
            return InvestmentProjectErrors.InvalidActualRevenue;
        if (actualCost < 0)
            return InvestmentProjectErrors.InvalidActualCost;

        ActualRevenue = actualRevenue;
        ActualCost = actualCost;
        ActualProfit = actualRevenue - actualCost;

        if (ActualProfit > 0 && SharePercentage.HasValue)
        {
            var investorsTotalProfit = ActualProfit.Value * (SharePercentage.Value / 100);

            foreach (var contribution in _contributions.Where(c => c.Status == ContributionStatus.Approved))
            {
                var contributionShare = contribution.Amount / AmountCollected;
                var profitShare = investorsTotalProfit * contributionShare;
                contribution.SetProfitShare(profitShare);
            }
        }

        Status = ProjectStatus.Completed;
        return Result.Updated;
    }

    public Result<Updated> AddContribution(InvestorContribution contribution)
    {
        if (Status != ProjectStatus.Funding)
            return InvestmentProjectErrors.ProjectNotOpen;

        _contributions.Add(contribution);
        return Result.Updated;
    }
}