using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Application.Features.Consultations.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Queries.GetMyConsultations;

public class GetMyConsultationsQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetMyConsultationsQuery, Result<PaginatedList<ConsultationSummaryDto>>>
{
    public async Task<Result<PaginatedList<ConsultationSummaryDto>>> Handle(
        GetMyConsultationsQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.ConsultationRequests
            .AsNoTracking()
            .Include(c => c.Responses)
            .Where(c => c.FarmerId == request.FarmerId);

        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var consultations = await query
            .OrderByDescending(c => c.CreatedAtUtc)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var farmerName = users.FirstOrDefault(u => u.Id == request.FarmerId)?.FullName ?? string.Empty;

        var items = consultations.Select(c => c.ToSummaryDto(farmerName)).ToList();

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