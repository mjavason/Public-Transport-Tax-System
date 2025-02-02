using MediatR;
using PTTS.Core.Domain.Common;
using PTTS.Core.Domain.Constants;
using PTTS.Core.Domain.Interfaces;
using PTTS.Core.Domain.TaxPaymentAggregate.DTOs;
using PTTS.Core.Domain.TaxPaymentAggregate.Interfaces;
using PTTS.Core.Domain.TaxRateAggregate.DTOs;
using PTTS.Core.Domain.TaxRateAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Commands.TaxPayment
{
	public class CreateTaxPaymentCommand : IRequest<Result>
	{
		public required string LocalGovernment { get; set; }
		public required int vehicleId { get; set; }
	}

	public class CreateTaxPaymentCommandHandler : IRequestHandler<CreateTaxPaymentCommand, Result>
	{
		private readonly ITaxPaymentRepository _taxPaymentRepository;
		private readonly IPublicTransportVehicleRepository _vehicleRepository;
		private readonly ITaxRateRepository _taxRateRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CreateTaxPaymentCommandHandler(ITaxPaymentRepository taxPaymentRepository, IPublicTransportVehicleRepository vehicleRepository, ITaxRateRepository taxRateRepository, IUnitOfWork unitOfWork)
		{
			_taxPaymentRepository = taxPaymentRepository;
			_vehicleRepository = vehicleRepository;
			_taxRateRepository = taxRateRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(CreateTaxPaymentCommand request, CancellationToken cancellationToken)
		{

			var vehicle = await _vehicleRepository.GetVehicleByIdAsync(request.vehicleId, cancellationToken);
			if (vehicle is null) return Result.NotFound(["Vehicle does not exist"]);

			//validate local government
			if (!AppConstants.EnuguLocalGovernments.Contains(request.LocalGovernment)) return Result.BadRequest([$"Invalid local government: {request.LocalGovernment}. Valid options are: {string.Join(", ", AppConstants.EnuguLocalGovernments)}."]);

			var filterTaxRateDto = new FilterTaxRateDto { LocalGovernment = request.LocalGovernment, VehicleType = vehicle.VehicleType };
			var taxRate = await _taxRateRepository.FilterTaxRateAsync(filterTaxRateDto, cancellationToken);
			if (taxRate == null || !taxRate.Any()) return Result.NotFound(["Tax rate unavailable for LGA and Vehicle type"]);

			var filter = new FilterTaxPaymentDto { TaxPayerId = vehicle.UserId, MinimumDate = DateTime.Today.Date };
			var existingPayment = await _taxPaymentRepository.FilterTaxPaymentAsync(filter, cancellationToken);
			if (existingPayment != null && existingPayment.Any()) return Result.BadRequest(["A tax payment has already been made for this vehicle today"]);

			var newTaxPayment = Core.Domain.TaxPaymentAggregate.TaxPayment.Create(request.LocalGovernment, taxRate[0].Rate, vehicle.User.FullName, vehicle.UserId, vehicle.Id);
			_taxPaymentRepository.CreateTaxPayment(newTaxPayment, cancellationToken);
			
			await _unitOfWork.SaveChangesAsync(cancellationToken);
			return Result.Success();
		}
	}
}