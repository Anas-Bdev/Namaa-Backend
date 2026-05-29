using MediatR;
using Namaa.Application.Features.Traders.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Traders.Queries.GetTraderProfileById;
public sealed record GetTraderProfileByIdQuery(Guid TraderId):IRequest<Result<TraderListItemDto>>;