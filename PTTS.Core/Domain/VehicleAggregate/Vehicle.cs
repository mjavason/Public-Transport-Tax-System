using System.ComponentModel.DataAnnotations.Schema;
using PTTS.Core.Domain.UserAggregate;

namespace PTTS.Core.Domain.VehicleAggregate
{
    public class PublicTransportVehicle
    {
        public Guid Id { get; private set; }
        public string? UserId { get; private set; } = null!;
        public string VehicleId { get; private set; }
        public string VehicleType { get; private set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; } = null!;

        private PublicTransportVehicle(string vehicleType, string userId)
        {
            if (!VehicleConstants.ValidVehicleTypes.Contains(vehicleType))
                throw new ArgumentException($"Invalid vehicle type: {vehicleType}.", nameof(vehicleType));

            VehicleId = GenerateVehicleId(vehicleType);
            VehicleType = vehicleType;
            UserId = userId;
        }

        public static PublicTransportVehicle Create(string vehicleType, string userId)
        {
            return new PublicTransportVehicle(vehicleType, userId);
        }

        public static void UpdateVehicleType(PublicTransportVehicle existingVehicle, string vehicleType)
        {
            existingVehicle.VehicleType = vehicleType;
            existingVehicle.VehicleId = existingVehicle.GenerateVehicleId(vehicleType);
        }

        private string GenerateVehicleId(string vehicleType)
        {
            string pretext = vehicleType[..2].ToUpper();

            // Generate a random 5-digit number
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);

            return $"{pretext}{randomNumber}";
        }
    }
}
