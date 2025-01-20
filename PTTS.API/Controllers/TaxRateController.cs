using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTTS.API.Filters.Model;
using PTTS.Application.Queries.TaxRate;
using PTTS.Core.Domain.TaxRateAggregate.DTOs;

namespace PTTS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxRateController : ApiBaseController
    {
        public TaxRateController(IMediator mediator) : base(mediator) { }

        [HttpGet()]
        [ProducesResponseType(typeof(SuccessResponse<Core.Domain.TaxRateAggregate.TaxRate>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> FilterTaxRate([FromQuery] FilterTaxRateDto filter)
        {
            var query = new FilterTaxRateQuery { Filter = filter };
            var rates = await _mediator.Send(query);
            // throw new Exception("Just an exception");
            return GetActionResult(rates, "Rates retrieved successfully");
        }
    }
}
