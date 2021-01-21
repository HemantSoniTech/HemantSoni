using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizationUsingAcceptLangauage.WebAPI.Extentions
{
    //public class RouteDataRequestCultureProvider : RequestCultureProvider
    //{
    //    public int IndexOfCulture;
    //    public int IndexOfUICulture;
    //    public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
    //    {
    //        if (httpContext == null)
    //            throw new ArgumentNullException(nameof(httpContext));
    //        string culture = null;
    //        string defaultCulture = null;
    //        var userLang = httpContext.Request.Headers["Accept-Language"].ToString();
    //        culture = userLang.Split(',').FirstOrDefault();
    //        defaultCulture = string.IsNullOrEmpty(culture) ? "en" : culture;
    //        if (!string.IsNullOrEmpty(culture))
    //        {
    //            httpContext.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
    //                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
    //        }
    //        var provideResultCulture = new ProviderCultureResult(culture, defaultCulture);
    //        return Task.FromResult(provideResultCulture);
    //    }
    //}
}
