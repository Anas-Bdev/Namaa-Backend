using MediatR;
using Namaa.Application.Features.Traders.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Traders.Commands.CreateProfile;
public sealed record CreateTraderProfileCommand(Guid UserId,string BusinessName,TraderType TraderType,int GovernorateId,string? AddressDetail) : IRequest<Result<TraderProfileDto>>;