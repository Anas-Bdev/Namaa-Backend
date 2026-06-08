using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Consultations;
using Namaa.Domain.Identity;
using Namaa.Domain.Investments;
using Namaa.Domain.Lands;
using Namaa.Domain.MarketPlace;
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

    public DbSet<InvestmentProject> InvestmentProjects => Set<InvestmentProject>();

    public DbSet<InvestorContribution> InvestorContributions => Set<InvestorContribution>();

    public DbSet<ProductListing> ProductListings => Set<ProductListing>();

    public DbSet<ProductOrder> ProductOrders => Set<ProductOrder>();

    public DbSet<FarmerRating> FarmerRatings => Set<FarmerRating>();

    public DbSet<ConsultationMessage> ConsultationMessages => Set<ConsultationMessage>();

    public DbSet<ConsultationRequest> ConsultationRequests => Set<ConsultationRequest>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
       base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}