using PTTS.Core.Domain.TaxPaymentAggregate.DTOs;

namespace PTTS.Core.Domain.TaxPaymentAggregate.Interfaces
{
	public interface ITaxPaymentRepository
	{
		/// <summary>
		/// Creates a new TaxPayment.
		/// </summary>
		/// <param name="taxPayment">The TaxPayment to create.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The created TaxPayment.</returns>
		void CreateTaxPayment(TaxPayment taxPayment, CancellationToken cancellationToken);

		/// <summary>
		/// Filter TaxPayments based on the provided params.
		/// </summary>
		/// <param name="filter">The filter dto to be applied.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The matching TaxPayment or null if not found.</returns>
		Task<IReadOnlyList<TaxPayment>> FilterTaxPaymentAsync(FilterTaxPaymentDto filter, CancellationToken cancellationToken);

		/// <summary>
		/// Gets all TaxPayments.
		/// </summary>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A list of all TaxPayments.</returns>
		Task<IReadOnlyList<TaxPayment>> GetAllTaxPaymentsAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Gets a TaxPayment by its identifier.
		/// </summary>
		/// <param name="taxPaymentId">The identifier of the TaxPayment.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The matching TaxPayment or null if not found.</returns>
		Task<TaxPayment?> GetTaxPaymentByIdAsync(int taxPaymentId, CancellationToken cancellationToken);

		/// <summary>
		/// Updates an existing TaxPayment.
		/// </summary>
		/// <param name="taxPayment">The TaxPayment with updated values.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The updated TaxPayment.</returns>
		void UpdateTaxPayment(TaxPayment taxPayment, CancellationToken cancellationToken);

		/// <summary>
		/// Deletes a TaxPayment by its identifier.
		/// </summary>
		/// <param name="taxPaymentId">The identifier of the TaxPayment to delete.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A Task representing the asynchronous operation.</returns>
		void DeleteTaxPayment(TaxPayment taxPayment, CancellationToken cancellationToken);
	}
}