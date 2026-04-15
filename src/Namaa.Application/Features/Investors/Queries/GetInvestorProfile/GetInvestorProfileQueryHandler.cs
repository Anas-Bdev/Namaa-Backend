using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investors.Dtos;
using Namaa.Application.Features.Investors.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Investor;

namespace Namaa.Application.Features.Investors.Queries.GetInvestorProfile;

public class GetInvestorProfileQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetInvestorProfileQuery, Result<InvestorProfileDto>>
{
    public async Task<Result<InvestorProfileDto>> Handle(
        GetInvestorProfileQuery request,
        CancellationToken cancellationToken)
    {
        var investor = await context.InvestorProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == request.UserId, cancellationToken);

        if (investor is null)
            return ApplicationErrors.InvestorNotFound;

        var users = await userReadRepository.Query()
            .ToListAsync(cancellationToken);

        var fullName = users.FirstOrDefault(u => u.Id == investor.Id)?.FullName ?? string.Empty;

        return investor.ToDto(fullName);
    }
}
