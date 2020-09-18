using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectGuard.Ef.Entities;
using ProjectGuard.Services;

namespace ProjectGuard.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly ProjectService _projectService;

        public ProjectController(DataService dataService, ProjectService projectService)
            : base (dataService)
        {
            _dataService = dataService;
            _projectService = projectService;
        }


        [HttpGet]
        public async Task<IActionResult> AddProject()
        {
            return View("AddProject");
        }

        [HttpGet]
        public async Task<IActionResult> SelectProject(int projectId)
        {
            var project = await _dataService.GetAllQuery<Project>()
                .Include(p => p.HashValues)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            var projectFileListViewModel = _projectService.GetProjectFilesViewModel(project);

            return PartialView("~/Views/Hash/ProjectFileList.cshtml", projectFileListViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(string name, string path)
        {
            await _projectService.AddProjectAsync(name, path);
            var indexViewModel = await CreateIndexViewModel();

            return View("~/Views/Main/Index.cshtml", indexViewModel);
        }
    }
}
