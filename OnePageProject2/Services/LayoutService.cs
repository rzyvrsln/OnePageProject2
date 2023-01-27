using Microsoft.AspNetCore.Mvc;
using OnePageProject2.DAL;

namespace OnePageProject2.Services
{
    public class LayoutService
    {
        readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.ToDictionary(s => s.Key,s => s.Value);
        }
    }
}
