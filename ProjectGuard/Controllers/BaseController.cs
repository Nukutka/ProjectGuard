using System.Diagnostics;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectGuard.Ef.Entities;
using ProjectGuard.Models;
using ProjectGuard.Services;

namespace ProjectGuard.Controllers
{
    public class BaseController : AbpController
    {
        protected DataService _dataService;

        public BaseController(DataService dataService)
        {
            _dataService = dataService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // мб потом уберу костыль
        protected async Task<IndexViewModel> CreateIndexViewModel()
        {
            var projects = await _dataService.GetAllQuery<Project>()
               .Include(p => p.HashValues)
               .ToListAsync();
            var indexViewModel = new IndexViewModel(projects);

            return indexViewModel;
        }
    }
}
