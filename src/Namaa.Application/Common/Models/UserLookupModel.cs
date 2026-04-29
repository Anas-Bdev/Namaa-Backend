using Namaa.Domain.Enums;

namespace Namaa.Application.Common.Models;
public record UserLookupModel(
    Guid Id, 
    string FullName, 
    UserStatus Status,
    string? PhoneNumber,
    string? ProfileImageUrl
);