using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investors.Dtos;
using Namaa.Application.Features.Investors.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investors.Queries.GetInvestorProfile;

public class GetInvestorProfileQueryHandler(IAppDbContext context) : IRequestHandler<GetInvestorProfileQuery, Result<InvestorProfileDto>>
{
    public async Task<Result<InvestorProfileDto>> Handle(GetInvestorProfileQuery request, CancellationToken cancellationToken)
    {
        var investor = await context.InvestorProfiles
        .Include(x => x.Governorate)
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if(investor is null)
        return ApplicationErrors.InvestorNotFound;

        return investor.ToDto();
    }
}