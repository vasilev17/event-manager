using EventManager.Data;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Web.Setup
{
    public static class DatabaseSetup
    {
        public static void SetupDataBase(this IServiceCollection services, ConfigurationManager configurationManager) 
        {
            var connectionString = configurationManager.GetConnectionString("ApplicationString");

            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }
    }
}
