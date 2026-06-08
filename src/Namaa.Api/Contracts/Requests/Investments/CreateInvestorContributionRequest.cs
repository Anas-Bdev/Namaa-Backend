using System.ComponentModel.DataAnnotations;

namespace Namaa.Api.Contracts.Requests.Investments;
public class CreateInvestorContributionRequest
{
    [Required(ErrorMessage = "Contribution amount is required.")]
    public decimal Amount { get; init; }

}