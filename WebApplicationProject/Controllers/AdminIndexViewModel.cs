using Microsoft.AspNetCore.Identity;

namespace WebApplicationProject.Controllers
{
    internal class AdminIndexViewModel
    {
        public List<IdentityUser> Users { get; set; }
        public string SearchName { get; set; }
        public List<IdentityRole> AvailableRoles { get; set; }
    }
}