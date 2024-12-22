using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using EventManager.Data.Models;
using EventManager.Data.Repositories;
using EventManager.Data;

namespace EventManager.Tests.Data.Repositories
{
    public class RepositoryTests
    {
        private readonly EventRepository _eventRepository;
        private readonly ApplicationDbContext _context;

        public RepositoryTests()
        {
            // Configure in-memory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _eventRepository = new EventRepository(_context);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEntities()
        {
            // Arrange
            _context.Events.AddRange(new List<Event>
            {
                new Event
                {
                    Id = Guid.NewGuid(),
                    Name = "Event 1",
                    Address = "123 Test Street",
                    User = new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" }
                },
                new Event
                {
                    Id = Guid.NewGuid(),
                    Name = "Event 2",
                    Address = "456 Test Avenue",
                    User = new User { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith" }
                }
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _eventRepository.GetAllEventsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEntity()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            _context.Events.Add(new Event
            {
                Id = eventId,
                Name = "Event 1",
                Address = "123 Test Street",
                User = new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" }
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _eventRepository.GetByIdAsync(eventId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(eventId, result.Id);
        }

        [Fact]
        public async Task DeleteAsync_ValidEntity_RemovesEntityFromDatabase()
        {
            // Arrange
            var existingEvent = new Event
            {
                Id = Guid.NewGuid(),
                Name = "Test Event",
                Description = "Test Description",
                StartDateTime = DateTime.Now.AddDays(1),
                Address = "123 Test Street",
                User = new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" }
            };
            _context.Events.Add(existingEvent);
            await _context.SaveChangesAsync();

            // Act
            await _eventRepository.DeleteAsync(existingEvent);

            // Assert
            var result = await _context.Events.FindAsync(existingEvent.Id);
            Assert.Null(result);
        }
    }
}
