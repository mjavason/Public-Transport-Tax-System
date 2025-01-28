using System;
using MediatR;
using PTTS.Core.Domain;
using PTTS.Core.Domain.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Queries.PublicTransportVehicle
{
	public class GetVehicleByIdQuery : IRequest<Result<Core.Domain.VehicleAggregate.PublicTransportVehicle>>
	{
		public int Id { get; set; }
	}

	public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, Result<Core.Domain.VehicleAggregate.PublicTransportVehicle>>
	{
		private readonly IPublicTransportVehicleRepository _vehicleRepository;

		public GetVehicleByIdQueryHandler(IPublicTransportVehicleRepository vehicleRepository)
		{
			_vehicleRepository = vehicleRepository;
		}

		public async Task<Result<Core.Domain.VehicleAggregate.PublicTransportVehicle>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
		{
			var vehicle = await _vehicleRepository.GetVehicleByIdAsync(request.Id, cancellationToken);

			return vehicle == null ?
				Result.NotFound<Core.Domain.VehicleAggregate.PublicTransportVehicle>(["Vehicle not found"]) :
                Result.Success(vehicle);
		}
	}
}
