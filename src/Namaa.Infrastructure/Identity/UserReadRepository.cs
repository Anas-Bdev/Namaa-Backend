using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Infrastructure.Persistence.Context;

namespace Namaa.Infrastructure.Identity;

public class UserReadRepository(AppDbContext context) : IUserReadRepository
{
    public async Task<UserLookupModel?> GetByIdAsync(Guid Id, CancellationToken ct)
    {
        return await context.Users.Where(u => u.Id==Id).Select(u => new UserLookupModel
        {
        Id=u.Id,
        ProfileImageUrl=u.ProfileImageUrl,
        FullName=u.FullName,
        PhoneNumber=u.PhoneNumber,
        Status=u.Status,
        }
        ).FirstOrDefaultAsync(ct);
    }

    public async Task<string?> GetFullNameByIdAsync(Guid id, CancellationToken ct)
    {
        return await context.Users.Where(u => u.Id==id)
                     .Select(u => u.FullName)
                     .FirstOrDefaultAsync(ct) ?? "Unknown User";
    }

    public IQueryable<UserLookupModel> Query()
    {
        return context.Users.Select(u => new UserLookupModel
        {
            Id=u.Id,
            FullName=u.FullName,
            Status=u.Status,
            PhoneNumber=u.PhoneNumber,
            ProfileImageUrl=u.ProfileImageUrl,
            Email=u.Email
        }
    );
    }
}