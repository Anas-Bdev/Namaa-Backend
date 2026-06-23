using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Admin.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(IUserReadRepository userReadRepository) : IRequestHandler<GetAllUsersQuery, Result<PaginatedList<UserLookupModel>>>
{
    public async Task<Result<PaginatedList<UserLookupModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
{

    var query = userReadRepository.Query();

    if (request.Status.HasValue)
    {
        query = query.Where(u => u.Status == request.Status.Value);
    }

    if (!string.IsNullOrWhiteSpace(request.Search))
    {
        var search = request.Search.ToLower();
        query = query.Where(u => 
            u.FirstName!.ToLower().Contains(search) || 
            u.LastName!.ToLower().Contains(search) || 
            u.Email.ToLower().Contains(search));
    }
    var totalCount = await query.CountAsync(cancellationToken);
    var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

    var items = await query
            .OrderByDescending(u => u.CreationTime) 
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

    var paginatedList = new PaginatedList<UserLookupModel>
    {
        PageNumber = request.PageNumber,
        PageSize = request.PageSize,
        TotalCount = totalCount,
        TotalPages = totalPages,
        Items = items
    };

    return paginatedList;
}
}