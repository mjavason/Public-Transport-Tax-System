namespace PTTS.Core.Domain.UserAggregate.DTOs
{
	public class ConfirmEmailDTO
	{
		public required string ConfirmationLink { get; set; }
		public required string Name { get; set; }
	}
}