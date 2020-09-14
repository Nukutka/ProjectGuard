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
    public class MainController : AbpController
    {
        private readonly DataService _dataService;
        private readonly FileHashService _fileHashService;

        public MainController(DataService dataService, FileHashService fileHashService)
        {
            _dataService = dataService;
            _fileHashService = fileHashService;
        }

        public async Task<IActionResult> Index()
        {
            var indexViewModel = await CreateIndexViewModel();
            ViewData["SelectedProjectId"] = 0;

            return View(indexViewModel);
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
