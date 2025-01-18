using MediatR;
using PTTS.Core.Domain.Common;
using PTTS.Core.Domain.TaxRateAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Commands.TaxRate
{
    public class CreateTaxRateCommand : IRequest<Result>
    {
        public required string VehicleType { get; set; }
        public required decimal Rate { get; set; }
    }

    public class CreateTaxRateCommandHandler : IRequestHandler<CreateTaxRateCommand, Result>
    {
        private readonly ITaxRateRepository _taxRateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTaxRateCommandHandler(ITaxRateRepository taxRateRepository, IUnitOfWork unitOfWork)
        {
            _taxRateRepository = taxRateRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateTaxRateCommand request, CancellationToken cancellationToken)
        {
            var newTaxRate = Core.Domain.TaxRateAggregate.TaxRate.Create(request.VehicleType, request.Rate);

            await _taxRateRepository.CreateTaxRate(newTaxRate, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}