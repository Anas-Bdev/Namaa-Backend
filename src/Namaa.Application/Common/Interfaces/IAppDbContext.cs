namespace Namaa.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Namaa.Domain.Identity;
using Namaa.Domain.Lands;
using Namaa.Domain.Profiles.Expert;
using Namaa.Domain.ReferenceData;
using Namaa.Domain.SeedingCycles;

public interface IAppDbContext
{
 
 DbSet<RefreshToken> RefreshTokens {get;}
 DbSet<SeedingCycle> SeedingCycles {get;}
 DbSet<Crop> Crops {get;}
 DbSet<SoilType> SoilTypes {get;}
 DbSet<Governorate> Governorates {get;}
 DbSet<Land> Lands {get;}
DbSet<ExpertProfile> ExpertProfiles { get; }
DbSet<ExpertAvailability> ExpertAvailabilities { get; }
Task<int> SaveChangesAsync(CancellationToken ct=default);

}