namespace PTTS.Core.Domain.UserAggregate.DTOs
{
	public class UserProfileDto
	{
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }

		public UserProfileDto(string userId, string firstName, string lastName, string email, string phoneNumber)
		{
			UserId = userId;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			PhoneNumber = phoneNumber;
		}
	}
}
