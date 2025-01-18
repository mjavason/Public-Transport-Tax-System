using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PTTS.Core.Domain.TaxRateAggregate;
using PTTS.Core.Domain.TaxRateAggregate.Interfaces;
using PTTS.Infrastructure.DatabaseContext;

namespace PTTS.Infrastructure.Repositories
{
    public class TaxRateRepository : ITaxRateRepository
    {
        private readonly ApplicationDbContext _context;

        public TaxRateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaxRate?> GetTaxRateByTransportTypeAsync(string vehicleType, CancellationToken cancellationToken)
        {
            return await _context.TaxRates
                .FirstOrDefaultAsync(t => t.VehicleType == vehicleType);
        }

        public async Task<TaxRate> CreateTaxRate(TaxRate taxRate, CancellationToken cancellationToken)
        {
            await _context.TaxRates.AddAsync(taxRate, cancellationToken);
            return taxRate;
        }

        public Task<TaxRate> UpdateTaxRateAsync(TaxRate taxRate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTaxRateAsync(Guid taxRateId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TaxRate>> GetAllTaxRatesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<TaxRate?> GetTaxRateByIdAsync(Guid taxRateId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TaxRateExistsAsync(string vehicleType, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}