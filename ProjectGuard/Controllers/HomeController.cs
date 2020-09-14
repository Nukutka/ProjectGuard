using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectGuard.Ef.Entities;
using ProjectGuard.Models;
using ProjectGuard.Services;

namespace ProjectGuard.Controllers
{
    public class HomeController : AbpController
    {
        private readonly DataService _dataService;
        private readonly ProjectService _projectService;
        private readonly FileHashService _fileHashService;

        public HomeController(DataService dataService, ProjectService projectService, FileHashService fileHashService)
        {
            _dataService = dataService;
            _projectService = projectService;
            _fileHashService = fileHashService;
        }

        public async Task<IActionResult> Index()
        {
            var indexViewModel = await CreateIndexViewModel();
            ViewData["SelectedProjectId"] = 0;

            return View(indexViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddProject()
        {
            return View("AddProject");
        }

        [HttpGet]
        public async Task<IActionResult> SelectProject(int projectId)
        {
            var indexViewModel = await CreateIndexViewModel();
            ViewData["SelectedProjectId"] = projectId;

            return View("Index", indexViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(string name, string path)
        {
            await _projectService.AddProjectAsync(name, path);
            var indexViewModel = await CreateIndexViewModel();

            ViewData["SelectedProjectId"] = 0;

            return View("Index", indexViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> HashFiles(int[] hashValueIds, int projectId)
        {
            await _fileHashService.SetControlHashesAsync(hashValueIds, projectId);

            var indexViewModel = await CreateIndexViewModel();
            ViewData["SelectedProjectId"] = 0;
            return View("Index", indexViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CheckFiles(int[] hashValueIds, int projectId)
        {
            await _fileHashService.CheckFileHashesAsync(projectId);

            var indexViewModel = await CreateIndexViewModel();
            ViewData["SelectedProjectId"] = 0;

            // TODO: result
            return View("Index", indexViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<IndexViewModel> CreateIndexViewModel()
        {
            var projects = await _dataService.GetAllQuery<Project>()
               .Include(p => p.HashValues)
               .ToListAsync();
            var indexViewModel = new IndexViewModel(projects);

            return indexViewModel;
        }
    }
}
