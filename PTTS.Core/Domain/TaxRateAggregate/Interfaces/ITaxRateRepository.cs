namespace PTTS.Core.Domain.TaxRateAggregate.Interfaces
{
    public interface ITaxRateRepository
    {
        /// <summary>
        /// Gets a TaxRate based on the provided transport type.
        /// </summary>
        /// <param name="vehicleType">The type of the vehicle.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching TaxRate or null if not found.</returns>
        Task<TaxRate?> GetTaxRateByTransportTypeAsync(string vehicleType, CancellationToken cancellationToken);

        /// <summary>
        /// Creates a new TaxRate.
        /// </summary>
        /// <param name="taxRate">The TaxRate to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The created TaxRate.</returns>
        Task CreateTaxRate(TaxRate taxRate, CancellationToken cancellationToken);

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
        /// Checks if a TaxRate exists for a specific vehicle type.
        /// </summary>
        /// <param name="vehicleType">The type of the vehicle.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if a TaxRate exists for the specified vehicle type, otherwise false.</returns>
        Task<bool> TaxRateExistsAsync(string vehicleType, CancellationToken cancellationToken);
    }
}
