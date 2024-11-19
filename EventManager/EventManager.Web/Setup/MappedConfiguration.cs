namespace EventManager.Web.Setup
{
    public static class MappedConfiguration
    {
        public static void SetupMapper(this IServiceCollection services)
        {
            //Looks for mappings in the current assembly
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
