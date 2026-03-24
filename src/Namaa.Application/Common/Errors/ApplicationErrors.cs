using Namaa.Domain.Common.Results;

namespace Namaa.Application.Common.Errors;
public static class ApplicationErrors
{
    public static readonly Error ExpiredAccessTokenInvalid=Error.Conflict("Auth.ExpiredAccessToken.Invalid","Expired access token is invalid.");
    public static readonly Error UserIdClaimInvalid=Error.Conflict("Auth.UserIdClaim.Invalid","Invalid userId claim.");
    public static readonly Error RefreshTokenExpired = Error.Conflict( "Auth.RefreshToken.Expired", "Refresh token is invalid or has expired.");
    public static readonly Error LandNotFound=Error.NotFound("ApplicationErrors.Land.NotFound","Land does not exist.");

}