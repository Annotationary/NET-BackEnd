using Jso.Annotationary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jso.Annotationary.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.AvatarUrl)
                .HasMaxLength(255);

            builder.Property(u => u.CoverImageUrl)
                .HasMaxLength(255);

            builder.Property(u => u.Specialization)
                .HasMaxLength(100);

            builder.Property(u => u.UserStatus)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(u => u.UserRole)
                .HasConversion<string>()
                .HasMaxLength(50);
        }
    }
}
