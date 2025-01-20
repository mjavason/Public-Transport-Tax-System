// DeleteTaxRateCommand
using MediatR;
using PTTS.Core.Domain.Common;
using PTTS.Core.Domain.TaxRateAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Commands.TaxRate
{
    public class DeleteTaxRateCommand : IRequest<Result>
    {
        public required int TaxRateId { get; set; }
    }

    public class DeleteTaxRateCommandHandler : IRequestHandler<DeleteTaxRateCommand, Result>
    {
        private readonly ITaxRateRepository _taxRateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaxRateCommandHandler(ITaxRateRepository taxRateRepository, IUnitOfWork unitOfWork)
        {
            _taxRateRepository = taxRateRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteTaxRateCommand request, CancellationToken cancellationToken)
        {
            var taxRateToDelete = await _taxRateRepository.GetTaxRateByIdAsync(request.TaxRateId, cancellationToken);

            if (taxRateToDelete == null)
                return Result.NotFound(["Tax rate not found."]);

            await _taxRateRepository.DeleteTaxRateAsync(request.TaxRateId, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
