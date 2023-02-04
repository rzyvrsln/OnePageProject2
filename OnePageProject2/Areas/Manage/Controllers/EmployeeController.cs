using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnePageProject2.DAL;
using OnePageProject2.Models;
using OnePageProject2.ViewModels.Employee;

namespace OnePageProject2.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class EmployeeController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await _context.Employees.Include(e=>e.Position).ToListAsync());

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = new SelectList(_context.Positions,nameof(Position.Id), nameof(Position.Name));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM createEmployeeVM)
        {
            if (!ModelState.IsValid) return View();

            IFormFile file = createEmployeeVM.Image;

            if (!file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "Sekil deyil");
                return View();
            }
            if(file.Length > 200 * 1024)
            {
                ModelState.AddModelError("Image", "sekilin gecmi 2kb dan artiq ola bilmez");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + file.FileName;

            using(var stream = new FileStream(Path.Combine(_env.WebRootPath, "assets", "img", "employee", fileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Employee employee = new Employee
            {
                Name = createEmployeeVM.Name,
                Surname = createEmployeeVM.Surname,
                Facebook = createEmployeeVM.Facebook,
                Instagram = createEmployeeVM.Instagram,
                Twitter = createEmployeeVM.Twitter,
                PositionId = createEmployeeVM.PositionId,
                ImageUrl = fileName
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), "Employee");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(!ModelState.IsValid) return View();
            if (id is null) return BadRequest();
            var employee = await _context.Employees.FindAsync(id);

            _context.Employees.Remove(employee);

            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index),"Employee");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if(!ModelState.IsValid || id is null) return BadRequest();
            var employee = await _context.Employees.FindAsync(id);
            if(employee is null) return NotFound();

            UpdateEmployeeVM employeeVM = new UpdateEmployeeVM
            {
                Name = employee.Name,
                Surname = employee.Surname,
                Twitter = employee.Twitter,
                Instagram = employee.Instagram,
                Facebook = employee.Facebook,
                PositionId = employee.PositionId,
            };

            ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
            ViewBag.ImageUrl = employee.ImageUrl;

            return View(employeeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id,UpdateEmployeeVM employeeVM)
        {
            if(id is null) return BadRequest();
            var employee = await _context.Employees.FindAsync(id);
            if (!ModelState.IsValid) { ViewBag.ImageUrl = employee.ImageUrl; ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name)); ; return View(); }
            if(employee is null) return NotFound();

            IFormFile file = employeeVM.Image;

            if (!file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", $"{file.FileName} fayli sekil deyil.");
                return View();
            }
            if(file.Length > 200 * 1024)
            {
                ModelState.AddModelError("Image", $"{file.FileName} faylinin uzunlugu 2kbdan cox ola bilmaz");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + file.FileName;

            using(var stream = new FileStream(Path.Combine(_env.WebRootPath, "assets", "img", "employee", fileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            employee.Name = employeeVM.Name;
            employee.Surname = employeeVM.Surname;
            employee.Twitter = employeeVM.Twitter;
            employee.Instagram = employeeVM.Instagram;
            employee.Facebook = employeeVM.Facebook;
            employee.ImageUrl = fileName;
            employee.PositionId = employeeVM.PositionId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> View(int? id)
        {
            var employee = await _context.Employees.Include(e => e.Position).FirstOrDefaultAsync(e=>e.Id==id);
            return View(employee);
        }
    }
}
