using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.ReferenceData;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class SoilTypeConfiguration : IEntityTypeConfiguration<SoilType>
{
    public void Configure(EntityTypeBuilder<SoilType> builder)
    {
        builder.ToTable("SoilTypes");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
        .IsRequired()
        .HasMaxLength(100);

        builder.HasData(
            new SoilType(1,"Red Soil"),
            new SoilType(2,"Rendzina Soil"),
            new SoilType(3,"Clay Soil"),
            new SoilType(4,"Sandy Soil")
        );
    }
}