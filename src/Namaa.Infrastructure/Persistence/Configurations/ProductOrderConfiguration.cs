using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.MarketPlace;
using Namaa.Domain.Profiles.Trader;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
{
    public void Configure(EntityTypeBuilder<ProductOrder> builder)
    {

        builder.Property(o => o.OrderNumber)
            .IsRequired()
            .HasMaxLength(30);

        builder.ToTable("ProductOrders");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Quantity)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

        builder.Property(x => x.PriceAtPurchase)
        .HasColumnType("decimal(18,2)")
        .IsRequired();
        builder.OwnsOne(x => x.DeliveryAddress,address =>
        {
            address.Property(a => a.Governorate).HasMaxLength(100).IsRequired();
            address.Property(a => a.CityOrVillage).HasMaxLength(100).IsRequired();
            address.Property(a => a.NeighborhoodOrStreet).HasMaxLength(250).IsRequired();
            address.Property(a => a.LandMark).HasMaxLength(250);
        });

        builder.Ignore(x => x.TotalPrice);

        builder.Property(x => x.Status)
        .HasConversion<string>()
        .HasMaxLength(50)
        .IsRequired();

        builder.Property(x => x.DeliveryNotes)
        .HasMaxLength(1000);

        builder.HasOne<TraderProfile>()
        .WithMany()
        .HasForeignKey(x => x.TraderId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ProductListing)
        .WithMany()
        .HasForeignKey(x => x.ProductListingId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(o => o.OrderNumber);
    }
}