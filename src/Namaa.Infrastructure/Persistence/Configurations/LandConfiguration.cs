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
        
        builder.Property(l => l.Id)
            .ValueGeneratedNever();

        builder.Property(l => l.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(l => l.Area)
            .IsRequired()
            .HasPrecision(18,2);

        builder.Property(l => l.WaterSourceType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(l => l.WaterAvailability)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(l => l.EnvironmentType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

             builder.Property(l => l.IrrigationMethod)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(l => l.GovernorateId)
            .IsRequired();

        builder.Property(l => l.SoilTypeId)
            .IsRequired();

        builder.HasOne(l => l.SoilType)
               .WithMany() // A SoilType can be found on many Lands
               .HasForeignKey(l => l.SoilTypeId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.Governorate)
               .WithMany() // A Governorate can have many Lands
               .HasForeignKey(l => l.GovernorateId)
               .OnDelete(DeleteBehavior.Restrict); // Important!

        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(l => l.FarmerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}