using MediatR;
using PTTS.Core.Domain.TaxRateAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Queries.TaxRate
{
    public class GetTaxRateByIdQuery : IRequest<Result<Core.Domain.TaxRateAggregate.TaxRate>>
    {
        public required int TaxRateId { get; set; }
    }

    public class GetTaxRateByIdQueryHandler : IRequestHandler<GetTaxRateByIdQuery, Result<Core.Domain.TaxRateAggregate.TaxRate>>
    {
        private readonly ITaxRateRepository _taxRateRepository;

        public GetTaxRateByIdQueryHandler(ITaxRateRepository taxRateRepository)
        {
            _taxRateRepository = taxRateRepository;
        }

        public async Task<Result<Core.Domain.TaxRateAggregate.TaxRate>> Handle(GetTaxRateByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _taxRateRepository.GetTaxRateByIdAsync(request.TaxRateId, cancellationToken);

            return result == null ?
                Result.NotFound<Core.Domain.TaxRateAggregate.TaxRate>(["Tax rate not found."])
                : Result.Success(result);
        }
    }
}