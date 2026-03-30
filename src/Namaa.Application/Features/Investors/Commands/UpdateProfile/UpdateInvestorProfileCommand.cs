using MediatR;
using Namaa.Application.Features.Investors.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investors.Commands.UpdateProfile;

public sealed record UpdateInvestorProfileCommand(
    Guid UserId,
    string OrganizationName,
    string? CompanyName,
    int CityId,
    string? AddressDetail
) : IRequest<Result<InvestorProfileDto>>;