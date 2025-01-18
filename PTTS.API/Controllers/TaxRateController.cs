using MediatR;
using Microsoft.AspNetCore.Mvc;
using PTTS.Application.Commands.TaxRate;

namespace PTTS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxRateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaxRateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{vehicleType}")]
        public async Task<IActionResult> GetTaxRate([FromBody] CalculateTaxRateCommand query)
        {
            var rate = await _mediator.Send(query);
            return Ok(rate);
        }
    }
}
