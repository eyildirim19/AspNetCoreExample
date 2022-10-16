using Microsoft.AspNetCore.Identity;

namespace AspNetCoreExample.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
