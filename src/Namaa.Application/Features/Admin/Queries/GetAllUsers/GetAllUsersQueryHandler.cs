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
    var totalCount = await query.CountAsync(cancellationToken);
    var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

    var items = await query
        // FIX: Order by actual database columns, not the computed C# property
        .OrderBy(u => u.FirstName)
        .ThenBy(u => u.LastName)
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize)
        .Select(user => new UserLookupModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Status = user.Status,
            PhoneNumber = user.PhoneNumber,
            ProfileImageUrl = user.ProfileImageUrl,
            Email = user.Email
        })
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