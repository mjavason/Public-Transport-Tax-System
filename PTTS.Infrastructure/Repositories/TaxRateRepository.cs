using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PTTS.Core.Domain.TaxRateAggregate;
using PTTS.Core.Domain.TaxRateAggregate.DTOs;
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

        public async Task<IReadOnlyList<TaxRate>> FilterTaxRateAsync(FilterTaxRateDto filter, CancellationToken cancellationToken)
        {
            IQueryable<TaxRate> query = _context.TaxRates;

            // Apply filters conditionally
            if (!string.IsNullOrEmpty(filter.State))
            {
                query = query.Where(tr => tr.State == filter.State);
            }

            if (!string.IsNullOrEmpty(filter.LocalGovernment))
            {
                query = query.Where(tr => tr.LocalGovernment == filter.LocalGovernment);
            }

            if (!string.IsNullOrEmpty(filter.VehicleType))
            {
                query = query.Where(tr => tr.VehicleType == filter.VehicleType);
            }

            if (filter.MinimumTaxRate.HasValue)
            {
                query = query.Where(tr => tr.Rate >= filter.MinimumTaxRate.Value);
            }

            if (filter.MaximumTaxRate.HasValue)
            {
                query = query.Where(tr => tr.Rate <= filter.MaximumTaxRate.Value);
            }

            // Return the first matching tax rate or null if none matches
            var result = await query.ToListAsync(cancellationToken);
            return result.AsReadOnly();
        }


        public async Task CreateTaxRate(TaxRate taxRate, CancellationToken cancellationToken)
        {
            await _context.TaxRates.AddAsync(taxRate, cancellationToken);
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