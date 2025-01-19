using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTTS.Core.Domain.Common;
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
            // Register Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaxRateRepository, TaxRateRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register DbContext with connection string from configuration
            services.AddDbContext<ApplicationDbContext>(dbContextOptions =>
                dbContextOptions.UseNpgsql(configuration.GetConnectionString("Database"), options =>
                {
                    options.EnableRetryOnFailure();
                }));

            // Register Identity
            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddApiEndpoints();

            return services;
        }
    }
}
