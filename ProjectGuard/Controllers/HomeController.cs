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
        private readonly ProjectService _projectService;

        public HomeController(DataService dataService, ProjectService projectService)
        {
            _dataService = dataService;
            _projectService = projectService;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _dataService.GetAllListAsync<Project>();
            var indexViewModel = new IndexViewModel(projects);

            return View(indexViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddProject()
        {
            return View("AddProject");
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(string name, string path)
        {
            await _projectService.AddProjectAsync(name, path);

            var projects = await _dataService.GetAllListAsync<Project>();
            var indexViewModel = new IndexViewModel(projects);

            return View("Index", indexViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
