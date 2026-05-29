using System.ComponentModel.DataAnnotations;

namespace Namaa.Api.Contracts.Requests.Investments;
public class CompleteInvestmentProjectRequest
{
    [Required(ErrorMessage = "Actual revenue is required.")]
    public decimal ActualRevenue { get; set; }

    [Required(ErrorMessage = "Actual cost is required.")]
    public decimal ActualCost { get; set; }
}