namespace Namaa.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Namaa.Domain.Identity;

public interface IAppDbContext
{
 
 public DbSet<RefreshToken> RefreshTokens {get;set;}
Task<int> SaveChangesAsync(CancellationToken ct=default);
}