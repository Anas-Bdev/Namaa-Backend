using Namaa.Domain.Enums;

namespace Namaa.Api.Contracts.Requests.Investors;
public class GetInvestorsRequest
{
    public int PageNumber {get;init;}=1;
    public int PageSize {get;init;}=10;
    public int? CityId {get;init;}
    public InvestorType? InvestorType {get;init;}
}