
// UpdateTaxRateCommand
using MediatR;
using PTTS.Core.Domain.Common;
using PTTS.Core.Domain.TaxRateAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Commands.TaxRate
{
    public class UpdateTaxRateCommand : IRequest<Result>
    {
        public required UpdateTaxRateDto Update { get; set; }
    }

    public class UpdateTaxRateCommandHandler : IRequestHandler<UpdateTaxRateCommand, Result>
    {
        private readonly ITaxRateRepository _taxRateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaxRateCommandHandler(ITaxRateRepository taxRateRepository, IUnitOfWork unitOfWork)
        {
            _taxRateRepository = taxRateRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateTaxRateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var taxRateToUpdate = await _taxRateRepository.GetTaxRateByIdAsync(request.Update.TaxRateId, cancellationToken);
                if (taxRateToUpdate == null)
                    return Result.NotFound(["Tax rate not found."]);

                taxRateToUpdate.Update(request.Update);
                _taxRateRepository.UpdateTaxRate(taxRateToUpdate, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.BadRequest(new List<string> { ex.Message });
            }
        }
    }
}
