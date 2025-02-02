using PTTS.API.Filters;
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
			services.AddRazorPages();
			services.AddMvc();
			services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
			services.AddCors(options =>
			{
				options.AddPolicy("*", (config) =>
					config
						.AllowAnyOrigin()
						.AllowAnyHeader()
						.AllowAnyMethod()
				);
			});
			services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });
			services.AddInfrastructureServices(configuration);
			services.AddApplicationServices();
			services.AddControllers(cfg =>
			{
				cfg.Filters.Add<ResultFilter>();
			});

		}

		private static void Configure(WebApplication app)
		{
			app.UseCors("*");
			app.UseAuthentication();
			app.UseAuthorization();

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
			app.MapControllers();
			app.UseMiddleware<LoggingMiddleware>();
			// app.UseMiddleware<ExceptionMiddleware>();
		}
	}
}
