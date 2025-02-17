﻿using EventManager.Data.Models.Picture;
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
                .Property(b => b.StartDateTime)
                .IsRequired(false);

            builder
                .Property(b => b.EndDateTime)
                .IsRequired(false);

            builder
                .HasOne(x => x.EventPicture)
                .WithOne(x => x.Event)
                .HasForeignKey<EventPicture>(x => x.EventId);

            builder
                .HasMany(b => b.Types)
                .WithMany(b => b.Events);

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
                .HasMany(b => b.Ratings)
                .WithOne(b => b.Event)
                .HasForeignKey(b => b.EventId);

            builder
                .HasMany(e => e.Attendees)
                .WithMany(u => u.AttendedEvents)
                .UsingEntity<Attendance>(
                 j => j.HasOne(a => a.User).WithMany().HasForeignKey(a => a.UserId),
                 j => j.HasOne(a => a.Event).WithMany().HasForeignKey(a => a.EventId));

            builder
                .HasMany(e => e.AvailableTickets)
                .WithOne(e => e.Event)
                .HasForeignKey(e => e.EventId);
        }
    }
}
