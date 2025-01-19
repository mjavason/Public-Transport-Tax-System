using MediatR;
using PTTS.Core.Domain.Interfaces;

namespace PTTS.Application.Queries.PublicTransportVehicle
{
    public class CheckVehicleExistsQuery : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class CheckVehicleExistsQueryHandler : IRequestHandler<CheckVehicleExistsQuery, bool>
    {
        private readonly IPublicTransportVehicleRepository _vehicleRepository;

        public CheckVehicleExistsQueryHandler(IPublicTransportVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<bool> Handle(CheckVehicleExistsQuery request, CancellationToken cancellationToken)
        {
            return await _vehicleRepository.VehicleExistsAsync(request.Id, cancellationToken);
        }
    }
}
