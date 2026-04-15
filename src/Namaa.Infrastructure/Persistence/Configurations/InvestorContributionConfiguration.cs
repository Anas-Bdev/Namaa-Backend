using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Investment;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class InvestorContributionConfiguration : IEntityTypeConfiguration<InvestorContribution>
{
    public void Configure(EntityTypeBuilder<InvestorContribution> builder)
    {
        builder.ToTable("InvestorContributions");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();
        builder.Property(c => c.Amount).HasColumnType("decimal(18,2)");
        builder.Property(c => c.Status).HasConversion<string>();
    }
}