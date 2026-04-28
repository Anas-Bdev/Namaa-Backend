namespace Namaa.Domain.Common.Constants;
public static class AppRoles{
    public const string Admin="Admin";
    public const string Farmer="Farmer";
    public const string Expert="Expert";
    public const string Trader="Trader";
    public const string Investor="Investor";
    public static readonly HashSet<string> RegistrationRoles = new(StringComparer.OrdinalIgnoreCase)
    {
        Farmer,Expert,Trader,Investor
    };

    public static readonly HashSet<string> NormalRegistrationRoles=new(StringComparer.OrdinalIgnoreCase)
    {
        Farmer,
        Trader,
        Investor
    };
}