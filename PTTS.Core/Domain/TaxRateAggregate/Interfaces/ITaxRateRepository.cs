namespace PTTS.Core.Domain.TaxRateAggregate.Interfaces
{
    public interface ITaxRateRepository
    {
        Task<TaxRate> GetTaxRateByTransportTypeAsync(string transportType);
    }
}