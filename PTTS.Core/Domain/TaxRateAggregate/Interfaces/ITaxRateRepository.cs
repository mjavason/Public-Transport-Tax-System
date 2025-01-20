using Microsoft.AspNetCore.Mvc.Filters;
using PTTS.Core.Domain.TaxRateAggregate.DTOs;

namespace PTTS.Core.Domain.TaxRateAggregate.Interfaces
{
    public interface ITaxRateRepository
    {
        /// <summary>
        /// Creates a new TaxRate.
        /// </summary>
        /// <param name="taxRate">The TaxRate to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The created TaxRate.</returns>
        Task CreateTaxRate(TaxRate taxRate, CancellationToken cancellationToken);

        /// <summary>
        /// Filter TaxRates based on the provided params.
        /// </summary>
        /// <param name="filter">The filter dto to be applied.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching TaxRate or null if not found.</returns>
        Task<IReadOnlyList<TaxRate>> FilterTaxRateByTransportTypeAsync(FilterTaxRateDto filter, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all TaxRates.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of all TaxRates.</returns>
        Task<IReadOnlyList<TaxRate>> GetAllTaxRatesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets a TaxRate by its identifier.
        /// </summary>
        /// <param name="taxRateId">The identifier of the TaxRate.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching TaxRate or null if not found.</returns>
        Task<TaxRate?> GetTaxRateByIdAsync(Guid taxRateId, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing TaxRate.
        /// </summary>
        /// <param name="taxRate">The TaxRate with updated values.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated TaxRate.</returns>
        Task<TaxRate> UpdateTaxRateAsync(TaxRate taxRate, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a TaxRate by its identifier.
        /// </summary>
        /// <param name="taxRateId">The identifier of the TaxRate to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task DeleteTaxRateAsync(Guid taxRateId, CancellationToken cancellationToken);
    }
}
