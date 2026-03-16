namespace Namaa.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Namaa.Domain.Identity;
using Namaa.Domain.Land;

public interface IAppDbContext
{
 
 public DbSet<RefreshToken> RefreshTokens {get;}
 public DbSet<Land> Lands {get;}
Task<int> SaveChangesAsync(CancellationToken ct=default);
}