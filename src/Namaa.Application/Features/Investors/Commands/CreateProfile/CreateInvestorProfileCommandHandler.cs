using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Investor;

namespace Namaa.Application.Features.Investors.Commands.CreateProfile;

public class CreateInvestorProfileCommandHandler(IAppDbContext context)
    : IRequestHandler<CreateInvestorProfileCommand, Result<Created>>
{
    public async Task<Result<Created>> Handle(
        CreateInvestorProfileCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await context.InvestorProfiles
            .AnyAsync(x => x.Id == request.UserId, cancellationToken);

        if (exists)
            return InvestorErrors.InvestorAlreadyExists;

        var result = InvestorProfile.Create(request.UserId);

        if (result.IsError)
            return result.Errors;

        context.InvestorProfiles.Add(result.Value);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Created;
    }
}
