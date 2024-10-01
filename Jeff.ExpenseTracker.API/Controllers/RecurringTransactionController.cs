using Jeff.ExpenseTracker.Contracts.DTOs;
using Jeff.ExpenseTracker.Core.Exceptions;
using Jeff.ExpenseTracker.Core.Handlers.Commands;
using Jeff.ExpenseTracker.Core.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jeff.ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecurringTransactionController : ExpenseTrackerControllerBase
    {
        private readonly IMediator mediator;

        public RecurringTransactionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRecurringTransactionDTO model)
        {
            try
            {
                this.SetEmailId(model);
                var command = new CreateRecurringTransaction(model);
                var id = await this.mediator.Send(command);
                return Ok(id);
            }
            catch (InvalidRequestException ex)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateRecurringTransactionDTO model)
        {
            try
            {
                this.SetEmailId(model);
                var command = new UpdateRecurringTransaction(model);
                var RecurringTransaction = await this.mediator.Send(command);
                return Ok(RecurringTransaction);
            }
            catch (InvalidRequestException ex)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var model = new UserSpecificDTO();
                this.SetEmailId(model);
                var query = new GetRecurringTransactionsByEmailId(model);
                var RecurringTransactions = await this.mediator.Send(query);
                return Ok(RecurringTransactions);
            }
            catch (InvalidRequestException ex)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteEntityDTO model)
        {
            try
            {
                var command = new DeleteRecurringTransaction(model);
                var id = await this.mediator.Send(command);
                return Ok(id);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    Errors = new string[] { ex.Error }
                });
            }
        }
    }
}
