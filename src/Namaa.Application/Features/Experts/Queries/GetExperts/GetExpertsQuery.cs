using MediatR;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Experts.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Experts.Queries.GetExperts;
public sealed record GetExpertsQuery(
    int PageNumber, 
    int PageSize,
    int? CityId, 
    ExpertSpecialization ? Specialization 
) : IRequest<Result<PaginatedList<ExpertSummaryDto>>>;