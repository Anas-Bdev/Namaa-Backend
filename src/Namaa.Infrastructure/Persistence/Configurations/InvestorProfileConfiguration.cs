using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Profiles.Investor;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class InvestorProfileConfiguration : IEntityTypeConfiguration<InvestorProfile>
{
    public void Configure(EntityTypeBuilder<InvestorProfile> builder)
    {
        builder.ToTable("InvestorProfiles");

        // 2. Primary Key
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedNever();

        // 3. Properties
        builder.Property(i => i.Type)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(i => i.OrganizationName)
            .HasMaxLength(200); // Matches standard company name lengths. Optional, so no IsRequired()

        builder.Property(i => i.AddressDetail)
            .HasMaxLength(250); // Matches the length we used for the other profiles

        // 4. Relationships
        builder.HasOne(i => i.Governorate)
            .WithMany() // One Governorate can have many Investors
            .HasForeignKey(i => i.GovernorateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}