using MediatR;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Investors.Commands.UpdateProfile;
public sealed record UpdateInvestorProfileCommand(Guid UserId,InvestorType Type,string? OrganizationName,int GovernorateId,string? AddressDetail):IRequest<Result<Updated>>;