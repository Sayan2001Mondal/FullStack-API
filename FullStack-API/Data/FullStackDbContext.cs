using FullStack_API.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStack_API.Data
{
    public class FullStackDbContext : DbContext
    {
        public FullStackDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
