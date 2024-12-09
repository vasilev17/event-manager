using EventManager.Data.Models;
using EventManager.Data.Models.Configurations;
using EventManager.Data.Models.Picture;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new UserConfigurations().Configure(builder.Entity<User>());

            base.OnModelCreating(builder);
        }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<ProfilePicture> ProfilePictures { get; set; }
    }
}
