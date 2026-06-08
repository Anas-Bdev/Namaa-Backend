using MediatR;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Investors.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Investors.Queries.GetInvestors;
public sealed record GetInvestorsQuery(
    int PageNumber, 
    int PageSize,
    int? CityId,
    InvestorType? InvestorType
) : IRequest<Result<PaginatedList<InvestorListItemDto>>>;