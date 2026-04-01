namespace Namaa.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Namaa.Domain.Identity;
using Namaa.Domain.Investment;
using Namaa.Domain.Land;
using Namaa.Domain.Profiles.Expert;
using Namaa.Domain.Profiles.Farmer;
using Namaa.Domain.Profiles.Investor;
using Namaa.Domain.Profiles.Trader;

public interface IAppDbContext
{
 
 DbSet<RefreshToken> RefreshTokens {get;}
 DbSet<Land> Lands {get;}
DbSet<ExpertProfile> ExpertProfiles { get; }
DbSet<ExpertAvailability> ExpertAvailabilities { get; }
DbSet<FarmerProfile> FarmerProfiles { get; }
DbSet<InvestorProfile> InvestorProfiles { get; }
DbSet<TraderProfile> TraderProfiles { get; } Task<int> SaveChangesAsync(CancellationToken ct=default);
DbSet<InvestmentProject> InvestmentProjects { get; }
DbSet<InvestorContribution> InvestorContributions { get; }
}