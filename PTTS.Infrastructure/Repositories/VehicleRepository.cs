using Microsoft.EntityFrameworkCore;
using PTTS.Core.Domain.Interfaces;
using PTTS.Infrastructure.DatabaseContext;

namespace PTTS.Infrastructure.Repositories
{
	public class PublicTransportVehicleRepository : IPublicTransportVehicleRepository
	{
		private readonly ApplicationDbContext _context;

		public PublicTransportVehicleRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Core.Domain.VehicleAggregate.PublicTransportVehicle?> GetVehicleByIdAsync(int id, CancellationToken cancellationToken)
		{
			return await _context.PublicTransportVehicles.Include(t => t.User)
			.FirstOrDefaultAsync(vehicle => vehicle.Id == id, cancellationToken);
		}

		public async Task<IReadOnlyList<Core.Domain.VehicleAggregate.PublicTransportVehicle>> GetAllVehiclesAsync(CancellationToken cancellationToken)
		{
			return await _context.PublicTransportVehicles.Include(t => t.User)
			.ToListAsync(cancellationToken);
		}

		public async Task CreateVehicleAsync(Core.Domain.VehicleAggregate.PublicTransportVehicle vehicle, CancellationToken cancellationToken)
		{
			await _context.PublicTransportVehicles.AddAsync(vehicle, cancellationToken);
		}

		public void UpdateVehicle(Core.Domain.VehicleAggregate.PublicTransportVehicle vehicle, CancellationToken cancellationToken)
		{
			_context.PublicTransportVehicles.Update(vehicle);
		}

		public void DeleteVehicleAsync(Core.Domain.VehicleAggregate.PublicTransportVehicle vehicle, CancellationToken cancellationToken)
		{
			_context.PublicTransportVehicles.Remove(vehicle);
		}

		public async Task<IReadOnlyList<Core.Domain.VehicleAggregate.PublicTransportVehicle>> GetVehiclesByUserIdAsync(string userId, CancellationToken cancellationToken)
		{
			return await _context.PublicTransportVehicles.Include(t => t.User)
				.Where(vehicle => vehicle.UserId == userId)
				.ToListAsync(cancellationToken);
		}
	}
}
