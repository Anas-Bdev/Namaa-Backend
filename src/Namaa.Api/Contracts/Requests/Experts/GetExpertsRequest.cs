
using Namaa.Application.Features.Experts.Queries.GetExperts;
using Namaa.Domain.Enums;
using Org.BouncyCastle.Ocsp;

namespace Namaa.Api.Contracts.Requests.Experts;
public class GetExpertsRequest
{
    public int PageNumber {get;init;}=1;
    public int PageSize {get;init;}=10;
    public ExpertSpecialization? Specialization {get;init;}
    public int? CityId {get;init;}

    public GetExpertsQuery ToQuery()
    {
        return new(PageNumber,PageSize,CityId,Specialization);
    }
} 