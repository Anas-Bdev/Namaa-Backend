using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Namaa.Application.Common.Interfaces;
namespace Namaa.Infrastructure.Services;

public class NominatimGeocodingService(HttpClient httpClient) : IGeocodingService
{
    public async Task<(double Latitude, double Longitude)?> GetCoordinatesAsync(string address, CancellationToken ct = default)
    {
        httpClient.DefaultRequestHeaders.Add("User-Agent", "NamaaProject/1.0");
        var url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(address + ", Palestine")}&format=json&limit=1";
        var results = await httpClient.GetFromJsonAsync<List<NominatimResponse>>(url, ct);
        if (results is not null && results.Count > 0)
        {
            if (double.TryParse(results[0].Lat, out double lat) && 
                double.TryParse(results[0].Lon, out double lon))
            {
                return (lat, lon);
            }
        }
        return null;

    }





    private class NominatimResponse
    {
        [JsonPropertyName("lat")]
        public string Lat { get; set; } = string.Empty;

        [JsonPropertyName("lon")]
        public string Lon { get; set; } = string.Empty;
    }
}




