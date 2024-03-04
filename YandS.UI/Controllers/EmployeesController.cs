using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YandS.UI.Models;

namespace YandS.UI.Controllers
{
    [RBAC]
    public class EmployeesController : Controller
    {
        private RBACDbContext db = new RBACDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            List<Employees> ResultList = new List<Employees>();
            ResultList = GetEmployees();

            //foreach(var employees in ResultList)
            //{
            //    //Create User...
            //    var user = new ApplicationUser { UserName = employees.EmployeeNumber, Email = employees.Email, Firstname = employees.FullName, LastModified = DateTime.Now, Inactive = false, EmailConfirmed = true };

            //    ApplicationUserManager UserManager = new ApplicationUserManager(new ApplicationUserStore(db));
            //    var result = UserManager.Create(user, "Oman123");


            //}

            return View(ResultList);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Employees> ResultList = new List<Employees>();
            ResultList = GetEmployees();


            //Employees employees = db.Employees.Find(id);
            Employees employees =  ResultList.Where(i => i.EmployeeId == (int)id).FirstOrDefault();
            
            if (employees == null)
            {
                return HttpNotFound();
            }

            string UploadRoot = Helper.GetStorageRoot;

            string UploadPath = Path.Combine(UploadRoot, "StaffImages");

            DirectoryInfo d = new DirectoryInfo(UploadPath);
            var StaffImage = d.GetFiles(employees.EmployeeNumber + ".*").OrderByDescending(f => f.FullName).FirstOrDefault();
            ViewBag.ImageName = StaffImage;
            ViewBag.GratuityAmount = GetGratuity(employees.EmployeeNumber, DateTime.Today);

