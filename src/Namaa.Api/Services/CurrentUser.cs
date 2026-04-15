using System.Security.Claims;
using Namaa.Application.Common.Interfaces;

namespace Namaa.Api.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : IUser
{

    public string? Id => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}