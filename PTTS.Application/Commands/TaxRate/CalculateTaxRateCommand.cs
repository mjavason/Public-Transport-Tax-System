using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PTTS.Core.Domain.TaxRateAggregate.Interfaces;
using PTTS.Core.Domain.VehicleAggregate.Enums;

namespace PTTS.Application.Commands.TaxRate
{
    public class CalculateTaxRateCommand : IRequest<decimal>
    {
        public VehicleType VehicleType { get; set; }

        public CalculateTaxRateCommand(VehicleType vehicleType)
        {
            VehicleType = vehicleType;
        }
    }

    public class CalculateTaxRateCommandHandler : IRequestHandler<CalculateTaxRateCommand, decimal>
    {
        private readonly ITaxRateRepository _taxRateRepository;

        public CalculateTaxRateCommandHandler(ITaxRateRepository taxRateRepository)
        {
            _taxRateRepository = taxRateRepository;
        }

        public async Task<decimal> Handle(CalculateTaxRateCommand request, CancellationToken cancellationToken)
        {
            return await _taxRateRepository.GetTaxRateByTransportTypeAsync(request.VehicleType);
        }
    }
}