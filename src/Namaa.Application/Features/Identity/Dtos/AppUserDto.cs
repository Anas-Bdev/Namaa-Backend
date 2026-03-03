using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Identity.Dtos;
public sealed record AppUserDto(Guid UserId,string Email,string Role,string FullName,UserStatus Status);