namespace Namaa.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Namaa.Domain.Identity;
using Namaa.Domain.Land;
using Namaa.Domain.Profiles.Expert;

public interface IAppDbContext
{
 
 DbSet<RefreshToken> RefreshTokens {get;}
 DbSet<Land> Lands {get;}
DbSet<ExpertProfile> ExpertProfiles { get; }
DbSet<ExpertAvailability> ExpertAvailabilities { get; }
Task<int> SaveChangesAsync(CancellationToken ct=default);
}