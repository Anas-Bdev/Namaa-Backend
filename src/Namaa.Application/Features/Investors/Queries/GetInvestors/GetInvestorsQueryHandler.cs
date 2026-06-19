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

    var query = from investor in context.InvestorProfiles.AsNoTracking()
                 .Include(x => x.Governorate)
                join user in usersQuery on investor.Id equals user.Id
                where user.Status == UserStatus.Active
                select new 
                { 
                    investor, 
                    user.FirstName, 
                    user.LastName, 
                    user.ProfileImageUrl 
                };

    if (request.CityId.HasValue)
        query = query.Where(x => x.investor.GovernorateId == request.CityId);

    if (request.InvestorType.HasValue)
        query = query.Where(x => x.investor.Type == request.InvestorType);

    var totalCount = await query.CountAsync(cancellationToken);
    var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

    // Sort by database columns instead of computed FullName
    var rawItems = await query
        .OrderBy(x => x.FirstName)
        .ThenBy(x => x.LastName)
        .ThenBy(x => x.investor.Id)
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize)
        .ToListAsync(cancellationToken);

    var items = rawItems.Select(x => new InvestorListItemDto
    {
        Id = x.investor.Id,
        FullName = string.IsNullOrWhiteSpace(x.LastName) 
            ? x.FirstName 
            : $"{x.FirstName} {x.LastName}".Trim(),
        OrganizationName = x.investor.OrganizationName,
        InvestorType = x.investor.Type.ToString(),
        Governorate = x.investor.Governorate!.Name!,
        ProfileImageUrl = x.ProfileImageUrl
    }).ToList();

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