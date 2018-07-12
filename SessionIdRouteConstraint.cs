using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Common.Types;
using System.Diagnostics;

namespace API.Filters
{
    /// <summary>
    /// Check session Id se un intero oppure un Code formalmente valido
    /// </summary>
    public class SessionIdRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var id = values["id"].ToString();            

            if (int.TryParse(id, out int intValue))
                return true;

            if (Code.TryParse(id, out Code code))
                return true;

            return false;            
        }
    }
}
