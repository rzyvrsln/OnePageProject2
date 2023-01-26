using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePageProject2.DAL;
using OnePageProject2.Models;

namespace OnePageProject2.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PositionController : Controller
    {
        readonly AppDbContext _context;

        public PositionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await _context.Positions.ToListAsync());

        [HttpGet]
        public async Task<IActionResult> Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Position position)
        {
            if (position is null) return BadRequest();
            if (!ModelState.IsValid) return View();

            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var position = await _context.Positions.FindAsync(id);
            if (position is null) return NotFound();
            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),"Position");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if(id is null) return BadRequest();
            var posdition = await _context.Positions.FindAsync(id);
            if (posdition is null) return NotFound();
            return View(posdition);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, Position position)
        {
            if (id is null) return BadRequest();
            if (!ModelState.IsValid) return View();

            var position2 = await _context.Positions.FindAsync(id);
            if (position2 is null) return NotFound();
            position2.Name = position.Name;
            _context.Positions.Update(position2);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index),"Position");
        }

        [HttpGet]
        public async Task<IActionResult> View(int? id)
        {
            if (id is null) return BadRequest();
            var position = await _context.Positions.FindAsync(id);
            if(position is null) return NotFound();
            return View(position);
        }
    }
}
