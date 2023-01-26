using Microsoft.AspNetCore.Mvc;
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
            ViewBag.Positions = _context.Positions;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM createEmployeeVM)
        {
            if (!ModelState.IsValid) return View();

            if(!(_context.Employees.Any(e=>e.PositionId == createEmployeeVM.PositionId)))
            {
                ModelState.AddModelError("PositionId", "secdiyiniz Position movcud deyil.");
                return View();
            }

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
    }
}
