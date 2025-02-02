using MediatR;
using PTTS.Core.Domain;
using PTTS.Core.Domain.Interfaces;
using PTTS.Core.Shared;
using System.Collections.Generic;

namespace PTTS.Application.Queries.PublicTransportVehicle
{
	public class GetVehiclesByUserIdQuery : IRequest<Result<IReadOnlyList<Core.Domain.VehicleAggregate.PublicTransportVehicle>>>
	{
		public required string UserId { get; set; }
	}

	public class GetVehiclesByUserIdQueryHandler : IRequestHandler<GetVehiclesByUserIdQuery, Result<IReadOnlyList<Core.Domain.VehicleAggregate.PublicTransportVehicle>>>
	{
		private readonly IPublicTransportVehicleRepository _vehicleRepository;

		public GetVehiclesByUserIdQueryHandler(IPublicTransportVehicleRepository vehicleRepository)
		{
			_vehicleRepository = vehicleRepository;
		}

		public async Task<Result<IReadOnlyList<Core.Domain.VehicleAggregate.PublicTransportVehicle>>> Handle(GetVehiclesByUserIdQuery request, CancellationToken cancellationToken)
		{
			var vehicles = await _vehicleRepository.GetVehiclesByUserIdAsync(request.UserId, cancellationToken);
			return Result.Success(vehicles);
		}
	}
}
