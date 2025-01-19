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

        // GET api/publictransportvehicle
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

        // POST api/publictransportvehicle
        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleDto createVehicleDto)
        {
            string userId = GetUserId();
            var command = new CreateVehicleCommand { UserId = userId, VehicleType = createVehicleDto.vehicleType };
            var result = await _mediator.Send(command);
            return GetActionResult(result, "Vehicle created successfully");
        }

        // PUT api/publictransportvehicle
        [HttpPut]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> UpdateVehicle([FromBody] UpdateVehicleCommand command)
        {
            var result = await _mediator.Send(command);
            return GetActionResult(result, "Vehicle updated successfully");
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
            return GetActionResult(result, "Vehicle deleted successfully");
        }

        // GET api/publictransportvehicle/exists/{id}
        [HttpGet("exists/{id}")]
        [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> VehicleExists([FromRoute] int id)
        {
            var query = new CheckVehicleExistsQuery { Id = id };
            var result = await _mediator.Send(query);
            return GetActionResult(result, "Vehicle existence checked successfully");
        }

        // GET api/publictransportvehicle/user/{userId}
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(SuccessResponse<IReadOnlyList<Core.Domain.VehicleAggregate.PublicTransportVehicle>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetVehiclesByUserId([FromRoute] string userId)
        {
            var query = new GetVehiclesByUserIdQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return GetActionResult(result, "Vehicles retrieved by user successfully");
        }
    }
}