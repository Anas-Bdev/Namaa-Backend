using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Profiles.Trader;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class TraderProfileConfiguration : IEntityTypeConfiguration<TraderProfile>
{
    public void Configure(EntityTypeBuilder<TraderProfile> builder)
    {
        builder.ToTable("TraderProfiles");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedNever();
        builder.Property(t => t.BusinessName).HasMaxLength(200);
        builder.Property(t => t.BusinessType).HasMaxLength(100);
        builder.Property(t => t.PreferredCategories).HasMaxLength(500);
        builder.Property(t => t.AddressDetail).HasMaxLength(250);
    }
}
