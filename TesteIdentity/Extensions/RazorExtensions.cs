using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteIdentity.Extensions
{
    public static class RazorExtensions
    {
        public static bool IfClaim(this RazorPage page, string claimName, string claimValue)
        {
            return CustomClains.ValidarAcesso(page.Context, claimName, claimValue);
        }
        public static string ifClaimShow(this RazorPage page, string claimName, string claimValue)
        {
            return CustomClains.ValidarAcesso(page.Context, claimName, claimValue) ? "" : "disabled";
        }
        public static IHtmlContent ifShow(this IHtmlContent page, HttpContext httpContext, string claimName, string claimValue)
        {
            return CustomClains.ValidarAcesso(httpContext, claimName, claimValue) ? page : null;
        }
    }
}
