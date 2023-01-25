using OnePageProject2.Models.Base;

namespace OnePageProject2.Models
{
    public class Position:BaseEntity
    {
        public string name { get; set; }
        public ICollection<Position> positions { get; set; }
    }
}
