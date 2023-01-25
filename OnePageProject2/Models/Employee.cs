using OnePageProject2.Models.Base;

namespace OnePageProject2.Models
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? ImageUrl { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
