using MediatR;
using PTTS.Core.Domain;
using PTTS.Core.Domain.Common;
using PTTS.Core.Domain.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Commands.PublicTransportVehicle
{
    public class UpdateVehicleCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string? VehicleType { get; set; }
        // public string UserId { get; set; }
    }

    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, Result>
    {
        private readonly IPublicTransportVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVehicleCommandHandler(IPublicTransportVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicle = await _vehicleRepository.GetVehicleByIdAsync(request.Id, cancellationToken);
                if (vehicle == null)
                    return Result.NotFound<Core.Domain.VehicleAggregate.PublicTransportVehicle>(["Vehicle not found"]);

                // Update the vehicle properties
                // vehicle = Core.Domain.VehicleAggregate.PublicTransportVehicle.Create(request.VehicleType, request.UserId);
                if (request.VehicleType is not null) Core.Domain.VehicleAggregate.PublicTransportVehicle.UpdateVehicleType(vehicle, request.VehicleType);

                _vehicleRepository.UpdateVehicle(vehicle, cancellationToken);
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
