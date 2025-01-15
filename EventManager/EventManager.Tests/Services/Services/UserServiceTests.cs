using AutoMapper;
using EventManager.Common.Constants;
using EventManager.Common.Exceptions;
using EventManager.Common.Models;
using EventManager.Data.Models;
using EventManager.Data.Repositories.Interfaces;
using EventManager.Services.Models.User;
using EventManager.Services.Services;
using EventManager.Services.Services.Interfaces;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EventManager.Tests.Services.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IVerificationRequestsRepository> _mockVerificationRequestsRepository;
        private readonly Mock<IProfilePictureRepository> _mockProfilePictureRepository;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<IJwtService> _mockJwtService;
        private readonly Mock<ICloudinaryService> _mockCloudinaryService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly string mockLocalTokenLocation = "test_token_location";
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockVerificationRequestsRepository = new Mock<IVerificationRequestsRepository>();
            _mockProfilePictureRepository = new Mock<IProfilePictureRepository>();
            _mockEmailService = new Mock<IEmailService>();
            _mockJwtService = new Mock<IJwtService>();
            _mockCloudinaryService = new Mock<ICloudinaryService>();
            _mockMapper = new Mock<IMapper>();

            _userService = new UserService(
                _mockUserRepository.Object,
                _mockVerificationRequestsRepository.Object,
                _mockProfilePictureRepository.Object,
                _mockEmailService.Object,
                _mockJwtService.Object,
                _mockCloudinaryService.Object,
                _mockMapper.Object,
                mockLocalTokenLocation);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnToken_WhenUserIsValid()
        {
            var newUser = new User { Id = Guid.NewGuid(), UserName = "testuser" };
            _mockMapper.Setup(mapper => mapper.Map<User>(It.IsAny<RegisterServiceModel>())).Returns(newUser);
            _mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<Roles>())).ReturnsAsync(true);
            _mockUserRepository.Setup(repo => repo.GetByUserNameAsync(It.IsAny<string>())).ReturnsAsync(newUser);
            _mockUserRepository.Setup(repo => repo.GerUserRoleAsync(It.IsAny<User>())).ReturnsAsync(new List<string> { "User" });
            _mockJwtService.Setup(jwt => jwt.GenerateJwtToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<IList<string>>())).Returns("mockToken");

            var token = await _userService.RegisterAsync(new RegisterServiceModel
            {
                UserName = "testuser",
                Password = "P@ssw0rd",
                Role = Roles.User,
                Email = "testuser@example.com",
                FirstName = "Test",
                LastName = "User"
            });

            Assert.NotNull(token);
            Assert.Equal("mockToken", token.Token);
        }

        [Fact]
        public async Task RegisterAsync_ShouldThrowException_WhenRepositoryFails()
        {
            _mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<Roles>()))
                .ThrowsAsync(new Exception("Database error"));

            await Assert.ThrowsAsync<Exception>(() =>
                _userService.RegisterAsync(new RegisterServiceModel
                {
                    UserName = "testuser",
                    Password = "P@ssw0rd",
                    Email = "testuser@example.com",
                    FirstName = "Test",
                    LastName = "User",
                    Role = Roles.User
                }));
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
        {
            var existingUser = new User { Id = Guid.NewGuid(), UserName = "testuser" };
            _mockUserRepository.Setup(repo => repo.LoginAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(existingUser);
            _mockUserRepository.Setup(repo => repo.GerUserRoleAsync(It.IsAny<User>())).ReturnsAsync(new List<string> { "User" });
            _mockJwtService.Setup(jwt => jwt.GenerateJwtToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<IList<string>>())).Returns("mockToken");

            var token = await _userService.LoginAsync(new LoginServiceModel { UserName = "testuser", Password = "P@ssw0rd" });

            Assert.NotNull(token);
            Assert.Equal("mockToken", token.Token);
        }

        [Fact]
        public async Task LoginAsync_ShouldThrowException_WhenCredentialsAreInvalid()
        {
            _mockUserRepository.Setup(repo => repo.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new InvalidDataException("Invalid credentials"));

            await Assert.ThrowsAsync<InvalidDataException>(() => _userService.LoginAsync(new LoginServiceModel { UserName = "invaliduser", Password = "wrongpass" }));
        }
    }
}
