using PTTS.Core.Domain.VehicleAggregate.Enums;

namespace PTTS.Core.Domain.TaxRateAggregate
{
    public class TaxRate
    {
        public int Id { get; set; }
        public required VehicleType VehicleType { get; set; }
        public decimal Rate { get; set; }
    }
}