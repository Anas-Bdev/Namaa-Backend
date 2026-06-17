using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Investments;
public static class InvestorContributionErrors
{
     public static readonly Error IdRequired =
        Error.Validation("InvestorContribution_Id_Required", "Contribution ID is required.");

    public static readonly Error InvestmentProjectIdRequired =
        Error.Validation("InvestorContribution_InvestmentProjectId_Required", "Investment project ID is required.");

    public static readonly Error InvestorIdRequired =
        Error.Validation("InvestorContribution_InvestorId_Required", "Investor ID is required.");

    public static readonly Error InvalidAmount =
        Error.Validation("InvestorContribution_Amount_Invalid", "Contribution amount must be greater than zero.");

    public static readonly Error InvalidProfitAmount =
        Error.Validation("InvestorContribution_ProfitAmount_Invalid", "Profit amount cannot be negative.");

    public static readonly Error InvalidStatusTransition =
        Error.Validation("InvestorContribution_InvalidStatusTransition", "The contribution status transition is invalid.");
    public static readonly Error AmountBelowMinimumInvestment =
    Error.Validation(
        "InvestorContribution_Amount_BelowMinimumInvestment",
        "Contribution amount cannot be less than the minimum investment.");

    public static readonly Error NotFound =
        Error.NotFound("InvestorContribution_NotFound", "The specified investor contribution was not found.");
}