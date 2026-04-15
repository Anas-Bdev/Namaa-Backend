using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Identity.Dtos;
using Namaa.Infrastructure.Persistence.Context;

namespace Namaa.Infrastructure.Identity;

public class UserReadRepository(AppDbContext context) : IUserReadRepository
{
    public IQueryable<UserLookupModel> Query()
    {
        return context.Users.Select(u => new UserLookupModel(
        u.Id,
        u.FullName!,
        u.Status
    ));
    }
}