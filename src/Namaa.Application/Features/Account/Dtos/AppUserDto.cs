using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Account.Dtos;
public sealed record AppUserDto(Guid UserId,string Email,string Role,string FirstName,string? LastName,string? PhoneNumber,UserStatus Status,bool IsProfileComplete,string? ProfileImageUrl,string? StatusReason);