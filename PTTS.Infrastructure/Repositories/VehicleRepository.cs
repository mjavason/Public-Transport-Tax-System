using Microsoft.EntityFrameworkCore;
using PTTS.Core.Domain;
using PTTS.Core.Domain.Interfaces;
using PTTS.Infrastructure.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PTTS.Infrastructure.Repositories
{
    public class PublicTransportVehicleRepository : IPublicTransportVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public PublicTransportVehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Core.Domain.VehicleAggregate.PublicTransportVehicle?> GetVehicleByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.PublicTransportVehicles
                .FirstOrDefaultAsync(vehicle => vehicle.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Core.Domain.VehicleAggregate.PublicTransportVehicle>> GetAllVehiclesAsync(CancellationToken cancellationToken)
        {
            return await _context.PublicTransportVehicles
                .ToListAsync(cancellationToken);
        }

        public async Task CreateVehicleAsync(Core.Domain.VehicleAggregate.PublicTransportVehicle vehicle, CancellationToken cancellationToken)
        {
            await _context.PublicTransportVehicles.AddAsync(vehicle, cancellationToken);
        }

        public Core.Domain.VehicleAggregate.PublicTransportVehicle UpdateVehicle(Core.Domain.VehicleAggregate.PublicTransportVehicle vehicle, CancellationToken cancellationToken)
        {
            _context.PublicTransportVehicles.Update(vehicle);
            return vehicle;
        }

        public async Task DeleteVehicleAsync(Guid id, CancellationToken cancellationToken)
        {
            var vehicle = await _context.PublicTransportVehicles
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);

            if (vehicle != null)
            {
                _context.PublicTransportVehicles.Remove(vehicle);
            }
        }

        public async Task<bool> VehicleExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.PublicTransportVehicles
                .AnyAsync(vehicle => vehicle.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Core.Domain.VehicleAggregate.PublicTransportVehicle>> GetVehiclesByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _context.PublicTransportVehicles
                .Where(vehicle => vehicle.UserId == userId)
                .ToListAsync(cancellationToken);
        }
    }
}
