using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Lands;
using Namaa.Domain.Profiles.Farmer;
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

        builder.HasOne<FarmerProfile>()
            .WithMany()
            .HasForeignKey(l => l.FarmerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(l => l.AddressDetail)
        .IsRequired()
        .HasMaxLength(250); // Limits the column size so it doesn't default to NVARCHAR(MAX)

    // 2. Configure Latitude
    builder.Property(l => l.Latitude)
        .IsRequired(); 

    // 3. Configure Longitude
    builder.Property(l => l.Longitude)
        .IsRequired();
    }
}