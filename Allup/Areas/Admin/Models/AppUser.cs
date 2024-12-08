using Microsoft.AspNetCore.Identity;

namespace Allup.Areas.Admin.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
