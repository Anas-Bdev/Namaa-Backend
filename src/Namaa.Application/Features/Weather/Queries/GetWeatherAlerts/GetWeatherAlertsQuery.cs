using MediatR;
using Namaa.Application.Features.Weather.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Weather.Queries.GetWeatherAlerts;
public sealed record GetWeatherAlertsQuery(Guid FarmerId):IRequest<Result<List<WeatherAlertDto>>>;