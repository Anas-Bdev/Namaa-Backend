using Namaa.Application.Authentication.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Abstractions.Authentication;
public interface IAuthService{
 Task<Result<AuthResponse>> LoginAsync(LoginRequest request,CancellationToken ct=default);
}
