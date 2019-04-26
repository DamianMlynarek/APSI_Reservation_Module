using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace APSI_ResevationMod.Core_Logic
{
    public class AuthorizeADAttribute : AuthorizeAttribute
    {
        public string GroupId { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(base.AuthorizeCore(httpContext))
            {
                if(String.IsNullOrEmpty(GroupId))
                    return true;
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var claim = identity.Claims.Where(c => c.Value == GroupId).FirstOrDefault();

                if(claim != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if(!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new
                        {
                            controller = "Home",
                            action = "UnauthorizedRequest"
                        }));

                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
            }
        }

    }
}
