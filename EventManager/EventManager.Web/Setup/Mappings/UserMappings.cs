﻿using AutoMapper;
using EventManager.Data.Models;
using EventManager.Data.Models.Picture;
using EventManager.Services.Models.Picture;
using EventManager.Services.Models.User;
using EventManager.Web.Models.User;

namespace EventManager.Web.Setup.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<RegisterWebModel, RegisterServiceModel>();
            CreateMap<LoginWebModel, LoginServiceModel>();
            CreateMap<ResetPasswordWebModel, ResetPasswordServiceModel>();
            CreateMap<ResetPasswordTokenWebModel, ResetPasswordTokenServiceModel>();
            CreateMap<UpdateUserWebModel, UpdateUserServiceModel>();
            CreateMap<VerificationRequestWebModel, VerificationRequestServiceModel>();
            CreateMap<UserWebModel, UserServiceModel>();

            CreateMap<UserPasswordResetModel, UserPasswordResetServiceModel>();
            CreateMap<ProfilePicture, PictureServiceModel>();
            CreateMap<User, GetUserServiceModel>();
            CreateMap<VerificationRequestServiceModel, VerificationRequest>();
            CreateMap<VerificationRequest, VerificationRequestInfo>();

            CreateMap<User, UserServiceModel>()
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.ProfilePicture.Url));

            CreateMap<RegisterServiceModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
        }
    }
}
