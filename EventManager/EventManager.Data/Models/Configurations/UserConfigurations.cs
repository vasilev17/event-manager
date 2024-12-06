using EventManager.Data.Models.Picture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManager.Data.Models.Configurations
{
    internal class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(b => b.FirstName)
                .IsRequired();

            builder
                .Property(b => b.LastName)
                .IsRequired();

            builder
                .HasIndex(u => u.Email)
                .IsUnique();

            builder
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users);

            builder
                .HasOne(x => x.ProfilePicture)
                .WithOne(x => x.User)
                .HasForeignKey<ProfilePicture>(x => x.UserId);

            builder
                .HasMany(x => x.Events)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
