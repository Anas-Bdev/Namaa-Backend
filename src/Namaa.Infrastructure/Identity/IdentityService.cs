
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Identity.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Common.Constants;


using Namaa.Domain.Enums;

namespace Namaa.Infrastructure.Identity;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class IdentityService(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager,
    IAppDbContext dbContext,
    IConfiguration configuration,
    SignInManager<AppUser> signInManager) : IIdentityService
{
    // Authenticates a user and returns their JWT-ready data
    public async Task<Result<AppUserDto>> AuthenticateAsync(string email, string password)
{
    var user = await userManager.FindByEmailAsync(email);
    
    if (user is null)
        return Error.Unauthorized("Auth.InvalidCredentials", "Invalid email or password.");
        
    var signInResult=await signInManager.CheckPasswordSignInAsync(user,password,lockoutOnFailure:true);

    if (signInResult.IsLockedOut)
        return Error.Forbidden("Auth.LockedOut", "Your account is temporarily locked due to multiple failed login attempts. Please try again later.");

    if (!signInResult.Succeeded)
        return Error.Unauthorized("Auth.InvalidCredentials", "Invalid email or password.");

    if (!await userManager.IsEmailConfirmedAsync(user))
        return Error.Forbidden("Auth.EmailNotConfirmed", "Please confirm your email address before logging in.");
        
    if (user.Status == UserStatus.Disabled || user.Status == UserStatus.Suspended)
        return Error.Forbidden("Auth.AccessDenied", $"Your account is currently '{user.Status}'. Please contact support.");

    
    var roles = await userManager.GetRolesAsync(user);
    var role = roles.Count >0 ? roles[0]:null;
    if (role is null)
    return Error.Validation("Auth.RoleMissing", "User has no role.");
        
    return new AppUserDto(user.Id, user.Email!, role, user.FullName!, user.Status);
}

    // Creates a new user with a specific role (Experts start as Pending)
    public async Task<Result<string>> CreateUserAsync(string password,string email,string role,string firstName,string? lastName,string? phoneNumber)
    {
        var existingUser = await userManager.FindByEmailAsync(email);
    if (existingUser is not null)
    {
        return Error.Conflict("User.DuplicateEmail", "An account with this email address already exists.");
    }

      var exactRole=AppRoles.RegistrationRoles.FirstOrDefault(r => r.Equals(role,StringComparison.OrdinalIgnoreCase));
        if (exactRole is null)
       return Error.Validation("Role.Invalid", "TThe specified role does not exist.");

        if (!await roleManager.RoleExistsAsync(exactRole))
            return Error.NotFound("Role.NotFound", "The specified role does not exist.");

        var initialStatus = string.Equals(role, AppRoles.Expert, StringComparison.OrdinalIgnoreCase) 
            ? UserStatus.Pending 
            : UserStatus.Active;

        var user = new AppUser
        {
            UserName=email,
            Email = email,
            FullName = $"{firstName} {lastName ?? string.Empty}".Trim(),
            PhoneNumber = phoneNumber,
            Status = initialStatus
        };

        var createResult = await userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
            return createResult.Errors.Select(e => Error.Validation(e.Code,e.Description)).ToList();

        var roleResult = await userManager.AddToRoleAsync(user, exactRole);
        if (!roleResult.Succeeded)
        {
            await userManager.DeleteAsync(user); 
            return Error.Failure(roleResult.Errors.First().Code, roleResult.Errors.First().Description);
        }

        return user.Id.ToString();
    }
    
   public async Task<Result<Success>> ConfirmEmailAsync(string userId, string token)
{
    var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
    var user = await userManager.FindByIdAsync(userId);
    if (user is null)
        return Error.NotFound("User.NotFound", "The user account was not found.");

    if (await userManager.IsEmailConfirmedAsync(user))
        return Result.Success;

    var result = await userManager.ConfirmEmailAsync(user, decodedToken);
    if (!result.Succeeded)
        return Error.Validation("Token.Invalid", "The confirmation link is invalid or has expired.");

    return Result.Success;
}

public async Task<Result<Success>> ResetPasswordAsync(string email, string code, string newPassword)
{
    var user = await userManager.FindByEmailAsync(email);
    if (user is null)
        return Error.NotFound("User.NotFound", "The user account was not found.");

    if (string.IsNullOrWhiteSpace(user.ResetCode))
        return Error.Validation("PasswordReset.NotRequested", "No password reset has been requested for this account.");

    if (user.ResetCodeExpiresAt is null || user.ResetCodeExpiresAt < DateTime.UtcNow)
        return Error.Validation("PasswordReset.ExpiredCode", "The password reset code has expired.");

    var verifyResult = userManager.PasswordHasher.VerifyHashedPassword(user, user.ResetCode, code);
    if (verifyResult == PasswordVerificationResult.Failed)
        return Error.Validation("PasswordReset.InvalidCode", "The password reset code is incorrect.");

    var token = await userManager.GeneratePasswordResetTokenAsync(user);
    var resetResult = await userManager.ResetPasswordAsync(user, token, newPassword);
    
    if (!resetResult.Succeeded)
        return Error.Validation("PasswordReset.Failed", resetResult.Errors.First().Description);

    user.ResetCode = null;
    user.ResetCodeExpiresAt = null;
    await userManager.UpdateAsync(user);

    return Result.Success;
}

public async Task<Result<AppUserDto>> GetUserByIdAsync(string userId)
{
    var user = await userManager.FindByIdAsync(userId);
    if (user is null)
        return Error.NotFound("User.NotFound", "The user account was not found.");

    var roles = await userManager.GetRolesAsync(user);
    var role = roles.Count > 0 ? roles[0] : null;
    
    if (role is null)
        return Error.Validation("User.RoleMissing", "The user has no assigned role.");
    
    return new AppUserDto(user.Id, user.Email!, role, user.FullName!, user.Status);
}

public async Task<Result<AppUserDto>> GetUserByEmailAsync(string email)
{
    var user = await userManager.FindByEmailAsync(email);
    if (user is null)
        return Error.NotFound("User.NotFound", "The user account was not found.");

    var roles = await userManager.GetRolesAsync(user);
    var role = roles.Count > 0 ? roles[0] : null;
    
    if (role is null)
        return Error.Validation("User.RoleMissing", "The user has no assigned role.");
    
    return new AppUserDto(user.Id, user.Email!, role, user.FullName!, user.Status);
}

public async Task<Result<string>> GetUserRoleAsync(string userId)
{
    var user = await userManager.FindByIdAsync(userId);
    if (user is null) 
        return Error.NotFound("User.NotFound", "The user account was not found.");

    var roles = await userManager.GetRolesAsync(user);
    var role = roles.Count > 0 ? roles[0] : null;

    if (role is null)
        return Error.Validation("User.RoleMissing", "The user has no assigned role.");

    return role;
}

public async Task<bool> IsInRoleAsync(string userId, string role)
{
    var user = await userManager.FindByIdAsync(userId);
    return user is not null && await userManager.IsInRoleAsync(user, role);
}

public async Task<Result<string>> GenerateEmailConfirmationAsync(string userId)
{
    var user = await userManager.FindByIdAsync(userId);
    if (user is null) 
        return Error.NotFound("User.NotFound", "The user account was not found.");

    return await userManager.GenerateEmailConfirmationTokenAsync(user);
}

public async Task<Result<string>> GeneratePasswordResetCodeAsync(string email)
{
    var user = await userManager.FindByEmailAsync(email);
    if (user is null)
        return Error.NotFound("User.NotFound", "The user account was not found.");

    var resetCode = UtilityService.GenerateRandomNumericCode();
    
    user.ResetCode = userManager.PasswordHasher.HashPassword(user, resetCode);
    user.ResetCodeExpiresAt = DateTime.UtcNow.AddMinutes(15);
    await userManager.UpdateAsync(user);

    return resetCode;
}

public async Task<string?> GetUserNameAsync(string userId) 
{
    var user = await userManager.FindByIdAsync(userId);
    return user?.UserName;
}

public async Task<string?> GetUserIdAsync(string email) 
{
    var user = await userManager.FindByEmailAsync(email);
    return user?.Id.ToString();
}

public async Task<Result<string>> GenerateConfirmationLinkAsync(string userId)
{
    var tokenResult = await GenerateEmailConfirmationAsync(userId);
    if (!tokenResult.IsSuccess)
        return tokenResult.Errors;

    var baseUrl = configuration["App:BaseUrl"];
    var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(tokenResult.Value));
    
    return $"{baseUrl}/api/identity/confirm-email?userId={userId}&token={encodedToken}";
}

