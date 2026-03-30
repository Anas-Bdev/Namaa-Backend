namespace Namaa.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Namaa.Domain.Identity;
using Namaa.Domain.Land;
using Namaa.Domain.Profiles.Expert;
using Namaa.Domain.Profiles.Farmer;

public interface IAppDbContext
{
 
 DbSet<RefreshToken> RefreshTokens {get;}
 DbSet<Land> Lands {get;}
DbSet<ExpertProfile> ExpertProfiles { get; }
DbSet<ExpertAvailability> ExpertAvailabilities { get; }
DbSet<FarmerProfile> FarmerProfiles { get; }
Task<int> SaveChangesAsync(CancellationToken ct=default);
}