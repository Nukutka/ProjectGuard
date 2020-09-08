using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectGuard.Ef.Entities;

namespace ProjectGuard.Ef
{
    public class ProjectGuardDbContext : AbpDbContext
    {
        public DbSet<HashValue> HashValues { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }

        public ProjectGuardDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
