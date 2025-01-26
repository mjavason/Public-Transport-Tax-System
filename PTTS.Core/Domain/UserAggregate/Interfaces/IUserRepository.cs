using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PTTS.Core.Domain.UserAggregate.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Gets a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user entity if found; otherwise, null.</returns>
        Task<User?> GetByIdAsync(string userId, CancellationToken cancellationToken);

        /// <summary>
        /// Adds a new user to the repository.
        /// </summary>
        /// <param name="user">The user entity to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task AddAsync(User user, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing user in the repository.
        /// </summary>
        /// <param name="user">The user entity with updated values.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        void Update(User user, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task DeleteAsync(string userId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all users from the repository.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of all user entities.</returns>
        Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken);
    }
}
