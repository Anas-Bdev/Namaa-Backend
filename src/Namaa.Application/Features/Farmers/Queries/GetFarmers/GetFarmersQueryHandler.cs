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

    var query = from farmer in context.FarmerProfiles.AsNoTracking()
                 .Include(x => x.Governorate)
                join user in usersQuery on farmer.Id equals user.Id
                where user.Status == UserStatus.Active
                select new 
                { 
                    farmer, 
                    user.FirstName, 
                    user.LastName, 
                    user.PhoneNumber, 
                    user.ProfileImageUrl 
                };

    if (request.CityId.HasValue)
    {
        query = query.Where(x => x.farmer.GovernorateId == request.CityId);
    }

    var totalCount = await query.CountAsync(cancellationToken);
    var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

    // Sort by name components to keep it database-friendly
    var rawItems = await query
        .OrderBy(x => x.FirstName)
        .ThenBy(x => x.LastName)
        .ThenBy(x => x.farmer.Id)
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize)
        .ToListAsync(cancellationToken);

    var items = rawItems.Select(x => new FarmerListItemDto
    {
        Id = x.farmer.Id,
        FullName = string.IsNullOrWhiteSpace(x.LastName) 
            ? x.FirstName 
            : $"{x.FirstName} {x.LastName}".Trim(),
        Description = x.farmer.Description,
        Governorate = x.farmer.Governorate!.Name!,
        PhoneNumber = x.PhoneNumber,
        ProfileImageUrl = x.ProfileImageUrl,
        AiSummary = x.farmer.AiReviewSummary
    }).ToList();

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