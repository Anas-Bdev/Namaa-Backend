using Microsoft.AspNetCore.Identity;
using Namaa.Domain.Enums;

namespace Namaa.Infrastructure.Identity;
public class AppUser: IdentityUser<Guid>
{
    public UserStatus Status {get;set;}=UserStatus.Pending;
    public string? ProfileImageUrl {get;set;}
    public DateTime CreatedAtUtc {get;set;}=DateTime.UtcNow;
    public DateTime? UpdatedAtUtc {get;set;}
}