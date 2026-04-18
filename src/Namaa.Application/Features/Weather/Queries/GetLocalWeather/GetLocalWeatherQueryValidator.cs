using FluentValidation;

namespace Namaa.Application.Features.Weather.Queries.GetLocalWeather;
public class GetLocalWeatherQueryValidator : AbstractValidator<GetLocalWeatherQuery>
{
    public GetLocalWeatherQueryValidator()
    {
        RuleFor(x => x.LandId)
            .NotEmpty()
            .WithMessage("A valid Land ID must be provided to fetch weather data.");
    }
}