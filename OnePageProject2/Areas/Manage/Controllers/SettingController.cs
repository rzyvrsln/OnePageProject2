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

        public SettingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await _context.Settings.ToListAsync());

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest();
            var setting = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            if (setting is null) return NotFound();

            return View(setting);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, Setting setting)
        {
            if (!ModelState.IsValid) return View();
            if (id is null) return NotFound();
            if (setting is null) return BadRequest();
            var setting1 = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);

            if(!(setting.Key.Contains("Logo")))
            {
                setting1.Value = setting.Value;
                _context.Settings.Update(setting1);
                await _context.Settings.SingleAsync();
            }

            return RedirectToAction(nameof(Index), "Setting");
        }
    }
}
