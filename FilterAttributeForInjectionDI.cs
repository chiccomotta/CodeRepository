using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace API.Filters
{
    /// <summary>
    /// Pattern per inserire le dipendenze in un filter implementato come Attributo
    /// vedi ASP.NET Core in Action (Manning) pagina 402
    /// </summary>
    public class EnsureRecipeExistsAttribute : TypeFilterAttribute
    {
        public EnsureRecipeExistsAttribute() : base(typeof(EnsureRecipeExistsFilter))
        {
        }

        public class EnsureRecipeExistsFilter : IActionFilter
        {
            private readonly IConfiguration Service;

            public EnsureRecipeExistsFilter(IConfiguration service)
            {
                Service = service;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                var recipeId = (int) context.ActionArguments["id"];
                if (true)
                {
                    context.Result = new NotFoundResult();
                }
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
        }
    }
}
