using Microsoft.AspNetCore.Identity;
using OnePageProject2.Models.Base;

namespace OnePageProject2.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
