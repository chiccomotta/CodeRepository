using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using API.Models.Responses;
using System;
using Common.Exceptions;

namespace API.Filters
{
    /// <summary>
    /// This filters all unhandled exceptions and provide a conformant JSON response with a 500 http status code
    /// </summary>
    public class UnhandledExceptionFilter : IExceptionFilter
    {
        ILogger<UnhandledExceptionFilter> Logger;

        public UnhandledExceptionFilter(ILogger<UnhandledExceptionFilter> logger)
        {
            Logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            ResponseBase response;
            JsonResult result;
            if (context.Exception is ResponseException)
            {
                ResponseException ex = context.Exception as ResponseException;
                response = new ResponseBase(context.HttpContext)
                {
                    _StatusCode = "KO_" + ex.KoSubCode,
                    _StatusDescription = ex.Description
                };
                if (context.Exception.InnerException != null)
                {
                    response.SetExceptionInfo(context.Exception.InnerException, false);
                }
                result = new JsonResult(response);
                result.StatusCode = ex.HttpCode;
            }
            else
            {
                response = new UnhandledExceptionResponse(context.HttpContext, context.Exception);
                result = new JsonResult(response);
                result.StatusCode = 500;

            }
            context.ExceptionHandled = true;
            context.Result = result;

            try
            {
                Logger.LogError("Message: " + context.Exception.Message + " Stack Trace: " + context.Exception.StackTrace);
            }
            catch (Exception)
            {
                
            }

        }
    }
}
