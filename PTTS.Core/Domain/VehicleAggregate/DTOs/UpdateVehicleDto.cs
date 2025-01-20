namespace PTTS.Core.Domain.VehicleAggregate.DTOs
{
    public class UpdateVehicleDto
    {
        public required int VehicleId { get; set; }
        public string? VehicleType { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? PlateNumber { get; set; }
    }
}