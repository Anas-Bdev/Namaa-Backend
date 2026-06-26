using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investors.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Investors.Queries.GetInvestorProfileById;

public class GetInvestorProfileByIdQueryHandler(IAppDbContext context, IUserReadRepository userReadRepository) : IRequestHandler<GetInvestorProfileByIdQuery, Result<InvestorListItemDto>>
{
    public async Task<Result<InvestorListItemDto>> Handle(GetInvestorProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var investor=await context.InvestorProfiles.AsNoTracking().
        Include(x => x.Governorate)
        .FirstOrDefaultAsync(f => f.Id==request.InvestorId,cancellationToken);

        if(investor is null)
        return ApplicationErrors.InvestorNotFound;
        var user=await userReadRepository.GetByIdAsync(request.InvestorId,cancellationToken);
        if (user is null || user.Status != UserStatus.Active)
        return ApplicationErrors.InvestorNotFound;

       return new InvestorListItemDto
        {
            Id = investor.Id,
            FullName = user.FullName,
            ProfileImageUrl = user.ProfileImageUrl,
            Governorate = investor.Governorate!.Name!,
            OrganizationName=investor.OrganizationName,
            InvestorType=investor.Type.ToString(),
            PhoneNumber=user.PhoneNumber
        };
    }
}