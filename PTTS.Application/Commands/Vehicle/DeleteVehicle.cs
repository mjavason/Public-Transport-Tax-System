using MediatR;
using PTTS.Core.Domain.Common;
using PTTS.Core.Domain.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Commands.PublicTransportVehicle
{
    public class DeleteVehicleCommand : IRequest<Result>
    {
        public required int Id { get; set; }
        public required string UserId { get; set; }
    }

    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, Result>
    {
        private readonly IPublicTransportVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVehicleCommandHandler(IPublicTransportVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {

            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(request.Id, cancellationToken);
            if (vehicle == null)
                return Result.NotFound(new List<string> { "Vehicle not found" });
            if (vehicle.UserId != request.UserId)
                return Result.Unauthorized(new List<string> { "Unauthorized" });

            _vehicleRepository.DeleteVehicleAsync(vehicle, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
