using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Application.Features.Consultations.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Queries.GetMyResponses;

public class GetMyResponsesQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetMyResponsesQuery, Result<PaginatedList<ConsultationSummaryDto>>>
{
    public async Task<Result<PaginatedList<ConsultationSummaryDto>>> Handle(
        GetMyResponsesQuery request,
        CancellationToken cancellationToken)
    {
        var respondedIds = await context.ExpertResponses
            .AsNoTracking()
            .Where(r => r.ExpertId == request.ExpertId)
            .Select(r => r.RequestId)
            .Distinct()
            .ToListAsync(cancellationToken);

        var query = context.ConsultationRequests
            .AsNoTracking()
            .Include(c => c.Responses)
            .Where(c => respondedIds.Contains(c.Id));

        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var consultations = await query
            .OrderByDescending(c => c.CreatedAtUtc)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);

        var items = consultations.Select(c =>
            c.ToSummaryDto(users.FirstOrDefault(u => u.Id == c.FarmerId)?.FullName ?? string.Empty))
            .ToList();

        return new PaginatedList<ConsultationSummaryDto>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Items = items
        };
    }
}