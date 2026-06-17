using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Investments;
public static class InvestmentProjectErrors
{
    public static readonly Error ProjectIdRequired =
        Error.Validation("InvestmentProject_Id_Required", "Investment project ID is required.");

    public static readonly Error FarmerIdRequired =
        Error.Validation("InvestmentProject_FarmerId_Required", "Farmer ID is required.");

    public static readonly Error LandIdRequired =
        Error.Validation("InvestmentProject_LandId_Required", "Land ID is required.");

    public static readonly Error TitleRequired =
        Error.Validation("InvestmentProject_Title_Required", "Project title is required.");

    public static readonly Error DescriptionRequired =
        Error.Validation("InvestmentProject_Description_Required", "Project description is required.");

    public static readonly Error InvalidRequiredAmount =
        Error.Validation("InvestmentProject_RequiredAmount_Invalid", "Required amount must be greater than zero.");

    public static readonly Error InvalidMinimumInvestment =
        Error.Validation("InvestmentProject_MinimumInvestment_Invalid", "Minimum investment must be greater than zero.");

    public static readonly Error MinimumInvestmentCannotExceedRequiredAmount =
        Error.Validation("InvestmentProject_MinimumInvestment_ExceedsRequiredAmount", "Minimum investment cannot exceed the required amount.");

    public static readonly Error InvalidFundingDeadline =
        Error.Validation("InvestmentProject_FundingDeadline_Invalid", "Funding deadline must be a future date.");

    public static readonly Error InvalidExpectedRevenue =
        Error.Validation("InvestmentProject_ExpectedRevenue_Invalid", "Expected revenue cannot be negative.");

    public static readonly Error InvalidExpectedCost =
        Error.Validation("InvestmentProject_ExpectedCost_Invalid", "Expected cost cannot be negative.");

    public static readonly Error ExpectedCostCannotExceedExpectedRevenue =
        Error.Validation("InvestmentProject_ExpectedCost_ExceedsExpectedRevenue", "Expected cost cannot exceed expected revenue.");

    public static readonly Error InvalidInvestorProfitSharePercentage =
        Error.Validation("InvestmentProject_InvestorProfitSharePercentage_Invalid", "Investor profit share percentage must be greater than zero and less than or equal to 100.");

    public static readonly Error InvalidDurationInMonths =
        Error.Validation("InvestmentProject_DurationInMonths_Invalid", "Duration in months must be greater than zero.");

    public static readonly Error InvalidExpectedDateRange =
        Error.Validation("InvestmentProject_ExpectedDateRange_Invalid", "Expected end date cannot be earlier than expected start date.");

    public static readonly Error CannotUpdateProject =
        Error.Validation("InvestmentProject_CannotUpdate", "The project cannot be updated in its current status.");

    public static readonly Error InvalidStatusTransition =
        Error.Validation("InvestmentProject_InvalidStatusTransition", "The project status transition is invalid.");

    public static readonly Error InvalidActualRevenue =
        Error.Validation("InvestmentProject_ActualRevenue_Invalid", "Actual revenue cannot be negative.");

    public static readonly Error InvalidActualCost =
        Error.Validation("InvestmentProject_ActualCost_Invalid", "Actual cost cannot be negative.");
    
    public static readonly Error InsufficientCollectedAmount =
    Error.Validation(
        "InvestmentProject_InsufficientCollectedAmount",
        "The collected amount is not enough to mark the project as funded.");
    public static readonly Error ContributionExceedsRemainingAmount =
    Error.Validation(
        "InvestmentProject_Contribution_ExceedsRemainingAmount",
        "Contribution amount cannot exceed the remaining required amount.");

    public static readonly Error ProjectNotOpenForFunding =
    Error.Validation(
        "InvestmentProject_NotOpenForFunding",
        "The project is not open for funding.");

    public static readonly Error NotFound =
    Error.NotFound("InvestmentProject_NotFound", "The specified investment project was not found.");
}