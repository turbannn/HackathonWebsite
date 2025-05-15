using HackathonWebsite.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackathonWebsite.DAL.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(u => u.Username).IsUnique();
            builder.Property(u => u.Username).IsRequired().HasMaxLength(40);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(70);
            builder.Property(u => u.Role).IsRequired().HasMaxLength(40);
        }
    }
}
