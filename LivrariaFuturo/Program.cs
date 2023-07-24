using LivrariaFuturo.Authentication.Api.Setup;

var builder = WebApplication.CreateBuilder(args);

var appSettingsName = String.IsNullOrEmpty(Environment.GetEnvironmentVariable("SETTINGS_FILE")) ? String.Empty : $".{Environment.GetEnvironmentVariable("SETTINGS_FILE")}";

builder.Configuration
            .AddJsonFileAfterLastJsonFile($"appsettings{appSettingsName}.json", optional: false, reloadOnChange: true);

builder.Services.AddSwaggerConfigServices();
builder.Services.AddGeneralConfigServices(builder.Configuration);

builder.Services.AddDependencyInjectionConfig(builder.Configuration);
builder.Services.AddAuthorizationConfigServices(builder.Configuration);

var app = builder.Build();

app.UseSwaggerConfigure();

app.UseHttpsRedirection();

app.UseGeneralConfigure(builder.Configuration);

app.UseAuthorizationConfigure();

app.MapControllers();

app.Run();
