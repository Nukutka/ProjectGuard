using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ProjectGuard.Services.Utils;

namespace ProjectGuard.Ef
{
    public class ProjecGuardDbContextFactory : IDesignTimeDbContextFactory<ProjectGuardDbContext>
    {
        public ProjectGuardDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ProjectGuardDbContext>();
            var configuration = AppSettingsManager.Get();
            DbContextOptionsConfigurer.Configure(builder, configuration.GetConnectionString("PostgreSQL"));

            return new ProjectGuardDbContext(builder.Options);
        }
    }
}
