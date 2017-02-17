using sheego.Framework.Domain.Shared;
using sheego.Framework.Domain.Shared.Locator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace sheego.Framework.Presentation.Web.Authorization
{
    public class FrameworkAuthorizationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            using (var authorizationService = DomainLocator.GetAuthorizationService())
            {
                var authorized = authorizationService.Object.IsAuthorized(filterContext.HttpContext.User);

                if (!authorized)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{
                    {"action", "Unauthorized"},
                    {"controller","Home"}
                    });
                }
            }
        }
    }
}