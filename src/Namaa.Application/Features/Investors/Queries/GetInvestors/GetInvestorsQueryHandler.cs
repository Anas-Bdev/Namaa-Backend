using System.Runtime.InteropServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Investors.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Investors.Queries.GetInvestors;

public class GetInvestorQueryHandler(IAppDbContext context, IUserReadRepository userReadRepository) : IRequestHandler<GetInvestorsQuery, Result<PaginatedList<InvestorListItemDto>>>
{
    public async Task<Result<PaginatedList<InvestorListItemDto>>> Handle(GetInvestorsQuery request, CancellationToken cancellationToken)
    {
    
  var usersQuery = userReadRepository.Query();

// 1. Build the base join query
var query = from investor in context.InvestorProfiles.AsNoTracking()
            join user in usersQuery on investor.Id equals user.Id
            where user.Status == UserStatus.Active // Only show active investors
            select new { investor, user };

// 2. Apply Filters
// Filter by Governorate (City)
if (request.CityId.HasValue)
{
    query = query.Where(x => x.investor.GovernorateId == request.CityId);
}

// Filter by Investor Type (e.g., Corporate, Individual)
if (request.InvestorType.HasValue) // Using the new filter we added to the record!
{
    query = query.Where(x => x.investor.Type == request.InvestorType);
}

// 3. Calculate Pagination Metadata
var totalCount = await query.CountAsync(cancellationToken);
var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

// 4. Apply Sorting, Pagination, and Projection
var items = await query
    // See note below about why we sort by FullName instead of OrganizationName
    .OrderBy(x => x.user.FullName) 
    .ThenBy(x => x.investor.Id)
    .Skip((request.PageNumber - 1) * request.PageSize)
    .Take(request.PageSize)
    .Select(x => new InvestorListItemDto
    {
        Id = x.investor.Id,
        FullName = x.user.FullName,
        OrganizationName = x.investor.OrganizationName,
        InvestorType = x.investor.Type.ToString(), 
        Governorate = x.investor.Governorate!.Name!,
        ProfileImageUrl=x.user.ProfileImageUrl

    })
    .ToListAsync(cancellationToken);

return new PaginatedList<InvestorListItemDto>
{
    PageNumber = request.PageNumber,
    PageSize = request.PageSize,
    TotalCount = totalCount,
    TotalPages = totalPages,
    Items = items
};
    }
}