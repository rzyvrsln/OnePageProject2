using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePageProject2.DAL;
using OnePageProject2.Models;

namespace OnePageProject2.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;

        public SettingController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await _context.Settings.ToListAsync());

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest();
            var setting = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            if (setting is null) return NotFound();

            ViewBag.Values = setting.Value;

            return View(setting);
        }

        //[HttpPost]
        //public async Task<IActionResult> Update(int? id)
        //{
        //    if (!ModelState.IsValid) return View();
        //    if (id is null) return NotFound();
        //    var setting1 = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
        //    if(setting1 is null) return NotFound();



        //    return RedirectToAction(nameof(Index), "Setting");
        //}
    }
}
