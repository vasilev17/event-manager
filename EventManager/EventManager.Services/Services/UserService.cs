using AutoMapper;
using EventManager.Common.Constants;
using EventManager.Common.Models;
using EventManager.Data.Exceptions;
using EventManager.Data.Models;
using EventManager.Data.Models.Picture;
using EventManager.Data.Repositories.Interfaces;
using EventManager.Services.Exceptions;
using EventManager.Services.Models.Picture;
using EventManager.Services.Models.User;
using EventManager.Services.Services.Interfaces;

namespace EventManager.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IVerificationRequestsRepository _verificationRequestsRepository;
        private readonly IProfilePictureRepository _profilePictureRepository;
        private readonly IEmailService _emailService;
        private readonly IJwtService _jwtService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;
        private readonly string _localTokenLocation;

        public UserService(IUserRepository userRepository,
            IVerificationRequestsRepository verificationRequestsRepository,
            IProfilePictureRepository profilePictureRepository,
            IEmailService emailService,
            IJwtService jwtService,
            ICloudinaryService cloudinaryService,
            IMapper mapper,
            string localTokenLocation)
        {
            _emailService = emailService;
            _profilePictureRepository = profilePictureRepository;
            _jwtService = jwtService;
            _userRepository = userRepository;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
            _localTokenLocation = localTokenLocation;
            _verificationRequestsRepository = verificationRequestsRepository;
        }

        public async Task<TokenModel> LoginAsync(LoginServiceModel loginServiceModel)
        {
            var loggedUser = await _userRepository.LoginAsync(loginServiceModel.UserName, loginServiceModel.Password);
            var roleNames = await _userRepository.GerUserRoleAsync(loggedUser);

            return new TokenModel(_jwtService.GenerateJwtToken(loggedUser.Id, loggedUser.UserName!, roleNames!));
        }

        public async Task<TokenModel> RegisterAsync(RegisterServiceModel userServiceModel)
        {
            var user = _mapper.Map<User>(userServiceModel);

            await _userRepository.AddAsync(user, userServiceModel.Role);

            var createdUser = await _userRepository.GetByUserNameAsync(userServiceModel.UserName);
            var roleNames = await _userRepository.GerUserRoleAsync(createdUser);

            return new TokenModel(_jwtService.GenerateJwtToken(createdUser.Id, createdUser.UserName!, roleNames!));
        }

        public async Task<GetUserServiceModel> GetOrganizerAsync(string oganizerName)
        {
            var user = await _userRepository.GetByUserNameAsync(oganizerName);

            if (user == null)
                throw new DatabaseException(string.Format(ExceptionConstants.NotFound, "User"));

            var isOrganizer = await _userRepository.IsInRoleAsync(user, Roles.Organizer.ToString());

            if(!isOrganizer)
                throw new DatabaseException(string.Format(ExceptionConstants.NotFound, "User"));

            return _mapper.Map<GetUserServiceModel>(user);
        }

        public async Task SendResendPasswordAsync(ResetPasswordServiceModel resetPasswordServiceModel)
        {
            var passwordModel = await _userRepository.GeneratePasswordTokenModelAsync(resetPasswordServiceModel.Email);

            var serviceModel = _mapper.Map<UserPasswordResetServiceModel>(passwordModel);

            var result = await _emailService.SendResetPasswordMailAsync(serviceModel);

            if (!result)
                throw new EmailSenderException(ExceptionConstants.CantSendEmail);
        }

        public async Task ResetPasswordAsync(ResetPasswordTokenServiceModel resetPasswordTokenServiceModel)
        {
            var succeeded = await _userRepository.ResetPassword(
                resetPasswordTokenServiceModel.Email,
                resetPasswordTokenServiceModel.Token,
                resetPasswordTokenServiceModel.Password);

            if (!succeeded)
                throw new ResetPasswordException(ExceptionConstants.FailedToRestorePassword);

        }

        public async Task ResendPasswordLocalAsync(ResetPasswordServiceModel resetPasswordServiceModel)
        {
            var token = await _userRepository.GeneratePasswordTokenAsync(resetPasswordServiceModel.Email);

            using (StreamWriter writer = new StreamWriter(_localTokenLocation, append: false))
                writer.WriteLine(token);
        }

        public async Task UpdateUserAsync(Guid id, UpdateUserServiceModel updateUserServiceModel)
        {
            var user = await _userRepository.GetByIdAsync(id);

            PopulateUser(user, updateUserServiceModel);

            var result = await _userRepository.EditAsync(id, user);

            if (!result)
                throw new DatabaseException(string.Format(ExceptionConstants.FailedToUpdate, "user"));

        }

        public async Task UploadProfilePictureAsync(ProfilePictureServiceModel model)
        {
            await DeleteProfilePictureAsync(model.UserId);

            await SavePicture(model);
        }

        private async Task SavePicture(ProfilePictureServiceModel model)
        {
            model.Picture.Stream.Seek(0, SeekOrigin.Begin);
            var cloudinaryResult = await _cloudinaryService.UploadPictureAsync(model.Picture);

            var profilePicture = new ProfilePicture
            {
                PublicId = cloudinaryResult.PublicId,
                ResourceType = cloudinaryResult.ResourceType,
                Url = cloudinaryResult.Url.AbsoluteUri
            };
            var user = await _userRepository.GetByIdAsync(model.UserId);

            user.ProfilePicture = profilePicture;
            profilePicture.User = user;
            profilePicture.UserId = user.Id;

            var pictureIsSaved = await _profilePictureRepository.AddAsync(profilePicture);
            if (!pictureIsSaved)
                throw new DatabaseException(string.Format(ExceptionConstants.FailedToUpload, "profile picture"));

            var userResult = await _userRepository.EditAsync(user.Id, user);
            if (!userResult)
                throw new DatabaseException(string.Format(ExceptionConstants.FailedToUpdate, "user"));


        }

        private void PopulateUser(User user, UpdateUserServiceModel updateUserServiceModel)
        {
            if (!string.IsNullOrEmpty(updateUserServiceModel.UserName))
                user.UserName = updateUserServiceModel.UserName;

            if (!string.IsNullOrEmpty(updateUserServiceModel.FirstName))
                user.FirstName = updateUserServiceModel.FirstName;

            if (!string.IsNullOrEmpty(updateUserServiceModel.LastName))
                user.LastName = updateUserServiceModel.LastName;

            if (!string.IsNullOrEmpty(updateUserServiceModel.Email))
                user.Email = updateUserServiceModel.Email;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var result = await _userRepository.DeleteAsync(id);

            if (!result)
                throw new DatabaseException(string.Format(ExceptionConstants.FailedToDelete, "user"));

        }

        public async Task DeleteProfilePictureAsync(Guid id)
        {
            var picturte = await _profilePictureRepository.GetByUserIdAsync(id);

            if (picturte != null)
            {
                var pictureServiceModel = _mapper.Map<PictureServiceModel>(picturte);
                await _cloudinaryService.DeletePictureAsync(pictureServiceModel);
            }

            await _profilePictureRepository.DeleteAsync(picturte);

            var user = await _userRepository.GetByIdAsync(id);
            user.ProfilePicture = new ProfilePicture
            {
                Url = PictureConstants.DefaultUserPicture
            };

            await _userRepository.EditAsync(id, user);
        }

        public async Task<UserServiceModel> GetUserByName(string userName)
        {
            var user = await _userRepository.GetByUserNameAsync(userName);
            var userServiceModel = _mapper.Map<UserServiceModel>(user);

            var roleNames = await _userRepository.GerUserRoleAsync(user);
            userServiceModel.Roles = roleNames.ToList();

            return userServiceModel;
        }
    }
}
