using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YandS.UI.Models;

namespace YandS.UI.Controllers
{
    public class ErrorController : Controller
    {
        private string ExceptionMessage = string.Empty;
        private string StackTrace = string.Empty;
        private string actionName = string.Empty;

        public ActionResult Index(string id)
        {
            switch (id)
            {
                case "404":
                    actionName = "PageNotFound";
                    break;
                case "500":
                    actionName = "ServerError";
                    break;
                case "499":
                    actionName = "UnAuthorizedAccess";
                    break;
                case "420":
                    actionName = "UnAuthorizedClientAccess";
                    break;
                case "599":
                    actionName = "SessionTimedOut";
                    break;
                case "503":
                    actionName = "Forbidden";
                    break;
                default:
                    actionName = "ServerError";
                    break;
            }
            return RedirectToAction(actionName);
        }

        [HttpPost]
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(SessionHandler.User.EmployeeNumber))
            {
                string[] companyIdParts = Request.ServerVariables["LOGON_USER"].Split(new char[] { '\\' });
                string companyId = companyIdParts.Length > 1 ? companyIdParts[1].Replace("-D", string.Empty).ToUpper() : companyIdParts[0];
                Session["User"] = new UserVM
                {
                    EmployeeNumber = companyId,
                    FullName = SessionHandler.User.FullName
                };
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult PageNotFound()
        {
            ExceptionMessage = SessionHandler.errorVM.ErrorMessage;
            StackTrace = SessionHandler.errorVM.StackTrace;
            var ViewModel = new ErrorVM
            {
                ErrorMessage = ExceptionMessage,
                StackTrace = StackTrace
            };

            ViewBag.ExceptionMessage = ExceptionMessage;
            ViewBag.StackTrace = StackTrace;
            ViewBag.actionName = "Page Not Found";


            return View(ViewModel);
        }
        public ActionResult ServerError()
        {
            ExceptionMessage = SessionHandler.errorVM.ErrorMessage;
            StackTrace = SessionHandler.errorVM.StackTrace;
            var ViewModel = new ErrorVM
            {
                ErrorMessage = ExceptionMessage,
                StackTrace = StackTrace
            };

            ViewBag.ExceptionMessage = ExceptionMessage;
            ViewBag.StackTrace = StackTrace;
            ViewBag.actionName = "Server Error";


            return View(ViewModel);
        }
        public ActionResult UnAuthorizedAccess()
        {
            return View();
        }
        public ActionResult UnAuthorizedClientAccess()
        {
            return View();
        }
        public ActionResult SessionTimedOut()
        {
            return View();
        }
        public ActionResult Forbidden()
        {
            return View();
        }


    }
}