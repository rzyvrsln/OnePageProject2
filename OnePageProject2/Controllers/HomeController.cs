using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePageProject2.DAL;

namespace OnePageProject2.Controllers
{
    public class HomeController : Controller
    {
        readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View();

    }
}
