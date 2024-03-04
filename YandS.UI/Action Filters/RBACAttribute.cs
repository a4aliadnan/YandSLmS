using System;
using System.Web.Mvc;
using System.Web.Routing;
public class RBACAttribute : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        try
        {
            if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                //Redirect user to login page if not yet authenticated.  This is a protected resource!
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login", returnUrl = filterContext.HttpContext.Request.FilePath }));
            }
            else
            {
                //Create permission string based on the requested controller name and action name in the format 'controllername-action'
                string requiredPermission = String.Format("{0}-{1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName);

                if (!filterContext.HttpContext.User.HasPermission(requiredPermission) & !filterContext.HttpContext.User.IsSysAdmin())
                {
                    //User doesn't have the required permission and is not a SysAdmin, return our custom “401 Unauthorized” access error
                    //Since we are setting filterContext.Result to contain an ActionResult page, the controller's action will not be run.
                    //The custom “401 Unauthorized” access error will be returned to the browser in response to the initial request.
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "Unauthorised" } });
                }
                //If the user has the permission to run the controller's action, the filterContext.Result will be uninitialized and
                //executing the controller's action is dependant on whether filterContext.Result is uninitialized.
            }
        }
        catch (Exception ex)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Unauthorised", action = "Error", _errorMsg = ex.Message }));
        }
    }
}