using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTTS.API.Filters.Model;
using PTTS.Application.Commands.TaxRate;
using PTTS.Application.Queries.TaxRate;
using PTTS.Core.Domain.TaxRateAggregate.DTOs;
using PTTS.Core.Shared;

namespace PTTS.API.Controllers
{
    [ApiController]
    [Route("api[controller]")]
    public class TaxRateController : ApiBaseController
    {
        public TaxRateController(IMediator mediator) : base(mediator) { }

        [HttpGet()]
        [ProducesResponseType(typeof(SuccessResponse<IReadOnlyList<Core.Domain.TaxRateAggregate.TaxRate>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> FilterTaxRate([FromQuery] FilterTaxRateDto filter)
        {
            var query = new FilterTaxRateQuery { Filter = filter };

            var rates = await _mediator.Send(query);
            return GetActionResult(rates, "Rates retrieved successfully");
        }

        [HttpPost()]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> CreateTaxRate([FromBody] CreateTaxRateCommand command)
        {
            var result = await _mediator.Send(command);
            return GetActionResult(result, "Tax rate created successfully");
        }
    }
}
