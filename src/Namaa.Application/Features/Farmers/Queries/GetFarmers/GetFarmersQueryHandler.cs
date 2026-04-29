using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Farmers.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Farmers.Queries.GetFarmers;

public class GetFarmersQueryHandler(IAppDbContext context, IUserReadRepository userReadRepository) : IRequestHandler<GetFarmersQuery, Result<PaginatedList<FarmerListItemDto>>>{
    public async Task<Result<PaginatedList<FarmerListItemDto>>> Handle(GetFarmersQuery request, CancellationToken cancellationToken)
    {
        var usersQuery = userReadRepository.Query();

    // 2. Build the base join query
    var query = from farmer in context.FarmerProfiles.AsNoTracking()
                join user in usersQuery on farmer.Id equals user.Id
                where user.Status == UserStatus.Active // Only show active farmers
                select new { farmer, user };

    // 3. Apply Filters
    // Filtering by Governorate (City)
    if (request.CityId.HasValue)
    {
        query = query.Where(x => x.farmer.GovernorateId == request.CityId);
    }

    // 4. Calculate Pagination Metadata
    var totalCount = await query.CountAsync(cancellationToken);
    var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

    // 5. Apply Sorting, Pagination, and Projection
    var items = await query
        .OrderBy(x => x.user.FullName) // Professional alphabetical sorting
        .ThenBy(x => x.farmer.Id)
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize)
        .Select(x => new FarmerListItemDto
        {
            Id = x.farmer.Id,
            FullName = x.user.FullName,
            Description = x.farmer.Description,
            Governorate = x.farmer.Governorate!.Name!,
            PhoneNumber=x.user.PhoneNumber,
            ProfileImageUrl=x.user.ProfileImageUrl
        })
        .ToListAsync(cancellationToken);

    // 6. Return the result
    return new PaginatedList<FarmerListItemDto>
    {
        PageNumber = request.PageNumber,
        PageSize = request.PageSize,
        TotalCount = totalCount,
        TotalPages = totalPages,
        Items = items
    };
    }
}