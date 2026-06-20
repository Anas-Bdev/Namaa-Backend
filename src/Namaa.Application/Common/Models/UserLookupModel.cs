using Namaa.Domain.Enums;

namespace Namaa.Application.Common.Models;

public class UserLookupModel
{
    public Guid Id {get;set;}
    public string Role {get;set;}=string.Empty;
    public string FirstName {get;set;}=string.Empty;
    public string? LastName {get;set;}=string.Empty;
    public string FullName => string.IsNullOrWhiteSpace(LastName) 
        ? FirstName 
        : $"{FirstName} {LastName}";
    public UserStatus Status {get;set;}
    public string? PhoneNumber {get;set;}
    public string? ProfileImageUrl {get;set;}
    public string Email {get;set;}=string.Empty;
}