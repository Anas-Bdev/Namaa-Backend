using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Profiles.Farmer;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class FarmerProfileConfiguration : IEntityTypeConfiguration<FarmerProfile>
{
    public void Configure(EntityTypeBuilder<FarmerProfile> builder)
    {
        builder.ToTable("FarmerProfiles");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .ValueGeneratedNever();

        builder.Property(f => f.Description)
            .HasMaxLength(1000);

        builder.Property(f => f.AddressDetail)
            .HasMaxLength(250);

        builder.Property(f => f.ExperienceLevel)
            .HasMaxLength(100);
    }
}
