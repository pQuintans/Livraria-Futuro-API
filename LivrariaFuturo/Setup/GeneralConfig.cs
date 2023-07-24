using LivrariaFuturo.Authentication.Api.Middlewares;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using System.Globalization;
using System.IO.Compression;

namespace LivrariaFuturo.Authentication.Api.Setup
{
    public static class GeneralConfig
    {
        public static void AddGeneralConfigServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDistributedMemoryCache();
            services.AddCors();
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            // Habilita a compressão do response
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
        }

        public static void UseGeneralConfigure(this IApplicationBuilder app, IConfiguration config)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // Definindo a cultura padrão: pt-BR
            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            //Middleware customizado para interceptar erros HTTP e exceptions não tratadas
            app.UseCustomExceptionHandler();

            app.UseHttpsRedirection();

            // Utiliza a compressão do response
            app.UseResponseCompression();

            app.UseRouting();
        }
    }
}
