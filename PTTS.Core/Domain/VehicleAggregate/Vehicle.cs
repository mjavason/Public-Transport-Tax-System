using System.ComponentModel.DataAnnotations.Schema;
using PTTS.Core.Domain.UserAggregate;

namespace PTTS.Core.Domain
{

    public class PublicTransportVehicle
    {
        public Guid Id { get; private set; }
        public string? UserId { get; set; } = null!;
        public string VehicleId { get; private set; }
        public string VehicleType { get; private set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; } = null!;

        public PublicTransportVehicle(string vehicleType, string userId)
        {
            VehicleId = GenerateVehicleId(vehicleType);
            VehicleType = vehicleType;
            UserId = userId;
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
