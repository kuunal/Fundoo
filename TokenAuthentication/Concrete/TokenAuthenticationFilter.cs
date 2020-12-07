using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenAuthentication;

namespace Greeting.TokenAuthentication
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var _tokenManager = (ITokenManager) context.HttpContext.RequestServices.GetService(typeof(ITokenManager));

            if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.HttpContext.Request.Headers.First(cookie => cookie.Key == "Authorization").Value;
                try
                {
                    var claimPrinciple = _tokenManager.Decode(token.ToString().Split(" ")[1]);
                    var claimList = claimPrinciple.Claims.ToList();
                    context.HttpContext.Items["userId"] = claimList[0].Value;
                    context.HttpContext.Items["email"] = claimList[1].Value;

                }
                catch (Exception)
                {
                    context.ModelState.AddModelError("Unauthorized", "Invalid token");
                }
            }
            else
            {
                context.ModelState.AddModelError("Unauthorized", "Missing token");
            }
        }
    }
}
