using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Profiles.Expert;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class ExpertAvailabilityConfiguration : IEntityTypeConfiguration<ExpertAvailability>
{
    public void Configure(EntityTypeBuilder<ExpertAvailability> builder)
    {
        builder.ToTable("ExpertAvailabilities");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();

        builder.Property(a => a.Day)
            .IsRequired();

            builder.Property(a => a.StartTime)
            .IsRequired();

            builder.Property(a => a.EndTime)
            .IsRequired();

            builder.Property(a => a.ExpertProfileId)
            .IsRequired();

            builder.HasIndex(a => a.ExpertProfileId);
    }
}