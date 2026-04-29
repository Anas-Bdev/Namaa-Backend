using MediatR;
using Namaa.Application.Features.Traders.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Traders.Queries.GetTraderProfile;

public sealed record GetTraderProfileQuery(Guid UserId):IRequest<Result<TraderProfileDto>>;