
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common;

namespace Namaa.Infrastructure.Persistence.Interceptors;
public class AuditableEntityInterceptor(IUser user,TimeProvider timeProvider) : SaveChangesInterceptor{
 public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,InterceptionResult<int> result,CancellationToken ct = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, ct);
    }
    private void UpdateEntities(DbContext? context)
    {
        if(context is null)
        return;
        var entries=context.ChangeTracker.Entries<AuditableEntity>();
        foreach(var entry in entries)
        {
            var utcNow=timeProvider.GetUtcNow();
            var userId=user.Id;
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAtUtc=utcNow;
                entry.Entity.CreatedBy=userId;
            }
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                entry.Entity.LastModifiedUtc = utcNow;
                entry.Entity.LastModifiedBy = userId;
            }
        }
    }
}
public static class EntityEntryExtensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
    {
        return entry.References.Any(r => r.TargetEntry?.Metadata.IsOwned()==true &&
        (r.TargetEntry.State==EntityState.Added || r.TargetEntry.State==EntityState.Modified));
    }

    
}