using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace API.Filters
{
    /// <summary>
    /// Filtro per formattare il json di risposta secondo lo standard di Choso
    /// </summary>
    public class ChosoJsonConverterAttribute : TypeFilterAttribute
    {
        public ChosoJsonConverterAttribute() : base(typeof(ChosoJsonConverter))
        {
        }

        public class ChosoJsonConverter : IAsyncResultFilter
        {
            private readonly HttpContext Context;

            public ChosoJsonConverter(IHttpContextAccessor contextAccessor)
            {
                this.Context = contextAccessor.HttpContext;
            }

            public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
            {
                dynamic originResult = context.Result;

                // Se mi chiama XXX cambio name strategy
                if (IsChosoCalling())
                {
                    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        TypeNameHandling = TypeNameHandling.None
                    };

                    var contractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };

                    jsonSerializerSettings.ContractResolver = contractResolver;

                    context.Result = new JsonResult(originResult.Value, jsonSerializerSettings);
                }
                else
                {
                    context.Result = new JsonResult(originResult.Value, Globals.JsonSerializerSettings);
                }

                await next();
            }

            protected bool IsChosoCalling()
            {
                var userAgent = Context.Request.Headers["User-Agent"].FirstOrDefault()?.ToLower();
                if (string.IsNullOrEmpty(userAgent))
                    return false;

                return userAgent == "user-aget-name";
            }
        }
    }
}
