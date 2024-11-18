using EventManager.Data;
using EventManager.Data.Models;
using EventManager.Data.Repositories;
using EventManager.Data.Repositories.Interfaces;

namespace EventManager.Web.Setup
{
    public static class DependancyInjectionSetup
    {
        public static void SetupDependancyInjection(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddIdentity<User, Role>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
