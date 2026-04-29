using MediatR;
using Namaa.Application.Features.Weather.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Weather.Queries.GetLocalWeather;
public sealed record GetLocalWeatherQuery(Guid FarmerId,Guid LandId):IRequest<Result<WeatherDto>>;