using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet(Name = "GetTaxRate")]
        public async Task<IActionResult> GetTaxRate([FromQuery] CalculateTaxRateCommand query)
        {
            var rate = await _mediator.Send(query);
            if(rate == null) return NotFound();
            
            return Ok(rate);
        }
    }
}
