using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Infrastructure.Persistence.Context;

namespace Namaa.Infrastructure.Identity;

public class UserReadRepository(AppDbContext context) : IUserReadRepository
{
    public async Task<UserLookupModel?> GetByIdAsync(Guid Id, CancellationToken ct)
{
    return await context.Users
        .Where(u => u.Id == Id)
        .Select(u => new UserLookupModel
        {
            Id = u.Id,
            FirstName = u.FirstName!, // Map component
            LastName = u.LastName,   // Map component
            ProfileImageUrl = u.ProfileImageUrl,
            PhoneNumber = u.PhoneNumber,
            Status = u.Status,
            Email=u.Email!
        })
        .FirstOrDefaultAsync(ct);
}

    public async Task<string?> GetFullNameByIdAsync(Guid id, CancellationToken ct)
   {
    return await context.Users
        .Where(u => u.Id == id)
        .Select(u => string.Concat(u.FirstName, " ", u.LastName).Trim())
        .FirstOrDefaultAsync(ct) ?? "Unknown User";
   }
  public IQueryable<UserLookupModel> Query()
{
    return context.Users.Select(u => new UserLookupModel
    {
        Id = u.Id,
        FirstName = u.FirstName!, // Map component
        LastName = u.LastName,   // Map component
        Status = u.Status,
        PhoneNumber = u.PhoneNumber,
        ProfileImageUrl = u.ProfileImageUrl,
        Email = u.Email!
    });
}
}