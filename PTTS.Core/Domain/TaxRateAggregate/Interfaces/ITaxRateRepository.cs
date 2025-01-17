using PTTS.Core.Domain.VehicleAggregate.Enums;

namespace PTTS.Core.Domain.TaxRateAggregate.Interfaces
{
    public interface ITaxRateRepository
    {
        Task<decimal> GetTaxRateByTransportTypeAsync(VehicleType vehicleType);
    }
}