using System.Text.RegularExpressions;

namespace Namaa.Api.Extensions;
public static class EnumExtension
{
    public static string ToSpacedName(this Enum value)
    {
        return Regex.Replace(value.ToString(),"([a-z])([A-Z])", "$1 $2");
    }
}