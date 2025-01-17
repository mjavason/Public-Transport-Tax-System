using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            // Register DbContext with connection string from configuration
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Database")));

            // Register Identity
            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddApiEndpoints();

            // Register Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            // Add other repositories here as needed

            return services;
        }
    }
}
