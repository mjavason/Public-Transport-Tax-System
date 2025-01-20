using MediatR;
using PTTS.Core.Domain.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Queries.PublicTransportVehicle
{
    public class GetAllVehiclesQuery : IRequest<Result<IReadOnlyList<Core.Domain.VehicleAggregate.PublicTransportVehicle>>>
    {
    }

    public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, Result<IReadOnlyList<Core.Domain.VehicleAggregate.PublicTransportVehicle>>>
    {
        private readonly IPublicTransportVehicleRepository _vehicleRepository;

        public GetAllVehiclesQueryHandler(IPublicTransportVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Result<IReadOnlyList<Core.Domain.VehicleAggregate.PublicTransportVehicle>>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.GetAllVehiclesAsync(cancellationToken);
            return Result.Success(vehicles);
        }
    }
}
