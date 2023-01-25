using Microsoft.AspNetCore.Mvc;

namespace OnePageProject2.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index() => View();
    }
}
