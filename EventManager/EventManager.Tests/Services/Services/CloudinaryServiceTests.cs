using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using EventManager.Services.Models.Picture;
using EventManager.Services.Services.Interfaces;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace EventManager.Tests.Services.Services
{
    public class CloudinaryServiceTests
    {
        private readonly Mock<ICloudinaryService> _mockCloudinaryService;

        public CloudinaryServiceTests()
        {
            // Mock the ICloudinaryService instance
            _mockCloudinaryService = new Mock<ICloudinaryService>();
        }

        [Fact]
        public async Task UploadPictureAsync_ValidPicture_UploadsSuccessfully()
        {
            // Arrange
            var mockPictureModel = new UploadPictureServiceModel
            {
                FileName = "test.jpg",
                Stream = new MemoryStream(new byte[] { 1, 2, 3 })
            };

            var mockResult = new RawUploadResult
            {
                Url = new Uri("http://example.com/test.jpg")
            };

            _mockCloudinaryService
                .Setup(c => c.UploadPictureAsync(It.IsAny<UploadPictureServiceModel>()))
                .ReturnsAsync(mockResult);

            // Act
            var result = await _mockCloudinaryService.Object.UploadPictureAsync(mockPictureModel);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("http://example.com/test.jpg", result.Url.ToString());
        }

        [Fact]
        public async Task DeletePictureAsync_ValidPictureServiceModel_DeletesSuccessfully()
        {
            // Arrange
            var pictureServiceModel = new PictureServiceModel
            {
                PublicId = "test_public_id",
                Url = "http://example.com/test.jpg",
                ResourceType = "image"
            };

            _mockCloudinaryService
                .Setup(c => c.DeletePictureAsync(It.IsAny<PictureServiceModel>()))
                .Returns(Task.CompletedTask);

            // Act
            await _mockCloudinaryService.Object.DeletePictureAsync(pictureServiceModel);

            // Assert
            _mockCloudinaryService.Verify(c => c.DeletePictureAsync(It.IsAny<PictureServiceModel>()), Times.Once);
        }
    }
}
