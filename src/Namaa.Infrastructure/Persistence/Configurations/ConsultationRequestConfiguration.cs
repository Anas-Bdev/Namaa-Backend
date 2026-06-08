using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Consultations;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class ConsultationRequestConfiguration : IEntityTypeConfiguration<ConsultationRequest>
{
    public void Configure(EntityTypeBuilder<ConsultationRequest> builder)
    {
        builder.ToTable("ConsultationRequests");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
        .IsRequired()
        .HasMaxLength(255);

        builder.Property(x => x.Description)
        .IsRequired();

        builder.Property(x => x.Status)
        .HasConversion<string>()
        .IsRequired();

        builder.HasMany(x => x.Messages)
        .WithOne()
        .HasForeignKey(x => x.ConsultationRequestId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Messages)
        .UsePropertyAccessMode(PropertyAccessMode.Field);
        
    }
}