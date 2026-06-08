using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Investments;
using Namaa.Domain.Profiles.Investor;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class InvestorContributionConfiguration : IEntityTypeConfiguration<InvestorContribution>
{
    public void Configure(EntityTypeBuilder<InvestorContribution> builder)
    {
        builder.ToTable("InvestorContributions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.InvestmentProjectId)
        .IsRequired();

        builder.Property(x => x.InvestorId)
        .IsRequired();

        builder.Property(x => x.Amount)
        .HasPrecision(18,2)
        .IsRequired();

        builder.Property(x => x.Status)
        .HasConversion<string>()
        .IsRequired();

        builder.Property(x => x.ProfitAmount)
        .HasPrecision(18,2);

        builder.HasOne<InvestorProfile>()
              .WithMany()
              .HasForeignKey(x => x.InvestorId)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.InvestmentProject)
              .WithMany(x => x.Contributions)
              .HasForeignKey(x => x.InvestmentProjectId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}