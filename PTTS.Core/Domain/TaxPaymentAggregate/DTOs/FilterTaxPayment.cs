using PTTS.Core.Domain.Common;

namespace PTTS.Core.Domain.TaxPaymentAggregate.DTOs
{
	public class FilterTaxPaymentDto
	{
		public string? TaxPayerId { get; set; }
		public string? TaxType { get; set; }
		public decimal? MinimumAmount { get; set; }
		public decimal? MaximumAmount { get; set; }
	}
}