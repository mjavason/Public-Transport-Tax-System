using System.ComponentModel.DataAnnotations;
using MediatR;
using PTTS.Core.Domain.TaxRateAggregate.Interfaces;

namespace PTTS.Application.Commands.TaxRate
{
    public class CalculateTaxRateCommand : IRequest<Core.Domain.TaxRateAggregate.TaxRate?>
    {
        [Required]
        public required string VehicleType { get; set; }
    }

    public class CalculateTaxRateCommandHandler : IRequestHandler<CalculateTaxRateCommand, Core.Domain.TaxRateAggregate.TaxRate?>
    {
        private readonly ITaxRateRepository _taxRateRepository;

        public CalculateTaxRateCommandHandler(ITaxRateRepository taxRateRepository)
        {
            _taxRateRepository = taxRateRepository;
        }

        public async Task<Core.Domain.TaxRateAggregate.TaxRate?> Handle(CalculateTaxRateCommand request, CancellationToken cancellationToken)
        {
            return await _taxRateRepository.GetTaxRateByTransportTypeAsync(request.VehicleType, cancellationToken);
        }
    }
}