using MediatR;
using Namaa.Application.Features.Traders.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Traders.Commands.UpdateProfile;

public sealed record UpdateTraderProfileCommand(
    Guid UserId,
    string BusinessName,
    string BusinessType,
    string? PreferredCategories,
    int CityId,
    string? AddressDetail
) : IRequest<Result<TraderProfileDto>>;