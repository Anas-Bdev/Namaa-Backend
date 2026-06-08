using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Investments;
using Namaa.Domain.Lands;
using Namaa.Domain.Profiles.Farmer;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class InvestmentProjectConfiguration : IEntityTypeConfiguration<InvestmentProject>
{
    public void Configure(EntityTypeBuilder<InvestmentProject> builder)
    {
        builder.ToTable("InvestmentProjects");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.LandId)
        .IsRequired();

        builder.Property(x => x.LandId)
        .IsRequired();

        builder.Property(x => x.Title)
        .IsRequired()
        .HasMaxLength(200);

        builder.Property(x => x.Description)
        .IsRequired()
        .HasMaxLength(2000);

        builder.Property(x => x.CoverImageUrl)
        .HasMaxLength(1000);

        builder.Property(x => x.RequiredAmount)
        .HasPrecision(18,2)
        .IsRequired();

        builder.Property(x => x.MinimumInvestment)
        .HasPrecision(18,2)
        .IsRequired();

        builder.Property(x => x.ExpectedRevenue)
        .HasPrecision(18,2)
        .IsRequired();

        builder.Property(x => x.ExpectedCost)
        .HasPrecision(18,2)
        .IsRequired();

        builder.Property(x => x.InvestorProfitSharePercentage)
        .HasPrecision(5,2)
        .IsRequired();

        builder.Property(x => x.ActualRevenue)
            .HasPrecision(18, 2);

        builder.Property(x => x.ActualCost)
            .HasPrecision(18, 2);

        builder.Property(x => x.DurationInMonths)
            .IsRequired();

        builder.Property(x => x.FundingDeadline)
            .IsRequired();

        builder.Property(x => x.ExpectedStartDate);

        builder.Property(x => x.ExpectedEndDate);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired();
        
        builder.Ignore(x => x.ExpectedProfit);
        builder.Ignore(x => x.ActualProfit);
        builder.Ignore(x => x.AmountCollected);

        builder.HasOne<FarmerProfile>()
            .WithMany()
            .HasForeignKey(x => x.FarmerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Land>()
            .WithMany()
            .HasForeignKey(x => x.LandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Contributions)
        .WithOne()
        .HasForeignKey(x => x.InvestmentProjectId)
        .OnDelete(DeleteBehavior.Cascade);

          builder.Navigation(c => c.Contributions)
       .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}