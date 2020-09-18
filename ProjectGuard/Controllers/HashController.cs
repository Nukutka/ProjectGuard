﻿using System.Threading.Tasks;
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
            await _fileHashService.SetControlHashesAsync(projectId);
            var projectFileListViewModel = await _projectService.GetProjectFilesViewModel(projectId);

            return PartialView("~/Views/Hash/ProjectFileList.cshtml", projectFileListViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CheckFiles(int[] hashValueIds, int projectId)
        {
            await _fileHashService.CheckFileHashesAsync(projectId);
            var projectFileListViewModel = await _projectService.GetProjectFilesViewModel(projectId);

            return PartialView("~/Views/Hash/ProjectFileList.cshtml", projectFileListViewModel);
        }

        [HttpPost]
        public async Task ChangeNeedHash(int fileId, bool needHash)
        {
            await _fileHashService.ChangeFileNeedHash(fileId, needHash);
        }
    }
}
