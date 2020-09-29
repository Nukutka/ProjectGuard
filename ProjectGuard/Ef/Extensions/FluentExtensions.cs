using Microsoft.EntityFrameworkCore;
using ProjectGuard.Ef.Entities;

namespace ProjectGuard.Ef.Extensions
{
    public static class FluentExtensions
    {
        public static ModelBuilder ConfigureUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user => {
                user.HasIndex(u => u.Login).IsUnique();
            });

            return modelBuilder;
        }
    }
}
