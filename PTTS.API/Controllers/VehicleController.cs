using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTTS.API.Filters.Model;
using PTTS.Application.Queries.PublicTransportVehicle;
using PTTS.Application.Commands.PublicTransportVehicle;
using PTTS.Core.Domain.VehicleAggregate.DTOs;

namespace PTTS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PublicTransportVehicleController : ApiBaseController
    {
        public PublicTransportVehicleController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [EndpointSummary("Get all vehicles")]
        [EndpointDescription("Retrieves a list of all public transport vehicles.")]
        [ProducesResponseType(typeof(SuccessResponse<IReadOnlyList<Core.Domain.VehicleAggregate.PublicTransportVehicle>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllVehicles()
        {
            var query = new GetAllVehiclesQuery();
            var result = await _mediator.Send(query);
            return GetActionResult(result, "Vehicles retrieved successfully");
        }

        [HttpGet("user")]
        [EndpointSummary("Get users vehicles")]
        [EndpointDescription("Retrieves a list of public transport vehicles associated with the authenticated user.")]
        [ProducesResponseType(typeof(SuccessResponse<IReadOnlyList<Core.Domain.VehicleAggregate.PublicTransportVehicle>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVehiclesByUserId()
        {
            string userId = GetUserId();
            var query = new GetVehiclesByUserIdQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return GetActionResult(result, "Users vehicles retrieved successfully");
        }

        [HttpGet("{id}")]
        [EndpointSummary("Get vehicle by ID")]
        [EndpointDescription("Retrieves a public transport vehicle by its ID.")]
        [ProducesResponseType(typeof(SuccessResponse<Core.Domain.VehicleAggregate.PublicTransportVehicle>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVehicleById([FromRoute] int id)
        {
            var query = new GetVehicleByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return GetActionResult(result, "Vehicle retrieved successfully");
        }

        [HttpPost]
        [EndpointSummary("Create a new vehicle")]
        [EndpointDescription("Creates a new public transport vehicle.")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleDto createVehicleDto)
        {
            string userId = GetUserId();
            var command = new CreateVehicleCommand { UserId = userId, VehicleType = createVehicleDto.VehicleType, Make = createVehicleDto.Make, Model = createVehicleDto.Model, PlateNumber = createVehicleDto.PlateNumber };
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : GetActionResult(result);
        }

        [HttpPatch]
        [EndpointSummary("Update an existing vehicle")]
        [EndpointDescription("Updates the details of an existing public transport vehicle.")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVehicle([FromBody] UpdateVehicleDto updateVehicleDto)
        {
            var command = new UpdateVehicleCommand { UpdateVehicleDto = updateVehicleDto };
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : GetActionResult(result);
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Delete a vehicle by ID")]
        [EndpointDescription("Deletes a public transport vehicle by its ID.")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int id)
        {
            var command = new DeleteVehicleCommand { Id = id };
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : GetActionResult(result);
        }
    }
}
