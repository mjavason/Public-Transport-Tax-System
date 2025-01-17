using PTTS.Core.Domain.UserAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTTS.Core.Domain.UserAggregate.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Gets a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>The user entity if found; otherwise, null.</returns>
        Task<User?> GetByIdAsync(string userId);

        /// <summary>
        /// Adds a new user to the repository.
        /// </summary>
        /// <param name="user">The user entity to add.</param>
        Task AddAsync(User user);

        /// <summary>
        /// Updates an existing user in the repository.
        /// </summary>
        /// <param name="user">The user entity to update.</param>
        Task UpdateAsync(User user);

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        Task DeleteAsync(string userId);

        /// <summary>
        /// Gets all users from the repository.
        /// </summary>
        /// <returns>A collection of all user entities.</returns>
        Task<IEnumerable<User>> GetAllAsync();
    }
}
