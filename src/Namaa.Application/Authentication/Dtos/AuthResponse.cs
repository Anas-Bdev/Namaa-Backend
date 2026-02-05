namespace Namaa.Application.Authentication.Dtos;
public sealed class AuthResponse
{
    public string? Email {get;set;}
    public Guid UserId {get;set;}
    public string? UserName {get;set;}
    public string? AccessToken {get;set;}
    public DateTime ExpiresAt {get;set;}
    public IReadOnlyCollection<string> Roles {get;set;}=Array.Empty<string>();
}