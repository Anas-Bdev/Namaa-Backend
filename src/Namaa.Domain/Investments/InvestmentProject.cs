using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Domain.Investments;
public sealed class InvestmentProject : AuditableEntity
{
    public Guid FarmerId {get;}
    public Guid LandId {get;}
    public string Title {get;private set;}
    public string Description {get;private set;}
    public string? CoverImageUrl {get;private set;}
    public decimal RequiredAmount {get;private set;}
    public decimal MinimumInvestment {get;private set;}
    public DateTime FundingDeadline {get;private set;}
    public decimal ExpectedRevenue {get;private set;}
    public decimal ExpectedCost {get;private set;}
    public decimal ExpectedProfit => ExpectedRevenue-ExpectedCost;
    public decimal InvestorProfitSharePercentage {get;private set;}
    public decimal? ActualRevenue {get;private set;}
    public decimal? ActualCost {get;private set;}
    public decimal? ActualProfit => ActualRevenue.HasValue && ActualCost.HasValue ? ActualRevenue.Value-ActualCost.Value : null;
    public int DurationInMonths {get;private set;}
    public DateTime? ExpectedStartDate {get;private set;}
    public DateTime? ExpectedEndDate {get;private set;}
    public InvestmentProjectStatus Status {get;private set;}
    private readonly List<InvestorContribution> _contributions=[];
    public IEnumerable<InvestorContribution> Contributions => _contributions.AsReadOnly();
    public decimal AmountCollected => _contributions.Where(c => c.Status == ContributionStatus.Paid).Sum(c => c.Amount);

