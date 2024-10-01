using Jeff.ExpenseTracker.Contracts.DTOs;
using Jeff.ExpenseTracker.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jeff.ExpenseTracker.API.Controllers
{
    public class ExpenseTrackerControllerBase : ControllerBase
    {
        public ExpenseTrackerControllerBase()
        {
            
        }

        protected void SetEmailId(UserSpecificDTO model)
        {
            var identity = HttpContext.User.Identity;
            if (identity != null && !identity.IsAuthenticated)
            {
                throw new InvalidRequestException()
                {
                    Errors = new string[] { "Request is not authenticated." }
                };
            }
            var emailId = HttpContext.User.FindFirst(ClaimTypes.Email);
            if (emailId != null)
            {
                model.EmailId = emailId.Value;
            }
        }
    }
}
