using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Farmers.Dtos;
using Namaa.Application.Features.Farmers.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Farmer;

namespace Namaa.Application.Features.Farmers.Commands.CreateProfile;

public class CreateFarmerProfileCommandHandler(IAppDbContext context)
    : IRequestHandler<CreateFarmerProfileCommand, Result<FarmerProfileDto>>
{
    public async Task<Result<FarmerProfileDto>> Handle(CreateFarmerProfileCommand request, CancellationToken cancellationToken)
    {

        var exists = await context.FarmerProfiles
            .AnyAsync(x => x.Id == request.UserId, cancellationToken);

        if (exists)
            return ApplicationErrors.AlreadyExists;

        var result = FarmerProfile.Create(request.UserId,request.Description,request.GovernorateId,request.AddressDetail);

        if (result.IsError)
            return result.Errors;

        context.FarmerProfiles.Add(result.Value);
        await context.SaveChangesAsync(cancellationToken);
         var farmer = await context.FarmerProfiles
        .Include(x => x.Governorate)
        .FirstAsync(x => x.Id == request.UserId, cancellationToken);

     return farmer.ToDto();

    }
}