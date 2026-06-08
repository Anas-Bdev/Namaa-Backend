using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.MarketPlace;
using Namaa.Domain.Profiles.Farmer;
using Namaa.Domain.Profiles.Trader;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class FarmerRatingConfiguration : IEntityTypeConfiguration<FarmerRating>
{
    public void Configure(EntityTypeBuilder<FarmerRating> builder)
    {
        builder.ToTable("FarmerRatings");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.RatingValue)
        .IsRequired();

        builder.Property(x => x.Comment)
        .HasMaxLength(2000);

        builder.HasIndex(x => x.OrderId).IsUnique();

        builder.HasOne<ProductOrder>()
            .WithMany()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<TraderProfile>()
            .WithMany()
            .HasForeignKey(x => x.ReviewerTraderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<FarmerProfile>()
            .WithMany()
            .HasForeignKey(x => x.FarmerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}