using LivrariaFuturo.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LivrariaFuturo.Authentication.Infrastructure.Setup
{
    public static class DependencyInjectionRepositories
    {
        public static void AddDependencyInjectionApplicationRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
