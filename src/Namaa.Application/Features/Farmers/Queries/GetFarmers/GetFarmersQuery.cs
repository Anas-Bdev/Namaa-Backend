using MediatR;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Farmers.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Farmers.Queries.GetFarmers;
public sealed record GetFarmersQuery(
    int PageNumber, 
    int PageSize,
    int? CityId
) : IRequest<Result<PaginatedList<FarmerListItemDto>>>;