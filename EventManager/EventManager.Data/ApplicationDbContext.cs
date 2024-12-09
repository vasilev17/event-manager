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
            new EventConfigurations().Configure(builder.Entity<Event>());
            new EventPictureConfigurations().Configure(builder.Entity<EventPicture>());
            new EventTypeConfigurations().Configure(builder.Entity<EventType>());

            base.OnModelCreating(builder);
        }

        public DbSet<EventType> Types { get; set; }

        public DbSet<ProfilePicture> ProfilePictures { get; set; }

        public DbSet<EventPicture> EventPictures { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Rating> Ratings { get; set; }
    }
}
