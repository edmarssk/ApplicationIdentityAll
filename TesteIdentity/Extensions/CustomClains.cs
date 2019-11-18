using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TesteIdentity.Extensions
{
    public class CustomClains
    {

        public static bool ValidarAcesso(HttpContext httpContext, string claimName, string claimValue)
        {
            return (httpContext.User.Identity.IsAuthenticated && httpContext.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue)));
        }

    }
    public class RequisitoClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RequisitoClaimFilter(Claim claim)
        {
            _claim = claim;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Metodo usado para customizar para uma pagina especifica quando o usuario não tem acesso

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(values: new { area = "Identity", page = "/Account/Login", ReturnUrl = context.HttpContext.Request.Path.ToString() }));
            }
            if (!CustomClains.ValidarAcesso(context.HttpContext, claimName: _claim.Type, claimValue: _claim.Value))
            {
                context.Result = new ForbidResult();
            }
        }
    }
    public class CustomClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomClaimsAuthorizeAttribute(string claimName, string claimValue): base(type: typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(type: claimName, value: claimValue) };
        }
    }
}
