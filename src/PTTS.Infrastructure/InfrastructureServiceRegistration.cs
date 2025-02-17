using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PTTS.Core.Domain.Common;
using PTTS.Core.Domain.Interfaces;
using PTTS.Core.Domain.TaxPaymentAggregate.Interfaces;
using PTTS.Core.Domain.TaxRateAggregate.Interfaces;
using PTTS.Core.Domain.UserAggregate;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Infrastructure.Credentials;
using PTTS.Infrastructure.DatabaseContext;
using PTTS.Infrastructure.Repositories;
using PTTS.Infrastructure.Services;

namespace PTTS.Infrastructure
{
	public static class InfrastructureServiceRegistration
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

			// Register Repositories & Services
			services.AddScoped<RazorViewToStringRenderer>();
			services.AddScoped<ITaxPaymentRepository, TaxPaymentRepository>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IPublicTransportVehicleRepository, PublicTransportVehicleRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<ITaxRateRepository, TaxRateRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddTransient<IEmailSender, EmailSender>();

			// Database Context Configuration
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseNpgsql(configuration.GetConnectionString("Database"), npgsqlOptions =>
					npgsqlOptions.EnableRetryOnFailure()));

			// Identity Configuration
			services.AddIdentity<User, IdentityRole>(options =>
			{
				options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				options.User.RequireUniqueEmail = true;
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 6;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = true;
			})
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();

			// JWT Authentication Configuration
			services.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = false,
						ValidIssuer = configuration["JwtSettings:Issuer"],
						ValidAudience = configuration["JwtSettings:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"] ?? string.Empty))
					};
				});

			return services;
		}
	}
}