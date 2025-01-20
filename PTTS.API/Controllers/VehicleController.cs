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
    public class PublicTransportVehicleController : ApiBaseController
    {
        public PublicTransportVehicleController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [ProducesResponseType(typeof(SuccessResponse<IReadOnlyList<Core.Domain.VehicleAggregate.PublicTransportVehicle>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetAllVehicles()
        {
            var query = new GetAllVehiclesQuery();
            var result = await _mediator.Send(query);
            return GetActionResult(result, "Vehicles retrieved successfully");
        }

        [HttpGet("user")]
        [EndpointSummary("Get users vehicles")]
        [ProducesResponseType(typeof(SuccessResponse<IReadOnlyList<Core.Domain.VehicleAggregate.PublicTransportVehicle>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetVehiclesByUserId()
        {
            string userId = GetUserId();
            var query = new GetVehiclesByUserIdQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return GetActionResult(result, "Users vehicles retrieved successfully");
        }

        // GET api/publictransportvehicle/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SuccessResponse<Core.Domain.VehicleAggregate.PublicTransportVehicle>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetVehicleById([FromRoute] int id)
        {
            var query = new GetVehicleByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return GetActionResult(result, "Vehicle retrieved successfully");
        }

        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleDto createVehicleDto)
        {
            string userId = GetUserId();
            var command = new CreateVehicleCommand { UserId = userId, VehicleType = createVehicleDto.vehicleType };
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : GetActionResult(result);
        }

        // PUT api/publictransportvehicle
        [HttpPut]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> UpdateVehicle([FromBody] UpdateVehicleCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : GetActionResult(result);
        }

        // DELETE api/publictransportvehicle/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int id)
        {
            var command = new DeleteVehicleCommand { Id = id };
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : GetActionResult(result);
        }
    }
}