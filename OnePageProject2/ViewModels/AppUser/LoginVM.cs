using System.ComponentModel.DataAnnotations;

namespace OnePageProject2.ViewModels.AppUser
{
    public class LoginVM
    {
        [Required]
        public string UserName { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsParsistance { get; set; }
    }
}
