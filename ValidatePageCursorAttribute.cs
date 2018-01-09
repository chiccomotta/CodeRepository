using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repower.LeMA.API.Extensions;
using Repower.LeMA.API.Properties;
using Repower.LeMA.Common.Exceptions;
using Repower.LeMA.Common.Extensions;
using System;

namespace Repower.LeMA.API.Filters
{
    /// <summary>
    /// An action decorated requires the presence of page cursor
    /// param names into the query string.
    /// </summary>
    public class ValidatePageCursorAttribute : ActionFilterAttribute
    {
            private readonly ILogger logger;

            /// <summary>
            /// Set to ZERO to remove any upper bound
            /// </summary>
            public int MaxPageSize { get; set; } = 100;
            /// <summary>
            /// Set to ZERO to allow infinite page size (all records)
            /// </summary>
            public int MinPageSize { get; set; } = 1;

            public bool RequirePageSize { get; set; } = false;
            public bool RequirePageIndex { get; set; } = false;

            public bool IsReusable => throw new NotImplementedException();

            public ValidatePageCursorAttribute()
            {
                logger = Globals.ServiceProvider.GetRequiredService<ILogger<ValidateRequestAttribute>>();
            }

            public override void OnActionExecuting(ActionExecutingContext context)
            {
                int? p_size, p_index;

                context.HttpContext.Request.Query.ParsePageCursor(out p_size, out p_index);

                if (RequirePageSize && p_size == null)
                {
                    logger.LogDebug("Missing required page size");

                    throw new BadRequestException(SystemMessages.PageCursorRequiredSize.FormatSystemMessage(Globals.AppSettings.PageCursorSizeParamName));
                }

                if (RequirePageIndex && p_index == null)
                {
                    logger.LogDebug("Missing required page index");
                    throw new BadRequestException(SystemMessages.PageCursorRequiredIndex.FormatSystemMessage(Globals.AppSettings.PageCursorIndexParamName));
                }

                if (p_size != null && (p_size < MinPageSize || (MaxPageSize > 0 && p_size > MaxPageSize)))
                {
                    logger.LogDebug("Page size {PageSize} out of bounds [{MinPageSize}, {MaxPageSize}]", p_size.Value, MinPageSize, MaxPageSize == 0 ? "Infinite" : MaxPageSize.ToString());

                    throw new BadRequestException(SystemMessages.PageCursorSizeNotAllowed.FormatSystemMessage(MinPageSize, MaxPageSize));
                }

            }
    }
}
