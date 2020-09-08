using System.Diagnostics;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using ProjectGuard.Ef.Entities;
using ProjectGuard.Models;
using ProjectGuard.Services;

namespace ProjectGuard.Controllers
{
    public class HomeController : AbpController
    {
        private readonly DataService _dataService;

        public HomeController(DataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _dataService.GetAllListAsync<Project>();
            var indexViewModel = new IndexViewModel(projects);

            return View(indexViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
