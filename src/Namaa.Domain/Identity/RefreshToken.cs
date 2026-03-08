using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Identity;
public sealed class RefreshToken : AuditableEntity
{
    public string? Token {get;}
    public Guid UserId {get;}
    public DateTimeOffset ExpiresOnUtc {get;}
    private RefreshToken() {}
    
    private RefreshToken(Guid id,string? token,Guid userId,DateTimeOffset expiresOnUtc):base(id)
    {
     UserId=userId;
     Token=token;
     ExpiresOnUtc=expiresOnUtc;
    }
    public static Result<RefreshToken> Create(Guid id,string? token,Guid userId,DateTimeOffset expiresOnUtc)
    {
        if(id==Guid.Empty)

        return RefreshTokenErrors.IdRequired;

        if(string.IsNullOrWhiteSpace(token))

        return RefreshTokenErrors.TokenRequired;
        
        if(userId==Guid.Empty)

        return RefreshTokenErrors.UserIdRequired;

        if(expiresOnUtc<=DateTimeOffset.UtcNow)
        
        return RefreshTokenErrors.ExpiryInValid;
        
        return new RefreshToken(id,token,userId,expiresOnUtc);
     
    }
}