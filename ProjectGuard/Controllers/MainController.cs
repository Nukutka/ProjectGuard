using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectGuard.Services;

namespace ProjectGuard.Controllers
{
    [Authorize]
    public class MainController : BaseController
    {
        public MainController(DataService dataService)
            : base(dataService) { }

        public async Task<IActionResult> Index()
        {
            var indexViewModel = await CreateIndexViewModel();
            ViewData["SelectedProjectId"] = 0;

            return View(indexViewModel);
        }
    }
}
