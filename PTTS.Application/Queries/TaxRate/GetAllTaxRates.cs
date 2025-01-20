using MediatR;
using PTTS.Core.Domain.TaxRateAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Queries.TaxRate
{
    public class GetAllTaxRatesQuery : IRequest<Result<IReadOnlyList<Core.Domain.TaxRateAggregate.TaxRate>>>
    {
    }

    public class GetAllTaxRatesQueryHandler : IRequestHandler<GetAllTaxRatesQuery, Result<IReadOnlyList<Core.Domain.TaxRateAggregate.TaxRate>>>
    {
        private readonly ITaxRateRepository _taxRateRepository;

        public GetAllTaxRatesQueryHandler(ITaxRateRepository taxRateRepository)
        {
            _taxRateRepository = taxRateRepository;
        }

        public async Task<Result<IReadOnlyList<Core.Domain.TaxRateAggregate.TaxRate>>> Handle(GetAllTaxRatesQuery request, CancellationToken cancellationToken)
        {
            var result = await _taxRateRepository.GetAllTaxRatesAsync(cancellationToken);
            return Result.Success(result);
        }
    }
}