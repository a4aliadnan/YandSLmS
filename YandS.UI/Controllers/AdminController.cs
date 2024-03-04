using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YandS.UI.Models;

namespace YandS.UI.Controllers
{
    [RBAC]
    public class AdminController : Controller
    {
        private RBACDbContext database = new RBACDbContext();

        #region USERS
        // GET: Admin
        public ActionResult Index()
        {
            return View(ApplicationUserManager.GetUsers());
        }
       
        public ViewResult UserDetails(int id)
        {
            ApplicationUser user = ApplicationUserManager.GetUser(id);
            SetViewBagData(id);
            return View(user);
        }

        [HttpGet]
        public ActionResult UserCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserCreate(UserViewModel user)
        {
            ApplicationUserManager.CreateUser(user);
            return RedirectToAction("UserDetails", new RouteValueDictionary(new { id = user.Id }));
        }

        [HttpGet]
        public ActionResult UserEdit(int id)
        {
            ApplicationUser user = ApplicationUserManager.GetUser(id);
            SetViewBagData(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult UserEdit(UserViewModel user)
        {
            ApplicationUserManager.UpdateUser(user);
            return RedirectToAction("UserDetails", new RouteValueDictionary(new { id = user.Id }));
        }

        [HttpPost]
        public ActionResult UserDetails(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser _user = database.Users.Where(p => p.UserName == user.UserName).FirstOrDefault();
                //database.Entry(_user).Entity.Inactive = user.Inactive;
                database.Entry(_user).Entity.LastModified = System.DateTime.Now;
                database.Entry(_user).State = EntityState.Modified;
                database.SaveChanges();
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult DeleteUserRole(int id, int userId)
        {
            ApplicationUserManager.RemoveUser4Role(userId, id);
            return RedirectToAction("Details", "USER", new { id = userId });
        }

        [HttpGet]
        public PartialViewResult filter4Users(string _surname)
        {
            return PartialView("_ListUserTable", GetFilteredUserList(_surname));
        }

        [HttpGet]
        public PartialViewResult filterReset()
        {
            return PartialView("_ListUserTable", ApplicationUserManager.GetUsers());
        }

        [HttpGet]
        public PartialViewResult DeleteUserReturnPartialView(int userId)
        {
            ApplicationUserManager.DeleteUser(userId);
            return this.filterReset();
        }

        private IEnumerable<ApplicationUser> GetFilteredUserList(string _surname)
        {
            IEnumerable<ApplicationUser> _ret = null;
            try
            {
                if (string.IsNullOrEmpty(_surname))
                {
                    _ret = ApplicationUserManager.GetUsers();
                }
                else
                {
                    _ret = ApplicationUserManager.GetUsers4Surname(_surname);
                }
            }
            catch
            {
            }
            return _ret;
        }

        protected override void Dispose(bool disposing)
        {
            database.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult DeleteUserRoleReturnPartialView(int id, int userId)
        {
            ApplicationUserManager.RemoveUser4Role(userId, id);

            SetViewBagData(userId);
            return PartialView("_ListUserRoleTable", ApplicationUserManager.GetUser(userId));
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult AddUserRoleReturnPartialView(int id, int userId)
        {

            ApplicationUserManager.AddUser2Role(userId, id);

            SetViewBagData(userId);
            return PartialView("_ListUserRoleTable", ApplicationUserManager.GetUser(userId));
        }

        private void SetViewBagData(int _userId)
        {
            SetViewBagData(_userId.ToString());
        }

        private void SetViewBagData(string _userId)
        {
            ViewBag.UserId = _userId;
            ViewBag.List_boolNullYesNo = this.List_boolNullYesNo();
            ViewBag.RoleId = new SelectList(ApplicationRoleManager.GetRoles4SelectList(), "Id", "Name");
        }

        public List<SelectListItem> List_boolNullYesNo()
        {
            var _retVal = new List<SelectListItem>();
            try
            {
                _retVal.Add(new SelectListItem { Text = "Not Set", Value = null });
                _retVal.Add(new SelectListItem { Text = "Yes", Value = bool.TrueString });
                _retVal.Add(new SelectListItem { Text = "No", Value = bool.FalseString });
            }
            catch { }
            return _retVal;
        }
        #endregion

        #region ROLES
        public ActionResult RoleIndex()
        {
            List<ApplicationRole> _roles = ApplicationRoleManager.GetRoles();
            return View(_roles);
        }

        public ViewResult RoleDetails(int id)
        {

            ApplicationRole role = ApplicationRoleManager.GetRole(id);

            // USERS combo
            ViewBag.UserId = new SelectList(ApplicationUserManager.GetUsers4SelectList(), "Id", "UserName");
            ViewBag.RoleId = id;

            // Rights combo
            ViewBag.PermissionId = new SelectList(ApplicationRoleManager.GetPermissions4SelectList(), "PermissionId", "PermissionDescription");
            ViewBag.List_boolNullYesNo = this.List_boolNullYesNo();

            return View(role);
        }

        public ActionResult RoleCreate()
        {
            ViewBag.List_boolNullYesNo = this.List_boolNullYesNo();
            return View();
        }

        [HttpPost]
        public ActionResult RoleCreate(RoleViewModel _role)
        {
            if (_role.RoleDescription == null)
            {
                ModelState.AddModelError("Role Description", "Role Description must be entered");
            }


            ApplicationRole role = new ApplicationRole(_role.Name, _role.RoleDescription);
            role.IsSysAdmin = _role.IsSysAdmin;

            if (ModelState.IsValid)
            {
                ApplicationRoleManager.CreateRole(role);
                return RedirectToAction("RoleIndex");
            }
            ViewBag.List_boolNullYesNo = this.List_boolNullYesNo();
            return View(_role);
        }


        public ActionResult RoleEdit(int id)
        {
            ApplicationRole _role = ApplicationRoleManager.GetRole(id);

            // USERS combo
            ViewBag.UserId = new SelectList(ApplicationUserManager.GetUsers4SelectList(), "Id", "Username");
            ViewBag.RoleId = id;

            // Rights combo
            ViewBag.PermissionId = new SelectList(ApplicationRoleManager.GetPermissions4SelectList(), "PermissionId", "PermissionDescription");
            ViewBag.List_boolNullYesNo = this.List_boolNullYesNo();

            return View(_role);
        }

        [HttpPost]
        public ActionResult RoleEdit(RoleViewModel _role)
        {
            if (string.IsNullOrEmpty(_role.RoleDescription))
            {
                ModelState.AddModelError("Role Description", "Role Description must be entered");
            }

            if (ModelState.IsValid)
            {
                if (ApplicationRoleManager.UpdateRole(_role))
                    return RedirectToAction("RoleDetails", new RouteValueDictionary(new { id = _role.Id }));
            }
            // USERS combo
            ViewBag.UserId = new SelectList(ApplicationUserManager.GetUsers4SelectList(), "Id", "UserName");

            // Rights combo
            ViewBag.PermissionId = new SelectList(ApplicationRoleManager.GetPermissions4SelectList(), "PermissionId", "PermissionDescription");
            ViewBag.List_boolNullYesNo = this.List_boolNullYesNo();
            return View(_role);
        }


        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult DeleteUserFromRoleReturnPartialView(int id, int userId)
        {
            ApplicationUserManager.RemoveUser4Role(userId, id);
            return PartialView("_ListUsersTable4Role", ApplicationRoleManager.GetRole(id));
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult AddUser2RoleReturnPartialView(int id, int userId)
        {
            ApplicationUserManager.AddUser2Role(userId, id);
            return PartialView("_ListUsersTable4Role", ApplicationRoleManager.GetRole(id));
        }

        public ActionResult RoleDelete(int id)
        {
            ApplicationRoleManager.DeleteRole(id);
            return RedirectToAction("RoleIndex");
        }

        #endregion

        #region PERMISSIONS

        public ViewResult PermissionIndex()
        {
            return View(ApplicationRoleManager.GetPermissions());
        }

        public ViewResult PermissionDetails(int id)
        {
            return View(ApplicationRoleManager.GetPermission(id));
        }

        public ActionResult PermissionCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PermissionCreate(PERMISSION _permission)
        {
            if (_permission.PermissionDescription == null)
            {
                ModelState.AddModelError("Permission Description", "Permission Description must be entered");
            }

            if (ModelState.IsValid)
            {
                ApplicationRoleManager.AddPermission(_permission);
                return RedirectToAction("PermissionIndex");
            }
            return View(_permission);
        }

        public ActionResult PermissionEdit(int id)
        {
            PERMISSION _permission = ApplicationRoleManager.GetPermission(id);
            ViewBag.RoleId = new SelectList(ApplicationRoleManager.GetRoles4SelectList(), "Id", "Name");
            return View(_permission);
        }

        [HttpPost]
        public ActionResult PermissionEdit(PERMISSION _permission)
        {
            if (ModelState.IsValid)
            {
                ApplicationRoleManager.UpdatePermission(_permission);
                return RedirectToAction("PermissionDetails", new RouteValueDictionary(new { id = _permission.PermissionId }));
            }
            return View(_permission);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult DeletePermissionReturnPartialView(int id)
        {
            ApplicationRoleManager.DeletePermission(id);
            return PartialView("_ListPermissionsTable", ApplicationRoleManager.GetPermissions());
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult AddPermission2RoleReturnPartialView(int id, int permissionId)
        {
            ApplicationRoleManager.AddPermission2Role(id, permissionId);
            return PartialView("_ListPermissions", ApplicationRoleManager.GetRole(id));
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult AddAllPermissions2RoleReturnPartialView(int id)
        {
            ApplicationRoleManager.AddAllPermissions2Role(id);
            return PartialView("_ListPermissions", ApplicationRoleManager.GetRole(id));
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult DeletePermissionFromRoleReturnPartialView(int id, int permissionId)
        {
            ApplicationRoleManager.RemovePermission4Role(id, permissionId);
            return PartialView("_ListPermissions", ApplicationRoleManager.GetRole(id));
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult DeleteRoleFromPermissionReturnPartialView(int id, int permissionId)
        {
            ApplicationRoleManager.RemovePermission4Role(id, permissionId);
            return PartialView("_ListRolesTable4Permission", ApplicationRoleManager.GetPermission(permissionId));
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult AddRole2PermissionReturnPartialView(int permissionId, int roleId)
        {
            ApplicationRoleManager.AddPermission2Role(roleId, permissionId);
            return PartialView("_ListRolesTable4Permission", ApplicationRoleManager.GetPermission(permissionId));
        }

        public ActionResult PermissionsImport()
        {
            var _controllerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t != null
                    && t.IsPublic
                    && t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                    && !t.IsAbstract
                    && typeof(IController).IsAssignableFrom(t));

            var _controllerMethods = _controllerTypes.ToDictionary(controllerType => controllerType,
                    controllerType => controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => typeof(ActionResult).IsAssignableFrom(m.ReturnType)));

            foreach (var _controller in _controllerMethods)
            {
                string _controllerName = _controller.Key.Name;
                foreach (var _controllerAction in _controller.Value)
                {
                    string _controllerActionName = _controllerAction.Name;
                    if (_controllerName.EndsWith("Controller"))
                    {
                        _controllerName = _controllerName.Substring(0, _controllerName.LastIndexOf("Controller"));
                    }

                    string _permissionDescription = string.Format("{0}-{1}", _controllerName, _controllerActionName);
                    PERMISSION _permission = database.PERMISSIONS.Where(p => p.PermissionDescription == _permissionDescription).FirstOrDefault();
                    if (_permission == null)
                    {
                        if (ModelState.IsValid)
                        {
                            PERMISSION _perm = new PERMISSION();
                            _perm.PermissionDescription = _permissionDescription;

                            database.PERMISSIONS.Add(_perm);
                            database.SaveChanges();
                        }
                    }
                }
            }
            return RedirectToAction("PermissionIndex");
        }
        #endregion
    }
}
