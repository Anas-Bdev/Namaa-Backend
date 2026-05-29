using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Consultation;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class ConsultationRequestConfiguration : IEntityTypeConfiguration<ConsultationRequest>
{
    public void Configure(EntityTypeBuilder<ConsultationRequest> builder)
    {
        builder.ToTable("ConsultationRequests");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();
        builder.Property(c => c.Title).HasMaxLength(200);
        builder.Property(c => c.Description).HasMaxLength(1000);
        builder.Property(c => c.ImageUrl).HasMaxLength(500);
        builder.Property(c => c.Location).HasMaxLength(250);
        builder.Property(c => c.Status).HasConversion<string>();

        builder.HasMany(c => c.Responses)
            .WithOne(r => r.Request)
            .HasForeignKey(r => r.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(c => c.Responses)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}