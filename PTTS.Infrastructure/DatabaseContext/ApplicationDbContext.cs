using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PTTS.Core.Domain.TaxRateAggregate;
using PTTS.Core.Domain.UserAggregate;
using TodoAppWithAuth.Database;

namespace PTTS.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<TaxRate> TaxRates { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply entity-specific configurations
            builder.ApplyConfiguration(new UserConfiguration());

            // Set default schema for Identity
            builder.HasDefaultSchema("identity");
        }
    }
}