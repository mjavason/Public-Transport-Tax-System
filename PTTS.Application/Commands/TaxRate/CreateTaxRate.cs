using MediatR;
using PTTS.Core.Domain.Common;
using PTTS.Core.Domain.Constants;
using PTTS.Core.Domain.TaxRateAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Commands.TaxRate
{
    public class CreateTaxRateCommand : IRequest<Result>
    {
        public required string VehicleType { get; set; }
        public required decimal Rate { get; set; }
        public required string LocalGovernment { get; set; }
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
            // if (!AppConstants.EnuguLocalGovernments.Contains(request.LocalGovernment))
            //     return Result.BadRequest([$"Invalid LGA: {request.VehicleType}. Must be one of {string.Join(", ", AppConstants.EnuguLocalGovernments)}", nameof(request.LocalGovernment)]);
            // if (!AppConstants.VehicleTypes.Contains(request.VehicleType))
            //     return Result.BadRequest([$"Invalid vehicle type: {request.VehicleType}. Must be one of {string.Join(", ", AppConstants.VehicleTypes)}", nameof(request.VehicleType)]);
            // if (request.Rate < 1) return Result.BadRequest(["Invalid rate. Must be greater than 0"]);
            try
            {
                var newTaxRate = Core.Domain.TaxRateAggregate.TaxRate.Create(request.LocalGovernment, request.VehicleType, request.Rate);

                await _taxRateRepository.CreateTaxRate(newTaxRate, cancellationToken);
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