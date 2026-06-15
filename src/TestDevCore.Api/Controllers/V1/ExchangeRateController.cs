using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestDevCore.Application.ExchangeRates.DTOs;
using TestDevCore.Application.ExchangeRates.Commands.GetCurrentExchangeRate;
using TestDevCore.Core.Results;

namespace TestDevCore.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/exchange-rate")]
    public class ExchangeRateController : BaseController
    {
        private readonly IMediator _mediator;

        public ExchangeRateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent([FromQuery] string symbolBase, [FromQuery] string symbolTarget)
        {
            var command = new GetCurrentExchangeRateCommand(symbolBase, symbolTarget);
            Result<ExchangeRateDTO> result = await _mediator.Send(command);
            return HandlerResult(result);
        }
    }
}