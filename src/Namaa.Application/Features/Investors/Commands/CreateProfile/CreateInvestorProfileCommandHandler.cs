using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investors.Dtos;
using Namaa.Application.Features.Investors.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Investor;

namespace Namaa.Application.Features.Investors.Commands.CreateProfile;

public class CreateInvestorCommandHandler(IAppDbContext context) : IRequestHandler<CreateInvestorProfileCommand, Result<InvestorProfileDto>>
{
    public async Task<Result<InvestorProfileDto>> Handle(CreateInvestorProfileCommand request, CancellationToken cancellationToken)
    {
          var exists = await context.InvestorProfiles
            .AnyAsync(x => x.Id == request.UserId, cancellationToken);

        if (exists)
            return ApplicationErrors.AlreadyExists;

        var result = InvestorProfile.Create(request.UserId,request.Type,request.OrganizationName,request.GovernorateId,request.AddressDetail);

        if (result.IsError)
            return result.Errors;

        context.InvestorProfiles.Add(result.Value);
        await context.SaveChangesAsync(cancellationToken);
        return result.Value.ToDto();
    }
}