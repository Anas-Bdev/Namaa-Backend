using System.Diagnostics.Contracts;

namespace Namaa.Application.Authentication.Dtos;
public sealed record UserIdentity(Guid UserId,string Email,string UserName)
{
    
}