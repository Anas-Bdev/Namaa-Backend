using Namaa.Application.Common.Models;

namespace Namaa.Application.Common.Interfaces;
public interface IUserReadRepository
{
    IQueryable<UserLookupModel> Query();
    Task<string> GetFullNameByIdAsync(Guid id, CancellationToken ct);
    Task<UserLookupModel?> GetByIdAsync(Guid Id,CancellationToken ct);
}