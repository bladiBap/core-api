using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestDevCore.Api.Contracts;
using TestDevCore.Application.Movements.Commands.CreateDeposit;
using TestDevCore.Application.Movements.Commands.CreateTransfer;
using TestDevCore.Application.Movements.Commands.CreateWithdraw;
using TestDevCore.Application.Movements.DTOs;
using TestDevCore.Application.Movements.Query.GetMovementHistoryByAccount;
using TestDevCore.Core.Results;

namespace TestDevCore.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/movement")]
    public class MovementController : BaseController
    {
        private readonly IMediator _mediator;

        public MovementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{id:guid}/deposits")]
        public async Task<IActionResult> CreateDeposit([FromRoute] Guid id, [FromBody] CreateMovementContract contract)
        {
            var command = new CreateDepositCommand(id, contract.Amount, contract.Description);
            Result<Guid> result = await _mediator.Send(command);
            return HandlerResult(result);
        }

        [HttpPost("{id:guid}/withdraw")]
        public async Task<IActionResult> CreateWithDraw([FromRoute] Guid id, [FromBody] CreateMovementContract contract)
        {
            var command = new CreateWithdrawCommand(id, contract.Amount, contract.Description);
            Result<Guid> result = await _mediator.Send(command);
            return HandlerResult(result);
        }

        [HttpPost("transfers")]
        public async Task<IActionResult> CreateTransfer([FromBody] CreateMovementTranferContract contract)
        {
            var command = new CreateTransferCommand(contract.SourceAccountId, contract.DestinationAccountId, contract.Amount, contract.Description);
            Result<Guid> result = await _mediator.Send(command);
            return HandlerResult(result);
        }

        [HttpGet("{id:guid}/history")]
        public async Task<IActionResult> GetHistoryByAccount([FromRoute] Guid id)
        {
            var query = new GetMovementHistoryByAccountQuery(id);
            Result<IEnumerable<MovementDTO>> result = await _mediator.Send(query);
            return HandlerResult(result);
        }
    }
}
