using MediatR;
using PTTS.Core.Domain.Common;
using PTTS.Core.Domain.Interfaces;
using PTTS.Core.Domain.VehicleAggregate.DTOs;
using PTTS.Core.Shared;

namespace PTTS.Application.Commands.PublicTransportVehicle
{
    public class UpdateVehicleCommand : IRequest<Result>
    {
        public required UpdateVehicleDto updateVehicleDto { get; set; }
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
                var vehicle = await _vehicleRepository.GetVehicleByIdAsync(request.updateVehicleDto.VehicleId, cancellationToken);
                if (vehicle == null)
                    return Result.NotFound(["Vehicle not found"]);

                vehicle.Update(request.updateVehicleDto);
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
