using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investors.Dtos;
using Namaa.Application.Features.Investors.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Investor;

namespace Namaa.Application.Features.Investors.Commands.UpdateProfile;

public class UpdateInvestorProfileCommandHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<UpdateInvestorProfileCommand, Result<InvestorProfileDto>>
{
    public async Task<Result<InvestorProfileDto>> Handle(
        UpdateInvestorProfileCommand request,
        CancellationToken cancellationToken)
    {
        var investor = await context.InvestorProfiles
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (investor is null)
            return ApplicationErrors.InvestorNotFound;

        var result = investor.UpdateProfile(
            request.OrganizationName,
            request.CompanyName,
            request.CityId,
            request.AddressDetail
        );

        if (result.IsError)
            return result.Errors;

        await context.SaveChangesAsync(cancellationToken);

        var users = await userReadRepository.Query()
            .ToListAsync(cancellationToken);

        var fullName = users.FirstOrDefault(u => u.Id == investor.Id)?.FullName ?? string.Empty;

        return investor.ToDto(fullName);
    }
}
