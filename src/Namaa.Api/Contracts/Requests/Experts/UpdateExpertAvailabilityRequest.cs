using System.ComponentModel.DataAnnotations;

namespace Namaa.Api.Contracts.Requests.Experts;
public class UpdateExpertAvailabilityRequest
{
    [Required(ErrorMessage = "Day of the week is required.")]
    public DayOfWeek? Day { get; init; }

    [Required(ErrorMessage = "Start time is required.")]
    public TimeSpan? StartTime { get; init; }

    [Required(ErrorMessage = "End time is required.")]
    public TimeSpan? EndTime { get; init; }
}