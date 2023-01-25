using Microsoft.AspNetCore.Mvc;

namespace OnePageProject2.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PositionController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index() => View();

        [HttpGet]
        public async Task<IActionResult> Create() => View();
    }
}
