using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePageProject2.DAL;

namespace OnePageProject2.ViewComponents.Team
{
    public class TeamViewComponent:ViewComponent
    {
        readonly AppDbContext _context;

        public TeamViewComponent(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync() => View(await _context.Employees.Include(e => e.Position).ToListAsync());
    }
}
