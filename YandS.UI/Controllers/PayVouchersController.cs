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
using System.Web.Routing;
using YandS.UI.Models;

namespace YandS.UI.Controllers
{
    [RBAC]
    public class PayVouchersController : Controller
    {
        private RBACDbContext db = new RBACDbContext();

        public ActionResult Index()
        {
            /*
            var objBankList = db.LinkBankAccounts.ToList();

            if (!objBankList.Any())
            {
                IList<Link_Bank_Account> obj = new List<Link_Bank_Account>();
                obj.Add(new Link_Bank_Account() { BankId = 25, AccountId = 42 });
                obj.Add(new Link_Bank_Account() { BankId = 25, AccountId = 43 });

                obj.Add(new Link_Bank_Account() { BankId = 26, AccountId = 42 });
                obj.Add(new Link_Bank_Account() { BankId = 26, AccountId = 43 });
                obj.Add(new Link_Bank_Account() { BankId = 26, AccountId = 44 });
                obj.Add(new Link_Bank_Account() { BankId = 26, AccountId = 45 });
                obj.Add(new Link_Bank_Account() { BankId = 26, AccountId = 46 });
                obj.Add(new Link_Bank_Account() { BankId = 26, AccountId = 47 });

                obj.Add(new Link_Bank_Account() { BankId = 27, AccountId = 42 });
                obj.Add(new Link_Bank_Account() { BankId = 27, AccountId = 43 });

                obj.Add(new Link_Bank_Account() { BankId = 29, AccountId = 42 });
                obj.Add(new Link_Bank_Account() { BankId = 29, AccountId = 43 });

                obj.Add(new Link_Bank_Account() { BankId = 30, AccountId = 42 });
                obj.Add(new Link_Bank_Account() { BankId = 30, AccountId = 43 });

                obj.Add(new Link_Bank_Account() { BankId = 31, AccountId = 42 });
                obj.Add(new Link_Bank_Account() { BankId = 31, AccountId = 43 });

                obj.Add(new Link_Bank_Account() { BankId = 32, AccountId = 42 });
                obj.Add(new Link_Bank_Account() { BankId = 32, AccountId = 43 });

                db.LinkBankAccounts.AddRange(obj);
                db.SaveChanges();
            }
            */

            List<PayVoucher> ViewIndexList = GetPayVoucherList();

            if (User.IsInRole("VoucherApproval"))
            {
                return View(ViewIndexList.Where(w => w.VoucherStatus == "0"));
            }
            else
            {
                return View(ViewIndexList);

            }
        }

      
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayVoucher payVoucher = db.PayVouchers.Find(id);
            if (payVoucher == null)
            {
                return HttpNotFound();
            }
            return View(payVoucher);
        }