public async Task<Result<Success>> RevokeRefreshTokenAsync(string userId, string refreshToken)
{
    if (!Guid.TryParse(userId, out var parsedUserId))
        return Error.Validation("User.InvalidId", "The provided user ID format is invalid.");

    var tokenEntity = await dbContext.RefreshTokens
        .FirstOrDefaultAsync(t => t.Token == refreshToken && t.UserId == parsedUserId);
        
    if (tokenEntity is null)
        return Error.NotFound("Token.NotFound", "The refresh token could not be found for this user.");

    dbContext.RefreshTokens.Remove(tokenEntity);
    await dbContext.SaveChangesAsync();
    
    return Result.Success;
}

public async Task<bool> IsEmailConfirmedAsync(string email)
{
    var user = await userManager.FindByEmailAsync(email);
    return user?.EmailConfirmed ?? false;
}

    public async Task<Result<Success>> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        var user=await userManager.FindByIdAsync(userId);
        if(user is null)
       return Error.NotFound("User.NotFound", "The user account was not found.");

       var identityResult=await userManager.ChangePasswordAsync(user,currentPassword,newPassword);
       if(identityResult.Succeeded)
       return Result.Success;
       var errors=identityResult.Errors.Select(e => Error.Validation(e.Code,e.Description)).ToList();
       return errors;


     }
}