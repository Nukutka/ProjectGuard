using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectGuard.Ef.Entities;
using ProjectGuard.Services;

namespace ProjectGuard.Controllers
{
    public class HashController : BaseController
    {
        private readonly FileHashService _fileHashService;
        private readonly ProjectService _projectService;

        public HashController(DataService dataService, FileHashService fileHashService, ProjectService projectService)
            : base(dataService)
        {
            _fileHashService = fileHashService;
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> HashFiles(int[] hashValueIds, int projectId)
        {
            await _fileHashService.SetControlHashesAsync(hashValueIds, projectId);

            var project = await _dataService.GetAllQuery<Project>()
                .Include(p => p.HashValues)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            var projectFileListViewModel = _projectService.GetProjectFilesViewModel(project);

            return PartialView("~/Views/Hash/ProjectFileList.cshtml", projectFileListViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CheckFiles(int[] hashValueIds, int projectId)
        {
            await _fileHashService.CheckFileHashesAsync(hashValueIds, projectId);

            // TODO: result
            var project = await _dataService.GetAllQuery<Project>()
                .Include(p => p.HashValues)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            var projectFileListViewModel = _projectService.GetProjectFilesViewModel(project);

            return PartialView("~/Views/Hash/ProjectFileList.cshtml", projectFileListViewModel);
        }
    }
}
