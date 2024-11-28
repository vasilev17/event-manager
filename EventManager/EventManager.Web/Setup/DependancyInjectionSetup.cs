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
            services.AddScoped<IUserServiceFactory, UserServiceFactory>();

            services.AddScoped<IUserRepository, UserRepository>();

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

            services.AddIdentity<User, Role>(options =>
            {
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
            })
               .AddRoles<Role>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();
        }
    }
}
