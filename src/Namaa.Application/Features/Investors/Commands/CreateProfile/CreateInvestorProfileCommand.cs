using MediatR;
using Namaa.Application.Features.Investors.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Investors.Commands.CreateProfile;
public sealed record CreateInvestorProfileCommand(Guid UserId,InvestorType Type,string? OrganizationName,int GovernorateId,string? AddressDetail):IRequest<Result<InvestorProfileDto>>;