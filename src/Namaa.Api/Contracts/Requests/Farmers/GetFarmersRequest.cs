namespace Namaa.Api.Contracts.Requests.Farmers;
public class GetFarmersRequest
{
    public int PageNumber {get;init;}=1;
    public int PageSize {get;init;}=10;
    public int? CityId {get;init;}

}