namespace PTTS.Core.Domain.VehicleAggregate.DTOs
{
	public class FilterVehicleDto
	{
		public int? Id { get; set; }
		public string? UserId { get; set; }
		public string? VehicleId { get; set; }
		public string? VehicleType { get; set; }
		public string? Make { get; set; }
		public string? Model { get; set; }
		public string? PlateNumber { get; set; }
	}
}
