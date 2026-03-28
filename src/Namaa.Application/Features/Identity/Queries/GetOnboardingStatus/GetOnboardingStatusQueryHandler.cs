using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Identity.Dtos;

namespace Namaa.Application.Features.Identity.Queries.GetOnboardingStatus;

public class GetOnboardingStatusQueryHandler(IAppDbContext context) 
    : IRequestHandler<GetOnboardingStatusQuery, OnboardingStatus>
{
    public async Task<OnboardingStatus> Handle(GetOnboardingStatusQuery request, CancellationToken ct)
    {
        switch (request.Role)
        {
            case "Expert":
                return await CheckExpertAsync(request.UserId, ct);

            // case "Farmer":
            //     var farmerComplete = await CheckFarmerAsync(request.UserId, ct);
            //     return new OnboardingStatus(null, farmerComplete);

            // case "Trader":
            //     var traderComplete = await CheckTraderAsync(request.UserId, ct);
            //     return new OnboardingStatus(null, traderComplete);

            // case "Investor":
            //     var investorComplete = await CheckInvestorAsync(request.UserId, ct);
            //     return new OnboardingStatus(null, investorComplete);

            default:
                return new OnboardingStatus(null, true); 
        }
    }

    private async Task<OnboardingStatus> CheckExpertAsync(Guid userId, CancellationToken ct)
    {
        var expert = await context.ExpertProfiles
            .Where(x => x.Id == userId) 
            .Select(e => new { e.CvUrl, e.CityId })
            .FirstOrDefaultAsync(ct);

        if (expert == null) 
            return new OnboardingStatus(false, false);

        bool hasCv = !string.IsNullOrEmpty(expert.CvUrl);
        

        bool isComplete = expert.CityId.HasValue; 

        return new OnboardingStatus(hasCv, isComplete);
    }

    // private async Task<bool> CheckFarmerAsync(Guid userId, CancellationToken ct)
    // {
    //     // Check if a farmer profile record exists for this user
    //     return await context.FarmerProfiles.AnyAsync(x => x.UserId == userId, ct);
    // }

    // private async Task<bool> CheckTraderAsync(Guid userId, CancellationToken ct)
    // {
    //     return await context.TraderProfiles.AnyAsync(x => x.UserId == userId, ct);
    // }

    // private async Task<bool> CheckInvestorAsync(Guid userId, CancellationToken ct)
    // {
    //     return await context.InvestorProfiles.AnyAsync(x => x.UserId == userId, ct);
    // }
}