using PTTS.Core.Domain.Constants;
using PTTS.Core.Domain.VehicleAggregate;

namespace PTTS.Core.Domain.TaxRateAggregate
{
    public class TaxRate
    {
        public int Id { get; set; }
        public string VehicleType { get; set; }
        public decimal Rate { get; set; }
        public string State { get; private set; }
        public string LocalGovernment { get; private set; }

        private TaxRate(string localGovernment, string vehicleType, decimal rate)
        {
            string state = "enugu";

            if (rate < 0)
                throw new ArgumentOutOfRangeException(nameof(rate), "Rate must be non-negative.");
            if (!AppConstants.States.Contains(state))
                throw new ArgumentException($"Invalid state: {state}.", nameof(state));
            if (!AppConstants.EnuguLocalGovernments.Contains(localGovernment))
                throw new ArgumentException($"Invalid local government: {localGovernment}.", nameof(localGovernment));
            if (!AppConstants.VehicleTypes.Contains(vehicleType))
                throw new ArgumentException($"Invalid vehicle type: {vehicleType}.", nameof(vehicleType));

            VehicleType = vehicleType;
            Rate = rate;
            State = state;
            LocalGovernment = localGovernment;
        }

        public static TaxRate Create(string localGovernment, string vehicleType, decimal rate)
        {
            return new TaxRate(localGovernment, vehicleType, rate);
        }
    }
}
