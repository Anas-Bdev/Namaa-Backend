using Namaa.Domain.Common.Results;

namespace Namaa.Application.Common.Errors;
public static class ApplicationErrors
{
    public static readonly Error ExpiredAccessTokenInvalid=Error.Conflict("Auth.ExpiredAccessToken.Invalid","Expired access token is invalid.");
    public static readonly Error UserIdClaimInvalid=Error.Conflict("Auth.UserIdClaim.Invalid","Invalid userId claim.");
    public static readonly Error RefreshTokenExpired = Error.Conflict( "Auth.RefreshToken.Expired", "Refresh token is invalid or has expired.");
    public static readonly Error LandNotFound=Error.NotFound("ApplicationErrors.Land.NotFound","Land does not exist.");
    public static readonly Error ExpertNotFound=Error.NotFound("Expert.NotFound","Profile not found. Please upload your CV first.");
    public static readonly Error ExpertAlreadyExists = Error.Conflict("Expert.AlreadyExists", "A profile already exists for this user.");
    public static readonly Error Forbidden = Error.Forbidden("General.Forbidden", "You do not have permission to access this resource.");

    

}