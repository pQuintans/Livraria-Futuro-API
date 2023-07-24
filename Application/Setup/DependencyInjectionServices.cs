using LivrariaFuturo.Authentication.Infrastructure.Setup;
using LivrariaFuturo.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LivrariaFuturo.Authentication.Application.Setup
{
    public static class DependencyInjectionServices
    {
        public static void AddDependencyInjectionApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();

            services.AddDependencyInjectionApplicationRepositories(configuration);
        }
    }
}
