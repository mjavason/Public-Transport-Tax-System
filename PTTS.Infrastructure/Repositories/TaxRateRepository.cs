using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PTTS.Core.Domain.TaxRateAggregate;
using PTTS.Core.Domain.TaxRateAggregate.Interfaces;
using PTTS.Core.Domain.VehicleAggregate.Enums;
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

        public async Task<decimal> GetTaxRateByTransportTypeAsync(VehicleType vehicleType)
        {
           var item = await _context.TaxRates
               .FirstOrDefaultAsync(t => t.VehicleType == vehicleType);

            return 1;
        }
    }
}