using AutoMapper;
using EventManager.Web.Setup.Mappings;

namespace EventManager.Web.Setup
{
    public static class MapperSetup
    {
        public static void SetupMapper(this IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                // Create the mapper configuration
                var config = new MapperConfiguration(cfg =>
                {
                    // Add all profiles manually
                    cfg.AddProfile<UserMappings>();
                    cfg.AddProfile<PictureMappings>();
                    cfg.AddProfile<EventMappings>();
                });

                // Create and return the mapper
                return config.CreateMapper();
            });
        }
    }
}
