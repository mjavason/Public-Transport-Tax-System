using Microsoft.AspNetCore.Identity;

namespace PTTS.Core.Domain.UserAggregate
{
    public class User : IdentityUser
    {
        public string? Initials { get; set; }
    }
}