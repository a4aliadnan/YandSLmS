using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YandS.DAL;
using YandS.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data.Entity;
using System.IO;

namespace YandS.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Initialization
        private RBACDbContext db = new RBACDbContext();
        private ApplicationUserManager _userManager;
        public HomeController()
        {
        }
        public HomeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion

        public ActionResult Index()
        {
            string strUser = User.Identity.Name;
            int IsClient = Helper.IsClientAccess(strUser);

            if (IsClient > 0)
            {
                var Clients = Helper.GetClients(UserName: strUser).FirstOrDefault();

                if (Clients == null)
                {
                    HttpContext ctx = System.Web.HttpContext.Current;
                    ctx.Response.Clear();
                    string UnAuthorizeURL = @"~/Error/Index/420";
                    ctx.Response.Redirect(UnAuthorizeURL);
                }
                else
                {
                    string ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == Clients.ClientCode).FirstOrDefault().Mst_Desc;
                    UserVM Employee = new UserVM()
                    {
                        EmployeeId = Clients.ClientId,
                        EmployeeNumber = Clients.UserName,
                        FullName = Clients.DisplayName,
                        LocationCode = "A",
                        ClientName = ClientName,
                        ClientCode = Clients.ClientCode,
                        UserType = 0
                    };

                    Session["User"] = Employee;
                    return RedirectToAction("ClientIndex", "Home");
                }
            }
            else
            {
                var Employee = EmployeesController.GetEmployeeById(strUser);
                Session["User"] = Employee;
            }

            if (User.Identity.Name.In("22", "10", "51", "54"))
                return RedirectToAction("Index", "SessionRoll");
            else
            {
                if (User.Identity.Name.In("31", "50"))
                    return RedirectToAction("Index", "CourtCases", new RouteValueDictionary(new { id = -4 }));
                if (User.Identity.Name == "9")
                    return RedirectToAction("Index", "CourtCases", new RouteValueDictionary(new { id = -5 }));
                else
                    return RedirectToAction("Index", "CourtCases");
            }

            /*
            if (strUser == "3")
                return RedirectToAction("Index", "CourtCases");
            else
                return RedirectToAction("Index", "CourtCases");
            
            ReportCourtCases objDAL = new ReportCourtCases();

            //List<CourtCases> ViewIndexList = CourtCasesController.GetCourtCasesList(UserLocation);
            var DB_STATIC_TEXT = objDAL.getDashBoardData();

            if (DB_STATIC_TEXT.Rows.Count > 0)
            {
                var CaseLevelCode = string.Empty;
                var Mst_Value = string.Empty;

                var query =
                    from r in DB_STATIC_TEXT.AsEnumerable()
                    where r.Field<string>("CaseLevelCode").Equals(CaseLevelCode) && r.Field<string>("Mst_Value").Equals(Mst_Value)
                    select r.Field<int>("DataValue");

                CaseLevelCode = "1";
                Mst_Value = "M";
                var DISP_VALUE = query.FirstOrDefault();
                ViewBag.ToBeRegMCT = DISP_VALUE;

                CaseLevelCode = "1";
                Mst_Value = "S";
                DISP_VALUE = query.FirstOrDefault();
                ViewBag.ToBeRegSLL = DISP_VALUE;

                ViewBag.ToBeRegTTL = Convert.ToInt32(ViewBag.ToBeRegMCT) + Convert.ToInt32(ViewBag.ToBeRegSLL);

                CaseLevelCode = "3";
                Mst_Value = "M";
                DISP_VALUE = query.FirstOrDefault();
                ViewBag.PrimaryMCT = DISP_VALUE;

                CaseLevelCode = "3";
                Mst_Value = "S";
                DISP_VALUE = query.FirstOrDefault();
                ViewBag.PrimarySLL = DISP_VALUE;

                ViewBag.PrimaryTTL = Convert.ToInt32(ViewBag.PrimaryMCT) + Convert.ToInt32(ViewBag.PrimarySLL);

                CaseLevelCode = "4";
                Mst_Value = "M";
                DISP_VALUE = query.FirstOrDefault();
                ViewBag.AppealMCT = DISP_VALUE;

                CaseLevelCode = "4";
                Mst_Value = "S";
                DISP_VALUE = query.FirstOrDefault();
                ViewBag.AppealSLL = DISP_VALUE;

                ViewBag.AppealTTL = Convert.ToInt32(ViewBag.AppealMCT) + Convert.ToInt32(ViewBag.AppealSLL);

                CaseLevelCode = "5";
                Mst_Value = "M";
                DISP_VALUE = query.FirstOrDefault();
                ViewBag.SupremeMCT = DISP_VALUE;

                CaseLevelCode = "5";
                Mst_Value = "S";
                DISP_VALUE = query.FirstOrDefault();
                ViewBag.SupremeSLL = DISP_VALUE;

                ViewBag.SupremeTTL = Convert.ToInt32(ViewBag.SupremeMCT) + Convert.ToInt32(ViewBag.SupremeSLL);

                CaseLevelCode = "6";
                Mst_Value = "M";
                DISP_VALUE = query.FirstOrDefault();
                ViewBag.EnforcementMCT = DISP_VALUE;

                CaseLevelCode = "6";
                Mst_Value = "S";
                DISP_VALUE = query.FirstOrDefault();
                ViewBag.EnforcementSLL = DISP_VALUE;

                ViewBag.EnforcementTTL = Convert.ToInt32(ViewBag.EnforcementMCT) + Convert.ToInt32(ViewBag.EnforcementSLL);
            }
            return View();
            */
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [RBAC]
        public ActionResult ManageClientAccess()
        {
            var Clients = Helper.GetClients();
            return View(Clients);
        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageClientAccess(Models.ClientAccessVM modal)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            try
            {
                if (ModelState.IsValid)
                {
                    if (modal.ClientId > 0)
                    {
                        var user = UserManager.FindByName(modal.UserName);
                        if (user != null)
                        {
                            modal.Code = UserManager.GeneratePasswordResetToken(user.Id);

                            var result = UserManager.ResetPassword(user.Id, modal.Code, modal.PassWord);
                            if (result.Succeeded)
                            {
                                var clientAccess = db.ClientAccess.Find(modal.ClientId);

                                db.Entry(clientAccess).Entity.ClientCode = modal.ClientCode;
                                db.Entry(clientAccess).Entity.DisplayName = modal.DisplayName;
                                db.Entry(clientAccess).Entity.PassWord = modal.PassWord;
                                db.Entry(clientAccess).Entity.Inactive = modal.Inactive;
                                db.Entry(clientAccess).Entity.LastModified = DateTime.UtcNow.AddHours(4);

                                db.Entry(clientAccess).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }

                    }
                    else
                    {
                        ClientAccess clientAccess = new ClientAccess();
                        clientAccess.ClientCode = modal.ClientCode;
                        clientAccess.UserName = modal.UserName;
                        clientAccess.DisplayName = modal.DisplayName;
                        clientAccess.PassWord = modal.PassWord;

                        db.ClientAccess.Add(clientAccess);
                        db.SaveChanges();

                        //Create User...
                        var user = new ApplicationUser { UserName = modal.UserName, Email = modal.Email, Firstname = modal.DisplayName, LastModified = DateTime.UtcNow.AddHours(4), Inactive = false, EmailConfirmed = true };

                        var result = UserManager.Create(user, modal.PassWord);

                        if (result.Succeeded)
                        {
                            //Add User to Admin Role...
                            UserManager.AddToRole(user.Id, "ClientAccess");
                        }
                    }

                    ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc", modal.ClientCode);

                    var Clients = Helper.GetClients(UserName: modal.UserName).FirstOrDefault();
                    return PartialView("_ManageClients", Clients);

                }

                return Json(new { errorMessage = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray()) });

            }
            catch(Exception ex)
            {
                return Json(new { errorMessage = ex.Message });
            }
            
        }

        [RBAC]
        public ActionResult ClientIndex()
        {
            var LoggedInUser = SessionHandler.User;
            string UserImageResult = string.Empty;
            string UserImageRoot = Helper.GetUserImageRoot;
            string UserImagePath = Path.Combine(UserImageRoot, "UserImage");

            try
           {
                DirectoryInfo d = new DirectoryInfo(UserImagePath);
                var Image = d.GetFiles(LoggedInUser.FullName + ".*").OrderByDescending(f => f.FullName).FirstOrDefault();

                if (Image == null)
                    UserImageResult = "NO_IMAGE.png";
                else
                    UserImageResult = Image.ToString();

                ViewBag.UserImage = UserImageResult;


            }
            catch (Exception e)
            {
                ViewBag.UserImage = "NO_IMAGE.png";
            }

            return View();
        }

        [RBAC]
        [HttpPost]
        public ActionResult AjaxIndexData()
        {
            string LocationId = string.Empty;
            string UserLocation = string.Empty;
            var request = HttpContext.Request;

            string strUser = User.Identity.Name;
            int IsClient = Helper.IsClientAccess(strUser);

            if (IsClient > 0)
            {

            }
            else
            {
                if (User.IsInRole("CourtCasesViewAll") || User.IsSysAdmin())
                    UserLocation = "A";
                else
                    UserLocation = Helper.GetEmployeeLocation(User.Identity.Name);
            }

            CourtCaseDTView DtView = new CourtCaseDTView();
            List<CourtCaseListForIndex> data = new List<CourtCaseListForIndex>();

            var start = (Convert.ToInt32(Request["start"]));
            var Length = (Convert.ToInt32(Request["length"])) == 0 ? 10 : (Convert.ToInt32(Request["length"]));
            var searchvalue = Request["search[value]"] ?? "";
            var sortcoloumnIndex = Convert.ToInt32(Request["order[0][column]"]);
            var sortDirection = Request["order[0][dir]"] ?? "asc";
            var recordsTotal = 0;
            string DataFor = string.Empty;
            string ProcedureName = string.Empty;
            string ClientCode = string.Empty;
            string CaseLevel = string.Empty;
            DateTime DateFrom = DateTime.Now;
            DateTime DateTo = DateTime.Now;
            string CallerName = string.Empty;
            int MCTRecords = 0;
            int SLLRecords = 0;

            //UserLocation.ToUpper().Substring(0, 1)

            try
            {
                LocationId = request.Params["LocationId"].ToString();
                UserLocation = LocationId.ToUpper().Substring(0, 1);
            }
            catch (Exception ex)
            {
                UserLocation = "A";
            }

            try
            {
                CaseLevel = request.Params["ClientCode"].ToString();
            }
            catch (Exception ex)
            {
                CaseLevel = "";
            }

            try
            {
                DataFor = request.Params["DataTableName"].ToString();

                if (DataFor == "CLIENTSLIST")
                {
                    var Clients = Helper.GetClients();
                    return Json(new { data = Clients, recordsTotal = Clients.Count, recordsFiltered = Clients.Count }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    List<string> parName = Helper.getDefaultParNames();
                    List<object> parValues = Helper.getDefaultParValues();
                    ClientCode = request.Params["ClientCode"].ToString();
                    ProcedureName = request.Params["ProcedureName"].ToString();

                    parName.AddRange(new[] { "@UserName", "@DataFor", "@Location", "@ClientCode" });
                    parValues.AddRange(new[] { User.Identity.Name, DataFor, UserLocation, ClientCode });

                    var ds = Helper.getDataSet(parName.ToArray(), parValues.ToArray(), TableNames: new string[] { "data", "summary" }, procedureName: ProcedureName);
                    DataTable dt = ds.Tables["data"];
                    DataTable Summarydt = ds.Tables["summary"];

                    var jsondata = dt.ToDictionary();

                    recordsTotal = Summarydt.Rows.Count > 0 ? int.Parse(Summarydt.Rows[0]["recordsTotal"].ToString()) : 0;
                    MCTRecords = Summarydt.Rows.Count > 0 ? int.Parse(Summarydt.Rows[0]["MCTRecords"].ToString()) : 0;
                    SLLRecords = Summarydt.Rows.Count > 0 ? int.Parse(Summarydt.Rows[0]["SLLRecords"].ToString()) : 0;

                    return Json(new { data = jsondata, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, MuscatTotal = MCTRecords, SalalahTotal = SLLRecords }, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
            }
            return Json(new { data = data, recordsTotal = recordsTotal, recordsFiltered = recordsTotal }, JsonRequestBehavior.AllowGet);

        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            ClientAccess clientAccess = new ClientAccess();
            clientAccess = db.ClientAccess.Find(id);

            if (clientAccess != null)
            {
                var user = db.Users.Where(w => w.UserName == clientAccess.UserName && w.Inactive == false).FirstOrDefaultAsync();

                if (user != null)
                {
                    var Res = ApplicationUserManager.DeleteUser(user.Id);

                    clientAccess.Inactive = true;
                    clientAccess.LastModified = DateTime.UtcNow.AddHours(4);

                    db.Entry(clientAccess).State = EntityState.Modified;
                    db.SaveChanges();

                }
            }

            return Json(new { redirectTo = "#btn_PayVoucherPDC" });
        }

        [RBAC]
        [HttpGet]
        public ActionResult GetTab(string PartialViewName, string UserName = null, int ClientId = 0, string ClientCode = null)
        {
            if (PartialViewName == "_ManageClients")
            {

                Models.ClientAccessVM modal = new Models.ClientAccessVM();

                if (UserName == null)
                    ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                else
                {
                    modal = Helper.GetClients(UserName: UserName).FirstOrDefault();
                    ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc", modal.ClientCode);
                }

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "SaveResult")
            {
                return PartialView(PartialViewName);
            }
            else
                return PartialView(PartialViewName);
        }

    }
}