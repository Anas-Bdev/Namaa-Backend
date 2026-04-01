using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Investment;

public static class InvestmentProjectErrors
{
    public static readonly Error ProjectIdRequired = Error.Validation(
        "InvestmentProject.ProjectIdRequired", "A valid Project ID must be provided.");
    public static readonly Error CreatorIdRequired = Error.Validation(
        "InvestmentProject.CreatorIdRequired", "A valid Creator ID must be provided.");
    public static readonly Error TitleRequired = Error.Validation(
        "InvestmentProject.TitleRequired", "Project title is required.");
    public static readonly Error InvalidRequiredAmount = Error.Validation(
        "InvestmentProject.InvalidRequiredAmount", "Required amount must be greater than zero.");
    public static readonly Error Unauthorized = Error.Forbidden(
        "InvestmentProject.Unauthorized", "You are not authorized to modify this project.");
    public static readonly Error CannotDeleteWithContributions = Error.Conflict(
        "InvestmentProject.CannotDelete", "Cannot delete a project with approved contributions.");
    public static readonly Error ProjectNotOpen = Error.Conflict(
        "InvestmentProject.NotOpen", "This project is no longer open for contributions.");
    public static readonly Error CannotContributeToOwnProject = Error.Conflict(
        "InvestmentProject.CannotContribute", "You cannot contribute to your own project.");
}