using Jeff.ExpenseTracker.Contracts.DTOs;
using Jeff.ExpenseTracker.Core.Exceptions;
using Jeff.ExpenseTracker.Core.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jeff.ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinanceDataController : ExpenseTrackerControllerBase
    {
        private readonly IMediator mediator;

        public FinanceDataController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("GetAllTransactionTypes")]
        public async Task<IActionResult> GetAllTransactionTypes()
        {
            var query = new GetAllTransactionTypes();
            var types = await this.mediator.Send(query);
            return Ok(types);
        }

        [HttpGet]
        [Route("GetAllRecurringTransactionTypes")]
        public async Task<IActionResult> GetAllRecurringTransactionTypes()
        {
            var query = new GetAllRecurringTransactionTypes();
            var types = await this.mediator.Send(query);
            return Ok(types);
        }

        [HttpGet]
        [Route("GetAllFrequencyTypes")]
        public async Task<IActionResult> GetAllFrequencyTypes()
        {
            var query = new GetAllFrequencyTypes();
            var types = await this.mediator.Send(query);
            return Ok(types);
        }

        [HttpGet]
        [Route("GetDashboardInfo")]
        public async Task<IActionResult> GetDashboardInfo(DateTime currMonth)
        {
            try
            {
                var model = new TimeSpecificDTO() { UpdatedOn = currMonth };
                this.SetEmailId(model);
                var query = new GetDashboardInfo(model);
                var dashInfo = await this.mediator.Send(query);
                return Ok(dashInfo);

            }
            catch (InvalidRequestException ex) 
            { 
                return BadRequest(new ErrorResponseDTO()
                {
                    Errors = ex.Errors,
                    IsSuccess = false
                });
            }
        }
    }
}
