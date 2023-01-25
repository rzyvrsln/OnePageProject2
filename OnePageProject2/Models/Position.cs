using OnePageProject2.Models.Base;

namespace OnePageProject2.Models
{
    public class Position:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set;}
    }
}
