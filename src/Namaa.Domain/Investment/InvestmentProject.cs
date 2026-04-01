using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Domain.Investment;

public sealed class InvestmentProject : AuditableEntity
{
    public Guid CreatorId { get; private set; }
    public ProjectCreatorRole CreatorRole { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public decimal RequiredAmount { get; private set; }
    public decimal AmountCollected { get; private set; }
    public decimal? ExpectedProfit { get; private set; }
    public decimal? SharePercentage { get; private set; }
    public ProjectStatus Status { get; private set; } = ProjectStatus.Open;

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

    public Result<Updated> Close()
    {
        Status = ProjectStatus.Closed;
        return Result.Updated;
    }

    public Result<Updated> Cancel()
    {
        Status = ProjectStatus.Cancelled;
        return Result.Updated;
    }

    public Result<Updated> AddContribution(InvestorContribution contribution)
    {
        if (Status != ProjectStatus.Open)
            return InvestmentProjectErrors.ProjectNotOpen;

        _contributions.Add(contribution);
        AmountCollected += contribution.Amount;

        return Result.Updated;
    }
}