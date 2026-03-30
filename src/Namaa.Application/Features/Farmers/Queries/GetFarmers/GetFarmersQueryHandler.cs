using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Farmers.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Farmers.Queries.GetFarmers;

public class GetFarmersQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetFarmersQuery, Result<PaginatedList<FarmerSummaryDto>>>
{
    public async Task<Result<PaginatedList<FarmerSummaryDto>>> Handle(
     GetFarmersQuery request,
     CancellationToken cancellationToken)
    {
        
        var farmersQuery = context.FarmerProfiles.AsNoTracking();

        if (request.CityId.HasValue)
            farmersQuery = farmersQuery.Where(x => x.CityId == request.CityId);

        var totalCount = await farmersQuery.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var farmers = await farmersQuery
            .OrderByDescending(x => x.CreatedAtUtc)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

       
        var users = await userReadRepository.Query()
            .ToListAsync(cancellationToken);

        var items = farmers.Select(farmer => new FarmerSummaryDto
        {
            Id = farmer.Id,
            FullName = users.FirstOrDefault(u => u.Id == farmer.Id)?.FullName ?? string.Empty,
            CityId = farmer.CityId ?? 0,
            ExperienceLevel = farmer.ExperienceLevel
        }).ToList();

        return new PaginatedList<FarmerSummaryDto>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Items = items
        };
    }
}
