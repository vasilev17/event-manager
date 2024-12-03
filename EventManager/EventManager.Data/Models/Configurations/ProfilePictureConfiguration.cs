using EventManager.Data.Models.Picture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManager.Data.Models.Configurations
{
    public class ProfilePictureConfiguration : IEntityTypeConfiguration<ProfilePicture>
    {
        public void Configure(EntityTypeBuilder<ProfilePicture> builder)
        {
            builder
                .HasKey(x => x.Id);
        }
    }
}
