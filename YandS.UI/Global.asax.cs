using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using YandS.UI.Models;

namespace YandS.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private RBACDbContext db = new RBACDbContext();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.DefaultBinder = new CustomModelBinder();
        }

        protected void Session_start()
        {
            string strUser = User.Identity.Name;

            if(Session["User"] == null)
            {
                if (strUser != string.Empty)
                {
                    /*CHECKING USER TYPE*/

                    int IsClient = Helper.IsClientAccess(strUser);

                    if(IsClient > 0)
                    {
                        var Clients = Helper.GetClients(UserName: strUser).FirstOrDefault();

                        UserVM Employee = new UserVM()
                        {
                            EmployeeId = Clients.ClientId,
                            EmployeeNumber = Clients.UserName,
                            FullName = Clients.DisplayName,
                            LocationCode = "A",
                            UserType = 0
                        };

                        if (Employee == null)
                        {
                            HttpContext ctx = HttpContext.Current;
                            ctx.Response.Clear();
                            string UnAuthorizeURL = @"~/Error/Index/420";
                            ctx.Response.Redirect(UnAuthorizeURL);
                        }
                        Session["User"] = Employee;
                    }
                    else
                    {
                        var Employee = Controllers.EmployeesController.GetEmployeeById(strUser);


                        if (Employee == null)
                        {
                            HttpContext ctx = HttpContext.Current;
                            ctx.Response.Clear();
                            string UnAuthorizeURL = @"~/Error/Index/499";
                            ctx.Response.Redirect(UnAuthorizeURL);
                        }
                        Session["User"] = Employee;
                    }
                }
            }
        }
        protected void Application_Error()
        {
            //application error handling starts here. If any error it will log into database & send an email to the  supporting person
            var exception = Server.GetLastError();
            string ipAddress = Request.ServerVariables["REMOTE_ADDR"], pcName = Request.ServerVariables["REMOTE_HOST"], httpErrorCode = string.Empty;
            if (SessionHandler.User != null && (exception.Message.Contains("/images/") == false && exception.Message.Contains("/Content/") == false))
            {
                var requestControllerName = Convert.ToString(Request.RequestContext?.RouteData?.Values["controller"]);
                var requestActionName = Convert.ToString(Request.RequestContext?.RouteData?.Values["action"]);
                string URL = string.Empty;
                if (HttpContext.Current.Request.Url.AbsoluteUri != null)
                    URL = HttpContext.Current.Request.Url.AbsoluteUri;

                ErrorVM errVM = new ErrorVM
                {
                    IPAddress = ipAddress,
                    PCName = pcName,
                    ControllerName = requestControllerName,
                    ActionName = requestActionName,
                    ErrorMessage = exception.Message,
                    StackTrace = exception.StackTrace,
                    EmployeeNumber = SessionHandler.User.EmployeeNumber,
                    Url = URL

                };

                ApplicationError err = new ApplicationError
                {
                    IPAddress = errVM.IPAddress,
                    PCName = errVM.PCName,
                    ControllerName = errVM.ControllerName,
                    ActionName = errVM.ActionName,
                    ErrorMessage = errVM.ErrorMessage,
                    StackTrace = errVM.StackTrace,
                    EmployeeNumber = errVM.EmployeeNumber,
                    Url = errVM.Url

                };

                db.ApplicationError.Add(err);
                db.SaveChanges();

                Session["Err"] = errVM;
                try
                {
                    HttpException httpException = (HttpException)exception;
                    httpErrorCode = httpException.GetHttpCode().ToString();
                }
                catch
                {
                    httpErrorCode = "500";
                }

                HttpContext ctx = HttpContext.Current;
                ctx.Response.Clear();
                string ErrorURL = SessionHandler.User.UserType > 0 ? string.Format("~/Error/Index/{0}", httpErrorCode) : string.Format("~/Error/Index/{0}", httpErrorCode);
                ctx.Response.Redirect(ErrorURL);
            }
        }
    }
}
