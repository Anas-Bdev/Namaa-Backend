namespace Namaa.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Namaa.Domain.Identity;
using Namaa.Domain.Investment;
using Namaa.Domain.Lands;
using Namaa.Domain.Marketplace;
using Namaa.Domain.Land;
using Namaa.Domain.Profiles.Expert;
using Namaa.Domain.Profiles.Farmer;
using Namaa.Domain.Profiles.Investor;
using Namaa.Domain.Profiles.Trader;
using Namaa.Domain.ReferenceData;
using Namaa.Domain.SeedingCycles;
using Land = Domain.Land.Land;

public interface IAppDbContext
{
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<SeedingCycle> SeedingCycles { get; }
    DbSet<Crop> Crops { get; }
    DbSet<SoilType> SoilTypes { get; }
    DbSet<Governorate> Governorates { get; }
    DbSet<Land> Lands { get; }
    DbSet<ExpertProfile> ExpertProfiles { get; }
    DbSet<ExpertAvailability> ExpertAvailabilities { get; }
    DbSet<FarmerProfile> FarmerProfiles { get; }
    DbSet<TraderProfile> TraderProfiles { get; }
    DbSet<InvestorProfile> InvestorProfiles { get; }
    DbSet<InvestmentProject> InvestmentProjects { get; }
    DbSet<InvestorContribution> InvestorContributions { get; }
    DbSet<ProductListing> ProductListings { get; }
    DbSet<Order> Orders { get; }
    DbSet<FarmerRating> FarmerRatings { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);

}