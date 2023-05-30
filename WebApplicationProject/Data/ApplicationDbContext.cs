using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplicationProject.Models;

namespace WebApplicationProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApplicationProject.Models.Post> Post { get; set; }
        public DbSet<WebApplicationProject.Models.User> User { get; set; }
    }
}