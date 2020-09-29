using Microsoft.EntityFrameworkCore;
using ProjectGuard.Ef.Entities;
using ProjectGuard.Ef.Extensions;
using ProjectGuard.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectGuard.Ef.Seeds
{
    public static class UserSeed
    {

        public static List<User> AddUsers(this ModelBuilder modelBuilder)
        {
            var id = 1;

            var users = new List<User>
            {
                new User("Aadmin", StreebogProvider.GetHashCode("Aadmin")) { Id = id++ }
            };

            modelBuilder.Entity<User>(m => m.HasData(users));
            modelBuilder.SeqStartAt<User>(id);

            return users;
        }
    }
}
