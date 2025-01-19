using MediatR;
using Microsoft.AspNetCore.Mvc;
using PTTS.API.Filters.Model;
using PTTS.Application.Queries.TaxRate;

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
        public async Task<IActionResult> GetTaxRate([FromQuery] CalculateTaxRateQuery query)
        {
            var rate = await _mediator.Send(query);
            return GetActionResult(rate, "Rate retrieved successfully");
        }
    }
}
