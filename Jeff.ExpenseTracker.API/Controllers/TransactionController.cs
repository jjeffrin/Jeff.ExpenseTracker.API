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
    public class TransactionController : ExpenseTrackerControllerBase
    {
        private readonly IMediator mediator;

        public TransactionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTransactionDTO model)
        {
            try
            {
                this.SetEmailId(model);
                var command = new CreateTransaction(model);
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
        public async Task<IActionResult> Put([FromBody] UpdateTransactionDTO model)
        {
            try
            {
                this.SetEmailId(model);
                var command = new UpdateTransaction(model);
                var transaction = await this.mediator.Send(command);
                return Ok(transaction);
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
                var query = new GetTransactionsByEmailId(model);
                var transactions = await this.mediator.Send(query);
                return Ok(transactions);
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

        [HttpGet]
        [Route("GetCurrMonthExpenses")]
        public async Task<IActionResult> Get(DateTime currMonth)
        {
            try
            {
                var model = new TimeSpecificDTO() { UpdatedOn = currMonth };
                this.SetEmailId(model);
                var query = new GetCurrentMonthTransactionsByEmailId(model);
                var transactions = await this.mediator.Send(query);
                return Ok(transactions);
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
                var command = new DeleteTransaction(model);
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
