using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Consultation;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class ExpertResponseConfiguration : IEntityTypeConfiguration<ExpertResponse>
{
    public void Configure(EntityTypeBuilder<ExpertResponse> builder)
    {
        builder.ToTable("ExpertResponses");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();
        builder.Property(r => r.Message).HasMaxLength(2000);
    }
}