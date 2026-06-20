using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namaa.Domain.Identity;
using Namaa.Infrastructure.Identity;

namespace Namaa.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
    builder.ToTable("Users");
    builder.Property(u => u.FirstName).HasMaxLength(150).IsRequired();
    builder.Property(u => u.StatusReason).HasMaxLength(500).IsRequired(false);
    builder.Property(u => u.LastName).HasMaxLength(150).IsRequired(false);
    builder.Property(u => u.ProfileImageUrl).HasMaxLength(500);
    builder.Property(u => u.ResetCode).HasMaxLength(256);
    builder.Property(u => u.Status).HasConversion<string>().IsRequired();

    }
}