using Microsoft.AspNetCore.Identity;
using Namaa.Domain.Enums;

namespace Namaa.Infrastructure.Identity;
public class AppUser: IdentityUser<Guid>
{
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    public UserStatus Status {get;set;}=UserStatus.Active;
    public string? ProfileImageUrl {get;set;}
    public string? FirstName {get;set;}
    public string? LastName {get;set;}
    public string? ResetCode {get;set;}
    public string? StatusReason {get;set;}
    public DateTime? ResetCodeExpiresAt {get;set;}
    public string FullName => $"{FirstName} {LastName}".Trim();
    public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; } = new List<IdentityUserRole<Guid>>();
}