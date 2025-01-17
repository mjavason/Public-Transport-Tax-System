using Microsoft.AspNetCore.Mvc;
using PTTS.Application.Services;
using PTTS.Core.Domain.VehicleAggregate.Enums;

namespace PTTS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxRateController : ControllerBase
    {
        private readonly TaxRateService _taxRateService;

        public TaxRateController(TaxRateService taxRateService)
        {
            _taxRateService = taxRateService;
        }

        [HttpGet("{transportType}")]
        public async Task<IActionResult> GetTaxRate(VehicleType vehicleType)
        {
            var rate = await _taxRateService.CalculateTaxRateAsync(vehicleType);
            return Ok(rate);
        }
    }
}
