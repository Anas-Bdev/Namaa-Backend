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
    builder.Property(u => u.FullName).HasMaxLength(150);
    builder.Property(u => u.ProfileImageUrl).HasMaxLength(500);
    builder.Property(u => u.ResetCode).HasMaxLength(256);
    builder.Property(u => u.Status).HasConversion<string>().IsRequired();
    }
}