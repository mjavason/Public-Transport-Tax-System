using MediatR;
using PTTS.Core.Domain.TaxPaymentAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Queries.TaxPayment
{
	public class GetAllTaxPaymentsQuery : IRequest<Result<IReadOnlyList<Core.Domain.TaxPaymentAggregate.TaxPayment>>>
	{
	}

	public class GetAllTaxPaymentsQueryHandler : IRequestHandler<GetAllTaxPaymentsQuery, Result<IReadOnlyList<Core.Domain.TaxPaymentAggregate.TaxPayment>>>
	{
		private readonly ITaxPaymentRepository _taxPaymentRepository;

		public GetAllTaxPaymentsQueryHandler(ITaxPaymentRepository taxPaymentRepository)
		{
			_taxPaymentRepository = taxPaymentRepository;
		}

		public async Task<Result<IReadOnlyList<Core.Domain.TaxPaymentAggregate.TaxPayment>>> Handle(GetAllTaxPaymentsQuery request, CancellationToken cancellationToken)
		{
			var result = await _taxPaymentRepository.GetAllTaxPaymentsAsync(cancellationToken);
			return Result.Success(result);
		}
	}
}