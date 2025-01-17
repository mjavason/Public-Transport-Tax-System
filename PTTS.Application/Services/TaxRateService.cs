using MediatR;
using PTTS.Application.Commands.TaxRate;
using PTTS.Core.Domain.VehicleAggregate.Enums;

namespace PTTS.Application.Services
{
    public class TaxRateService
    {
        private readonly IMediator _mediator;

        public TaxRateService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<decimal> CalculateTaxRateAsync(VehicleType vehicleType)
        {
            return await _mediator.Send(new CalculateTaxRateCommand(vehicleType));
        }
    }
}
