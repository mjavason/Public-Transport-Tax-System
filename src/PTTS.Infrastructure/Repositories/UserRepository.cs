using Microsoft.EntityFrameworkCore;
using PTTS.Core.Domain.UserAggregate;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Infrastructure.DatabaseContext;

namespace PTTS.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _context;

		public UserRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<User?> GetByIdAsync(string userId, CancellationToken cancellationToken)
		{
			return await _context.Users.FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
		}

		public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken)
		{
			return await _context.Users.ToListAsync(cancellationToken);
		}

		public async Task AddAsync(User user, CancellationToken cancellationToken)
		{
			await _context.Users.AddAsync(user, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
		}

		public void Update(User user, CancellationToken cancellationToken)
		{
			_context.Users.Update(user);
		}

		public async Task DeleteAsync(string userId, CancellationToken cancellationToken)
		{
			var user = await GetByIdAsync(userId, cancellationToken);
			if (user != null)
			{
				_context.Users.Remove(user);
				await _context.SaveChangesAsync(cancellationToken);
			}
		}
	}
}
