using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Profiles.Trader;
using Namaa.Infrastructure.Identity;

namespace Namaa.Infrastructure.Persistence.Configurations;
public sealed class TraderProfileConfiguration : IEntityTypeConfiguration<TraderProfile>
{
    public void Configure(EntityTypeBuilder<TraderProfile> builder)
    {
        // 1. Table Name
        builder.ToTable("TraderProfiles");

        // 2. Primary Key
        // Since the Id comes from the AppUser (Identity), we don't let SQL generate it.
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedNever();

        // 3. Properties
        builder.Property(t => t.BusinessName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.BusinessType)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(t => t.AddressDetail)
            .HasMaxLength(250); // Optional field

        // 4. Relationships
        builder.HasOne(t => t.Governorate)
            .WithMany() // One Governorate can have many Traders
            .HasForeignKey(t => t.GovernorateId)
            .OnDelete(DeleteBehavior.Restrict); 
            // Restrict means you can't delete a city if a trader is still registered there.

        builder.HasOne<AppUser>()
        .WithOne()
        .HasForeignKey<TraderProfile>(t => t.Id)
        .OnDelete(DeleteBehavior.Cascade);
    }

    
}