        public ActionResult Create()
        {

            var DebitList = GetBankList().Where(w => Convert.ToInt32(w.Mst_Value) > 0).ToList();

            var CreditList = GetBankList();

            PayVoucher payVoucher = new PayVoucher();

            payVoucher.ListLinkBankAccount = DebitList;
            payVoucher.ListLinkBankAccountCR = CreditList;
            payVoucher.Voucher_Date = DateTime.Now;
            ViewBag.Payment_Head = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.PaymentHeads), "Mst_Value", "Mst_Desc");
            ViewBag.Payment_To = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.PayTo), "Mst_Value", "Mst_Desc");
            ViewBag.StatusName = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ChequeStatus && m.Mst_Value != "0" && m.Mst_Value != "-1"), "Mst_Value", "Mst_Desc","3");
            ViewBag.CourtType = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CourtType), "Mst_Value", "Mst_Desc");
            ViewBag.LocationCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Location), "Mst_Value", "Mst_Desc");

            ViewBag.VoucherDate = DateTime.Now.ToString("dd/MM/yyyy");

            return View(payVoucher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Voucher_No,Voucher_Date,Payment_Type,Debit_Account,Payment_Head,Credit_Account,Amount,Remarks,Payment_Mode,Cheque_Number,Cheque_Date,Received_on,Status,Payment_To,VoucherType,PV_No,AuthorizeBy,AuthorizeDate,VoucherStatus,CourtType,LocationCode")] PayVoucher payVoucher, HttpPostedFileBase upload)
        {
            string UploadRoot = Helper.GetStorageRoot;

            if (ModelState.IsValid)
            {
                db.PayVouchers.Add(payVoucher);
                db.SaveChanges();

                if (upload != null && upload.ContentLength > 0)
                {
                    string FileExtension = Path.GetExtension(upload.FileName);

                    string FileName = payVoucher.Voucher_No + FileExtension;

                    string UploadPath = Path.Combine(UploadRoot, "PVDocuments", FileName);

                    upload.SaveAs(UploadPath);
                }
                return RedirectToAction("/Finance/PayVoucherIndex");

            }

            return View(payVoucher);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PayVoucher payVoucher = db.PayVouchers.Find(id);

            if (payVoucher == null)
            {
                return HttpNotFound();
            }

            List<PayVoucher> ViewEditModal = GetPayVoucherList();

            PayVoucher EditpayVoucher = ViewEditModal.Where(i => i.Voucher_No == (int)id).FirstOrDefault();

            var UserRec = db.Users.Where(w => w.Id == payVoucher.AuthorizeBy).FirstOrDefault();

            //PayVoucher EditpayVoucherProcedure = GetPayVoucherByid(id);

            payVoucher.PaymentModeName = EditpayVoucher.PaymentModeName;
            payVoucher.PaymentHeadName = EditpayVoucher.PaymentHeadName;
            payVoucher.PaymentTypeName = EditpayVoucher.PaymentTypeName;
            payVoucher.VoucherStatusName = EditpayVoucher.VoucherStatusName;
            payVoucher.VoucherTypeName = EditpayVoucher.VoucherTypeName;
            payVoucher.DebitAccountName = EditpayVoucher.DebitAccountName;
            payVoucher.CreditAccountName = EditpayVoucher.CreditAccountName;
            payVoucher.PaymentToName = EditpayVoucher.PaymentToName;
            payVoucher.CourtTypeName = EditpayVoucher.CourtTypeName;
            payVoucher.LocationName = EditpayVoucher.LocationName;
            payVoucher.AuthorizeByName = string.Format(@"{0} {1}", UserRec.Firstname, UserRec.Lastname);

            //ViewBag.VoucherDate = payVoucher.Voucher_Date.ToString("dd/MM/yyyy");
            //ViewBag.PVNumber = payVoucher.PV_No;


            var ListStatus = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ChequeStatus && m.Mst_Value != "-1").ToList();
            payVoucher.ListStatus = ListStatus;

            ViewBag.VoucherDoc = GetVoucherDoc((int)id);

            return View(payVoucher);
        }
        public ActionResult PrintVoucher(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PayVoucher payVoucher = db.PayVouchers.Find(id);

            if (payVoucher == null)
            {
                return HttpNotFound();
            }

            List<PayVoucher> ViewEditModal = GetPayVoucherList();

            PayVoucher EditpayVoucher = ViewEditModal.Where(i => i.Voucher_No == (int)id).FirstOrDefault();

            var UserRec = db.Users.Where(w => w.Id == payVoucher.AuthorizeBy).FirstOrDefault();
            var UserReccrt = db.Users.Where(w => w.Id == payVoucher.CreatedBy).FirstOrDefault();

            //PayVoucher EditpayVoucherProcedure = GetPayVoucherByid(id);

            payVoucher.PaymentModeName = EditpayVoucher.PaymentModeName;
            payVoucher.PaymentHeadName = EditpayVoucher.PaymentHeadName;
            payVoucher.PaymentTypeName = EditpayVoucher.PaymentTypeName;
            payVoucher.VoucherStatusName = EditpayVoucher.VoucherStatusName;
            payVoucher.VoucherTypeName = EditpayVoucher.VoucherTypeName;
            payVoucher.DebitAccountName = EditpayVoucher.DebitAccountName;
            payVoucher.CreditAccountName = EditpayVoucher.CreditAccountName;
            payVoucher.PaymentToName = EditpayVoucher.PaymentToName;
            payVoucher.CourtTypeName = EditpayVoucher.CourtTypeName;
            payVoucher.LocationName = EditpayVoucher.LocationName;
            payVoucher.AuthorizeByName = string.Format(@"{0} {1}", UserRec.Firstname, UserRec.Lastname);
            payVoucher.CreatedByName = string.Format(@"{0} {1}", UserReccrt.Firstname, UserReccrt.Lastname);

            string strBenef = string.Empty;

            if(payVoucher.Payment_Type == "1")
            {
                payVoucher.Beneficiary = EditpayVoucher.Payment_To == "0" ? EditpayVoucher.PaymentHeadName : EditpayVoucher.PaymentHeadName + " - " + EditpayVoucher.PaymentToName;
            }
            else
            {
                payVoucher.Beneficiary = EditpayVoucher.CreditAccountName;
            }


            return View(payVoucher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Voucher_No,Voucher_Date,Payment_Type,Debit_Account,Payment_Head,Credit_Account,Amount,Remarks,Payment_Mode,Cheque_Number,Cheque_Date,Received_on,Status,Payment_To,CreatedBy,CreatedOn,VoucherType,PV_No,AuthorizeBy,AuthorizeDate,VoucherStatus,CourtType,LocationCode")] PayVoucher payVoucher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payVoucher).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("/Finance/PayVoucherIndex");
            }
            return View(payVoucher);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayVoucher payVoucher = db.PayVouchers.Find(id);
            if (payVoucher == null)
            {
                return HttpNotFound();
            }
            return View(payVoucher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PayVoucher payVoucher = new PayVoucher();
            payVoucher = db.PayVouchers.Find(id);

            payVoucher.Status = "-1"; // To mimick Cancelled

            db.Entry(payVoucher).State = EntityState.Modified;
            db.SaveChanges();

            //return RedirectToAction("Index");
            return RedirectToAction("/Finance/PayVoucherIndex");

        }

        [RBAC]
        public ActionResult ApproveVoucher(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PayVoucher payVoucher = db.PayVouchers.Find(id);
            if (payVoucher == null)
            {
                return HttpNotFound();
            }

            List<PayVoucher> ViewEditModal = GetPayVoucherList();

            PayVoucher EditpayVoucher = ViewEditModal.Where(i => i.Voucher_No == (int)id).FirstOrDefault();

            var UserRec = db.Users.Where(w => w.Id == payVoucher.CreatedBy).FirstOrDefault();

            payVoucher.PaymentModeName = EditpayVoucher.PaymentModeName;
            payVoucher.PaymentHeadName = EditpayVoucher.PaymentHeadName;
            payVoucher.PaymentTypeName = EditpayVoucher.PaymentTypeName;
            payVoucher.VoucherStatusName = EditpayVoucher.VoucherStatusName;
            payVoucher.VoucherTypeName = EditpayVoucher.VoucherTypeName;
            payVoucher.DebitAccountName = EditpayVoucher.DebitAccountName;
            payVoucher.CreditAccountName = EditpayVoucher.CreditAccountName;
            payVoucher.PaymentToName = EditpayVoucher.PaymentToName;
            payVoucher.CourtTypeName = EditpayVoucher.CourtTypeName;
            payVoucher.LocationName = EditpayVoucher.LocationName;
            payVoucher.CreatedByName = string.Format(@"{0} {1}", UserRec.Firstname, UserRec.Lastname);

            var ListStatus = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ChequeStatus && m.Mst_Value != "-1").ToList();
            payVoucher.ListStatus = ListStatus;

            ViewBag.VoucherDoc = GetVoucherDoc((int)id);

            return View(payVoucher);
        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveVoucher([Bind(Include = "Voucher_No,Voucher_Date,Payment_Type,Debit_Account,Payment_Head,Credit_Account,Amount,Remarks,Payment_Mode,Cheque_Number,Cheque_Date,Received_on,Status,Payment_To,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn,VoucherType,VoucherStatus,CourtType,LocationCode")] PayVoucher payVoucher)
        {
            if (ModelState.IsValid)
            {

                payVoucher.AuthorizeBy = User.Identity.GetUserId();
                payVoucher.AuthorizeDate = DateTime.Now;

                string PV_Vooucher = string.Empty;

                if (payVoucher.VoucherStatus == "1")
                {
                    PV_Vooucher = GeneratePVNumber(payVoucher.Voucher_No);

                    payVoucher.PV_No = PV_Vooucher;
                }


                db.Entry(payVoucher).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("/Finance/PayVoucherIndex");

            }
            return View(payVoucher);
        }

        public string GeneratePVNumber(int Voucher_No)
        {
            string PV_Vooucher = string.Empty;
            using (var context = new RBACDbContext())
            {
                var VoucherNo = new SqlParameter { ParameterName = "@VoucherNo", Value = Voucher_No };

                PV_Vooucher = context.Database.SqlQuery<string>("SELECT dbo.FnGeneratePV_Number(@VoucherNo)", VoucherNo).FirstOrDefault();


            }
            return PV_Vooucher;
        }
        public string GetVoucherDoc(int Voucher_No)
        {
            string ReturnResult = string.Empty;

            string UploadRoot = Helper.GetStorageRoot;

            string UploadPath = Path.Combine(UploadRoot, "PVDocuments");

            DirectoryInfo d = new DirectoryInfo(UploadPath);
            var Image = d.GetFiles(Voucher_No + ".*").OrderByDescending(f => f.FullName).FirstOrDefault();

            if (Image == null)
                ReturnResult = "#";
            else
                ReturnResult = Image.ToString();
            return ReturnResult;
        }
        public List<SelectListItem> List_PaymentType()
        {
            var _retVal = new List<SelectListItem>();
            try
            {
                _retVal.Add(new SelectListItem { Text = "Normal", Value = "1" });
                _retVal.Add(new SelectListItem { Text = "Bank Transfer", Value = "2" });
            }
            catch { }
            return _retVal;
        }
        public List<PayVoucher> GetPayVoucherList()
        {
            List<PayVoucher> ResultList = new List<PayVoucher>();
            ResultList = db.PayVouchers.ToList();

            var ReturnResult = (from res in ResultList
                                join PMode in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.PaymentMode)
                                on res.Payment_Mode equals PMode.Mst_Value
                                join PType in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.PaymentType)
                                on res.Payment_Type equals PType.Mst_Value
                                join PHead in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.PaymentHeads)
                                on res.Payment_Head equals PHead.Mst_Value
                                join Status in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.ChequeStatus)
                                on res.Status equals Status.Mst_Value
                                join PayTo in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.PayTo)
                                on res.Payment_To equals PayTo.Mst_Value
                                join BankAccList in db.Set<Link_Bank_Account>()
                                 on res.Debit_Account equals BankAccList.LinkId
                                join bank_mas in db.Set<MasterSetups>()
                                on BankAccList.BankId equals bank_mas.MstId
                                join account_mas in db.Set<MasterSetups>()
                                on BankAccList.AccountId equals account_mas.MstId
                                join BankAccListCR in db.Set<Link_Bank_Account>()
                                on res.Credit_Account equals BankAccListCR.LinkId
                                join bank_masCR in db.Set<MasterSetups>()
                                on BankAccListCR.BankId equals bank_masCR.MstId
                                join account_masCR in db.Set<MasterSetups>()
                                on BankAccListCR.AccountId equals account_masCR.MstId
                                join VType in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.VoucherType)
                                on res.VoucherType equals VType.Mst_Value
                                join VStatus in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.VoucherStatus)
                                on res.VoucherStatus equals VStatus.Mst_Value
                                join CourtType in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.CourtType)
                                on res.CourtType equals CourtType.Mst_Value
                                join Loc in db.Set<MasterSetups>().Where(m => m.MstParentId == (int)MASTER_S.Location)
                                on res.LocationCode equals Loc.Mst_Value
                                select new PayVoucher
                                {
                                    Voucher_No = res.Voucher_No,
                                    Voucher_Date = res.Voucher_Date,
                                    Payment_Type = res.Payment_Type,
                                    Debit_Account = res.Debit_Account,
                                    Payment_Head = res.Payment_Head,
                                    Credit_Account = res.Credit_Account,
                                    Amount = res.Amount,
                                    Remarks = res.Remarks,
                                    Payment_Mode = res.Payment_Mode,
                                    Cheque_Number = res.Cheque_Number,
                                    Cheque_Date = res.Cheque_Date,
                                    Received_on = res.Received_on,
                                    Status = res.Status,
                                    Payment_To = res.Payment_To,
                                    PaymentHeadName = res.Payment_Head == "0" ? null : PHead.Mst_Desc,
                                    PaymentTypeName = PType.Mst_Desc,
                                    PaymentModeName = PMode.Mst_Desc,
                                    DebitAccountName = string.Format(@"{0} - {1}", bank_mas.Mst_Desc, account_mas.Mst_Desc),
                                    CreditAccountName = res.Credit_Account == 0 ? null : string.Format(@"{0} - {1}", bank_masCR.Mst_Desc, account_masCR.Mst_Desc),
                                    StatusName = res.Status == "0" ? "Voucher Created" : Status.Mst_Desc,
                                    PaymentToName = PayTo.Mst_Desc,
                                    VoucherType = res.VoucherType,
                                    VoucherTypeName = VType.Mst_Desc,
                                    VoucherStatus = res.VoucherStatus,
                                    VoucherStatusName =  VStatus.Mst_Value == "0" ? "Voucher Initialized" :  VStatus.Mst_Desc,
                                    CourtType = res.CourtType,
                                    CourtTypeName = CourtType.Mst_Value == "0" ? "" : CourtType.Mst_Desc,
                                    LocationCode = res.LocationCode,
                                    LocationName = Loc.Mst_Desc,
                                    PV_No = res.PV_No
                                 }).ToList();
            return ReturnResult;
        }
        public PayVoucher GetPayVoucherByid(int? id)
        {
            using (var context = new RBACDbContext())
            {
                SqlParameter param1 = new SqlParameter("@VoucherById", id);

                var RetResult = context.Database.SqlQuery<PayVoucher>("GetPayVoucherById @VoucherById", param1).SingleOrDefault();
                return RetResult;
            }
        }
        public List<SelectListItem> List_PaymentMode()
        {
            var _retVal = new List<SelectListItem>();
            try
            {
                _retVal.Add(new SelectListItem { Text = "Cheque", Value = "1" });
                _retVal.Add(new SelectListItem { Text = "Cash", Value = "2" });
            }
            catch { }
            return _retVal;
        }

        public static List<MasterSetups> GetBankList()
        {
            RBACDbContext db = new RBACDbContext();
            List<MasterSetups> ReturnResult = new List<MasterSetups>();

            var LinkBankAccoutListCR = db.LinkBankAccounts.ToList();
            ReturnResult = (from BA in LinkBankAccoutListCR
                              join bank_mas in db.Set<MasterSetups>()
                             on BA.BankId equals bank_mas.MstId
                              join account_mas in db.Set<MasterSetups>()
                                  on BA.AccountId equals account_mas.MstId
                              select new MasterSetups
                              {
                                  Mst_Value = BA.LinkId.ToString(),
                                  Mst_Desc = string.Format(@"{0} - {1} ( {2} )", bank_mas.Mst_Desc, account_mas.Mst_Desc, BA.AccountNumber)

                              }).ToList();

            return ReturnResult;

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
