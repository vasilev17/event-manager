using AutoMapper;
using EventManager.Data;
using EventManager.Data.Models;
using EventManager.Data.Repositories;
using EventManager.Data.Repositories.Interfaces;
using EventManager.Services.Factories;
using EventManager.Services.Factories.Interfaces;
using EventManager.Services.Services;
using EventManager.Services.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace EventManager.Web.Setup
{
    public static class DependancyInjectionSetup
    {
        public static void SetupDependancyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            //We are adding it this way because we need to get one of the properties from the configurations and the others from the injection
            services.AddScoped<IUserServiceFactory>(serviceProvider =>
            {
                var userRepository = serviceProvider.GetRequiredService<IUserRepository>();
                var profilePictureRepository = serviceProvider.GetRequiredService<IProfilePictureRepository>();
                var emailService = serviceProvider.GetRequiredService<IEmailService>();
                var jwtService = serviceProvider.GetRequiredService<IJwtService>();
                var cloudinaryService = serviceProvider.GetRequiredService<ICloudinaryService>();
                var mapper = serviceProvider.GetRequiredService<IMapper>();

                var tokenLocation = configuration.GetSection("localTokenLocation").Value;

                if(tokenLocation == null)
                    tokenLocation = Environment.CurrentDirectory;

                return new UserServiceFactory(userRepository, profilePictureRepository,
                    emailService, cloudinaryService, jwtService, mapper, tokenLocation);
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfilePictureRepository, ProfilePictureRepository>();

            services.AddSingleton<IJwtService, JwtService>(options =>
            new JwtService(
                    signingKey: Encoding.ASCII.GetBytes(configuration.GetSection("Jwt").GetSection("signingKey").Value),
                    tokenDuration: TimeSpan.Parse(configuration.GetSection("Jwt").GetSection("tokenDuration").Value),
                    issuer: configuration.GetSection("Jwt").GetSection("validationIssuer").Value,
                    audience: configuration.GetSection("Jwt").GetSection("audience").Value)); 
            
            services.AddSingleton<IEmailService, EmailService>(options =>
            new EmailService(
                    apiKey: configuration.GetSection("EmailSender").GetSection("apiKey").Value,
                    senderMail: configuration.GetSection("EmailSender").GetSection("senderMail").Value,
                    senderName: configuration.GetSection("EmailSender").GetSection("senderName").Value,
                    resetPasswordUri: configuration.GetSection("EmailSender").GetSection("resetPasswordUri").Value
                ));

            services.AddSingleton<ICloudinaryService, CloudinaryService>(options =>
            new CloudinaryService(
                cloudName: configuration.GetSection("Cloudinary").GetSection("Name").Value,
                apiKey: configuration.GetSection("Cloudinary").GetSection("Key").Value,
                apiSecret: configuration.GetSection("Cloudinary").GetSection("Secret").Value
            ));

            services.AddIdentity<User, Role>(options =>
            {
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
                options.User.RequireUniqueEmail = true;
            })
               .AddRoles<Role>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();
        }
    }
}
