using Namaa.Domain.Enums;

namespace Namaa.Api.Contracts.Requests.Traders;
public class GetTradersRequest
{
    public int PageNumber {get;init;}=1;
    public int PageSize {get;init;}=10;
    public int? CityId {get;init;}
    public TraderType? TraderType {get;init;}
     
}