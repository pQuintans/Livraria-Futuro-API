using LivrariaFuturo.Authentication.Application.Setup;

namespace LivrariaFuturo.Authentication.Api.Setup
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencyInjectionApplicationServices(configuration);
        }
    }
}
