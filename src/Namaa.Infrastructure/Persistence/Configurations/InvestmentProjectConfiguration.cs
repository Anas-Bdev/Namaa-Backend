using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Investment;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class InvestmentProjectConfiguration : IEntityTypeConfiguration<InvestmentProject>
{
    public void Configure(EntityTypeBuilder<InvestmentProject> builder)
    {
        builder.ToTable("InvestmentProjects");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.Title).HasMaxLength(200);
        builder.Property(p => p.RequiredAmount).HasColumnType("decimal(18,2)");
        builder.Property(p => p.AmountCollected).HasColumnType("decimal(18,2)");
        builder.Property(p => p.ExpectedProfit).HasColumnType("decimal(18,2)");
        builder.Property(p => p.SharePercentage).HasColumnType("decimal(5,2)");
        builder.Property(p => p.Status).HasConversion<string>();
        builder.Property(p => p.CreatorRole).HasConversion<string>();

        builder.HasMany(p => p.Contributions)
            .WithOne(c => c.Project)
            .HasForeignKey(c => c.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(p => p.Contributions)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
