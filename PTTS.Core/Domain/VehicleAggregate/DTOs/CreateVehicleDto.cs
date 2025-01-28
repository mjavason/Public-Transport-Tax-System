namespace PTTS.Core.Domain.VehicleAggregate.DTOs;

public class CreateVehicleDto
{
	public required string VehicleType { get; set; }
	// public required string userId { get; set; }
	public required string Make { get; set; }
	public required string Model { get; set; }
	public required string PlateNumber { get; set; }
}
