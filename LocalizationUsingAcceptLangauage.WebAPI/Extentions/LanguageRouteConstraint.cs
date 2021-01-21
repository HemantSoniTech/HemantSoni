using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizationUsingAcceptLangauage.WebAPI.Extentions
{
    public class LanguageRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey("culture"))
                return false;
            var culture = values["culture"].ToString();
            return culture == "en-us" || culture == "fr-FR" || culture == "it-IT" || culture == "ru-RU" || culture == "en" || culture == "fr" || culture == "it" || culture == "ru";
        }
    }
}
