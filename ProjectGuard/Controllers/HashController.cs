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

        public HashController(DataService dataService, FileHashService fileHashService)
            : base(dataService)
        {
            _fileHashService = fileHashService;
        }

        [HttpPost]
        public async Task<IActionResult> HashFiles(int[] hashValueIds, int projectId)
        {
            await _fileHashService.SetControlHashesAsync(hashValueIds, projectId);

            var project = await _dataService.GetAllQuery<Project>()
                .Include(p => p.HashValues)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            return PartialView("~/Views/Hash/ProjectFileList.cshtml", project);
        }

        [HttpPost]
        public async Task<IActionResult> CheckFiles(int projectId)
        {
            await _fileHashService.CheckFileHashesAsync(projectId);

            // TODO: result
            var project = await _dataService.GetAllQuery<Project>()
                .Include(p => p.HashValues)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            return PartialView("~/Views/Hash/ProjectFileList.cshtml", project);
        }
    }
}
