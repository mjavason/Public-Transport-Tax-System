using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PTTS.Core.Domain.Constants;
using PTTS.Core.Domain.UserAggregate;

namespace PTTS.Core.Domain.VehicleAggregate
{
    public class PublicTransportVehicle
    {
        public int Id { get; private set; }
        public string UserId { get; private set; }
        public string VehicleId { get; private set; }
        public string VehicleType { get; private set; }
        public string Make { get; private set; }
        public string Model { get; private set; }
        public string PlateNumber { get; private set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; } = null!;

        private PublicTransportVehicle(string vehicleType, string userId, string make, string model, string plateNumber)
        {
            if (!AppConstants.VehicleTypes.Contains(vehicleType))
            {
                throw new ArgumentException($"Invalid vehicle type: {vehicleType}.", nameof(vehicleType));
            }

            VehicleId = GenerateVehicleId(vehicleType);
            VehicleType = vehicleType;
            UserId = userId;
            Make = make;
            Model = model;
            PlateNumber = plateNumber;
        }

        public static PublicTransportVehicle Create(string vehicleType, string userId, string make, string model, string plateNumber)
        {
            return new PublicTransportVehicle(vehicleType, userId, make, model, plateNumber);
        }

        private static string GenerateVehicleId(string vehicleType)
        {
            string pretext = vehicleType[..2].ToUpper();

            // Generate a random 5-digit number
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);

            return $"{pretext}{randomNumber}";
        }
    }
}