    #pragma warning disable CS8618
   private InvestmentProject() {}
   #pragma warning restore CS8618
     private InvestmentProject(
        Guid id,
        Guid farmerId,
        Guid landId,
        string title,
        string description,
        string? coverImageUrl,
        decimal requiredAmount,
        decimal minimumInvestment,
        DateTime fundingDeadline,
        decimal expectedRevenue,
        decimal expectedCost,
        decimal investorProfitSharePercentage,
        int durationInMonths,
        DateTime? expectedStartDate,
        DateTime? expectedEndDate) : base(id)
    {
        FarmerId = farmerId;
        LandId = landId;
        Title = title;
        Description = description;
        CoverImageUrl = coverImageUrl;
        RequiredAmount = requiredAmount;
        MinimumInvestment = minimumInvestment;
        FundingDeadline = fundingDeadline;
        ExpectedRevenue = expectedRevenue;
        ExpectedCost = expectedCost;
        InvestorProfitSharePercentage = investorProfitSharePercentage;
        DurationInMonths = durationInMonths;
        ExpectedStartDate = expectedStartDate;
        ExpectedEndDate = expectedEndDate;
        Status = InvestmentProjectStatus.Funding;
    }
    public static Result<InvestmentProject> Create(
    Guid id,
    Guid farmerId,
    Guid landId,
    string title,
    string description,
    string? coverImageUrl,
    decimal requiredAmount,
    decimal minimumInvestment,
    DateTime fundingDeadline,
    decimal expectedRevenue,
    decimal expectedCost,
    decimal investorProfitSharePercentage,
    int durationInMonths,
    DateTime? expectedStartDate,
    DateTime? expectedEndDate)
{
    if (id == Guid.Empty)
        return InvestmentProjectErrors.ProjectIdRequired;

    if (farmerId == Guid.Empty)
        return InvestmentProjectErrors.FarmerIdRequired;

    if (landId == Guid.Empty)
        return InvestmentProjectErrors.LandIdRequired;

    if (string.IsNullOrWhiteSpace(title))
        return InvestmentProjectErrors.TitleRequired;

    if (string.IsNullOrWhiteSpace(description))
        return InvestmentProjectErrors.DescriptionRequired;

    if (requiredAmount <= 0)
        return InvestmentProjectErrors.InvalidRequiredAmount;

    if (minimumInvestment <= 0)
        return InvestmentProjectErrors.InvalidMinimumInvestment;

    if (minimumInvestment > requiredAmount)
        return InvestmentProjectErrors.MinimumInvestmentCannotExceedRequiredAmount;

    if (fundingDeadline <= DateTime.UtcNow)
        return InvestmentProjectErrors.InvalidFundingDeadline;

    if (expectedRevenue < 0)
        return InvestmentProjectErrors.InvalidExpectedRevenue;

    if (expectedCost < 0)
        return InvestmentProjectErrors.InvalidExpectedCost;

    if (expectedCost > expectedRevenue)
        return InvestmentProjectErrors.ExpectedCostCannotExceedExpectedRevenue;

    if (investorProfitSharePercentage <= 0 || investorProfitSharePercentage > 100)
        return InvestmentProjectErrors.InvalidInvestorProfitSharePercentage;

    if (durationInMonths <= 0)
        return InvestmentProjectErrors.InvalidDurationInMonths;

    if (expectedStartDate.HasValue && expectedEndDate.HasValue &&
        expectedEndDate.Value < expectedStartDate.Value)
    {
        return InvestmentProjectErrors.InvalidExpectedDateRange;
    }

    return new InvestmentProject(
        id,
        farmerId,
        landId,
        title,
        description,
        coverImageUrl,
        requiredAmount,
        minimumInvestment,
        fundingDeadline,
        expectedRevenue,
        expectedCost,
        investorProfitSharePercentage,
        durationInMonths,
        expectedStartDate,
        expectedEndDate);
}

public Result<Updated> Update(
    string title,
    string description,
    string? coverImageUrl,
    decimal requiredAmount,
    decimal minimumInvestment,
    DateTime fundingDeadline,
    decimal expectedRevenue,
    decimal expectedCost,
    decimal investorProfitSharePercentage,
    int durationInMonths,
    DateTime? expectedStartDate,
    DateTime? expectedEndDate)
{
    if (Status != InvestmentProjectStatus.Funding)
        return InvestmentProjectErrors.CannotUpdateProject;

    if (string.IsNullOrWhiteSpace(title))
        return InvestmentProjectErrors.TitleRequired;

    if (string.IsNullOrWhiteSpace(description))
        return InvestmentProjectErrors.DescriptionRequired;

    if (requiredAmount <= 0)
        return InvestmentProjectErrors.InvalidRequiredAmount;

    if (minimumInvestment <= 0)
        return InvestmentProjectErrors.InvalidMinimumInvestment;

    if (minimumInvestment > requiredAmount)
        return InvestmentProjectErrors.MinimumInvestmentCannotExceedRequiredAmount;

    if (fundingDeadline <= DateTime.UtcNow)
        return InvestmentProjectErrors.InvalidFundingDeadline;

    if (expectedRevenue < 0)
        return InvestmentProjectErrors.InvalidExpectedRevenue;

    if (expectedCost < 0)
        return InvestmentProjectErrors.InvalidExpectedCost;

    if (expectedCost > expectedRevenue)
        return InvestmentProjectErrors.ExpectedCostCannotExceedExpectedRevenue;

    if (investorProfitSharePercentage <= 0 || investorProfitSharePercentage > 100)
        return InvestmentProjectErrors.InvalidInvestorProfitSharePercentage;

    if (durationInMonths <= 0)
        return InvestmentProjectErrors.InvalidDurationInMonths;

    if (expectedStartDate.HasValue &&
        expectedEndDate.HasValue &&
        expectedEndDate.Value < expectedStartDate.Value)
    {
        return InvestmentProjectErrors.InvalidExpectedDateRange;
    }

    Title = title;
    Description = description;
    CoverImageUrl = coverImageUrl;
    RequiredAmount = requiredAmount;
    MinimumInvestment = minimumInvestment;
    FundingDeadline = fundingDeadline;
    ExpectedRevenue = expectedRevenue;
    ExpectedCost = expectedCost;
    InvestorProfitSharePercentage = investorProfitSharePercentage;
    DurationInMonths = durationInMonths;
    ExpectedStartDate = expectedStartDate;
    ExpectedEndDate = expectedEndDate;

    return Result.Updated;
}




public Result<Updated> StartProgress()
{
    if (Status != InvestmentProjectStatus.Funded)
        return InvestmentProjectErrors.InvalidStatusTransition;

    Status = InvestmentProjectStatus.InProgress;
    return Result.Updated;
}
public Result<Updated> Complete(decimal actualRevenue, decimal actualCost)
{
    if (Status != InvestmentProjectStatus.InProgress)
        return InvestmentProjectErrors.InvalidStatusTransition;

    if (actualRevenue < 0)
        return InvestmentProjectErrors.InvalidActualRevenue;

    if (actualCost < 0)
        return InvestmentProjectErrors.InvalidActualCost;

    ActualRevenue = actualRevenue;
    ActualCost = actualCost;
    Status = InvestmentProjectStatus.Completed;

    return Result.Updated;
}
public Result<Updated> Cancel()
{
    if (Status != InvestmentProjectStatus.Funding)
    {
        return InvestmentProjectErrors.InvalidStatusTransition;
    }

    Status = InvestmentProjectStatus.Cancelled;
    return Result.Updated;
}

    
public Result<Updated> AddContribution(InvestorContribution contribution)
    {
        if (Status != InvestmentProjectStatus.Funding)
        return InvestmentProjectErrors.ProjectNotOpenForFunding;

    if (contribution.Amount < MinimumInvestment)
        return InvestorContributionErrors.AmountBelowMinimumInvestment;

    var remainingAmount = RequiredAmount - AmountCollected;
    if (contribution.Amount > remainingAmount)
        return InvestmentProjectErrors.ContributionExceedsRemainingAmount;
        _contributions.Add(contribution);
        return Result.Updated;
    }

    public Result<Updated> ProcessPaymentForContribution(Guid contributionId)
{
    if (Status != InvestmentProjectStatus.Funding)
        return InvestmentProjectErrors.ProjectNotOpenForFunding;

    var contribution = _contributions.FirstOrDefault(c => c.Id == contributionId);
    if (contribution == null)
        return InvestorContributionErrors.NotFound;

    var paymentResult = contribution.ConfirmPayment();
    if (paymentResult.IsError)
        return paymentResult;

    if (AmountCollected >= RequiredAmount)
    {
        Status = InvestmentProjectStatus.Funded;
    }

    return Result.Updated;
}

}