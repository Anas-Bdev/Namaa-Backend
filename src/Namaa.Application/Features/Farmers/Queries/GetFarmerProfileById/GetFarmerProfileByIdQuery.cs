using MediatR;
using Namaa.Application.Features.Farmers.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Farmers.Queries.GetFarmerProfileById;
public sealed record GetFarmerProfileByIdQuery(Guid FarmerId):IRequest<Result<FarmerListItemDto>>;