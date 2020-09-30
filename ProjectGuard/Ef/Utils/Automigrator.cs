using Microsoft.EntityFrameworkCore;

namespace ProjectGuard.Ef.Utils
{
    public static class Automigrator
    {
        public static void Migrate(string connectionString)
        {
            using (var context = CreateDbContext(connectionString))
            {
                context.Database.Migrate();
            }
        }

        private static ProjectGuardDbContext CreateDbContext(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<ProjectGuardDbContext>();

            DbContextOptionsConfigurer.Configure(builder, connectionString);

            return new ProjectGuardDbContext(builder.Options);
        }
    }
}
