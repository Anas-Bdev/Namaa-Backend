using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Marketplace;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class ProductListingConfiguration : IEntityTypeConfiguration<ProductListing>
{
    public void Configure(EntityTypeBuilder<ProductListing> builder)
    {
        builder.ToTable("ProductListings");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.Title).HasMaxLength(200);
        builder.Property(p => p.Unit).HasMaxLength(50);
        builder.Property(p => p.PricePerUnit).HasColumnType("decimal(18,2)");
        builder.Property(p => p.DiscountPrice).HasColumnType("decimal(18,2)");

        builder.HasMany(p => p.Orders)
            .WithOne(o => o.Listing)
            .HasForeignKey(o => o.ListingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(p => p.Orders)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}