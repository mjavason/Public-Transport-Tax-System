using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PTTS.Core.Domain.TaxRateAggregate;
using PTTS.Core.Domain.UserAggregate;
using PTTS.Core.Domain.VehicleAggregate;

namespace PTTS.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<TaxRate> TaxRates { get; set; }
        public DbSet<PublicTransportVehicle> PublicTransportVehicles {get; set;}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Apply entity-specific configurations
            // builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Set default schema for Identity
            builder.HasDefaultSchema("identity");

            base.OnModelCreating(builder);
        }
    }
}