using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Consultation;

public static class ExpertResponseErrors
{
    public static readonly Error IdRequired = Error.Validation(
        "ExpertResponse.IdRequired", "A valid ID must be provided.");
    public static readonly Error MessageRequired = Error.Validation(
        "ExpertResponse.MessageRequired", "Response message is required.");
}