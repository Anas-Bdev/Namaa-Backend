using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Lands;
public static class LandErrors
{
   public static readonly Error IdRequired = 
        Error.Validation("Land_Id_Required", "Land ID is required.");

    public static readonly Error FarmerIdRequired = 
        Error.Validation("Land_FarmerId_Required", "Farmer ID is required.");

    public static readonly Error NameRequired = 
        Error.Validation("Land_Name_Required", "Land name is required.");

    public static readonly Error AreaInvalid = 
        Error.Validation("Land_Area_Invalid", "Area must be greater than zero.");

    public static readonly Error CityRequired = 
        Error.Validation("Land_CityId_Required", "City selection is required.");

    public static readonly Error SoilRequired = 
        Error.Validation("Land_SoilId_Required", "Soil type selection is required.");
        public static readonly Error AddressRequired = 
        Error.Validation("Land_AddressDetail_Required", "A detailed address description is required.");

    public static readonly Error InvalidLatitude = 
        Error.Validation("Land_Latitude_Invalid", "Latitude must be within the valid geographical boundaries of Palestine (31.0 to 33.0).");

    public static readonly Error InvalidLongitude = 
        Error.Validation("Land_Longitude_Invalid", "Longitude must be within the valid geographical boundaries of Palestine (34.0 to 36.0).");
}
