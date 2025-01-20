using PTTS.Core.Domain.VehicleAggregate;

namespace PTTS.Core.Domain.Interfaces
{
    public interface IPublicTransportVehicleRepository
    {
        /// <summary>
        /// Gets a PublicTransportVehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the vehicle.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching vehicle or null if not found.</returns>
        Task<PublicTransportVehicle?> GetVehicleByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all PublicTransportVehicles.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of all vehicles.</returns>
        Task<IReadOnlyList<PublicTransportVehicle>> GetAllVehiclesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Creates a new PublicTransportVehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The created vehicle.</returns>
        Task CreateVehicleAsync(PublicTransportVehicle vehicle, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing PublicTransportVehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle with updated values.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated vehicle.</returns>
        void UpdateVehicle(PublicTransportVehicle vehicle, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a PublicTransportVehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the vehicle to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        void DeleteVehicleAsync(PublicTransportVehicle vehicle, CancellationToken cancellationToken);

        /// <summary>
        /// Gets vehicles by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of vehicles owned by the user.</returns>
        Task<IReadOnlyList<PublicTransportVehicle>> GetVehiclesByUserIdAsync(string userId, CancellationToken cancellationToken);
    }
}
