using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTTS.API.Filters.Model;
using PTTS.Application.Commands.TaxRate;
using PTTS.Application.Queries.TaxRate;
using PTTS.Core.Domain.TaxRateAggregate.DTOs;

namespace PTTS.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Roles = "Admin")]
	public class TaxRateController : ApiBaseController
	{
		public TaxRateController(IMediator mediator) : base(mediator) { }

		[HttpGet]
		[EndpointSummary("Retrieve all tax rates")]
		[EndpointDescription("Returns a complete list of all tax rates stored in the system.")]
		[ProducesResponseType(typeof(SuccessResponse<IReadOnlyList<Core.Domain.TaxRateAggregate.TaxRate>>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetAllTaxRates([FromRoute] GetAllTaxRatesQuery query)
		{
			var result = await _mediator.Send(query);
			return GetActionResult(result, "Rates retrieved successfully");
		}

		[HttpGet("/filter")]
		[EndpointSummary("Filter tax rates")]
		[EndpointDescription("Filters the list of tax rates based on the provided parameters such as vehicle type, rate, and local government.")]
		[ProducesResponseType(typeof(SuccessResponse<IReadOnlyList<Core.Domain.TaxRateAggregate.TaxRate>>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> FilterTaxRate([FromQuery] FilterTaxRateDto filter)
		{
			var query = new FilterTaxRateQuery { Filter = filter };
			var rates = await _mediator.Send(query);

			return GetActionResult(rates, "Rates retrieved successfully");
		}

		[HttpGet("{TaxRateId}")]
		[EndpointSummary("Retrieve tax rate by ID")]
		[EndpointDescription("Fetches the details of a specific tax rate using its unique identifier.")]
		[ProducesResponseType(typeof(SuccessResponse<Core.Domain.TaxRateAggregate.TaxRate>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetTaxRatesById([FromRoute] GetTaxRateByIdQuery query)
		{
			var result = await _mediator.Send(query);
			return GetActionResult(result, "Rate retrieved successfully");
		}

		[HttpPost]
		[EndpointSummary("Create a new tax rate")]
		[EndpointDescription("Adds a new tax rate to the system with details like vehicle type, rate, and local government.")]
		[ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> CreateTaxRate([FromBody] CreateTaxRateCommand command)
		{
			var result = await _mediator.Send(command);
			return result.IsSuccess ? NoContent() : GetActionResult(result);
		}

		[HttpPatch]
		[EndpointSummary("Update an existing tax rate")]
		[EndpointDescription("Modifies the details of an existing tax rate, including vehicle type, rate, and local government.")]
		[ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> UpdateTaxRate([FromBody] UpdateTaxRateDto update)
		{
			var command = new UpdateTaxRateCommand { Update = update };
			var result = await _mediator.Send(command);

			return result.IsSuccess ? NoContent() : GetActionResult(result);
		}

		[HttpDelete("{TaxRateId}")]
		[EndpointSummary("Delete a tax rate by ID")]
		[EndpointDescription("Removes a tax rate from the system using its unique identifier.")]
		[ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> DeleteTaxRate([FromRoute] DeleteTaxRateCommand command)
		{
			var result = await _mediator.Send(command);
			return result.IsSuccess ? NoContent() : GetActionResult(result);
		}
	}
}
