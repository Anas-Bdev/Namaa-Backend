using Microsoft.EntityFrameworkCore;
using Namaa.Domain.Profiles.Expert;
using Namaa.Infrastructure.Identity;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class ExpertProfileConfiguration : IEntityTypeConfiguration<ExpertProfile>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ExpertProfile> builder)
    {
        builder.ToTable("ExpertProfiles");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.CvUrl)
        .IsRequired()
        .HasMaxLength(500);

        builder.Property(e => e.Specialization)
        .IsRequired()
        .HasMaxLength(500)
        .HasConversion<string>();

        builder.Property(e => e.AddressDetail)
        .HasMaxLength(250);

        builder.HasOne(e => e.Governorate)
            .WithMany() // One Governorate can have many Experts
            .HasForeignKey(e => e.GovernorateId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(e => e.Availabilities)
        .WithOne(a => a.Expert)
        .HasForeignKey(a => a.ExpertProfileId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(e => e.Availabilities)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        builder.HasOne<AppUser>()
        .WithOne()
        .HasForeignKey<ExpertProfile>(e => e.Id)
        .OnDelete(DeleteBehavior.Cascade);

    }
}