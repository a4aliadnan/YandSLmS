using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YandS.UI.Models;

namespace YandS.UI.Controllers
{
    [RBAC]
    public class EmpDocsController : Controller
    {
        private RBACDbContext db = new RBACDbContext();

        // GET: EmpDocs
        public ActionResult Index(int? id)
        {
            List<EmpDoc> ResultList = new List<EmpDoc>();
            ResultList = db.EmpDocs.Where(p => p.EmployeId == id).ToList();
            string UploadRoot = Helper.GetStorageRoot;
            ViewBag.UploadRoot = UploadRoot;

            var ReturnView = (from docs in ResultList
                              join ListDocs in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.DocumentType)
                             on docs.DocTypeId equals ListDocs.Mst_Value
                              select new EmpDoc
                              {
                                 DocId = docs.DocId,
                                 DocTypeName = ListDocs.Mst_Desc,
                                 FileName = docs.FileName,
                                 OriginalFileName = docs.OriginalFileName,
                                 EmployeId = docs.EmployeId
                              }).ToList();


            var CurrentEmployee = db.Employees.Find(id);

            ViewBag.EmployeeName = CurrentEmployee.FullName;
            ViewBag.EmployeeId = id;

            return View(ReturnView);
            //return View();
        }

        // GET: EmpDocs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpDoc empDoc = db.EmpDocs.Find(id);
            if (empDoc == null)
            {
                return HttpNotFound();
            }
            return View(empDoc);
        }

        // GET: EmpDocs/Create
        public ActionResult Create(int? id)
        {
            //.OrderBy(o => o.Mst_Value)
            var CurrentEmployee = db.Employees.Find(id);
            ViewBag.EmployeeId = id;
            ViewBag.EmployeeName = CurrentEmployee.FullName;
            ViewBag.DocTypeId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.DocumentType).OrderBy(o=> o.DisplaySeq), "Mst_Value", "Mst_Desc");

            return View();
        }

        // POST: EmpDocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocId,DocTypeId,OriginalFileName,FileName,EmployeId")] EmpDoc empDoc, HttpPostedFileBase upload)
        {
            string UploadRoot = Helper.GetStorageRoot;
            var CurrentEmployee = db.Employees.Find(empDoc.EmployeId);

            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    Guid g = Guid.NewGuid();

                    string UniqueFileName = g.ToString();

                    string FileExtension = Path.GetExtension(upload.FileName);

                    string FileName = UniqueFileName + FileExtension;

                    string UploadPath = Path.Combine(UploadRoot, "StaffImages", FileName);

                    empDoc.FileName = FileName;
                    empDoc.OriginalFileName = upload.FileName;

                    upload.SaveAs(UploadPath);

                    db.EmpDocs.Add(empDoc);
                    db.SaveChanges();

                    Session["Message"] = new MessageVM
                    {
                        Category = "Success",
                        Message = "Data Save Successfully"
                    };
                    return RedirectToAction("Index", new RouteValueDictionary(new { id = CurrentEmployee.EmployeeId }));

                }

            }
            else
            {
                ModelState.AddModelError("UploadedFile", "File must be selected");
                return View(empDoc);
            }
            return View(empDoc);
        }

        // GET: EmpDocs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EmpDoc empDoc = db.EmpDocs.Find(id);
            if (empDoc == null)
            {
                return HttpNotFound();
            }

            var CurrentEmployee = db.Employees.Find(empDoc.EmployeId);

            ViewBag.EmployeeId = empDoc.EmployeId;
            ViewBag.EmployeeName = CurrentEmployee.FullName;

            var ListDocType = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.DocumentType).Where(w=> w.Mst_Value != "").ToList();
            empDoc.ListDocType = ListDocType;

            return View(empDoc);
        }

        // POST: EmpDocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocId,DocTypeId,OriginalFileName,FileName,EmployeId")] EmpDoc empDoc, HttpPostedFileBase upload)
        {
            string UploadRoot = Helper.GetStorageRoot;
            var CurrentEmployee = db.Employees.Find(empDoc.EmployeId);
            ViewBag.EmployeId = empDoc.EmployeId;
            ViewBag.EmployeeName = CurrentEmployee.FullName;


            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    Guid g = Guid.NewGuid();

                    string UniqueFileName = g.ToString();

                    string FileExtension = Path.GetExtension(upload.FileName);

                    string FileName = UniqueFileName + FileExtension;
                    string OLDFileName = empDoc.FileName;

                    string UploadPath = Path.Combine(UploadRoot, "StaffImages", FileName);
                    string DeletePath = Path.Combine(UploadRoot, "StaffImages", OLDFileName);

                    empDoc.FileName = FileName;
                    empDoc.OriginalFileName = upload.FileName;

                    upload.SaveAs(UploadPath);

                    System.IO.File.Delete(DeletePath);

                    db.Entry(empDoc).State = EntityState.Modified;
                    db.SaveChanges();
                    Session["Message"] = new MessageVM
                    {
                        Category = "Success",
                        Message = "Data Save Successfully"
                    };
                    return RedirectToAction("Index", new RouteValueDictionary(new { id = CurrentEmployee.EmployeeId }));

                }
                else
                {
                    ModelState.AddModelError("UploadedFile", "File must be selected");
                    return View(empDoc);
                }

            }
            return View(empDoc);
        }

        // GET: EmpDocs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpDoc empDoc = db.EmpDocs.Find(id);

            if (empDoc == null)
            {
                return HttpNotFound();
            }

            var CurrentEmployee = db.Employees.Find(empDoc.EmployeId);

            ViewBag.EmployeeId = empDoc.EmployeId;
            ViewBag.EmployeeName = CurrentEmployee.FullName;

            List<EmpDoc> ResultList = new List<EmpDoc>();
            ResultList = db.EmpDocs.Where(p => p.EmployeId == empDoc.EmployeId).ToList();

            var ReturnView = (from docs in ResultList
                              join ListDocs in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.DocumentType)
                             on docs.DocTypeId equals ListDocs.Mst_Value
                              select new EmpDoc
                              {
                                  DocId = docs.DocId,
                                  DocTypeId = docs.DocTypeId,
                                  DocTypeName = ListDocs.Mst_Desc,
                                  FileName = docs.FileName,
                                  OriginalFileName = docs.OriginalFileName,
                                  EmployeId = docs.EmployeId
                              }).ToList();

           var SelectedDocs = ReturnView.Where(w => w.DocTypeId == empDoc.DocTypeId).FirstOrDefault();

           empDoc.DocTypeName = SelectedDocs.DocTypeName;

            return View(empDoc);
        }

        // POST: EmpDocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmpDoc empDoc = db.EmpDocs.Find(id);
            string UploadRoot = Helper.GetStorageRoot;
            string UploadPath = Path.Combine(UploadRoot, "StaffImages", empDoc.FileName);

            db.EmpDocs.Remove(empDoc);
            db.SaveChanges();
            
            System.IO.File.Delete(UploadPath);

            return RedirectToAction("Index", new RouteValueDictionary(new { id = empDoc.EmployeId }));
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
