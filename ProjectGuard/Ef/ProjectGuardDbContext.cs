using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectGuard.Ef.Entities;
using ProjectGuard.Ef.Extensions;
using ProjectGuard.Ef.Seeds;

namespace ProjectGuard.Ef
{
    public class ProjectGuardDbContext : AbpDbContext
    {
        public DbSet<HashValue> HashValues { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Verification> Verifications { get; set; }
        public DbSet<FileCheckResult> FileCheckResults { get; set; }

        public ProjectGuardDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUser().AddUsers();

            base.OnModelCreating(modelBuilder);
        }
    }
}
