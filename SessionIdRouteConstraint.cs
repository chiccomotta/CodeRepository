using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Repower.LeMA.Common.Types;
using System.Diagnostics;

namespace Repower.LeMA.API.Filters
{
    /// <summary>
    /// Check session Id se un intero oppure un RepowerCode formalmente valido
    /// </summary>
    public class SessionIdRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var id = values["id"].ToString();            

            if (int.TryParse(id, out int intValue))
                return true;

            if (RepowerCode.TryParse(id, out RepowerCode repowerCode))
                return true;

            return false;            
        }
    }
}
