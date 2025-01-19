using System.ComponentModel.DataAnnotations;
using MediatR;
using PTTS.Core.Domain.TaxRateAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Queries.TaxRate
{
    public class CalculateTaxRateQuery : IRequest<Result<Core.Domain.TaxRateAggregate.TaxRate>>
    {
        [Required]
        public required string VehicleType { get; set; }
    }

    public class CalculateTaxRateQueryHandler : IRequestHandler<CalculateTaxRateQuery, Result<Core.Domain.TaxRateAggregate.TaxRate>>
    {
        private readonly ITaxRateRepository _taxRateRepository;

        public CalculateTaxRateQueryHandler(ITaxRateRepository taxRateRepository)
        {
            _taxRateRepository = taxRateRepository;
        }

        public async Task<Result<Core.Domain.TaxRateAggregate.TaxRate>> Handle(CalculateTaxRateQuery request, CancellationToken cancellationToken)
        {
            var result = await _taxRateRepository.GetTaxRateByTransportTypeAsync(request.VehicleType, cancellationToken);

            return result == null ?
            Result.NotFound<Core.Domain.TaxRateAggregate.TaxRate>(["No tax rate matching vehicle type found"]) :
            Result.Success(result);
        }
    }
}