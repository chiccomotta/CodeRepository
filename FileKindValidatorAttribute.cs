using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Common.Exceptions;
using Common.Extensions;
using Common.Models;
using Common.Utils;

namespace API.Filters
{
    public class FileKindValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Get kind value from route
            var kind = context.HttpContext.GetRouteValue("kind")?.ToString();

            if (kind == null)
                throw new BadRequestException("File Kind not set");            

            if (!EnumUtility.ExistDescription<FileKind>(kind))
                throw new BadRequestException($"Bad File Kind '{kind}'");            
        }
    }
}
