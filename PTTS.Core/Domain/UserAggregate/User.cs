using Microsoft.AspNetCore.Identity;
using PTTS.Core.Domain.Common;

namespace PTTS.Core.Domain.UserAggregate
{
    public class User : IdentityUser, IAuditableEntity
    {
        public string? Initials { get; set; }
        public string? FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }

        private User(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            NormalizedUserName = email.ToUpperInvariant();
            NormalizedEmail = email.ToUpperInvariant();
        }

        public static User Create(string firstName, string lastName, string email)
        {
            return new User(firstName, lastName, email);
        }
    }
}