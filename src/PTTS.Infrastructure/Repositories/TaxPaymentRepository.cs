using Microsoft.EntityFrameworkCore;
using PTTS.Core.Domain.TaxPaymentAggregate;
using PTTS.Core.Domain.TaxPaymentAggregate.DTOs;
using PTTS.Core.Domain.TaxPaymentAggregate.Interfaces;
using PTTS.Infrastructure.DatabaseContext;

namespace PTTS.Infrastructure.Repositories
{
	public class TaxPaymentRepository : ITaxPaymentRepository
	{
		private readonly ApplicationDbContext _context;

		public TaxPaymentRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async void CreateTaxPayment(TaxPayment taxPayment, CancellationToken cancellationToken)
		{
			await _context.TaxPayments.AddAsync(taxPayment, cancellationToken);
		}

		public void DeleteTaxPayment(TaxPayment taxPayment, CancellationToken cancellationToken)
		{
			_context.TaxPayments.Remove(taxPayment);
		}

		public async Task<IReadOnlyList<TaxPayment>> FilterTaxPaymentAsync(FilterTaxPaymentDto filter, CancellationToken cancellationToken)
		{
			var query = _context.TaxPayments.AsQueryable();

			if (!string.IsNullOrEmpty(filter.TaxPayerId)) query = query.Where(tp => tp.TaxPayerId == filter.TaxPayerId);
			if (!string.IsNullOrEmpty(filter.TaxPayerName)) query = query.Where(tp => tp.TaxPayerName == filter.TaxPayerName);
			if (filter.MinimumAmount.HasValue) query = query.Where(tp => tp.Amount >= filter.MinimumAmount.Value);
			if (filter.MaximumAmount.HasValue) query = query.Where(tp => tp.Amount <= filter.MaximumAmount.Value);

			return await query.ToListAsync(cancellationToken);
		}

		public async Task<IReadOnlyList<TaxPayment>> GetAllTaxPaymentsAsync(CancellationToken cancellationToken)
		{
			return await _context.TaxPayments.ToListAsync(cancellationToken);
		}

		public async Task<TaxPayment?> GetTaxPaymentByIdAsync(int taxPaymentId, CancellationToken cancellationToken)
		{
			return await _context.TaxPayments.FindAsync(new object[] { taxPaymentId }, cancellationToken);
		}

		public void UpdateTaxPayment(TaxPayment taxPayment, CancellationToken cancellationToken)
		{
			_context.TaxPayments.Update(taxPayment);
		}
	}
}