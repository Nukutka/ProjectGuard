using Microsoft.EntityFrameworkCore;

namespace ProjectGuard.Ef
{
    public class DbContextOptionsConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ProjectGuardDbContext> dbContextOptions, string connectionString)
        {
            dbContextOptions.EnableSensitiveDataLogging();
            dbContextOptions.UseNpgsql(connectionString, x => x.MigrationsAssembly("ProjectGuard"));
        }
    }
}
