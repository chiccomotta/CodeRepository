using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using API.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Filters
{
    public class ValidateRequestAttribute: ActionFilterAttribute
    {
        private readonly ILogger logger;
        public ValidateRequestAttribute(ILogger<ValidateRequestAttribute> logger)
        {
            this.logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                logger.LogDebug("Request was not valid");
                context.Result = new BadRequestObjectResult(
                    new ModelBadRequestResponse(context.HttpContext,  context.ModelState));
            }
        }
    }
}
