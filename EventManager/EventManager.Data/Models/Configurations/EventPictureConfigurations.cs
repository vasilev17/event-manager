using EventManager.Data.Models.Picture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManager.Data.Models.Configurations
{
    public class EventPictureConfigurations : IEntityTypeConfiguration<EventPicture>
    {
        public void Configure(EntityTypeBuilder<EventPicture> builder)
        {
            builder
                .HasKey(x => x.Id);
        }
    }
}
