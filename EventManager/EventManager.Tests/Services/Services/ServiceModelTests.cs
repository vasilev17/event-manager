using EventManager.Common.Constants;
using EventManager.Services.Models.Event;
using EventManager.Services.Models.Picture;
using EventManager.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Tests.Services.Services
{
    public class ServiceModelTests
    {
        [Fact]
        public void CreateEventServiceModel_ValidInitialization_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var eventTypes = new HashSet<EventTypes> { EventTypes.Conference };

            // Act
            var model = new CreateEventServiceModel
            {
                Id = Guid.NewGuid(),
                Name = "Test Event",
                Description = "Test Description",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(2),
                Types = eventTypes,
                Address = "123 Test Street",
                IsActivity = false,
                IsThirdParty = true
            };

            // Assert
            Assert.NotNull(model);
            Assert.Equal("Test Event", model.Name);
            Assert.Equal(eventTypes, model.Types);
        }

        [Fact]
        public void EventFilterServiceModel_ValidInitialization_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var startDateTime = DateTime.Now.AddDays(-1);
            var endDateTime = DateTime.Now;

            // Act
            var model = new EventFilterServiceModel
            {
                Name = "Conference",
                StartDateTime = startDateTime,
                EndDateTime = endDateTime
            };

            // Assert
            Assert.NotNull(model);
            Assert.Equal("Conference", model.Name);
            Assert.Equal(startDateTime, model.StartDateTime);
            Assert.Equal(endDateTime, model.EndDateTime);
        }

        [Fact]
        public void UploadPictureServiceModel_ValidInitialization_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var stream = new MemoryStream(new byte[] { 1, 2, 3 });

            // Act
            var model = new UploadPictureServiceModel
            {
                FileName = "test.jpg",
                Stream = stream
            };

            // Assert
            Assert.NotNull(model);
            Assert.Equal("test.jpg", model.FileName);
            Assert.Equal(stream, model.Stream);
        }

        [Fact]
        public void UserServiceModel_ValidInitialization_ShouldSetPropertiesCorrectly()
        {
            // Act
            var model = new UserServiceModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            // Assert
            Assert.NotNull(model);
            Assert.Equal("John", model.FirstName);
            Assert.Equal("Doe", model.LastName);
            Assert.Equal("john.doe@example.com", model.Email);
        }
    }
}
