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
    public static readonly Error InvalidSoilType = Error.NotFound("SoilType.InvalidId", "The selected soil type does not exist.");
    public static readonly Error RegionCropsNotFound = Error.NotFound("Recommendations.NotFound", "The Governorate ID is invalid.");
    public static readonly Error AddressNotFound = Error.Validation("Land_Address_NotFound", "Could not find GPS coordinates for this address. Please try adding a nearby landmark or the city name.");
     public static readonly Error SeedingCycleNotFound=Error.NotFound("ApplicationErrors.SeedingCycle.NotFound","SeedingCycle does not exist.");
    public static readonly Error AlreadyExists = Error.Conflict(
        "Profile.AlreadyExists", "A profile of this type already exists for the current user.");

    public static readonly Error FarmerNotFound = Error.NotFound(
        "Farmer.NotFound", 
        "The farmer profile you are looking for was not found.");

    public static readonly Error TraderNotFound = Error.NotFound(
        "Trader.NotFound", 
        "The Trader profile you are looking for was not found.");

    
    public static readonly Error InvestorNotFound = Error.NotFound(
        "Investor.NotFound", 
        "The Investor profile you are looking for was not found.");

    public static readonly Error InvestmentProjectNotFound=Error.NotFound("ApplicationErrors.InvestmentProject.NotFound","InvestmentProject does not exist.");
    public static readonly Error InvestorContributionNotFound=Error.NotFound("ApplicationErrors.InvestorContribution.NotFound","InvestorContribution does not exist.");
    public static  readonly Error CropNotFound = Error.NotFound("Crop.NotFound", "The selected crop does not exist in the master database.");
    public static readonly Error ListingNotFound=Error.NotFound("Listing.NotFound", "The product listing could not be found.");
    public static readonly Error ListingNotActive=Error.Conflict("Listing.NotActive", "This listing is not currently active and cannot be purchased.");
    public static readonly Error OrderNotFound=Error.NotFound("ProductOrder.NotFound", "The order could not be found.");
    public static readonly Error InvalidRatingState=Error.Validation("Rating.InvalidState", "You can only leave a rating after the order is delivered.");
    public static readonly Error OrderAlreadyReviewed=Error.Conflict("Rating.Duplicate", "You have already submitted a rating for this order.");
    public static readonly Error UserNotFound=Error.Failure("User.NotFound","The requested user could not be found in the system");
    public static readonly Error InvalidStatusForUpdate=Error.Validation("Expert.InvalidStatus", "The current expert status does not allow CV updates.");
    public static readonly Error ConsultationNotFound = Error.NotFound("Consultation.NotFound", "The requested consultation could not be found.");

     
}
    

