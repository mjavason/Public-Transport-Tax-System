using PTTS.Core.Domain.Common;

namespace PTTS.Core.Domain.TaxPaymentAggregate.DTOs
{
	public class FilterTaxPaymentDto
	{
		public string? TaxPayerId { get; set; }
		public string? TaxPayerName { get; set; }
		public decimal? MinimumAmount { get; set; }
		public decimal? MaximumAmount { get; set; }
		public DateTime? MinimumDate { get; set; }
		public DateTime? MaximumDate { get; set; }
	}
}