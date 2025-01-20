namespace PTTS.Core.Domain.TaxRateAggregate.DTOs
{
    public class FilterTaxRateDto
    {
        public string? State { get; set; }
        public string? LocalGovernment { get; set; }
        public string? VehicleType { get; set; }
        public decimal? MinimumTaxRate { get; set; }
        public decimal? MaximumTaxRate { get; set; }
    }
}