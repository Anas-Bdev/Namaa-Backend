using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Namaa.Domain.Profiles.Farmer;
using Namaa.Infrastructure.Identity;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class FarmerProfileConfiguration : IEntityTypeConfiguration<FarmerProfile>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FarmerProfile> builder)
    {
        builder.ToTable("FarmerProfiles");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).ValueGeneratedNever();

        builder.Property(f => f.Description)
            .HasMaxLength(500);

        builder.Property(f => f.AddressDetail)
            .HasMaxLength(250);


        builder.HasOne(f => f.Governorate)
            .WithMany()
            .HasForeignKey(f => f.GovernorateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<AppUser>()
        .WithOne()
        .HasForeignKey<FarmerProfile>(f => f.Id)
        .OnDelete(DeleteBehavior.Cascade);;;
    }
}