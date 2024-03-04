using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YandS.UI.Models;

namespace YandS.UI.Controllers
{
    [RBAC]
    public class CaseInvoiceController : Controller
    {
        private RBACDbContext db = new RBACDbContext();

        // GET: CaseInvoice
        public ActionResult ManageInvoiceList()
        {
            List<CourtCases> ViewIndexList = CourtCasesController.GetCourtCasesList();

            return View(ViewIndexList);
        }
        public ActionResult CreateInvoiceList()
        {
            return View();
        }

        // GET: CaseInvoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseInvoice caseInvoice = db.CaseInvoice.Find(id);
            if (caseInvoice == null)
            {
                return HttpNotFound();
            }
            return View(caseInvoice);
        }

        // GET: CaseInvoice/Create
        public ActionResult Create(int? id)
        {
            ViewBag.CaseId = new SelectList(db.CourtCase, "CaseId", "OfficeFileNo");
            return View();
        }

        // POST: CaseInvoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceId,InvoiceDate,InvoiceNumber,CaseId,CourtType,InvoiceStatus,TransferType,TransferNumber,TransferDate,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn")] CaseInvoice caseInvoice)
        {
            if (ModelState.IsValid)
            {
                db.CaseInvoice.Add(caseInvoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CaseId = new SelectList(db.CourtCase, "CaseId", "OfficeFileNo");
            return View(caseInvoice);
        }

        // GET: CaseInvoice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseInvoice caseInvoice = db.CaseInvoice.Find(id);
            if (caseInvoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.CaseId = new SelectList(db.CourtCase, "CaseId", "OfficeFileNo");
            return View(caseInvoice);
        }

        // POST: CaseInvoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceId,InvoiceDate,InvoiceNumber,CaseId,CourtType,InvoiceStatus,TransferType,TransferNumber,TransferDate,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn")] CaseInvoice caseInvoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caseInvoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CaseId = new SelectList(db.CourtCase, "CaseId", "OfficeFileNo");
            return View(caseInvoice);
        }

        // GET: CaseInvoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseInvoice caseInvoice = db.CaseInvoice.Find(id);
            if (caseInvoice == null)
            {
                return HttpNotFound();
            }
            return View(caseInvoice);
        }

        // POST: CaseInvoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CaseInvoice caseInvoice = db.CaseInvoice.Find(id);
            db.CaseInvoice.Remove(caseInvoice);
            db.SaveChanges();
            return RedirectToAction("Index");
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
