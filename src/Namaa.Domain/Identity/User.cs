using System;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Domain.Identity;
public sealed class User
{
    private User() {}
    private User(Guid roleId,String userName,String email,String passwordHash)
    {
        this.RoleId=roleId;
        UserName=userName;
        this.Email=email;
        this.PasswordHash=passwordHash;
        Status=UserStatus.Pending;
        CreatedAt=DateTime.UtcNow;
    }
    private static bool IsMissing(string s) => string.IsNullOrWhiteSpace(s);
    private  void Touch() => UpdatedAt=DateTime.UtcNow;
    public Guid RoleId {get;private set;}
    public string UserName {get;private set;}=default!;
    public string Email {get;private set;}=default!;
    public string PasswordHash {get;private set;}=default!;
    public string? PhoneNumber {get;private set;}
    public string? ProfileImageUrl {get;private set;}
    public UserStatus Status {get;private set;}
    public DateTime CreatedAt {get;private set;}
    public DateTime? UpdatedAt {get; private set;}
    public bool IsEmailVerified {get;private set;}
    public string? EmailVerificationCode {get;private set;}
    public DateTime? EmailVerificationCodeExpiresAt {get;private set;}
 public static Result<User> Create(Guid roleId,string userName,string email,string passwordHash)
    {
    if(IsMissing(userName))
    return UserErrors.UsernameRequired;
    if(IsMissing(email))
    return UserErrors.EmailRequired;
    if(!email.Contains("@") || email.StartsWith("@") || email.EndsWith("@"))
    return UserErrors.InvalidEmail;
    if(IsMissing(passwordHash))
    return UserErrors.PasswordRequired;
    if(roleId==Guid.Empty)
     return UserErrors.RoleIdRequired;
     return new User(roleId,userName.Trim(),email.Trim().ToLowerInvariant(),passwordHash);

    }
    public Result<Success> ChangePhoneNumber(string? phoneNumber)
    {
        PhoneNumber=string.IsNullOrWhiteSpace(phoneNumber) ? null: phoneNumber.Trim();
     Touch();
     return Result.Success;
    }
public Result<Success> ChangeProfileImage(string? imageUrl)
{
    ProfileImageUrl = string.IsNullOrWhiteSpace(imageUrl)
        ? null
        : imageUrl.Trim();

    Touch();
    return Result.Success;
}
public Result<string> GenerateEmailVerificationCode(TimeSpan ttl)
    {
        if(IsEmailVerified)
        return Error.Conflict("User.EmailAlreadyVerified","Email is already verified");
        var code = Random.Shared.Next(100000, 1000000).ToString();
        EmailVerificationCode=code;
        EmailVerificationCodeExpiresAt=DateTime.UtcNow.Add(ttl);
        Touch();
        return code;
    }
   public Result<Success> VerifyEmail(string code)
    {
        if(IsEmailVerified)
        return Result.Success;
        if(EmailVerificationCode is null || EmailVerificationCodeExpiresAt is null )
        return Error.Validation("User.NoVerificationRequest","No email verification request exists.");
        if(DateTime.UtcNow> EmailVerificationCodeExpiresAt.Value)
          return Error.Validation(
            "User.VerificationExpired",
            "Verification code has expired."
        );
        
    if (!string.Equals(code, EmailVerificationCode, StringComparison.Ordinal))
        return Error.Validation(
            "User.InvalidVerificationCode",
            "Invalid verification code."
        );
        IsEmailVerified=true;
        EmailVerificationCode=null;
        EmailVerificationCodeExpiresAt=null;
        Status=UserStatus.Active;
        Touch();
        return Result.Success;

    }
}
