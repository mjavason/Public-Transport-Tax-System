using System.ComponentModel.DataAnnotations.Schema;
using PTTS.Core.Domain.Common;
using PTTS.Core.Domain.UserAggregate;

namespace PTTS.Core.Domain.TaxPaymentAggregate
{
	public class TaxPayment
	{
		public int Id { get; private set; }
		public decimal Amount { get; private set; }
		public DateTime PaymentDate { get; private set; }
		public string PayerName { get; private set; }
		public string PayerId { get; private set; }

		[ForeignKey(nameof(PayerId))]
		public User? Payer { get; private set; } = null!;
		private TaxPayment(int id, decimal amount, DateTime paymentDate, string payerName, string payerId)
		{
			Id = id;
			Amount = amount;
			PaymentDate = paymentDate;
			PayerName = payerName;
			PayerId = payerId;
		}

		public static TaxPayment Create(int id, decimal amount, DateTime paymentDate, string payerName, string payerId)
		{
			return new TaxPayment(id, amount, paymentDate, payerName, payerId);
		}

		public void UpdatePayment(decimal amount, DateTime paymentDate)
		{
			Amount = amount;
			PaymentDate = paymentDate;
		}

		public override string ToString()
		{
			return $"TaxPayment [Id={Id}, Amount={Amount}, PaymentDate={PaymentDate}, PayerName={PayerName}, PayerId={PayerId}]";
		}
	}
}