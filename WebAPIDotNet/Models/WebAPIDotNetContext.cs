using Microsoft.EntityFrameworkCore;

namespace WebAPIDotNet.Models
{
    public class WebAPIDotNetContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public WebAPIDotNetContext(DbContextOptions<WebAPIDotNetContext> options) : base(options)
        {
            
        }
    }
}
