using Microsoft.AspNetCore.Identity;

namespace PTTS.Core.Domain.UserAggregate
{
    public sealed class User : IdentityUser
    {
        public string? Initials { get; set; }

        private User(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            NormalizedUserName = email.ToUpperInvariant();
            NormalizedEmail = email.ToUpperInvariant();
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public static User Create(string firstName, string lastName, string email)
        {
            return new User(firstName, lastName, email);
        }
    }
}