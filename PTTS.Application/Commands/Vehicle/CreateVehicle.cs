using MediatR;
using PTTS.Core.Domain.Common;
using PTTS.Core.Domain.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Commands.PublicTransportVehicle
{
    public class CreateVehicleCommand : IRequest<Result>
    {
        public required string VehicleType { get; set; }
        public required string UserId { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public required string PlateNumber { get; set; }
    }

    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Result>
    {
        private readonly IPublicTransportVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateVehicleCommandHandler(IPublicTransportVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newVehicle = Core.Domain.VehicleAggregate.PublicTransportVehicle.Create(request.VehicleType, request.UserId, request.Make, request.Model, request.PlateNumber);
                await _vehicleRepository.CreateVehicleAsync(newVehicle, cancellationToken);
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
