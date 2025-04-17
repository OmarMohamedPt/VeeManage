using System.Text.Json.Serialization;
using Scalar.AspNetCore;
using VMTS.API.Extensions;

namespace VMTS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Logging setup
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // Add services to the container
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Convert enums to strings
                });

            builder.Services.AddOpenApi();

            VTMSServices.AddAppServices(builder.Services, builder.Configuration);
            AppUserIdentityServices.AddAppServices(builder.Services, builder.Configuration);

            
            var env = builder.Environment;
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            
            // CORS setup for development & production
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins(
                            "http://localhost:3000",            // Dev frontend
                            "https://veemanage.runasp.net"      // Deployment frontend
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            var app = builder.Build();

            // CORS middleware
            app.UseCors("AllowFrontend");

            // Swagger always enabled
            app.MapOpenApi();
            app.MapScalarApiReference();

            // Optional: comment this if HTTPS causes issues on runasp.net
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            // Add default root path to avoid 404 at /
            app.MapGet("/", () => "API is running...");

            app.Run();
        }
    }
}
