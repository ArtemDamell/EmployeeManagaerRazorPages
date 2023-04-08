using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FirstRazorApp
{
    public class EvenConstraint : IRouteConstraint
    {
        /// <summary>
        /// Checks if the given id is even and returns a boolean value.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="route">The route.</param>
        /// <param name="routeKey">The route key.</param>
        /// <param name="values">The route values.</param>
        /// <param name="routeDirection">The route direction.</param>
        /// <returns>A boolean value indicating if the given id is even.</returns>
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values,
                    RouteDirection routeDirection)
        {
            int id;
            if (Int32.TryParse(values["id"].ToString(), out id))
            {
                if (id % 2 == 0)
                    return true;
            }
            return false;
        }
    }
}
