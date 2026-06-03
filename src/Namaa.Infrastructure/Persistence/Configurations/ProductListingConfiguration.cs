using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.MarketPlace;
using Namaa.Domain.Profiles.Farmer;
using Namaa.Domain.ReferenceData;
using Namaa.Domain.SeedingCycles;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class ProductListingConfiguration : IEntityTypeConfiguration<ProductListing>
{
    public void Configure(EntityTypeBuilder<ProductListing> builder)
    {
        builder.ToTable("ProductListings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
        .IsRequired()
        .HasMaxLength(200);

        builder.Property(x => x.Description)
        .HasMaxLength(2000);

        builder.Property(x => x.ImageUrl)
        .HasMaxLength(500);

        builder.Property(x => x.PricePerUnit)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

        builder.Property(x => x.DiscountPrice)
        .HasColumnType("decimal(18,2)");

        builder.Property(x => x.QuantityAvailable)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

        builder.Ignore(x => x.IsAvailable);

        builder.Property(x => x.Status)
        .HasConversion<string>()
        .HasMaxLength(50)
        .IsRequired();

        builder.Property(x => x.Unit)
        .HasMaxLength(50)
        .IsRequired();

        builder.HasOne<FarmerProfile>()
        .WithMany()
        .HasForeignKey(x => x.FarmerId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Crop>(x => x.Crop)
        .WithMany()
        .HasForeignKey(x => x.CropId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<SeedingCycle>()
        .WithMany()
        .HasForeignKey(x => x.SeedingCycleId)
        .OnDelete(DeleteBehavior.SetNull);

    }
}