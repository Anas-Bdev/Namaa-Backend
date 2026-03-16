using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Land;
using Namaa.Infrastructure.Identity;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class LandConfiguration : IEntityTypeConfiguration<Land>
{
    public void Configure(EntityTypeBuilder<Land> builder)
    {
        builder.ToTable("Lands");
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Name).HasMaxLength(200);
        builder.Property(l => l.Area).IsRequired();
        builder.Property(l => l.WaterSourceType).IsRequired().HasConversion<string>();
        builder.Property(l => l.WaterAvailability).IsRequired().HasConversion<string>();
        builder.Property(l => l.EnvironmentType).IsRequired().HasConversion<string>();
        builder.Property(l => l.CityId).IsRequired();
        builder.Property(l => l.SoilId).IsRequired();

        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(l => l.FarmerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        


    }
}