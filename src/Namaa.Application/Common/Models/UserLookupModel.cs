using Namaa.Domain.Enums;

namespace Namaa.Application.Common.Models;

public class UserLookupModel
{
    public Guid Id {get;set;}
    public string FullName {get;set;}=string.Empty;
    public UserStatus Status {get;set;}
    public string? PhoneNumber {get;set;}
    public string? ProfileImageUrl {get;set;}
    public string? Email {get;set;}
}