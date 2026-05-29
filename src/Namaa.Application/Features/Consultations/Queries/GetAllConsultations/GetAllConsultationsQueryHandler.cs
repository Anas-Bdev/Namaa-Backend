using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Application.Features.Consultations.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Queries.GetAllConsultations;

public class GetAllConsultationsQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetAllConsultationsQuery, Result<PaginatedList<ConsultationSummaryDto>>>
{
    public async Task<Result<PaginatedList<ConsultationSummaryDto>>> Handle(
        GetAllConsultationsQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.ConsultationRequests
            .AsNoTracking()
            .Include(c => c.Responses);

        var filtered = request.Status.HasValue
            ? query.Where(c => c.Status == request.Status)
            : query;

        var totalCount = await filtered.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var consultations = await filtered
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