namespace Namaa.Application.Authentication.Dtos;
public sealed class TokenResponse
{
    public string? AccessToken {get;set;}
    public DateTime ExpiresAt {get;set;}
}