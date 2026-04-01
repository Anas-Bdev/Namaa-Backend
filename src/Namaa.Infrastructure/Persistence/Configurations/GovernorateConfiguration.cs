using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.ReferenceData;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class GovernorateConfiguration : IEntityTypeConfiguration<Governorate>
{
    public void Configure(EntityTypeBuilder<Governorate> builder)
    {
    builder.ToTable("Governorates");
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(100);

         builder.Property(g => g.WaterAvailability)
         .IsRequired()
         .HasMaxLength(100);

         builder.Property(g => g.AvgRainfall)
         .IsRequired();
            
            builder.HasData(
    new Governorate(1,  "Jerusalem", 26.0f, 10.0f, 525, "Low"),
    new Governorate(2,  "Ramallah",  24.0f, 9.0f,  615, "Medium"),
    new Governorate(3,  "Hebron",    24.0f, 11.0f, 425, "Low"),
    new Governorate(4,  "Bethlehem", 27.0f, 10.0f, 525, "Low to Medium"),
    new Governorate(5,  "Nablus",    29.5f, 8.0f,  650, "Medium to High"),
    new Governorate(6,  "Jenin",     31.5f, 9.0f,  500, "High"),
    new Governorate(7,  "Tulkarm",   27.0f, 11.0f, 575, "Medium"),
    new Governorate(8,  "Qalqilya",  31.5f, 13.0f, 650, "High"),
    new Governorate(9,  "Salfit",    28.5f, 9.0f,  550, "Medium"),
    new Governorate(10, "Tubas",     30.0f, 12.0f, 375, "Medium"),
    new Governorate(11, "Jericho",   36.0f, 15.0f, 175, "Low"),
    new Governorate(12, "Gaza",      30.0f, 12.5f, 375, "Very Low")
      );
    }
}