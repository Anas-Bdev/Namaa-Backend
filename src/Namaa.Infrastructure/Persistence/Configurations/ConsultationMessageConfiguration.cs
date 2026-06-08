using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Consultations;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class ConsultationMessageConfiguration : IEntityTypeConfiguration<ConsultationMessage>
{
    public void Configure(EntityTypeBuilder<ConsultationMessage> builder)
    {
        builder.ToTable("ConsultationMessages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Content)
        .IsRequired()
        .HasMaxLength(2000);

        builder.HasIndex(x => x.ConsultationRequestId);
    }
}