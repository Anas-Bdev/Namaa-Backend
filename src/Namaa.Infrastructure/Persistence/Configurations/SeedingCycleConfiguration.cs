using System;
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.ReferenceData;
using Namaa.Domain.SeedingCycles;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class SeedingCycleConfiguration : IEntityTypeConfiguration<SeedingCycle>
{
    public void Configure(EntityTypeBuilder<SeedingCycle> builder)
    {
        builder.ToTable("SeedingCycles");
         
        builder.HasKey(sc => sc.Id);

        builder.Property(sc => sc.LandId).IsRequired();

        builder.Property(sc => sc.CropId).IsRequired();

        builder.Property(sc => sc.StartDate).IsRequired();

        builder.Property(sc => sc.EstimatedHarvestDate).IsRequired();

        builder.Property(sc => sc.ActualHarvestDate).IsRequired(false);
        
        builder.Property(sc => sc.ActualYield).IsRequired(false);

        builder.Property(sc => sc.Status).IsRequired().HasConversion<string>();

        builder.Property(sc => sc.SeedQuantity).IsRequired();

        builder.Property(sc => sc.SeedingArea).IsRequired();

        builder.Property(sc => sc.ExpectedYield).IsRequired();

        builder.HasOne(sc => sc.Land)
        .WithMany()
        .HasForeignKey(sc => sc.LandId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Crop>()
        .WithMany()
        .HasForeignKey(sc => sc.CropId)
        .OnDelete(DeleteBehavior.Restrict);
        
    }
}