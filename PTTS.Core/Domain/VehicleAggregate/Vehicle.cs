using System.ComponentModel.DataAnnotations.Schema;
using PTTS.Core.Domain.UserAggregate;
using PTTS.Core.Domain.VehicleAggregate.Enums;

namespace PTTS.Core.Domain
{

    public class PublicTransportVehicle
    {
        public Guid Id { get; private set; }
        public string? UserId { get; set; } = null!;
        public string VehicleId { get; private set; }
        public VehicleType VehicleType { get; private set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; } = null!;

        public PublicTransportVehicle(VehicleType vehicleType, string userId)
        {
            VehicleId = GenerateVehicleId(vehicleType);
            VehicleType = vehicleType;
            UserId = userId;
        }

        private string GenerateVehicleId(VehicleType vehicleType)
        {
            string pretext = vehicleType switch
            {
                VehicleType.Bus => "BU",
                VehicleType.MiniBus => "MINBU",
                VehicleType.Tricycle => "TRI",
                VehicleType.Taxi => "TAXI",
                VehicleType.Van => "VAN",
                _ => throw new ArgumentException("Invalid vehicle type")
            };

            // Generate a random 5-digit number
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);

            return $"{pretext}{randomNumber}";
        }
    }
}
