using Abp.Application.Services;
using Microsoft.EntityFrameworkCore;
using ProjectGuard.Ef.Entities;
using ProjectGuard.Models.Requests;
using ProjectGuard.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectGuard.Services
{
    public class AuthService : ApplicationService
    {
        private readonly DataService _dataService;

        public AuthService(DataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<User> AuthAsync(AuthModel model)
        {
            var hashPassword = StreebogProvider.GetHashCode(model.Password);

            var user = await _dataService.GetAllQuery<User>()
                .FirstOrDefaultAsync(u => u.Login == model.Login && u.HashPassword == hashPassword);

            return user;
        }
    }
}
