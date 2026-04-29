using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Marketplace;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class FarmerRatingConfiguration : IEntityTypeConfiguration<FarmerRating>
{
    public void Configure(EntityTypeBuilder<FarmerRating> builder)
    {
        builder.ToTable("FarmerRatings");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();
        builder.Property(r => r.Comment).HasMaxLength(500);
    }
}