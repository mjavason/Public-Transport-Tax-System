using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTTS.Core.Domain.Common;
using PTTS.Core.Domain.Interfaces;
using PTTS.Core.Domain.TaxRateAggregate.Interfaces;
using PTTS.Core.Domain.UserAggregate;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Infrastructure.DatabaseContext;
using PTTS.Infrastructure.Repositories;

namespace PTTS.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Authorization and Authentication
            services.AddAuthorization();
            services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
            services.AddIdentityCore<User>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddApiEndpoints();

            // Register DbContext with connection string from configuration
            services.AddDbContext<ApplicationDbContext>(dbContextOptions =>
                dbContextOptions.UseNpgsql(configuration.GetConnectionString("Database"), options =>
                {
                    options.EnableRetryOnFailure();
                }));

            // Register Repositories
            services.AddScoped<IPublicTransportVehicleRepository, PublicTransportVehicleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaxRateRepository, TaxRateRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
