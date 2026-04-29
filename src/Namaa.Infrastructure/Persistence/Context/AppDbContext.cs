using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Identity;
using Namaa.Domain.Lands;
using Namaa.Domain.Marketplace;
using Namaa.Domain.Profiles.Expert;
using Namaa.Domain.Profiles.Farmer;
using Namaa.Domain.Profiles.Investor;
using Namaa.Domain.Profiles.Trader;
using Namaa.Domain.ReferenceData;
using Namaa.Domain.SeedingCycles;
using Namaa.Infrastructure.Identity;
namespace Namaa.Infrastructure.Persistence.Context;
public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser,AppRole,Guid>(options),IAppDbContext
{
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Land> Lands => Set<Land>();

    public DbSet<ExpertProfile> ExpertProfiles => Set<ExpertProfile>();

    public DbSet<ExpertAvailability> ExpertAvailabilities => Set<ExpertAvailability>();

    public DbSet<Crop> Crops => Set<Crop>();

    public DbSet<SoilType> SoilTypes => Set<SoilType>();

    public DbSet<Governorate> Governorates => Set<Governorate>();

    public DbSet<SeedingCycle> SeedingCycles => Set<SeedingCycle>();

    public DbSet<FarmerProfile> FarmerProfiles => Set<FarmerProfile>();

    public DbSet<TraderProfile> TraderProfiles => Set<TraderProfile>();

    public DbSet<InvestorProfile> InvestorProfiles => Set<InvestorProfile>();

    public DbSet<ProductListing> ProductListings => Set<ProductListing>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<FarmerRating> FarmerRatings => Set<FarmerRating>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
       base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}