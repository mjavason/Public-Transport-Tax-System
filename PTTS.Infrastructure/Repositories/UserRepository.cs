using Microsoft.EntityFrameworkCore;
using PTTS.Core.Domain.UserAggregate;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Infrastructure.DatabaseContext;
using System.Threading.Tasks;

namespace PTTS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        // Other CRUD operations as needed
    }
}
