using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Admin.Dtos;
public class UserStatusCountDto
{
    public UserStatus Status { get; set; }
    public int Count { get; set; }
}