using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common;
using Namaa.Domain.Identity;
using Namaa.Domain.Investment;
using Namaa.Domain.Land;
using Namaa.Domain.Profiles.Expert;
using Namaa.Domain.Profiles.Farmer;
using Namaa.Domain.Profiles.Investor;
using Namaa.Domain.Profiles.Trader;
using Namaa.Infrastructure.Identity;

namespace Namaa.Infrastructure.Persistence.Context;
public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser,AppRole,Guid>(options),IAppDbContext
{
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Land> Lands => Set<Land>();

    public DbSet<ExpertProfile> ExpertProfiles => Set<ExpertProfile>();

    public DbSet<FarmerProfile> FarmerProfiles { get; set; }

    public DbSet<TraderProfile> TraderProfiles => Set<TraderProfile>();

    public DbSet<InvestorProfile> InvestorProfiles => Set<InvestorProfile>();

    public DbSet<ExpertAvailability> ExpertAvailabilities => Set<ExpertAvailability>();

    public DbSet<InvestmentProject> InvestmentProjects => Set<InvestmentProject>();
    public DbSet<InvestorContribution> InvestorContributions => Set<InvestorContribution>();
    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await base.SaveChangesAsync(ct);
    }

   
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
       base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}