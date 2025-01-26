using Microsoft.AspNetCore.Identity;
using PTTS.Core.Domain.Common;
using PTTS.Core.Domain.Constants;
using PTTS.Core.Domain.UserAggregate.DTOs;

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
            ValidateCreateInputs(firstName, lastName, email);

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            NormalizedUserName = email.ToUpperInvariant();
            NormalizedEmail = email.ToUpperInvariant();
            UserName = email;
        }

        public static User Create(string firstName, string lastName, string email)
        {
            return new User(firstName, lastName, email);
        }

        public void Update(UpdateUserDto updateDto)
        {
            // ValidateUpdateInputs(updateDto);

            if (!string.IsNullOrEmpty(updateDto.FirstName))
                UpdateFirstName(updateDto.FirstName);
            if (!string.IsNullOrEmpty(updateDto.LastName))
                UpdateLastName(updateDto.LastName);
            // if (!string.IsNullOrEmpty(updateDto.Email))
            //     UpdateEmail(updateDto.Email);
        }

        private void UpdateFirstName(string firstName)
        {
            FirstName = firstName;
        }

        private void UpdateLastName(string lastName)
        {
            LastName = lastName;
        }

        private void UpdateEmail(string email)
        {
            Email = email;
            NormalizedUserName = email.ToUpperInvariant();
            NormalizedEmail = email.ToUpperInvariant();
            UserName = email;
        }

        private void ValidateCreateInputs(string firstName, string lastName, string email)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentException("First name cannot be empty.", nameof(firstName));
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be empty.", nameof(email));
        }

        // private void ValidateUpdateInputs(UpdateUserDto updateDto)
        // {
        // if (!string.IsNullOrEmpty(updateDto.Email) && !IsValidEmail(updateDto.Email))
        //     throw new ArgumentException("Invalid email format.", nameof(updateDto.Email));
        // }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}