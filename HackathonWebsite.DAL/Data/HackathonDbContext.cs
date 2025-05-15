using HackathonWebsite.DAL.Configuration;
using HackathonWebsite.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace HackathonWebsite.DAL.Data
{
    public class HackathonDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<HackathonTask> HackathonTasks { get; set; } = null!;

        public HackathonDbContext(DbContextOptions options) : base(options)
        {
            Console.WriteLine("Connecting");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
