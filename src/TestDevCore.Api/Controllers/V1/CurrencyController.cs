using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestDevCore.Application.Currencies.DTOs;
using TestDevCore.Application.Currencies.Queries.GetAllCurrencies;
using TestDevCore.Core.Results;

namespace TestDevCore.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/currency")]
    public class CurrencyController : BaseController
    {
        private readonly IMediator _mediator;

        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCurrenciesQuery();
            Result<IEnumerable<CurrencyDTO>> result = await _mediator.Send(query);
            return HandlerResult(result);
        }
    }
}