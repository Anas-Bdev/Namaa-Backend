using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Profiles.Investor;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class InvestorProfileConfiguration : IEntityTypeConfiguration<InvestorProfile>
{
    public void Configure(EntityTypeBuilder<InvestorProfile> builder)
    {
        builder.ToTable("InvestorProfiles");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedNever();
        builder.Property(i => i.OrganizationName).HasMaxLength(200);
        builder.Property(i => i.CompanyName).HasMaxLength(200);
        builder.Property(i => i.AddressDetail).HasMaxLength(250);
    }
}