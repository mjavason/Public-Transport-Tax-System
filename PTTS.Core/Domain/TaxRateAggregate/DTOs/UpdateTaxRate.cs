namespace PTTS.Core.Domain.TaxRateAggregate.DTOs;

public class UpdateTaxRateDto
{
	public string? LocalGovernment { get; set; }
	public string? VehicleType { get; set; }
	public decimal? Rate { get; set; }
	public required int TaxRateId { get; set; }
}
