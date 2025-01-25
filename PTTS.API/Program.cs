using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using PTTS.API.Helpers;
using PTTS.API.Middleware;
using PTTS.Application;
using PTTS.Infrastructure;
using Scalar.AspNetCore;

namespace PTTS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();
            Configure(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });
            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices();
        }

        private static void Configure(WebApplication app)
        {
            app.UseMiddleware<LoggingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("Public Transport Tax System API");
                    // .WithTheme(ScalarTheme.BluePlanet);
                });
                app.MapOpenApi();
                app.ApplyMigrations();
            }

            app.MapGet("/", () => "Live!");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
