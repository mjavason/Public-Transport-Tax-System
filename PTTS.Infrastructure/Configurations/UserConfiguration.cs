using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTTS.Core.Domain.UserAggregate;

namespace TodoAppWithAuth.Database
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Initials)
                .HasMaxLength(5);
        }
    }
}
