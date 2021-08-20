using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Mantimentos.App.Extensions
{
    public class CustomAuthorization //referencia para validar as claims
    {
        public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                    context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }

    }
        public class ClaimsAuthorizeAttribute : TypeFilterAttribute //Filtro do aspnet
        {
            public ClaimsAuthorizeAttribute(string claimName, string claimValue): base(typeof(RequisitoClainFilter))
            {
                Arguments = new object[] { new Claim(claimName, claimValue) };
            }
        }
        public class RequisitoClainFilter : IAuthorizationFilter //faz o filtro ser utilizado como atributo
        {
            readonly Claim _claim;
            public RequisitoClainFilter(Claim claim)
            {
                _claim = claim;
            }
            public void OnAuthorization(AuthorizationFilterContext context)
            {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Identity", page = "/Accont/Login", returnUrl = context.HttpContext.Request.Path.ToString()}));
                return;
            }
                if(!CustomAuthorization.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
                {
                    context.Result = new StatusCodeResult(403);
                }
            }
        }
    }

