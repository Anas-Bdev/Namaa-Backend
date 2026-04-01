namespace Namaa.Application.Common.Interfaces;
public interface IGeocodingService
{
    Task<(double Latitude, double Longitude)?> GetCoordinatesAsync(string address, CancellationToken ct = default);
}