            return View(employees);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {

            #region Dropdown Lists

            ViewBag.Gender = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Gender), "Mst_Value", "Mst_Desc");
            ViewBag.LocationCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Branch), "Mst_Value", "Mst_Desc");
            ViewBag.Nationality = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Nationality), "Mst_Value", "Mst_Desc");
            ViewBag.Department = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Department), "Mst_Value", "Mst_Desc");
            ViewBag.Designation = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Designation), "Mst_Value", "Mst_Desc");
            ViewBag.BankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc");

            #endregion

            return View();

        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,EmployeeNumber,FullName,Email,DOB,DOA,DOR,Gender,CreatedBy,CreatedOn,LocationCode,Nationality,Designation,Department,LeaveAllowed,PAddress,CAddress,ContactNumber,BasicSalary,Allowance,BankName,AccountNumber")] Employees employees, HttpPostedFileBase upload)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string UploadRoot = Helper.GetStorageRoot;

            if (ModelState.IsValid)
            {
                
                db.Employees.Add(employees);
                db.SaveChanges();

                //Create User...
                var user = new ApplicationUser { UserName = employees.EmployeeNumber, Email = employees.Email, Firstname = employees.FullName, LastModified = DateTime.Now, Inactive = false, EmailConfirmed = true };

                ApplicationUserManager UserManager = new ApplicationUserManager(new ApplicationUserStore(db));
                var result = UserManager.Create(user, "123456");

                if (upload != null && upload.ContentLength > 0)
                {
                    string FileExtension = Path.GetExtension(upload.FileName);

                    string FileName = employees.EmployeeNumber + FileExtension;

                    string UploadPath = Path.Combine(UploadRoot, "StaffImages", FileName);


                    upload.SaveAs(UploadPath);
                }
                Session["Message"] = new MessageVM
                {
                    Category = "Success",
                    Message = "Data Save Successfully"
                };
                return RedirectToAction("Index");
            }

            #region Dropdown Lists

            ViewBag.Gender = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Gender), "Mst_Value", "Mst_Desc");
            ViewBag.LocationCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Branch), "Mst_Value", "Mst_Desc");
            ViewBag.Nationality = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Nationality), "Mst_Value", "Mst_Desc");
            ViewBag.Department = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Department), "Mst_Value", "Mst_Desc");
            ViewBag.Designation = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Designation), "Mst_Value", "Mst_Desc");

            #endregion

            #region Remarked Code
            ///string[] Genders = { "Male", "Female" };
            //List<string> DropDownlist = Genders.ToList();

            //ViewBag.Gender = new SelectList(DropDownlist);
            //var LocationList = db.MasterSetup.Where(m => m.MstParentId == 1);
            //ViewBag.LocationCode = new SelectList(LocationList, "Mst_Value", "Mst_Desc");

            //string[] Genders = { "Male", "Female" };

            //ViewBag.Genders = new SelectList(Genders.Select((r, index) => new SelectListItem { Text = r, Value = index.ToString() }));
            #endregion

            Session["Message"] = new MessageVM
            {
                Category = "Error",
                Message = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray())
            };
            return View(employees);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);

            if (employees == null)
            {
                return HttpNotFound();
            }

            var ListGender = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Gender).ToList();
            var ListLocationCode = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Branch).ToList();
            var ListNationality = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Nationality).ToList();
            var ListDesignation = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Designation).ToList();
            var ListDepartment = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Department).ToList();
            ViewBag.BankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc", employees.BankName);

            employees.ListGender = ListGender;
            employees.ListLocationCode = ListLocationCode;
            employees.ListNationality = ListNationality;
            employees.ListDesignation = ListDesignation;
            employees.ListDepartment = ListDepartment;

            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,EmployeeNumber,FullName,Email,DOB,DOA,DOR,Gender,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn,LocationCode,Nationality,Designation,Department,LeaveAllowed,PAddress,CAddress,ContactNumber,BasicSalary,Allowance,BankName,AccountNumber")] Employees employees, HttpPostedFileBase upload)
        {
            string UploadRoot = Helper.GetStorageRoot;

            if (ModelState.IsValid)
            {
                db.Entry(employees).State = EntityState.Modified;
                db.SaveChanges();

                if (upload != null && upload.ContentLength > 0)
                {
                    string FileExtension = Path.GetExtension(upload.FileName);

                    string FileName = employees.EmployeeNumber + FileExtension;

                    string UploadPath = Path.Combine(UploadRoot, "StaffImages", FileName);


                    upload.SaveAs(UploadPath);
                }
                Session["Message"] = new MessageVM
                {
                    Category = "Success",
                    Message = "Data Save Successfully"
                };
                return RedirectToAction("Index");
            }

            #region Dropdown Lists

            ViewBag.Gender = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Gender), "Mst_Value", "Mst_Desc");
            ViewBag.LocationCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Branch), "Mst_Value", "Mst_Desc");
            ViewBag.Nationality = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Nationality), "Mst_Value", "Mst_Desc");
            ViewBag.Department = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Department), "Mst_Value", "Mst_Desc");
            ViewBag.Designation = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Designation), "Mst_Value", "Mst_Desc");
            ViewBag.BankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc", employees.BankName);

            #endregion

            #region Remarked Code

            //string[] Genders = { "Male", "Female" };
            //List<string> DropDownlist = Genders.ToList();

            //ViewBag.Gender = new SelectList(DropDownlist);
            //var LocationList = db.MasterSetup.Where(m => m.MstParentId == 1);
            //ViewBag.LocationCode = new SelectList(LocationList, "Mst_Value", "Mst_Desc");

            #endregion

            Session["Message"] = new MessageVM
            {
                Category = "Error",
                Message = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray())
            };
            return View(employees);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employees employees = db.Employees.Find(id);
            db.Employees.Remove(employees);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public List<Employees> GetEmployees()
        {
            List<Employees> ResultList = new List<Employees>();
            ResultList = db.Employees.OrderByDescending(o => o.DOR).OrderBy(a => a.FullName).ToList();

            var ReturnList = (from employee in ResultList
                                 join gender in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Gender)
                                 on employee.Gender equals gender.Mst_Value
                                 join location in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Branch)
                                 on employee.LocationCode equals location.Mst_Value
                                 join nat in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Nationality)
                                 on employee.Nationality equals nat.Mst_Value
                                 join dep in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Department)
                                 on employee.Department equals dep.Mst_Value
                              join BA in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Banks)
                              on employee.BankName equals BA.Mst_Value
                              join des in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Designation)
                                 on employee.Designation equals des.Mst_Value
                                 join crtby in db.Set<ApplicationUser>()
                                 on employee.CreatedBy equals crtby.Id
                                 select new Employees
                                 {
                                     EmployeeId = employee.EmployeeId,
                                     EmployeeNumber = employee.EmployeeNumber,
                                     FullName = employee.FullName,
                                     Email = employee.Email,
                                     DOB = employee.DOB,
                                     DOA = employee.DOA,
                                     DOR = employee.DOR,
                                     Gender = employee.Gender,
                                     LocationCode = employee.LocationCode,
                                     GenderName = gender.Mst_Desc,
                                     LocationName = location.Mst_Desc,
                                     Nationality = employee.Nationality,
                                     Designation = employee.Designation,
                                     Department = employee.Department,
                                     LeaveAllowed = employee.LeaveAllowed,
                                     PAddress = employee.PAddress,
                                     CAddress = employee.CAddress,
                                     ContactNumber = employee.ContactNumber,
                                     BasicSalary = employee.BasicSalary,
                                     Allowance = employee.Allowance,
                                     NationalityName = nat.Mst_Desc,
                                     DesignationName = des.Mst_Desc,
                                     DepartmentName = dep.Mst_Desc,
                                     CreatedByName = crtby.UserName,
                                     CreatedOn = employee.CreatedOn,
                                     UpdatedOn = employee.UpdatedOn,
                                     AccountNumber = employee.AccountNumber,
                                     EmployeeBankName = BA.Mst_Desc
                                 }).ToList();
            return ReturnList;

        }
        public static UserVM GetEmployeeById(string EmployeeNumber)
        {
            if(EmployeeNumber.ToUpper() == "ADMIN")
            {
                UserVM ReturnList = new UserVM()
                {
                    EmployeeId = 1,
                    EmployeeNumber = "Admin",
                    FullName = "System Administrator",
                    Email = "system.admin@yands.com",
                    DOB = DateTime.Now.AddYears(40),
                    DOA = DateTime.Now.AddYears(10),
                    DOR = null,
                    LocationCode = "MCT-M",
                    GenderName = "Male",
                    LocationName = "Muscat",
                    Nationality = "Pakistani",
                    Designation = "0",
                    Department = "0",
                    LeaveAllowed = 30,
                    PAddress = "",
                    CAddress = "",
                    ContactNumber = "",
                    BasicSalary = 1000,
                    Allowance = 200,
                    NationalityName = "Pakistani",
                    DesignationName = "",
                    DepartmentName = "",
                    CreatedByName = "",
                    UserType = 1
                };

                return ReturnList;
            }
            else
            {
                RBACDbContext db = new RBACDbContext();
                var ResultList = db.Employees.Where(w => w.EmployeeNumber.Trim().Equals(EmployeeNumber));

                var ReturnList = (from employee in ResultList
                                  join gender in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Gender)
                                  on employee.Gender equals gender.Mst_Value
                                  join location in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Branch)
                                  on employee.LocationCode equals location.Mst_Value
                                  join nat in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Nationality)
                                  on employee.Nationality equals nat.Mst_Value
                                  join dep in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Department)
                                  on employee.Department equals dep.Mst_Value
                                  join des in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Designation)
                                  on employee.Designation equals des.Mst_Value
                                  join crtby in db.Set<ApplicationUser>()
                                  on employee.CreatedBy equals crtby.Id
                                  select new UserVM
                                  {
                                      EmployeeId = employee.EmployeeId,
                                      EmployeeNumber = employee.EmployeeNumber,
                                      FullName = employee.FullName,
                                      Email = employee.Email,
                                      DOB = employee.DOB,
                                      DOA = employee.DOA,
                                      DOR = employee.DOR,
                                      LocationCode = employee.LocationCode,
                                      GenderName = gender.Mst_Desc,
                                      LocationName = location.Mst_Desc,
                                      Nationality = employee.Nationality,
                                      Designation = employee.Designation,
                                      Department = employee.Department,
                                      LeaveAllowed = employee.LeaveAllowed,
                                      PAddress = employee.PAddress,
                                      CAddress = employee.CAddress,
                                      ContactNumber = employee.ContactNumber,
                                      BasicSalary = employee.BasicSalary,
                                      Allowance = employee.Allowance,
                                      NationalityName = nat.Mst_Desc,
                                      DesignationName = des.Mst_Desc,
                                      DepartmentName = dep.Mst_Desc,
                                      CreatedByName = crtby.UserName,
                                      UserType = 1
                                  }).FirstOrDefault();

                return ReturnList;

            }
        }

        public static Employees GetEmployeeInfoId(int UserId)
        {
            RBACDbContext db = new RBACDbContext();
            var AppUser = db.Users.Where(w => w.Id == UserId).FirstOrDefault();
            var EmpList = db.Employees.Where(w=> w.EmployeeNumber == AppUser.UserName).ToList();

            var ReturnList = (from employee in EmpList
                              join gender in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Gender)
                              on employee.Gender equals gender.Mst_Value
                              join location in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Branch)
                              on employee.LocationCode equals location.Mst_Value
                              join nat in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Nationality)
                              on employee.Nationality equals nat.Mst_Value
                              join dep in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Department)
                              on employee.Department equals dep.Mst_Value
                              join des in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Designation)
                              on employee.Designation equals des.Mst_Value
                              select new Employees
                              {
                                  EmployeeId = employee.EmployeeId,
                                  EmployeeNumber = employee.EmployeeNumber,
                                  FullName = employee.FullName,
                                  Email = employee.Email,
                                  DOB = employee.DOB,
                                  DOA = employee.DOA,
                                  DOR = employee.DOR,
                                  Gender = employee.Gender,
                                  LocationCode = employee.LocationCode,
                                  GenderName = gender.Mst_Desc,
                                  LocationName = location.Mst_Desc,
                                  Nationality = employee.Nationality,
                                  Designation = employee.Designation,
                                  Department = employee.Department,
                                  LeaveAllowed = employee.LeaveAllowed,
                                  PAddress = employee.PAddress,
                                  CAddress = employee.CAddress,
                                  ContactNumber = employee.ContactNumber,
                                  BasicSalary = employee.BasicSalary,
                                  Allowance = employee.Allowance,
                                  NationalityName = nat.Mst_Desc,
                                  DesignationName = des.Mst_Desc,
                                  DepartmentName = dep.Mst_Desc,
                              }).FirstOrDefault();

            return ReturnList;

        }

        public decimal GetGratuity(string EmployeeNumber, DateTime UptoDate)
        {
            decimal GratuityAmount = 0;

            using (var context = new RBACDbContext())
            {
                var P_EMPNO = new SqlParameter { ParameterName = "@Emp", Value = EmployeeNumber };
                var P_UPTODT = new SqlParameter { ParameterName = "@Curr_Date", Value = UptoDate };

                GratuityAmount = context.Database.SqlQuery<decimal>("SELECT [dbo].[CalculateGratuity](@Emp,@Curr_Date)", P_EMPNO, P_UPTODT).FirstOrDefault();


            }
            return GratuityAmount;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
