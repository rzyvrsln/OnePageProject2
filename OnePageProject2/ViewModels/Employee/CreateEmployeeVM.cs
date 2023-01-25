using OnePageProject2.Models;

namespace OnePageProject2
{
    public class CreateEmployeeVm
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public IFormFile Image { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
        public int PositionId { get; set; }
    }
}
