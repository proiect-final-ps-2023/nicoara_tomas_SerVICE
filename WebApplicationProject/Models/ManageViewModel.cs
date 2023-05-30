using Microsoft.AspNetCore.Identity;

namespace WebApplicationProject.Models
{
    internal class ManageViewModel
    {
        public IdentityUser User { get; set; }
        public IList<string> UserRoles { get; set; }
        public List<IdentityRole> AvailableRoles { get; set; }
    }
}