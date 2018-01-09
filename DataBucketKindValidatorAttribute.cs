using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Repower.LeMA.Common.Models;
using System;
using Repower.LeMA.Common.Exceptions;

namespace Repower.LeMA.API.Filters
{
    public class DataBucketKindValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Get kind value from route
            var kind = context.HttpContext.GetRouteValue("kind")?.ToString();
            
            if (kind != null)
            {
                if (!Enum.IsDefined(typeof(DataBucketKind), kind))
                {
                    throw new BadRequestException("KO_BAD_KIND");
                }
            }
        }
    }
}
