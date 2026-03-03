using Namaa.Application.Features.Identity.Dtos;
using Namaa.Domain.Common.Constants;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Common.Interfaces;
public interface IIdentityService{

// Authentication & Retrieval
Task<Result<Success>> RevokeRefreshTokenAsync(string userId,string refreshToken);
 Task<Result<AppUserDto>> AuthenticateAsync(string email,string password);
 Task<Result<AppUserDto>> GetUserByIdAsync(string userId);
 Task<Result<AppUserDto>> GetUserByEmailAsync(string email);
 Task<bool> IsEmailConfirmedAsync(string email);
 Task<string?> GetUserIdAsync(string email);
 Task<string?> GetUserNameAsync(string userId);

 // User Creation
Task<Result<string>> CreateUserAsync(string password,string email,string role,string firstName,string? lastName,string? phoneNumber);

// Authorization (Permissions)
 Task<bool> IsInRoleAsync(string userId,string role);
 Task<Result<string>> GetUserRoleAsync(string userId);

 // Security & Recovery
 Task<Result<string>> GenerateConfirmationLinkAsync(string userId);
 Task<Result<string>> GenerateEmailConfirmationAsync(string userId);
 Task<Result<Success>> ConfirmEmailAsync(string userId,string token);
 Task<Result<string>> GeneratePasswordResetCodeAsync(string email);
 Task<Result<Success>> ResetPasswordAsync(string email,string code,string newPassword);

}
