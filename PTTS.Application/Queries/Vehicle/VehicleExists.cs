using MediatR;
using PTTS.Core.Domain.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Queries.PublicTransportVehicle
{
    public class CheckVehicleExistsQuery : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }

    public class CheckVehicleExistsQueryHandler : IRequestHandler<CheckVehicleExistsQuery, Result<bool>>
    {
        private readonly IPublicTransportVehicleRepository _vehicleRepository;

        public CheckVehicleExistsQueryHandler(IPublicTransportVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Result<bool>> Handle(CheckVehicleExistsQuery request, CancellationToken cancellationToken)
        {
            var exists = await _vehicleRepository.VehicleExistsAsync(request.Id, cancellationToken);
            return Result.Success(exists);
        }
    }
}
