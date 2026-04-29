using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;

namespace Namaa.Application.Features.Account.Queries.IsProfileCompleted;
public class IsProfileCompletedQueryHandler(IAppDbContext context) 
    : IRequestHandler<IsProfileCompletedQuery, bool>
{
    public async Task<bool> Handle(IsProfileCompletedQuery request, CancellationToken ct)
    {
        switch (request.Role)
        {
            case "Expert":
                return await CheckExpertAsync(request.UserId,ct);

            case "Farmer":
                var farmerComplete = await CheckFarmerAsync(request.UserId, ct);
                return farmerComplete;

            case "Trader":
                var traderComplete = await CheckTraderAsync(request.UserId, ct);
                return traderComplete;

            case "Investor":
                var investorComplete = await CheckInvestorAsync(request.UserId, ct);
                return investorComplete;

            default:
                return false;
        }
    }

    private async Task<bool> CheckExpertAsync(Guid userId, CancellationToken ct)
    {
        var expert = await context.ExpertProfiles
            .Where(x => x.Id == userId) 
            .Select(e => new { e.GovernorateId })
            .FirstOrDefaultAsync(ct);

        if (expert == null) 
            return false;
        

        bool isComplete = expert.GovernorateId.HasValue; 

        return isComplete;
    }

    private async Task<bool> CheckFarmerAsync(Guid userId, CancellationToken ct)
    {
        // Check if a farmer profile record exists for this user
        return await context.FarmerProfiles.AnyAsync(x => x.Id == userId, ct);
    }

    private async Task<bool> CheckTraderAsync(Guid userId, CancellationToken ct)
    {
        return await context.TraderProfiles.AnyAsync(x => x.Id == userId, ct);
    }

    private async Task<bool> CheckInvestorAsync(Guid userId, CancellationToken ct)
    {
        return await context.InvestorProfiles.AnyAsync(x => x.Id == userId, ct);
    }
}