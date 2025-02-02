using System.ComponentModel.DataAnnotations.Schema;
using PTTS.Core.Domain.UserAggregate;

namespace PTTS.Core.Domain.TaxPaymentAggregate
{
	public class TaxPayment
	{
		public int Id { get; private set; }
		public decimal Amount { get; private set; }
		public DateTime PaymentDate { get; private set; }
		public string TaxPayerName { get; private set; }
		public string TaxPayerId { get; private set; }
		public string LocalGovernment { get; set; }
		public int VehicleId { get; set; }

		[ForeignKey(nameof(TaxPayerId))]
		public User? TaxPayer { get; private set; } = null!;
		private TaxPayment(string localGovernment, decimal amount, string taxPayerName, string taxPayerId, int vehicleId)
		{
			LocalGovernment = localGovernment;
			Amount = amount;
			PaymentDate = DateTime.UtcNow;
			TaxPayerName = taxPayerName;
			TaxPayerId = taxPayerId;
			VehicleId = vehicleId;
		}

		public static TaxPayment Create(string localGovernment, decimal amount, string payerName, string payerId, int vehicleId)
		{
			return new TaxPayment(localGovernment, amount, payerName, payerId, vehicleId);
		}
		public override string ToString()
		{
			return $"TaxPayment [Id={Id}, Amount={Amount}, PaymentDate={PaymentDate}, PayerName={TaxPayerName}, PayerId={TaxPayerId}, VehicleId={VehicleId}]";
		}
	}
}