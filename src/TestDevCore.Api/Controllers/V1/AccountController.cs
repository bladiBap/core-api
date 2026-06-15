
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestDevCore.Api.Contracts;
using TestDevCore.Application.Accounts.Commands.Create;
using TestDevCore.Application.Accounts.DTOs;
using TestDevCore.Application.Accounts.Queries.GetBalanceReport;
using TestDevCore.Application.Accounts.Queries.GetById;
using TestDevCore.Core.Results;

namespace TestDevCore.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/account")]
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountContract contract)
        {
            var command = new CreateAccountCommand(contract.Holder, contract.Balance, contract.CurrencyId);
            Result<AccountDTO> result = await _mediator.Send(command);
            return HandlerResult(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var query = new GetAccountByIdQuery(id);
            Result<AccountDTO> result = await _mediator.Send(query);
            return HandlerResult(result);
        }

        [HttpGet("{id:guid}/balance-report")]
        public async Task<IActionResult> GetBalanceReport(
            [FromRoute] Guid id,
            [FromQuery] GetBalanceReportContract contract)
        {
            var query = new GetBalanceReportQuery(id, contract.StartDate, contract.EndDate, contract.Currency);
            Result<BalanceReportDTO> result = await _mediator.Send(query);
            return HandlerResult(result);
        }
    }
}
