using HackathonWebsite.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackathonWebsite.DAL.Configuration
{
    public class HackathonTaskConfiguration : IEntityTypeConfiguration<HackathonTask>
    {
        public void Configure(EntityTypeBuilder<HackathonTask> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Name).IsRequired().HasMaxLength(30);
            builder.Property(h => h.Description).IsRequired().HasMaxLength(100);
        }
    }
}
