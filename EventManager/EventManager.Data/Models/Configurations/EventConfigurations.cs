using EventManager.Data.Models.Picture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManager.Data.Models.Configurations
{
    internal class EventConfigurations : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder
                .HasKey(b => b.Id);

            builder
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(b => b.Description)
                .HasMaxLength(1000);

            builder
                .Property(b => b.StartDateTime);

            builder
                .Property(b => b.EndDateTime);

            builder
                .HasOne(x => x.EventPicture)
                .WithOne(x => x.Event)
                .HasForeignKey<EventPicture>(x => x.EventId);

            builder
                .HasMany(x => x.Types)
                .WithMany(x => x.Events);

            builder
                .Property(b => b.Webpage)
                .HasMaxLength(30);

            builder
                .Property(b => b.Address)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(b => b.IsActivity)
                .IsRequired();

            builder
                .Property(b => b.IsThirdParty)
                .IsRequired();

            builder
                .Property(b => b.Rating)
                .IsRequired();

        }
    }
}
