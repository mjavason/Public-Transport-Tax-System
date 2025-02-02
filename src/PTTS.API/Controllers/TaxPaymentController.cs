using MediatR;
using Microsoft.AspNetCore.Mvc;
using PTTS.API.Filters.Model;
using PTTS.Application.Commands.TaxPayment;
using PTTS.Application.Queries.TaxPayment;

namespace PTTS.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TaxPaymentController : ApiBaseController
	{
		public TaxPaymentController(IMediator mediator) : base(mediator) { }

		[HttpGet]
		[EndpointSummary("Retrieve all tax payments")]
		[EndpointDescription("Returns a complete list of all tax payments stored in the system.")]
		[ProducesResponseType(typeof(SuccessResponse<IReadOnlyList<Core.Domain.TaxPaymentAggregate.TaxPayment>>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetAllTaxPayments([FromRoute] GetAllTaxPaymentsQuery query)
		{
			var result = await _mediator.Send(query);
			return GetActionResult(result, "Payments retrieved successfully");
		}

		// [HttpGet("/filter")]
		// [EndpointSummary("Filter tax payments")]
		// [EndpointDescription("Filters the list of tax payments based on the provided parameters such as vehicle type, amount, and local government.")]
		// [ProducesResponseType(typeof(SuccessResponse<IReadOnlyList<Core.Domain.TaxPaymentAggregate.TaxPayment>>), StatusCodes.Status200OK)]
		// [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
		// public async Task<IActionResult> FilterTaxPayment([FromQuery] FilterTaxPaymentDto filter)
		// {
		// 	var query = new FilterTaxPaymentQuery { Filter = filter };
		// 	var payments = await _mediator.Send(query);

		// 	return GetActionResult(payments, "Payments retrieved successfully");
		// }

		// [HttpGet("{TaxPaymentId}")]
		// [EndpointSummary("Retrieve tax payment by ID")]
		// [EndpointDescription("Fetches the details of a specific tax payment using its unique identifier.")]
		// [ProducesResponseType(typeof(SuccessResponse<Core.Domain.TaxPaymentAggregate.TaxPayment>), StatusCodes.Status200OK)]
		// [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
		// public async Task<IActionResult> GetTaxPaymentById([FromRoute] GetTaxPaymentByIdQuery query)
		// {
		// 	var result = await _mediator.Send(query);
		// 	return GetActionResult(result, "Payment retrieved successfully");
		// }

		[HttpPost]
		[EndpointSummary("Create a new tax payment")]
		[EndpointDescription("Adds a new tax payment to the system with details like vehicle type, amount, and local government.")]
		[ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> CreateTaxPayment([FromBody] CreateTaxPaymentCommand command)
		{
			var result = await _mediator.Send(command);
			return result.IsSuccess ? NoContent() : GetActionResult(result);
		}

		// [HttpPatch]
		// [EndpointSummary("Update an existing tax payment")]
		// [EndpointDescription("Modifies the details of an existing tax payment, including vehicle type, amount, and local government.")]
		// [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status204NoContent)]
		// [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
		// public async Task<IActionResult> UpdateTaxPayment([FromBody] UpdateTaxPaymentDto update)
		// {
		// 	var command = new UpdateTaxPaymentCommand { Update = update };
		// 	var result = await _mediator.Send(command);

		// 	return result.IsSuccess ? NoContent() : GetActionResult(result);
		// }

		// [HttpDelete("{TaxPaymentId}")]
		// [EndpointSummary("Delete a tax payment by ID")]
		// [EndpointDescription("Removes a tax payment from the system using its unique identifier.")]
		// [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status204NoContent)]
		// [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
		// public async Task<IActionResult> DeleteTaxPayment([FromRoute] DeleteTaxPaymentCommand command)
		// {
		// 	var result = await _mediator.Send(command);
		// 	return result.IsSuccess ? NoContent() : GetActionResult(result);
		// }
	}
}