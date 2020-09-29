using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectGuard.Models;
using ProjectGuard.Services;

namespace ProjectGuard.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> HashFiles(int projectId)
        {
            await _fileHashService.SetControlHashesAsync(projectId);
            var projectFileListViewModel = await _projectService.GetProjectFilesViewModel(projectId);

            return PartialView("~/Views/Hash/ProjectFileList.cshtml", projectFileListViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CheckFiles(int projectId)
        {
            var verification = await _fileHashService.CheckFileHashesAsync(projectId);
           
            return PartialView("~/Views/Hash/Result.cshtml", verification);
        }

        [HttpPost]
        public async Task ChangeNeedHash(int fileId, bool needHash)
        {
            await _fileHashService.ChangeFileNeedHash(fileId, needHash);
        }

        [HttpPost]
        public async Task ChangeNeedHashs([FromBody] IEnumerable<FileNeedHash> model)
        {
            await _fileHashService.ChangeFilesNeedHash(model.ToList());
        }
    }
}
