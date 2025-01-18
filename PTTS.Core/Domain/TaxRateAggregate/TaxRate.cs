using PTTS.Core.Domain.VehicleAggregate;

namespace PTTS.Core.Domain.TaxRateAggregate
{
    public class TaxRate
    {
        public int Id { get; set; }
        public string VehicleType { get; private set; }
        public decimal Rate { get; private set; }

        private TaxRate(string vehicleType, decimal rate)
        {
            if (!VehicleConstants.ValidVehicleTypes.Contains(vehicleType))
                throw new ArgumentException($"Invalid vehicle type: {vehicleType}.", nameof(vehicleType));

            VehicleType = vehicleType;
            Rate = rate;
        }

        public static TaxRate Create(string vehicleType, decimal rate)
        {
            if (rate < 0)
                throw new ArgumentOutOfRangeException(nameof(rate), "Rate must be non-negative.");

            return new TaxRate(vehicleType, rate);
        }
    }
}
