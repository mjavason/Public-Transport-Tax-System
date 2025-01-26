using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PTTS.Core.Domain.TaxRateAggregate;
using PTTS.Core.Domain.UserAggregate;
using PTTS.Core.Domain.UserAggregate.Enums;
using PTTS.Core.Domain.VehicleAggregate;

namespace PTTS.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<TaxRate> TaxRates { get; set; }
        public DbSet<PublicTransportVehicle> PublicTransportVehicles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedDb(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(builder);
        }

        /// <summary>
        /// Seeds the database with initial data for roles and an admin user.
        /// </summary>
        /// <param name="builder">The <see cref="ModelBuilder"/> used to configure the model.</param>
        /// <remarks>
        /// This method creates and seeds the following:
        /// <list type="bullet">
        /// <item><description>SuperAdmin and Admin roles</description></item>
        /// <item><description>An admin user with the email "testerzero@gmail.com" and password "Strong@password123"</description></item>
        /// <item><description>Associates the admin user with the SuperAdmin role</description></item>
        /// </list>
        /// </remarks>

        private static void SeedDb(ModelBuilder builder)
        {
            //Seed user roles
            var superAdminRole = new IdentityRole
            {
                Id = "6107a220-ed7d-451a-a26a-c8fff0f845eb",
                Name = UserRole.SuperAdmin.ToString(),
                NormalizedName = UserRole.SuperAdmin.ToString().ToUpperInvariant()
            };
            builder.Entity<IdentityRole>(e =>
                {
                    e.HasData(
                        superAdminRole,
                        new IdentityRole
                        {
                            Id = "c73136f5-9102-4048-b4e4-c6f3756adec8",
                            Name = UserRole.Admin.ToString(),
                            NormalizedName = UserRole.Admin.ToString().ToUpperInvariant()
                        });
                }
            );

            //Seed super admin user
            var adminUser = User.Create("System", "Admin", "testerzero@gmail.com");
            adminUser.Id = "fd693413-20f6-48ab-8b40-f249c8624ad6";
            adminUser.EmailConfirmed = true;

            PasswordHasher<User> passwordHasher = new();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Strong@password123");
            // adminUser.PasswordHash = "AQAAAAIAAYagAAAAEJ+W9UqwJmLq5Dd1osxRCTZ/lS1Gpw4i7saurtSiarGGoYwBmDB1/UmacmjRMdATlw==";

            builder.Entity<User>().HasData(adminUser);

            //     //Link super admin user to role
            //     builder.Entity<IdentityUserRole<string>>(e =>
            //         {
            //             e.HasData(new IdentityUserRole<string>
            //             {
            //                 RoleId = superAdminRole.Id,
            //                 UserId = adminUser.Id
            //             });
            //         }
            //     );
        }
    }
}
