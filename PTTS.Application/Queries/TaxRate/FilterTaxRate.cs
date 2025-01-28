using System.ComponentModel.DataAnnotations;
using MediatR;
using PTTS.Core.Domain.TaxRateAggregate.DTOs;
using PTTS.Core.Domain.TaxRateAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Queries.TaxRate
{
	public class FilterTaxRateQuery : IRequest<Result<IReadOnlyList<Core.Domain.TaxRateAggregate.TaxRate>>>
	{
		[Required]
		public required FilterTaxRateDto Filter;
    }

	public class FilterTaxRateQueryHandler : IRequestHandler<FilterTaxRateQuery, Result<IReadOnlyList<Core.Domain.TaxRateAggregate.TaxRate>>>
	{
		private readonly ITaxRateRepository _taxRateRepository;

		public FilterTaxRateQueryHandler(ITaxRateRepository taxRateRepository)
		{
			_taxRateRepository = taxRateRepository;
		}

		public async Task<Result<IReadOnlyList<Core.Domain.TaxRateAggregate.TaxRate>>> Handle(FilterTaxRateQuery request, CancellationToken cancellationToken)
		{
			var result = await _taxRateRepository.FilterTaxRateAsync(request.Filter, cancellationToken);
			return Result.Success(result);
		}
	}
}
