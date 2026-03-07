using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Identity;
using Namaa.Infrastructure.Identity;

namespace Namaa.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(rt => rt.Id);
        builder.Property(rt => rt.Token).HasMaxLength(200).IsRequired();
        builder.HasIndex(rt => rt.Token).IsUnique();
        builder.HasOne<AppUser>().WithMany().HasForeignKey(rt => rt.UserId);

        builder.Property(rt => rt.UserId).IsRequired();
        builder.Property(rt => rt.ExpiresOnUtc).IsRequired();
    }
}