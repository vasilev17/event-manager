using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManager.Data.Models.Configurations
{
    internal class EventTypeConfigurations : IEntityTypeConfiguration<EventType>
    {
        public void Configure(EntityTypeBuilder<EventType> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Name)
                .IsUnique();
        }
    }
}
