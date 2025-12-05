using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPIDotNet.Models
{
    public class WebAPIDotNetContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }

        public WebAPIDotNetContext(DbContextOptions<WebAPIDotNetContext> options) : base(options)
        {

        }
    }
}
