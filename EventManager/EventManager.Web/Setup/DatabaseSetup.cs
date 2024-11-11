using EventManager.Data;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Web.Setup
{
    public static class DatabaseSetup
    {
        public static void SetupDataBase(WebApplicationBuilder builder) 
        {
            var connectionString = builder.Configuration.GetConnectionString("ApplicationString");

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }
    }
}
