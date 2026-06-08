using MediatR;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Traders.Commands.UpdateProfile;
public sealed record UpdateTraderProfileCommand(Guid UserId,string BusinessName,TraderType TraderType,int GovernorateId,string? AddressDetail) : IRequest<Result<Updated>>;