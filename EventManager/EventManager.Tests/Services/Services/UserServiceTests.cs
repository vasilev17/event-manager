using AutoMapper;
using EventManager.Common.Constants;
using EventManager.Common.Models;
using EventManager.Data.Exceptions;
using EventManager.Data.Models;
using EventManager.Data.Repositories.Interfaces;
using EventManager.Services.Exceptions;
using EventManager.Services.Models.User;
using EventManager.Services.Services;
using EventManager.Services.Services.Interfaces;
using Moq;
using Xunit;

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
        private readonly Mock<AutoMapper.IMapper> _mockMapper;
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
            // Arrange
            var newUser = new User { Id = Guid.NewGuid(), UserName = "testuser" };
            _mockMapper.Setup(mapper => mapper.Map<User>(It.IsAny<RegisterServiceModel>())).Returns(newUser);
            _mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<Roles>())).ReturnsAsync(true);
            _mockUserRepository.Setup(repo => repo.GetByUserNameAsync(It.IsAny<string>())).ReturnsAsync(newUser);
            _mockUserRepository.Setup(repo => repo.GerUserRoleAsync(It.IsAny<User>())).ReturnsAsync(new List<string> { "User" });
            _mockJwtService.Setup(jwt => jwt.GenerateJwtToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<IList<string>>())).Returns("mockToken");

            // Act
            var token = await _userService.RegisterAsync(new RegisterServiceModel
            {
                UserName = "testuser",
                Password = "P@ssw0rd",
                Role = Roles.User,
                Email = "testuser@example.com",
                FirstName = "Test",
                LastName = "User"
            });

            // Assert
            Assert.NotNull(token);
            Assert.Equal("mockToken", token.Token);
        }

        [Fact]
        public async Task RegisterAsync_ShouldThrowException_WhenRepositoryFails()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<Roles>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
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
            // Arrange
            var existingUser = new User { Id = Guid.NewGuid(), UserName = "testuser" };
            _mockUserRepository.Setup(repo => repo.LoginAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(existingUser);
            _mockUserRepository.Setup(repo => repo.GerUserRoleAsync(It.IsAny<User>())).ReturnsAsync(new List<string> { "User" });
            _mockJwtService.Setup(jwt => jwt.GenerateJwtToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<IList<string>>())).Returns("mockToken");

            // Act
            var token = await _userService.LoginAsync(new LoginServiceModel { UserName = "testuser", Password = "P@ssw0rd" });

            // Assert
            Assert.NotNull(token);
            Assert.Equal("mockToken", token.Token);
        }

        [Fact]
        public async Task LoginAsync_ShouldThrowException_WhenCredentialsAreInvalid()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new InvalidDataException(ExceptionConstants.InvalidCredentials));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidDataException>(() => _userService.LoginAsync(new LoginServiceModel { UserName = "invaliduser", Password = "wrongpass" }));
        }

        [Fact]
        public async Task SendResendPasswordAsync_ShouldCallEmailService_WhenEmailIsValid()
        {
            // Arrange
            var resetModel = new UserPasswordResetModel
            {
                Email = "test@example.com",
                Token = "resetToken",
                FirstName = "Test",
                LastName = "User"
            };
            _mockUserRepository.Setup(repo => repo.GeneratePasswordTokenModelAsync(It.IsAny<string>())).ReturnsAsync(resetModel);
            _mockMapper.Setup(mapper => mapper.Map<UserPasswordResetServiceModel>(resetModel)).Returns(new UserPasswordResetServiceModel { Email = resetModel.Email });
            _mockEmailService.Setup(service => service.SendResetPasswordMailAsync(It.IsAny<UserPasswordResetServiceModel>())).ReturnsAsync(true);

            // Act
            await _userService.SendResendPasswordAsync(new ResetPasswordServiceModel { Email = "test@example.com" });

            // Assert
            _mockEmailService.Verify(service => service.SendResetPasswordMailAsync(It.IsAny<UserPasswordResetServiceModel>()), Times.Once);
        }

        [Fact]
        public async Task SendResendPasswordAsync_ShouldThrowException_WhenEmailServiceFails()
        {
            // Arrange
            var resetModel = new UserPasswordResetModel
            {
                Email = "test@example.com",
                Token = "resetToken",
                FirstName = "Test",
                LastName = "User"
            };
            _mockUserRepository.Setup(repo => repo.GeneratePasswordTokenModelAsync(It.IsAny<string>())).ReturnsAsync(resetModel);
            _mockEmailService.Setup(service => service.SendResetPasswordMailAsync(It.IsAny<UserPasswordResetServiceModel>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<EmailSenderException>(() => _userService.SendResendPasswordAsync(new ResetPasswordServiceModel { Email = "test@example.com" }));
        }

        [Fact]
        public async Task ResetPasswordAsync_ShouldComplete_WhenTokenAndPasswordAreValid()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.ResetPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            await _userService.ResetPasswordAsync(new ResetPasswordTokenServiceModel
            {
                Email = "test@example.com",
                Token = "validToken",
                Password = "NewPassword123",
                PasswordConfirm = "NewPassword123"
            });

            // Assert
            _mockUserRepository.Verify(repo => repo.ResetPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ResetPasswordAsync_ShouldThrowException_WhenTokenIsInvalid()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.ResetPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<ResetPasswordException>(() => _userService.ResetPasswordAsync(new ResetPasswordTokenServiceModel
            {
                Email = "test@example.com",
                Token = "invalidToken",
                Password = "NewPassword123",
                PasswordConfirm = "NewPassword123"
            }));
        }

        [Fact]
        public async Task ResetPasswordAsync_ShouldThrowException_WhenPasswordIsIncorrect()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.ResetPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<ResetPasswordException>(() => _userService.ResetPasswordAsync(new ResetPasswordTokenServiceModel
            {
                Email = "test@example.com",
                Token = "validToken",
                Password = "WrongPassword123",
                PasswordConfirm = "WrongPassword123"
            }));
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldCallRepository_WhenUserExists()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            // Act
            await _userService.DeleteUserAsync(Guid.NewGuid());

            // Assert
            _mockUserRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldThrowException_WhenDeletionFails()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<DatabaseException>(() => _userService.DeleteUserAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task ResetPasswordAsync_ShouldThrowException_WhenPasswordsDoNotMatch()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ResetPasswordException>(() => _userService.ResetPasswordAsync(new ResetPasswordTokenServiceModel
            {
                Email = "test@example.com",
                Token = "validToken",
                Password = "NewPassword123",
                PasswordConfirm = "MismatchPassword123"
            }));
        }
    }
}