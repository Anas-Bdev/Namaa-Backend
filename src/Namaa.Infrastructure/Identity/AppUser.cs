using Microsoft.AspNetCore.Identity;
using Namaa.Domain.Enums;
using Namaa.Domain.Identity;

namespace Namaa.Infrastructure.Identity;
public class AppUser: IdentityUser<Guid>
{
    public UserStatus Status {get;set;}=UserStatus.Active;
    public string? ProfileImageUrl {get;set;}
    public string? FullName {get;set;}
    public string? ResetCode {get;set;}
    public DateTime? ResetCodeExpiresAt {get;set;}
}