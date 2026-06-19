using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Notifications;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.UserId).IsRequired();
        builder.Property(n => n.Title).IsRequired().HasMaxLength(200);
        builder.Property(n => n.Message).IsRequired().HasMaxLength(1000);
        builder.Property(n => n.Type).IsRequired().HasMaxLength(50);

        builder.Property(n => n.ReferencedId);
        builder.Property(n => n.TriggeredBy);

        builder.HasIndex(n => new { n.UserId, n.IsRead });

        builder.HasIndex(n => n.CreatedAtUtc);
    }
}