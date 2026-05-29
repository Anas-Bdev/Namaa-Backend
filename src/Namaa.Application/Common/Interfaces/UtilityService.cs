using System.Security.Cryptography;

namespace Namaa.Application.Common.Interfaces;
public static class UtilityService
{
    public static string GenerateRandomNumericCode()
    {
       var randomNumber = RandomNumberGenerator.GetInt32(100_000, 1_000_000);
        
        return randomNumber.ToString();
    }
}