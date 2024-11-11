using EventManager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
    
        public DbSet<Tag> Tags { get; set; }
    }
}
