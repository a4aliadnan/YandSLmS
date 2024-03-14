using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using WhatsAppApi;
using YandS.DAL;
using YandS.UI.Models;

namespace YandS.UI.Controllers
{
    [RBAC]
    public class FinanceController : Controller
    {
        private RBACDbContext db = new RBACDbContext();
        const int FIRST_ROW = 0;

        #region INVOICE NOTIFICATION
        public ActionResult InvoiceNotificationIndex()
        {
            if (User.IsInRole("VoucherApproval"))
            {
                ViewBag.LocationId = "M";
                ViewBag.UserRole = "VoucherApproval";
                ViewBag.VoucherApproval = "checked";

            }
            else
                ViewBag.LocationId = Helper.GetEmployeeLocation(User.Identity.Name).Split('-')[1];


            ViewBag.PVTableActive = "";
            ViewBag.CaseTableActive = "";

            ViewBag.ViewContainer = "#div_Detail";
            ViewBag.PVTableActive = "PVTableActive";
            ViewBag.ViewToLoad = "_PVTable";


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InvoiceProcess(string Uid)
        {
            string strResult = Helper.ProcessInvoiceNotification(Uid);
            return Json(new { redirectTo = strResult });

        }

        #endregion
        #region Payment Voucher

        public ActionResult PayVoucherIndex()
        {
            if (User.IsInRole("VoucherApproval"))
            {
                ViewBag.UserRole = "VoucherApproval";
            }

            //if (id == 1)
            //    ViewBag.DataFor = "REF";
            //else if (id == 2)
            //    ViewBag.DataFor = "NONREF";
            //else if (id == 3)
            //    ViewBag.DataFor = "INTRA";

            //ReportCourtCases objDAL = new ReportCourtCases();

            //var DB_STATIC_TEXT = objDAL.getPVSummaryData();

            //if (DB_STATIC_TEXT.Rows.Count > 0)
            //{
            //    var VoucherTypeName = string.Empty;
            //    var LocationCode = string.Empty;
            //    var VoucherStatus = string.Empty;

            //    var query =
            //        from r in DB_STATIC_TEXT.AsEnumerable()
            //        where r.Field<string>("VoucherTypeName").Equals(VoucherTypeName) && r.Field<string>("LocationCode").Equals(LocationCode) && r.Field<string>("VoucherStatus").Equals(VoucherStatus)
            //        select r.Field<decimal>("PvAmount");

            //    VoucherTypeName = "REFUNDABLE";
            //    LocationCode = "MCT-M";
            //    VoucherStatus = "0";
            //    var DISP_VALUE = query.FirstOrDefault();
            //    ViewBag.MCTTotal = DISP_VALUE;

            //    VoucherTypeName = "REFUNDABLE";
            //    LocationCode = "SAL-S";
            //    VoucherStatus = "0";
            //    DISP_VALUE = query.FirstOrDefault();
            //    ViewBag.SLLTotal = DISP_VALUE;

            //    VoucherTypeName = "NON-REFUNDABLE";
            //    LocationCode = "MCT-M";
            //    VoucherStatus = "0";
            //    DISP_VALUE = query.FirstOrDefault();
            //    ViewBag.REFMCTTotal = DISP_VALUE;

            //    VoucherTypeName = "NON-REFUNDABLE";
            //    LocationCode = "SAL-S";
            //    VoucherStatus = "0";
            //    DISP_VALUE = query.FirstOrDefault();
            //    ViewBag.REFSLLTotal = DISP_VALUE;

            //    /**/
            //    VoucherTypeName = "REFUNDABLE";
            //    LocationCode = "MCT-M";
            //    VoucherStatus = "1";
            //    DISP_VALUE = query.FirstOrDefault();
            //    ViewBag.MCTTotalApr = DISP_VALUE;

            //    VoucherTypeName = "REFUNDABLE";
            //    LocationCode = "SAL-S";
            //    VoucherStatus = "1";
            //    DISP_VALUE = query.FirstOrDefault();
            //    ViewBag.SLLTotalApr = DISP_VALUE;

            //    VoucherTypeName = "NON-REFUNDABLE";
            //    LocationCode = "MCT-M";
            //    VoucherStatus = "1";
            //    DISP_VALUE = query.FirstOrDefault();
            //    ViewBag.NMCTTotalApr = DISP_VALUE;

            //    VoucherTypeName = "NON-REFUNDABLE";
            //    LocationCode = "SAL-S";
            //    VoucherStatus = "1";
            //    DISP_VALUE = query.FirstOrDefault();
            //    ViewBag.NSLLTotalApr = DISP_VALUE;


            //    VoucherTypeName = "PDC";
            //    LocationCode = "MCT-M";
            //    VoucherStatus = "0";
            //    DISP_VALUE = query.FirstOrDefault();
            //    ViewBag.MCTTotalPDC = DISP_VALUE;

            //    VoucherTypeName = "PDC";
            //    LocationCode = "SAL-S";
            //    VoucherStatus = "0";
            //    DISP_VALUE = query.FirstOrDefault();
            //    ViewBag.SLLTotalPDC = DISP_VALUE;


            //    ViewBag.ToBeRegTTL = Convert.ToInt32(ViewBag.ToBeRegMCT) + Convert.ToInt32(ViewBag.ToBeRegSLL);

            //    ViewBag.EnforcementTTL = Convert.ToInt32(ViewBag.EnforcementMCT) + Convert.ToInt32(ViewBag.EnforcementSLL);
            //}

            return View();
        }
        public ActionResult PayVoucherIndexRefNonRef(int? id, string mode = null, PayVoucher payVoucher = null)
        {
            if (User.IsInRole("VoucherApproval"))
            {
                ViewBag.UserRole = "VoucherApproval";
            }

            ViewBag.OfficeFileNo = "";
            ViewBag.IsEdit = "";

            ViewBag.PayVoucherCreateNewActive = "";
            ViewBag.PayVoucherPendingActive = "";
            ViewBag.PayVoucherPDCActive = "";
            ViewBag.PayVoucherRApprovedActive = "";
            ViewBag.PayVoucherNRApprovedActive = "";
            ViewBag.PayVoucherRejectedActive = "";
            ViewBag.ErrorReturn = "N";
            ViewBag.Voucher_No = "0";

            if (id == null)
            {
                if (mode == "E")
                {
                    ViewBag.StartTAB = "btn_PayVoucherCreateNew";
                    ViewBag.ErrorReturn = "Y";
                    ViewBag.mode = "C";
                    ViewBag.PayVoucherCreateNewActive = "PayVoucherCreateNewActive";

                    ViewBag.CourtType = new SelectList(GetListForInvoiceCaseLevel(0), "Mst_Value", "Mst_Desc");
                    ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
                    ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
                    ViewBag.Payment_Head = new SelectList(Helper.LoadPayForRefunableOnly(), "Mst_Value", "Mst_Desc", "10007");
                    ViewBag.PaymentHeadDetail = new SelectList(Helper.LoadDependentDDL("10007"), "Mst_Value", "Mst_Desc");
                    ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(), "Mst_Value", "Mst_Desc", "1901");
                    ViewBag.PaymentToBenificry = new SelectList(Helper.LoadDependentDDL("1901"), "Mst_Value", "Mst_Desc", User.Identity.Name);
                    ViewBag.PayToBankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc");
                    ViewBag.PayToMessageLang = new SelectList(Helper.GetLangForSelect(), "Mst_Value", "Mst_Desc");
                    ViewBag.VoucherDate = DateTime.Now.ToString("dd/MM/yyyy");
                    return View(payVoucher);
                }
                else
                {
                    ViewBag.mode = "C";
                    ViewBag.FormMode = "CREATE";
                    ViewBag.StartTAB = "btn_PayVoucherPending";
                    ViewBag.PayVoucherPendingActive = "PayVoucherPendingActive";
                    ViewBag.LoadTable = "dtPENDING";

                }
            }
            else
            {
                ViewBag.Voucher_No = id;

                if (mode == "A")
                {
                    ViewBag.mode = "A";
                    ViewBag.StartTAB = "btn_PayVoucherCreateNew";
                    ViewBag.PayVoucherCreateNewActive = "PayVoucherCreateNewActive";
                    ViewBag.LoadTable = "";

                }
                else if (mode == "SA")
                {
                    ViewBag.mode = "A";
                    ViewBag.StartTAB = "btn_PayVoucherPending";
                    ViewBag.PayVoucherPendingActive = "PayVoucherPendingActive";
                    ViewBag.LoadTable = "dtPENDING";

                }
                else if (mode == "EA")
                {
                    ViewBag.BankTransferStaff = new SelectList(GetListBankTransferAuth(), "Mst_Value", "Mst_Desc");
                    return View(payVoucher);
                }
                else if (mode == "SE")
                {
                    ViewBag.mode = "E";
                    ViewBag.StartTAB = "btn_PayVoucherPending";
                    ViewBag.PayVoucherPendingActive = "PayVoucherPendingActive";
                    ViewBag.LoadTable = "dtPENDING";

                }
                else if (mode == "EE")
                {
                    var ListStatus = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ChequeStatus && m.Mst_Value != "-1").ToList();
                    payVoucher.ListStatus = ListStatus;
                    ViewBag.VoucherDoc = Helper.GetVoucherDoc(payVoucher.Voucher_No);
                    ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", payVoucher.Debit_Account);
                    ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", payVoucher.Credit_Account);
                    ViewBag.DebitAccountName = Helper.GetBankList().Where(w => w.Mst_Value == Convert.ToString(payVoucher.Debit_Account)).FirstOrDefault().Mst_Desc;
                    return View(payVoucher);

                }
                else if (mode == "M")
                {
                    ViewBag.mode = "M";
                    ViewBag.StartTAB = "btn_PayVoucherCreateNew";
                    ViewBag.PayVoucherCreateNewActive = "PayVoucherCreateNewActive";
                    ViewBag.LoadTable = "";

                }
            }
            //var LC_TO = "923351353873";
            //var LC_TO = "923343898331";
            //var LC_MESSAGE = "Dear This is Test Message Please Ignore";

            //var res = Helper.SendWhatsApp(LC_TO, LC_MESSAGE);

            return View();
        }

        [AllowAnonymous]
        public ActionResult AjaxIndexDataPV(string DataFor)
        {
            var request = HttpContext.Request;
            List<PVListForIndex> data = new List<PVListForIndex>();

            var start = (Convert.ToInt32(Request["start"]));
            var Length = (Convert.ToInt32(Request["length"])) == 0 ? 10 : (Convert.ToInt32(Request["length"]));
            var searchvalue = Request["search[value]"] ?? "";
            var sortcoloumnIndex = Convert.ToInt32(Request["order[0][column]"]);
            var sortDirection = Request["order[0][dir]"] ?? "asc";
            var recordsTotal = 0;
            string MuscatTTL = string.Empty;
            string SalalahTTL = string.Empty;
            string[] ArrDataFor = { "PENDING", "PENDING_CE","PENDING_OE","PENDING_PDC", "REFAPPROVED", "NRAPPROVED", "REFAPPROVEDNEW", "NRAPPROVEDNEW", "NEWPDC", "REJECT","REJECTNEW", "PV-TABLE", "CASE-TABLE", "TRANS-TABLE", "INV-TABLE" };
            string strDataFor = string.Empty;
            string LocationId = string.Empty;
            string UserLocation = string.Empty;
            DateTime DateFrom = DateTime.Now;
            DateTime DateTo = DateTime.Now;
            string ProcedureName = string.Empty;
            int MCTRecords = 0;
            decimal TOTAL_VAT = 0;
            int SLLRecords = 0;

            if (User.IsInRole("CourtCasesViewAll") || User.IsSysAdmin())
                UserLocation = "A";
            else
                UserLocation = Helper.GetEmployeeLocation(User.Identity.Name);

            try
            {
                strDataFor = request.Params["DataTableName"].ToString();
            }
            catch (Exception e)
            {
                strDataFor = e.Message;
            }

            try
            {
                LocationId = request.Params["LocationId"].ToString();
            }
            catch (Exception ex)
            {
                LocationId = UserLocation.ToUpper().Substring(0, 1);
            }

            try
            {
                if (strDataFor.In("TAX_VAT-TAX", "TAX_VAT-LawyerFeeVAT", "TAX_VAT-ExpensesVAT", "TAX_VAT-QuarterltVAT", "MY_TASK-PV"))
                {
                    if (strDataFor.In("TAX_VAT-TAX", "TAX_VAT-LawyerFeeVAT", "TAX_VAT-ExpensesVAT", "TAX_VAT-QuarterltVAT"))
                    {
                        DateFrom = request.Params["DateFrom"].ToString() == "" ? DateTime.Now.AddYears(-100) : DateTime.ParseExact(request.Params["DateFrom"].ToString(), "dd/MM/yyyy", null);
                        DateTo = request.Params["DateTo"].ToString() == "" ? DateTime.Now.AddYears(100) : DateTime.ParseExact(request.Params["DateTo"].ToString(), "dd/MM/yyyy", null);
                    }

                    List<string> parName = Helper.getDefaultParNames();
                    List<object> parValues = Helper.getDefaultParValues();
                    ProcedureName = request.Params["ProcedureName"].ToString();

                    parName.AddRange(new[] { "@UserName", "@DataFor", "@Location" });
                    parValues.AddRange(new[] { User.Identity.Name, strDataFor, UserLocation });

                    parName.AddRange(new[] { "@DateFrom", "@DateTo" });
                    parValues.Add(DateFrom);
                    parValues.Add(DateTo);

                    if (strDataFor == "MY_TASK-PV")
                    {
                        parName.Add("@UserID");
                        parValues.Add(User.Identity.GetUserId());
                    }

                    var ds = Helper.getDataSet(parName.ToArray(), parValues.ToArray(), TableNames: new string[] { "data", "summary" }, procedureName: ProcedureName);
                    DataTable dt = ds.Tables["data"];
                    DataTable Summarydt = ds.Tables["summary"];

                    var jsondata = dt.ToDictionary();

                    recordsTotal = !string.IsNullOrEmpty(Summarydt.Rows[0]["recordsTotal"].ToString()) ? int.Parse(Summarydt.Rows[0]["recordsTotal"].ToString()) : 0;
                    TOTAL_VAT = !string.IsNullOrEmpty(Summarydt.Rows[0]["TOTAL_VAT"].ToString()) ? decimal.Parse(Summarydt.Rows[0]["TOTAL_VAT"].ToString()) : 0;

                    return Json(new { data = jsondata, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, TOTAL_VAT = TOTAL_VAT }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    if (ArrDataFor.Contains(strDataFor))
                    {
                        if (strDataFor == "PV-TABLE" || strDataFor == "CASE-TABLE" || strDataFor == "TRANS-TABLE" || strDataFor == "INV-TABLE")
                        {

                            var DtView = new List<InvoiceNotificationDTView>();
                            DtView = Helper.GetInvoiceNotificationDTView(sortcoloumnIndex, start, searchvalue, Length, sortDirection, User.Identity.Name, strDataFor, LocationId);
                            recordsTotal = DtView.Count > 0 ? DtView[0].TotalRecords : 0;
                            return Json(new { data = DtView, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, MuscatTotal = MuscatTTL, SalalahTotal = SalalahTTL }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var DtView = new PayVoucherDTView();
                            DtView = Helper.GetPVDTView(sortcoloumnIndex, start, searchvalue, Length, sortDirection, User.Identity.Name, strDataFor);
                            recordsTotal = data.Count > 0 ? data[0].TotalRecords : 0;
                            return Json(new { data = DtView.data, recordsTotal = DtView.recordsTotal, recordsFiltered = DtView.recordsFiltered, MuscatTotal = DtView.MuscatTTL, SalalahTotal = DtView.SalalahTTL }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else if (DataFor == "INTRA")
                    {
                        data = Helper.GetPVList(sortcoloumnIndex, start, searchvalue, Length, sortDirection, User.Identity.Name, DataFor).ToList();
                        recordsTotal = data.Count > 0 ? data[0].TotalRecords : 0;
                    }
                    else
                    {
                        DataFor = string.Empty;
                        try
                        {
                            DataFor = request.Params["DataTableName"].ToString();
                            data = Helper.GetPVList(sortcoloumnIndex, start, searchvalue, Length, sortDirection, User.Identity.Name, DataFor).ToList();
                            recordsTotal = data.Count > 0 ? data[0].TotalRecords : 0;

                        }
                        catch (Exception e)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Json(new { data = data, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, MuscatTotal = MuscatTTL, SalalahTotal = SalalahTTL }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult PayVoucherCreate()
        {
            ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
            ViewBag.Payment_Head = new SelectList(Helper.LoadPayForRefunableOnly(), "Mst_Value", "Mst_Desc", "10007");
            ViewBag.PaymentHeadDetail = new SelectList(Helper.LoadDependentDDL("10007"), "Mst_Value", "Mst_Desc");
            ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(), "Mst_Value", "Mst_Desc", "1901");
            ViewBag.PaymentToBenificry = new SelectList(Helper.LoadDependentDDL("1901"), "Mst_Value", "Mst_Desc", User.Identity.Name);
            ViewBag.PayToBankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc");
            ViewBag.PayToMessageLang = new SelectList(Helper.GetLangForSelect(), "Mst_Value", "Mst_Desc");
            ViewBag.VoucherDate = DateTime.Now.ToString("dd/MM/yyyy");

            return View();
        }
        public ActionResult PayVoucherTaxVat()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PayVoucherCreate([Bind(Include = "Voucher_No,Voucher_Date,Payment_Type,Debit_Account,Payment_Head,Credit_Account,Amount,Remarks,Payment_Mode,Cheque_Number,Cheque_Date,Received_on,Status,Payment_To,VoucherType,PV_No,AuthorizeBy,AuthorizeDate,VoucherStatus,CourtType,LocationCode,TransTypeCode,TransReasonCode,Payment_Head_Remarks,CaseId,CaseInvoices,BankTransferStaff")] PayVoucher payVoucher, HttpPostedFileBase upload)
        {
            string UploadRoot = Helper.GetStorageRoot;
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                payVoucher.Voucher_Date = DateTime.Today;

                db.PayVouchers.Add(payVoucher);
                db.SaveChanges();

                if (upload != null && upload.ContentLength > 0)
                {
                    string FileExtension = Path.GetExtension(upload.FileName);

                    string FileName = payVoucher.Voucher_No + FileExtension;

                    string UploadPath = Path.Combine(UploadRoot, "PVDocuments", FileName);

                    upload.SaveAs(UploadPath);
                }
                Session["Message"] = new MessageVM
                {
                    Category = "Success",
                    Message = "Data Save Successfully"
                };
                //return RedirectToAction("PayVoucherIndex");
                return RedirectToAction("PayVoucherIndex", new RouteValueDictionary(new { payVoucher.VoucherType }));


            }

            ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
            ViewBag.Payment_Head = new SelectList(Helper.LoadPayForRefunableOnly(), "Mst_Value", "Mst_Desc", "10007");
            ViewBag.PaymentHeadDetail = new SelectList(Helper.LoadDependentDDL("10007"), "Mst_Value", "Mst_Desc");
            ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(), "Mst_Value", "Mst_Desc", "1901");
            ViewBag.PaymentToBenificry = new SelectList(Helper.LoadDependentDDL("1901"), "Mst_Value", "Mst_Desc", User.Identity.Name);
            ViewBag.PayToBankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc");
            ViewBag.PayToMessageLang = new SelectList(Helper.GetLangForSelect(), "Mst_Value", "Mst_Desc");
            ViewBag.VoucherDate = DateTime.Now.ToString("dd/MM/yyyy");

            Session["Message"] = new MessageVM
            {
                Category = "Error",
                Message = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray())
            };
            return View(payVoucher);
        }

        public ActionResult PayVoucherCreateIntra()
        {

            ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
            ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
            ViewBag.TransTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.PVTransType), "Mst_Value", "Mst_Desc");
            ViewBag.TransReasonCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.PVTransReason), "Mst_Value", "Mst_Desc");
            ViewBag.VoucherDate = DateTime.Now.ToString("dd/MM/yyyy");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PayVoucherCreateIntra(PayVoucher payVoucher, HttpPostedFileBase upload)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            string UploadRoot = Helper.GetStorageRoot;

            if (ModelState.IsValid)
            {
                payVoucher.Voucher_Date = DateTime.Today;

                db.PayVouchers.Add(payVoucher);
                db.SaveChanges();

                if (upload != null && upload.ContentLength > 0)
                {
                    string FileExtension = Path.GetExtension(upload.FileName);

                    string FileName = payVoucher.Voucher_No + FileExtension;

                    string UploadPath = Path.Combine(UploadRoot, "PVDocuments", FileName);

                    upload.SaveAs(UploadPath);
                }
                Session["Message"] = new MessageVM
                {
                    Category = "Success",
                    Message = "Data Save Successfully"
                };

                return RedirectToAction("PayVoucherIndex", new RouteValueDictionary(new { id = payVoucher.VoucherType }));

                //return RedirectToAction("PayVoucherIndex");

            }

            ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
            ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
            ViewBag.TransTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.PVTransType), "Mst_Value", "Mst_Desc");
            ViewBag.TransReasonCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.PVTransReason), "Mst_Value", "Mst_Desc");
            ViewBag.VoucherDate = DateTime.Now.ToString("dd/MM/yyyy");

            Session["Message"] = new MessageVM
            {
                Category = "Error",
                Message = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray())
            };
            return View(payVoucher);
        }

        public ActionResult PayVoucherCreateRefunable(int? id)
        {

            PayVoucher payVoucher = new PayVoucher();

            //var courtCase = db.CourtCase.Find(id);

            //payVoucher.CaseId = courtCase.CaseId;
            //payVoucher.Defendant = courtCase.Defendant;
            //payVoucher.OfficeFileNo = courtCase.OfficeFileNo;
            //payVoucher.AccountContractNo = courtCase.AccountContractNo;
            //payVoucher.ClientFileNo = courtCase.ClientFileNo;
            //payVoucher.ClaimAmount = courtCase.ClaimAmount;
            //payVoucher.ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == courtCase.ClientCode).FirstOrDefault().Mst_Desc;


            //var CaseCreatedBy = EmployeesController.GetEmployeeInfoId(courtCase.CreatedBy);
            //ViewBag.BranchName = CaseCreatedBy.LocationName;


            ViewBag.CourtType = new SelectList(GetListForInvoiceCaseLevel(0), "Mst_Value", "Mst_Desc");
            //ViewBag.CaseInvoices = new SelectList(GetListForPVInvoicesByCase((int)payVoucher.CaseId), "Mst_Value", "Mst_Desc");
            ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
            ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
            ViewBag.Payment_Head = new SelectList(Helper.LoadPayForRefunableOnly(), "Mst_Value", "Mst_Desc", "10007");
            ViewBag.PaymentHeadDetail = new SelectList(Helper.LoadDependentDDL("10007"), "Mst_Value", "Mst_Desc");
            ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(), "Mst_Value", "Mst_Desc", "1901");
            ViewBag.PaymentToBenificry = new SelectList(Helper.LoadDependentDDL("1901"), "Mst_Value", "Mst_Desc", User.Identity.Name);
            ViewBag.PayToBankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc");
            ViewBag.PayToMessageLang = new SelectList(Helper.GetLangForSelect(), "Mst_Value", "Mst_Desc");
            ViewBag.VoucherDate = DateTime.Now.ToString("dd/MM/yyyy");

            if (id == null)
            {
                ViewBag.IsReturnToSessionRoll = "";
            }
            else
            {
                ViewBag.IsReturnToSessionRoll = id.ToString();
            }

            return View(payVoucher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PayVoucherCreateRefunable(PayVoucher payVoucher, HttpPostedFileBase upload)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            string UploadRoot = Helper.GetStorageRoot;

            if (ModelState.IsValid)
            {

                payVoucher.Voucher_Date = DateTime.UtcNow.AddHours(4);

                db.PayVouchers.Add(payVoucher);
                db.SaveChanges();

                if (upload != null && upload.ContentLength > 0)
                {
                    string FileExtension = Path.GetExtension(upload.FileName);

                    string FileName = payVoucher.Voucher_No + FileExtension;

                    string UploadPath = Path.Combine(UploadRoot, "PVDocuments", FileName);

                    upload.SaveAs(UploadPath);
                }

                Session["Message"] = new MessageVM
                {
                    Category = "Success",
                    Message = "Data Save Successfully"
                };

                if (payVoucher.ReturnApprove == "")
                    return RedirectToAction("PayVoucherIndex", new RouteValueDictionary(new { id = payVoucher.VoucherType }));
                else
                    return RedirectToAction("Index", "SessionRoll", new RouteValueDictionary(new { id = int.Parse(payVoucher.ReturnApprove) }));
            }

            ViewBag.CourtType = new SelectList(GetListForInvoiceCaseLevel((int)payVoucher.CaseId), "Mst_Value", "Mst_Desc");
            ViewBag.CaseInvoices = new SelectList(GetListForPVInvoicesByCase((int)payVoucher.CaseId), "Mst_Value", "Mst_Desc");
            ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
            ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
            ViewBag.Payment_Head = new SelectList(Helper.LoadPayForRefunableOnly(), "Mst_Value", "Mst_Desc", "10007");
            ViewBag.PaymentHeadDetail = new SelectList(Helper.LoadDependentDDL("10007"), "Mst_Value", "Mst_Desc");
            ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(), "Mst_Value", "Mst_Desc", "1901");
            ViewBag.PaymentToBenificry = new SelectList(Helper.LoadDependentDDL("1901"), "Mst_Value", "Mst_Desc", User.Identity.Name);
            ViewBag.PayToBankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc");
            ViewBag.PayToMessageLang = new SelectList(Helper.GetLangForSelect(), "Mst_Value", "Mst_Desc");

            ViewBag.VoucherDate = DateTime.Now.ToString("dd/MM/yyyy");

            Session["Message"] = new MessageVM
            {
                Category = "Error",
                Message = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray())
            };
            return View(payVoucher);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PayVoucherCreateRefNonRef(PayVoucher payVoucher, HttpPostedFileBase uploadPVSupDocs)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            string UploadRoot = Helper.GetStorageRoot;

            if (ModelState.IsValid)
            {
                string UserLocation = Helper.GetEmployeeLocation(User.Identity.Name);


                payVoucher.Voucher_Date = DateTime.UtcNow.AddHours(4);

                string strPVLocation = string.Empty;

                if (payVoucher.CaseId > 0)
                    strPVLocation = Helper.getPVLocation(payVoucher.CaseId ?? 0);
                else
                    strPVLocation = UserLocation.Substring(0, 3).ToUpper();

                payVoucher.PVLocation = strPVLocation;

                db.PayVouchers.Add(payVoucher);
                db.SaveChanges();

                if (uploadPVSupDocs != null && uploadPVSupDocs.ContentLength > 0)
                {
                    string FileExtension = Path.GetExtension(uploadPVSupDocs.FileName);

                    string FileName = payVoucher.Voucher_No + FileExtension;

                    string UploadPath = Path.Combine(UploadRoot, "PVDocuments", FileName);

                    uploadPVSupDocs.SaveAs(UploadPath);
                }
                
                if (payVoucher.ReturnApprove == "Y")
                {
                    PayVoucher ModelToapprove = db.PayVouchers.Find(payVoucher.Voucher_No);
                    PayVoucher EditpayVoucher = GetPayVoucherList("", ModelToapprove.Voucher_No).FirstOrDefault();

                    var UserRec = db.Users.Where(w => w.Id == ModelToapprove.CreatedBy).FirstOrDefault();

                    ModelToapprove.PaymentModeName = EditpayVoucher.PaymentModeName;
                    ModelToapprove.PaymentHeadName = EditpayVoucher.PaymentHeadName;
                    ModelToapprove.PaymentTypeName = EditpayVoucher.PaymentTypeName;
                    ModelToapprove.VoucherStatusName = EditpayVoucher.VoucherStatusName;
                    ModelToapprove.VoucherTypeName = EditpayVoucher.VoucherTypeName;
                    ModelToapprove.DebitAccountName = EditpayVoucher.DebitAccountName;
                    ModelToapprove.CreditAccountName = EditpayVoucher.CreditAccountName;
                    ModelToapprove.PaymentToName = EditpayVoucher.PaymentToName;
                    ModelToapprove.CourtTypeName = EditpayVoucher.CourtTypeName;
                    ModelToapprove.LocationName = EditpayVoucher.LocationName;
                    ModelToapprove.TransTypeCode = EditpayVoucher.TransTypeCode;
                    ModelToapprove.TransReasonCode = EditpayVoucher.TransReasonCode;
                    ModelToapprove.TransTypeName = EditpayVoucher.TransTypeName;
                    ModelToapprove.TransReasonName = EditpayVoucher.TransReasonName;
                    ModelToapprove.CreatedByName = string.Format(@"{0} {1}", UserRec.Firstname, UserRec.Lastname);

                    ViewBag.BankTransferStaff = new SelectList(GetListBankTransferAuth(), "Mst_Value", "Mst_Desc");

                    if (ModelToapprove.CaseId > 0)
                    {
                        var courtCase = db.CourtCase.Where(w => w.CaseId == ModelToapprove.CaseId).FirstOrDefault();
                        ModelToapprove.OfficeFileNo = courtCase.OfficeFileNo;
                        ModelToapprove.ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == courtCase.ClientCode).FirstOrDefault().Mst_Desc;
                        ModelToapprove.Defendant = courtCase.Defendant;

                    }
                    else
                    {
                        ViewBag.AppHidden = "AppHidden";
                    }

                    var ListStatus = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ChequeStatus && m.Mst_Value != "-1").ToList();
                    ModelToapprove.ListStatus = ListStatus;

                    ViewBag.VoucherDoc = Helper.GetVoucherDoc(ModelToapprove.Voucher_No);
                    ViewBag.PVTransferDoc = Helper.GetPVTranferDoc(ModelToapprove.Voucher_No);
                    ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", ModelToapprove.Debit_Account);
                    
                    ViewBag.CourtType = new SelectList(GetListForInvoiceCaseLevel(0), "Mst_Value", "Mst_Desc", payVoucher.CourtType);
                    ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", payVoucher.Credit_Account);

                    if (payVoucher.IsDeleted)
                    {
                        ViewBag.Payment_Head = new SelectList(Helper.LoadPayForRefunableOnly(), "Mst_Value", "Mst_Desc", "10007");
                        ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(), "Mst_Value", "Mst_Desc", "1901");
                    }
                    else
                    {
                        ViewBag.Payment_Head = new SelectList(Helper.LoadPayFor(), "Mst_Value", "Mst_Desc", payVoucher.Payment_Head);
                        ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(), "Mst_Value", "Mst_Desc", payVoucher.Payment_To);
                    }

                    ViewBag.PaymentHeadDetail = new SelectList(Helper.LoadDependentDDL(payVoucher.Payment_Head), "Mst_Value", "Mst_Desc", payVoucher.PaymentHeadDetail);
                    ViewBag.PaymentToBenificry = new SelectList(Helper.LoadDependentDDL(payVoucher.Payment_To), "Mst_Value", "Mst_Desc", payVoucher.PaymentToBenificry);
                    ViewBag.PayToBankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc");
                    ViewBag.PayToMessageLang = new SelectList(Helper.GetLangForSelect(), "Mst_Value", "Mst_Desc");

                    return PartialView("_PayVoucherApprove", ModelToapprove);
                }
                else
                    return Json(new { redirectTo = "#btn_PayVoucherPending" });
            }

            return PartialView(payVoucher);
        }
        
        public ActionResult PayVoucherEdit(int? id)
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

            string UserLocation = string.Empty;

            if (User.IsInRole("CourtCasesViewAll") || User.IsSysAdmin())
                UserLocation = string.Empty;
            else
                UserLocation = Helper.GetEmployeeLocation(User.Identity.Name);


            //List<PayVoucher> ViewEditModal = GetPayVoucherList(UserLocation);

            PayVoucher EditpayVoucher = GetPayVoucherList(UserLocation, (int)id).FirstOrDefault();

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
            payVoucher.TransTypeCode = EditpayVoucher.TransTypeCode;
            payVoucher.TransReasonCode = EditpayVoucher.TransReasonCode;
            payVoucher.TransTypeName = EditpayVoucher.TransTypeName;
            payVoucher.TransReasonName = EditpayVoucher.TransReasonName;

            payVoucher.AuthorizeByName = string.Format(@"{0} {1}", UserRec.Firstname, UserRec.Lastname);



            var ListStatus = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ChequeStatus && m.Mst_Value != "-1").ToList();
            payVoucher.ListStatus = ListStatus;

            ViewBag.VoucherDoc = Helper.GetVoucherDoc((int)id);
            ViewBag.PVTransferDoc = Helper.GetPVTranferDoc((int)id);
            ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", payVoucher.Debit_Account);
            ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", payVoucher.Credit_Account);

            ViewBag.DebitAccountName = Helper.GetBankList().Where(w => w.Mst_Value == Convert.ToString(payVoucher.Debit_Account)).FirstOrDefault().Mst_Desc;

            return View(payVoucher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PayVoucherEdit(PayVoucher payVoucher, HttpPostedFileBase upload)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string UploadRoot = Helper.GetStorageRoot;


            if (ModelState.IsValid)
            {
                db.Entry(payVoucher).State = EntityState.Modified;
                db.SaveChanges();

                if (upload != null && upload.ContentLength > 0)
                {
                    string FileExtension = Path.GetExtension(upload.FileName);

                    string FileName = payVoucher.Voucher_No + FileExtension;

                    string UploadPath = Path.Combine(UploadRoot, "PVTransfers", FileName);

                    upload.SaveAs(UploadPath);
                }

                Session["Message"] = new MessageVM
                {
                    Category = "Success",
                    Message = "Data Save Successfully"
                };
                //return RedirectToAction("PayVoucherIndex");
                return RedirectToAction("PayVoucherIndex", new RouteValueDictionary(new { id = payVoucher.VoucherType }));

            }
            var ListStatus = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ChequeStatus && m.Mst_Value != "-1").ToList();
            payVoucher.ListStatus = ListStatus;
            ViewBag.VoucherDoc = Helper.GetVoucherDoc(payVoucher.Voucher_No);
            ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", payVoucher.Debit_Account);
            ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", payVoucher.Credit_Account);

            ViewBag.DebitAccountName = Helper.GetBankList().Where(w => w.Mst_Value == Convert.ToString(payVoucher.Debit_Account)).FirstOrDefault().Mst_Desc;

            Session["Message"] = new MessageVM
            {
                Category = "Error",
                Message = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray())
            };
            return View(payVoucher);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PayVoucherEditRefNonRef(PayVoucher payVoucher, HttpPostedFileBase upload)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string UploadRoot = Helper.GetStorageRoot;


            if (ModelState.IsValid)
            {
                db.Entry(payVoucher).State = EntityState.Modified;
                db.SaveChanges();

                if (upload != null && upload.ContentLength > 0)
                {
                    string FileExtension = Path.GetExtension(upload.FileName);

                    string FileName = payVoucher.Voucher_No + FileExtension;

                    string UploadPath = Path.Combine(UploadRoot, "PVTransfers", FileName);

                    upload.SaveAs(UploadPath);
                }

                //TODO 
                //SENT WHATSAPP and EMAIL
                string beneficiry = @"Ali Adnan";
                string Amount = @"251.23";
                string AccountNo = @"001231564";
                string Bank = @"BANK MUSCAT";
                string PayFor = @"COURTFEE";
                string PayForDetail = @"REGISTRATION FEE";
                string Destination = @"a4aliadnan@gmail.com";
                string Subject = @"Transaction No 123 Completed";

                string EngBody = string.Format(@"To : {0}
                                   An amount of {1} has been sent to your account number {2} ({3}) for {4} - {5}
                                   Sender: Al Yahyai & Partners Law Firm", beneficiry, Amount, AccountNo, Bank, PayFor, PayForDetail);

                var lefttoright = ((Char)0x200E).ToString();

                string ArBody = @"إلى";
                ArBody += @" : ";
                ArBody += beneficiry;
                ArBody += Environment.NewLine;
                ArBody += @"تم ارسال مبلغ وقدره";
                ArBody += @" ";
                ArBody += Amount;
                ArBody += @"إلى حسابكم رقم";
                ArBody += @" ";
                ArBody += AccountNo;
                ArBody += @" ";
                ArBody += @"في";
                ArBody += @" ";
                ArBody += Bank;
                ArBody += @" ";
                ArBody += @"وذلك من أجل";
                ArBody += @" ";
                ArBody += PayFor;
                ArBody += @" ";
                ArBody += PayForDetail;
                ArBody += Environment.NewLine;
                ArBody += @"المُرسل: اليحيائي وشركاؤه للمحاماة";

                string Body = string.Format(@"{0}{1}{2}", ArBody, Environment.NewLine, EngBody);

                //bool IsemailSent = Helper.SendEmail(Destination, Subject, Body);
                return Json(new { redirectTo = "#btn_PayVoucherPending" });

                //Session["Message"] = new MessageVM
                //{
                //    Category = "Success",
                //    Message = "Data Save Successfully"
                //};
                //return RedirectToAction("PayVoucherIndexRefNonRef", new RouteValueDictionary(new { id = payVoucher.VoucherType, mode = "SE" }));
            }

            //Session["Message"] = new MessageVM
            //{
            //    Category = "Error",
            //    Message = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray())
            //};
            //return RedirectToAction("PayVoucherIndexRefNonRef", new RouteValueDictionary(new { id = payVoucher.VoucherType, mode = "EE", payVoucher }));
            return PartialView(payVoucher);
        }

        public ActionResult PayVoucherDelete(int? id)
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

        [HttpPost, ActionName("PayVoucherDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PayVoucher payVoucher = new PayVoucher();
            payVoucher = db.PayVouchers.Find(id);

            payVoucher.Status = "-1"; // To mimick Cancelled

            db.Entry(payVoucher).State = EntityState.Modified;
            db.SaveChanges();

            //return RedirectToAction("Index");
            //return RedirectToAction("PayVoucherIndex");
            return RedirectToAction("PayVoucherIndex", new RouteValueDictionary(new { payVoucher.VoucherType }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PayVoucherDeleteRefNonRef(int id)
        {
            PayVoucher payVoucher = new PayVoucher();
            payVoucher = db.PayVouchers.Find(id);

            payVoucher.Status = "-1"; // To mimick Cancelled

            db.Entry(payVoucher).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { redirectTo = "#btn_PayVoucherPDC" });
        }

        public ActionResult PayVoucherPrint(int? id)
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

            string UserLocation = string.Empty;

            if (User.IsInRole("CourtCasesViewAll") || User.IsSysAdmin())
                UserLocation = string.Empty;
            else
                UserLocation = Helper.GetEmployeeLocation(User.Identity.Name);

            //List<PayVoucher> ViewEditModal = GetPayVoucherList(UserLocation);
            //PayVoucher EditpayVoucher = ViewEditModal.Where(i => i.Voucher_No == (int)id).FirstOrDefault();

            //List<PayVoucher> ViewEditModal = GetPayVoucherList(UserLocation);

            PayVoucher EditpayVoucher = GetPayVoucherList(UserLocation, (int)id).FirstOrDefault();


            var UserRec = db.Users.Where(w => w.Id == payVoucher.AuthorizeBy).FirstOrDefault();
            var UserReccrt = db.Users.Where(w => w.Id == payVoucher.CreatedBy).FirstOrDefault();

            payVoucher.PaymentModeName = EditpayVoucher.PaymentModeName;
            payVoucher.DebitAccountName = EditpayVoucher.DebitAccountName;
            payVoucher.CreditAccountName = EditpayVoucher.CreditAccountName;
            payVoucher.CreatedByLoginId = UserReccrt.UserName;
            payVoucher.AuthorizeByLoginId = UserRec.UserName;

            if (payVoucher.VatAmount == null)
                payVoucher.TotalAmount = payVoucher.Amount;
            else
                payVoucher.TotalAmount = payVoucher.Amount + payVoucher.VatAmount;

            if (payVoucher.VoucherType == "3")
            {
                payVoucher.TransReasonName = EditpayVoucher.TransReasonName;
            }
            else
            {
                payVoucher.VoucherTypeName = EditpayVoucher.VoucherTypeName;
                payVoucher.OfficeFileNo = EditpayVoucher.OfficeFileNo;
                payVoucher.ClientName = EditpayVoucher.ClientName;
                payVoucher.Defendant = EditpayVoucher.Defendant;
                payVoucher.CourtTypeName = EditpayVoucher.CourtTypeName;
                payVoucher.PaymentHeadName = EditpayVoucher.PaymentHeadName;
                payVoucher.PaymentToName = EditpayVoucher.PaymentToName;

                if (payVoucher.CaseId > 0)
                {
                    var courtCase = db.CourtCase.Where(w => w.CaseId == payVoucher.CaseId).FirstOrDefault();
                    payVoucher.OfficeFileNo = courtCase.OfficeFileNo;
                    payVoucher.ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == courtCase.ClientCode).FirstOrDefault().Mst_Desc;
                    payVoucher.Defendant = courtCase.Defendant;

                }
            }
            return View(payVoucher);
        }

        public ActionResult PayVoucherApprove(int? id)
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

            string UserLocation = string.Empty;

            if (User.IsInRole("CourtCasesViewAll") || User.IsSysAdmin())
                UserLocation = string.Empty;
            else
                UserLocation = Helper.GetEmployeeLocation(User.Identity.Name);

            //List<PayVoucher> ViewEditModal = GetPayVoucherList(UserLocation);

            //PayVoucher EditpayVoucher = ViewEditModal.Where(i => i.Voucher_No == (int)id).FirstOrDefault();
            PayVoucher EditpayVoucher = GetPayVoucherList(UserLocation, (int)id).FirstOrDefault();

            var UserRec = db.Users.Where(w => w.Id == payVoucher.CreatedBy).FirstOrDefault();

            if (EditpayVoucher.CaseId != null)
                payVoucher.CaseId = EditpayVoucher.CaseId;

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
            payVoucher.TransTypeCode = EditpayVoucher.TransTypeCode;
            payVoucher.TransReasonCode = EditpayVoucher.TransReasonCode;
            payVoucher.TransTypeName = EditpayVoucher.TransTypeName;
            payVoucher.TransReasonName = EditpayVoucher.TransReasonName;
            payVoucher.CreatedByName = string.Format(@"{0} {1}", UserRec.Firstname, UserRec.Lastname);

            ViewBag.BankTransferStaff = new SelectList(GetListBankTransferAuth(), "Mst_Value", "Mst_Desc");

            if (payVoucher.CaseId > 0)
            {
                var courtCase = db.CourtCase.Where(w => w.CaseId == payVoucher.CaseId).FirstOrDefault();
                payVoucher.OfficeFileNo = courtCase.OfficeFileNo;
                payVoucher.ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == courtCase.ClientCode).FirstOrDefault().Mst_Desc;
                payVoucher.Defendant = courtCase.Defendant;

            }
            else
            {
                ViewBag.AppHidden = "AppHidden";
            }

            var ListStatus = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ChequeStatus && m.Mst_Value != "-1").ToList();
            payVoucher.ListStatus = ListStatus;

            ViewBag.VoucherDoc = Helper.GetVoucherDoc((int)id);
            ViewBag.PVTransferDoc = Helper.GetPVTranferDoc((int)id);
            ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", payVoucher.Debit_Account);

            return View(payVoucher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PayVoucherApprove(PayVoucher payVoucher, HttpPostedFileBase upload)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string UploadRoot = Helper.GetStorageRoot;

            if (ModelState.IsValid)
            {

                payVoucher.AuthorizeBy = User.Identity.GetUserId();
                payVoucher.AuthorizeDate = DateTime.Now;

                string PV_Vooucher = string.Empty;

                if (payVoucher.VoucherStatus == "1")
                {
                    PV_Vooucher = Helper.GeneratePVNumber(payVoucher.Voucher_No);

                    payVoucher.PV_No = PV_Vooucher;
                }


                db.Entry(payVoucher).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                if (upload != null && upload.ContentLength > 0)
                {
                    string FileExtension = Path.GetExtension(upload.FileName);

                    string FileName = payVoucher.Voucher_No + FileExtension;

                    string UploadPath = Path.Combine(UploadRoot, "PVTransfers", FileName);

                    upload.SaveAs(UploadPath);
                }
                Session["Message"] = new MessageVM
                {
                    Category = "Success",
                    Message = "Approve Successfully"
                };

                //return RedirectToAction("PayVoucherIndex");
                return RedirectToAction("PayVoucherIndex", new RouteValueDictionary(new { id = payVoucher.VoucherType }));


            }
            Session["Message"] = new MessageVM
            {
                Category = "Error",
                Message = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray())
            };
            ViewBag.BankTransferStaff = new SelectList(GetListBankTransferAuth(), "Mst_Value", "Mst_Desc");

            return View(payVoucher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PayVoucherApproveRefNonRef(PayVoucher payVoucher, HttpPostedFileBase upload)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string UploadRoot = Helper.GetStorageRoot;

            if (ModelState.IsValid)
            {

                payVoucher.AuthorizeBy = User.Identity.GetUserId();
                payVoucher.AuthorizeDate = DateTime.Now;

                string PV_Vooucher = string.Empty;
                string strMessage = string.Empty;
                string WhatsAppMessage = string.Empty;

                if (payVoucher.VoucherStatus == "1")
                    PV_Vooucher = Helper.GeneratePVNumber(payVoucher.Voucher_No);
                else
                    WhatsAppMessage = Helper.GetWhatsApp(payVoucher.Voucher_No, User.Identity.Name, out strMessage);

                payVoucher.PV_No = PV_Vooucher;


                db.Entry(payVoucher).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                if (upload != null && upload.ContentLength > 0)
                {
                    string FileExtension = Path.GetExtension(upload.FileName);

                    string FileName = payVoucher.Voucher_No + FileExtension;

                    string UploadPath = Path.Combine(UploadRoot, "PVTransfers", FileName);

                    upload.SaveAs(UploadPath);
                }
                //TODO 
                //SENT WHATSAPP and EMAIL

                if (!string.IsNullOrEmpty(payVoucher.Cheque_Number))
                {
                    //NotifyResult = Helper.NotifyWhatsAppEmail(payVoucher.Voucher_No, User.Identity.Name, out strMessage);
                    //NotifyResult = Helper.SendWhatsAppEmail(payVoucher.PV_No);
                    WhatsAppMessage = Helper.GetWhatsApp(payVoucher.Voucher_No, User.Identity.Name, out strMessage);
                }


                if (!string.IsNullOrEmpty(strMessage))
                    return Json(new { redirectTo = "#btn_PayVoucherPending", MessageWHM_Email = strMessage, WhatsAppMessage = WhatsAppMessage }); 
                else
                    return Json(new { redirectTo = "#btn_PayVoucherPending" });
            }
            return PartialView(payVoucher);
        }

        [HttpGet]
        public ActionResult GetTab(string Mode, string PartialViewName, int Voucher_No, PayVoucher modal = null)
        {
            if (PartialViewName.In("_PVTable", "_CaseTable"))
            {
                return PartialView(PartialViewName);
            }
            else if (PartialViewName.In("_Tax", "_LawyerFeeVAT", "_ExpensesVAT", "_QuarterltVAT"))
            {
                ToBeRegisterVM ViewModal = new ToBeRegisterVM();
                return PartialView(PartialViewName, ViewModal);
            }
            else
            {
                PayVoucher payVoucher = new PayVoucher();

                if (Mode == "C")
                {
                    if (modal != null)
                        payVoucher = modal;
                    var skipIDs = "1,2,3,13,25";

                    ViewBag.CourtType = new SelectList(GetListForInvoiceCaseLevel(0), "Mst_Value", "Mst_Desc");
                    ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
                    ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
                    ViewBag.Payment_Head = new SelectList(Helper.LoadPayFor(), "Mst_Value", "Mst_Desc");
                    ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(false), "Mst_Value", "Mst_Desc", "1901");
                    ViewBag.PaymentHeadDetail = new SelectList(Helper.InitiateDDL(), "Mst_Value", "Mst_Desc");
                    ViewBag.PaymentToBenificry = new SelectList(Helper.LoadDependentDDL("1901", skipIDs), "Mst_Value", "Mst_Desc", User.Identity.Name);
                    ViewBag.PayToBankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc");
                    ViewBag.PayToMessageLang = new SelectList(Helper.GetLangForSelect(), "Mst_Value", "Mst_Desc");
                    ViewBag.VoucherDate = DateTime.Now.ToString("dd/MM/yyyy");
                }
                else if(Mode == "CM")
                {
                    if (modal != null)
                        payVoucher = modal;
                    var skipIDs = "1,2,3,13,25";
                    ViewBag.CourtType = new SelectList(GetListForInvoiceCaseLevel(0), "Mst_Value", "Mst_Desc");
                    ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
                    ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
                    ViewBag.Payment_Head = new SelectList(Helper.LoadPayFor(), "Mst_Value", "Mst_Desc");
                    ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(), "Mst_Value", "Mst_Desc", "1901");
                    ViewBag.PaymentHeadDetail = new SelectList(Helper.InitiateDDL(), "Mst_Value", "Mst_Desc");
                    ViewBag.PaymentToBenificry = new SelectList(Helper.LoadDependentDDL("1901", skipIDs), "Mst_Value", "Mst_Desc", User.Identity.Name);
                    ViewBag.PayToBankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc");
                    ViewBag.VoucherDate = DateTime.Now.ToString("dd/MM/yyyy");
                    ViewBag.PayToMessageLang = new SelectList(Helper.GetLangForSelect(), "Mst_Value", "Mst_Desc");

                }
                else if (Mode == "E")
                {
                    payVoucher = db.PayVouchers.Find(Voucher_No);

                    string UserLocation = string.Empty;

                    if (User.IsInRole("CourtCasesViewAll") || User.IsSysAdmin())
                        UserLocation = string.Empty;
                    else
                        UserLocation = Helper.GetEmployeeLocation(User.Identity.Name);

                    PayVoucher EditpayVoucher = GetPayVoucherList(UserLocation, Voucher_No).FirstOrDefault();

                    var UserRec = db.Users.Where(w => w.Id == payVoucher.AuthorizeBy).FirstOrDefault();

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
                    payVoucher.TransTypeCode = EditpayVoucher.TransTypeCode;
                    payVoucher.TransReasonCode = EditpayVoucher.TransReasonCode;
                    payVoucher.TransTypeName = EditpayVoucher.TransTypeName;
                    payVoucher.TransReasonName = EditpayVoucher.TransReasonName;

                    payVoucher.AuthorizeByName = string.Format(@"{0} {1}", UserRec.Firstname, UserRec.Lastname);

                    var ListStatus = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ChequeStatus && m.Mst_Value != "-1").ToList();
                    payVoucher.ListStatus = ListStatus;

                    ViewBag.VoucherDoc = Helper.GetVoucherDoc(Voucher_No);
                    ViewBag.PVTransferDoc = Helper.GetPVTranferDoc(Voucher_No);
                    ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", payVoucher.Debit_Account);
                    ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", payVoucher.Credit_Account);
                    ViewBag.DebitAccountName = Helper.GetBankList().Where(w => w.Mst_Value == Convert.ToString(payVoucher.Debit_Account)).FirstOrDefault().Mst_Desc;
                }
                else if (Mode == "A")
                {
                    payVoucher = db.PayVouchers.Find(Voucher_No);
                    PayVoucher EditpayVoucher = GetPayVoucherList("", Voucher_No).FirstOrDefault();
                    DateTime cutOffDate = DateTime.ParseExact(Helper.GetcutOffDate(), "dd/MM/yyyy", null);
                    DateTime VoucherDate = EditpayVoucher.CreatedOn;

                    var UserRec = db.Users.Where(w => w.Id == payVoucher.CreatedBy).FirstOrDefault();

                    if (EditpayVoucher.CaseId != null)
                        payVoucher.CaseId = EditpayVoucher.CaseId;

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
                    payVoucher.TransTypeCode = EditpayVoucher.TransTypeCode;
                    payVoucher.TransReasonCode = EditpayVoucher.TransReasonCode;
                    payVoucher.TransTypeName = EditpayVoucher.TransTypeName;
                    payVoucher.TransReasonName = EditpayVoucher.TransReasonName;
                    payVoucher.CreatedByName = string.Format(@"{0} {1}", UserRec.Firstname, UserRec.Lastname);

                    ViewBag.BankTransferStaff = new SelectList(GetListBankTransferAuth(), "Mst_Value", "Mst_Desc");

                    if (payVoucher.CaseId > 0)
                    {
                        var courtCase = db.CourtCase.Where(w => w.CaseId == payVoucher.CaseId).FirstOrDefault();
                        payVoucher.OfficeFileNo = courtCase.OfficeFileNo;
                        payVoucher.ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == courtCase.ClientCode).FirstOrDefault().Mst_Desc;
                        payVoucher.Defendant = courtCase.Defendant;

                    }
                    else
                    {
                        ViewBag.AppHidden = "AppHidden";
                    }

                    var ListStatus = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ChequeStatus && m.Mst_Value != "-1").ToList();
                    payVoucher.ListStatus = ListStatus;

                    ViewBag.VoucherDoc = Helper.GetVoucherDoc(Voucher_No);
                    ViewBag.PVTransferDoc = Helper.GetPVTranferDoc(Voucher_No);
                    ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", payVoucher.Debit_Account);

                    ViewBag.CourtType = new SelectList(GetListForInvoiceCaseLevel(0), "Mst_Value", "Mst_Desc", payVoucher.CourtType);
                    ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", payVoucher.Credit_Account);
                    if (payVoucher.IsDeleted)
                    {
                        ViewBag.Payment_Head = new SelectList(Helper.LoadPayFor(), "Mst_Value", "Mst_Desc");
                        ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(false), "Mst_Value", "Mst_Desc");
                    }
                    else
                    {
                        ViewBag.Payment_Head = new SelectList(Helper.LoadPayFor(), "Mst_Value", "Mst_Desc", payVoucher.Payment_Head);
                        ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(false), "Mst_Value", "Mst_Desc", payVoucher.Payment_To);
                    }

                    ViewBag.PaymentHeadDetail = new SelectList(Helper.LoadDependentDDL(payVoucher.Payment_Head), "Mst_Value", "Mst_Desc", payVoucher.PaymentHeadDetail);
                    ViewBag.PaymentToBenificry = new SelectList(Helper.LoadDependentDDL(payVoucher.Payment_To), "Mst_Value", "Mst_Desc", payVoucher.PaymentToBenificry);
                    ViewBag.PayToBankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc");
                    ViewBag.PayToMessageLang = new SelectList(Helper.GetLangForSelect(), "Mst_Value", "Mst_Desc");
                }

                return PartialView(PartialViewName, payVoucher);
            }
        }
        #region Internal Procedures

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
        public List<PayVoucher> GetPayVoucherList(string LocationName, int VoucherNo = 0)
        {
            List<PayVoucher> ResultList = new List<PayVoucher>();

            if (!string.IsNullOrEmpty(LocationName))
                ResultList = Helper.GetPayVoucherList(LocationName.Substring(0, 3).ToUpper(), VoucherNo);
            else
                ResultList = Helper.GetPayVoucherList("A", VoucherNo);
           
            return ResultList;
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

        #endregion

        #endregion

        #region Case Invoice
        public ActionResult InvoiceCreateIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxIndexData()
        {
            string UserLocation = string.Empty;

            if (User.IsInRole("CourtCasesViewAll") || User.IsSysAdmin())
                UserLocation = "A";
            else
                UserLocation = Helper.GetEmployeeLocation(User.Identity.Name);

            List<CourtCaseListForIndex> data = new List<CourtCaseListForIndex>();

            var start = (Convert.ToInt32(Request["start"]));
            var Length = (Convert.ToInt32(Request["length"])) == 0 ? 10 : (Convert.ToInt32(Request["length"]));
            var searchvalue = Request["search[value]"] ?? "";
            var sortcoloumnIndex = Convert.ToInt32(Request["order[0][column]"]);
            var sortDirection = Request["order[0][dir]"] ?? "asc";
            var recordsTotal = 0;

            try
            {

                data = Helper.GetCaseListInvoice(sortcoloumnIndex, start, searchvalue, Length, sortDirection, UserLocation.ToUpper().Substring(0, 1)).ToList();
                recordsTotal = data.Count > 0 ? data[0].TotalRecords : 0;

            }
            catch (Exception ex)
            {
            }
            return Json(new { data = data, recordsTotal = recordsTotal, recordsFiltered = recordsTotal }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult InvoiceManageIndex()
        {
            if (User.IsInRole("VoucherApproval"))
            {
                ViewBag.UserRole = "VoucherApproval";
            }

            return View();
            #region Old Slow Code
            /*
            string UserLocation = string.Empty;

            if (User.IsInRole("CourtCasesViewAll") || User.IsSysAdmin())
                UserLocation = string.Empty;
            else
                UserLocation = Helper.GetEmployeeLocation(User.Identity.Name);

            return View(GetCaseInvoiceList(UserLocation));
            */
            #endregion
        }
        public ActionResult InvoiceManageIndexData()
        {
            string UserLocation = string.Empty;
            var request = HttpContext.Request;

            if (User.IsInRole("CourtCasesViewAll") || User.IsSysAdmin())
                UserLocation = "A";
            else
                UserLocation = Helper.GetEmployeeLocation(User.Identity.Name);

            List<CaseInvoiceList> data = new List<CaseInvoiceList>();

            var start = (Convert.ToInt32(Request["start"]));
            var Length = (Convert.ToInt32(Request["length"])) == 0 ? 10 : (Convert.ToInt32(Request["length"]));
            var searchvalue = Request["search[value]"] ?? "";
            var sortcoloumnIndex = Convert.ToInt32(Request["order[0][column]"]);
            var sortDirection = Request["order[0][dir]"] ?? "asc";
            var recordsTotal = 0;
            string DataFor = string.Empty;
            try
            {
                DataFor = request.Params["DataTableName"].ToString();

            }
            catch (Exception ex)
            {
                DataFor = string.Empty;
            }

            try
            {
                if (string.IsNullOrEmpty(DataFor))
                {
                    data = Helper.GetInvoiceList(sortcoloumnIndex, start, searchvalue, Length, sortDirection, UserLocation.ToUpper().Substring(0, 1)).ToList();
                    recordsTotal = data.Count > 0 ? data[0].TotalRecords : 0;

                }
                else
                {
                    data = Helper.GetInvoiceList(sortcoloumnIndex, start, searchvalue, Length, sortDirection, UserLocation.ToUpper().Substring(0, 1), int.Parse(DataFor)).ToList();
                    recordsTotal = data.Count > 0 ? data[0].TotalRecords : 0;

                }

            }
            catch (Exception ex)
            {
            }
            return Json(new { data = data, recordsTotal = recordsTotal, recordsFiltered = recordsTotal }, JsonRequestBehavior.AllowGet);

            //List<CaseInvoiceList> ViewIndexList = objDAL.getCaseInvoiceList(UserLocation.ToUpper().Substring(0, 1));

            //ReportCourtCases objDAL = new ReportCourtCases();

            ////List<CaseInvoice> ViewIndexList = GetCaseInvoiceList(UserLocation);


            //var serializer = new JavaScriptSerializer();

            //serializer.MaxJsonLength = Int32.MaxValue;
            //var result = new ContentResult
            //{
            //    Content = serializer.Serialize(ViewIndexList),
            //    ContentType = "application/json"
            //};
            //return result;
        }
        public ActionResult AddNewCourtFee()
        {
            ViewBag.InvClassification = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FeeClassification), "Mst_Value", "Mst_Desc");
            ViewBag.FeeTypeId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FeesType), "Mst_Value", "Mst_Desc");
            ViewBag.CaseLevel = new SelectList(Helper.GetCaseLevelList("C"), "Mst_Value", "Mst_Desc");
            ViewBag.FeeTypeCascadeDetail = new SelectList(Helper.GetFeeTypeCascadeDetail(""), "Mst_Value", "Mst_Desc");
            return PartialView("_CaseInvoiceFee", new CaseInvoiceFee());
        }
        public ActionResult AddNewFeeCalculation()
        {
            return PartialView("_CaseInvoiceFeeCalculation", new CaseInvoiceFeeCalculation());
        }
        public ActionResult InvoiceCreate(int id)
        {
            ViewBag.MstParentId = (int)MASTER_S.FeeTypeCascadeDetail;

            CaseInvoice caseInvoice = new CaseInvoice();

            var courtCase = db.CourtCase.Find(id);

            caseInvoice.CaseId = courtCase.CaseId;
            caseInvoice.Defendant = courtCase.Defendant;
            caseInvoice.OfficeFileNo = courtCase.OfficeFileNo;
            caseInvoice.AccountContractNo = courtCase.AccountContractNo;
            caseInvoice.ClientFileNo = courtCase.ClientFileNo;
            caseInvoice.ClaimAmount = courtCase.ClaimAmount;
            caseInvoice.Subject = courtCase.Subject;
            caseInvoice.ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == courtCase.ClientCode).FirstOrDefault().Mst_Desc;
            caseInvoice.CaseRegisterDate = courtCase.ReceptionDate?.ToString("dd/MM/yyyy");

            int TotalInvoices = db.CaseInvoice.Where(w => w.CaseId == id).Count();

            if (TotalInvoices > 0)
                caseInvoice.ExpectedFees = db.CaseInvoice.Where(w => w.CaseId == id && w.ExpectedFees != "").FirstOrDefault().ExpectedFees;

            //ViewBag.CourtType = new SelectList(GetListForInvoiceCaseLevel(caseInvoice.CaseId), "Mst_Value", "Mst_Desc");
            ViewBag.CourtType = new SelectList(Helper.GetCaseLevelList("C"), "Mst_Value", "Mst_Desc");
            ViewBag.Credit_Account = new SelectList(Helper.GetBankList(caseInvoice.OfficeFileNo.Substring(0, 1)), "Mst_Value", "Mst_Desc");

            ViewBag.CaseId = id;
            ViewBag.InvoiceDate = DateTime.Now.ToString("dd/MM/yyyy");

            //var LoggedInUserInfo = EmployeesController.GetEmployeeInfoId(User.Identity.GetUserId());
            var CaseCreatedBy = EmployeesController.GetEmployeeInfoId(courtCase.CreatedBy);
            ViewBag.BranchName = CaseCreatedBy.LocationName;
            ViewBag.SpecialInstructions = courtCase.SpecialInstructions;

            ViewBag.BeforeCourtStage = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.BeforeCourtStage), "Mst_Value", "Mst_Desc");
            ViewBag.EnforcementStage = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.EnforcementStage), "Mst_Value", "Mst_Desc");
            ViewBag.CounsultingFeeType = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CounsultingFeeType), "Mst_Value", "Mst_Desc");

            //ViewBag.SupportingDoc = Helper.GetInvoiceDoc((int)id);
            ViewBag.CaseAgreementDoc = Helper.GetCaseAgreementDoc((int)id);
            ViewBag.CollectionCount = 0;

            return View(caseInvoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InvoiceCreate(CaseInvoice caseInvoice, HttpPostedFileBase upload)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string UploadRoot = Helper.GetStorageRoot;
            var courtCase = db.CourtCase.Find(caseInvoice.CaseId);
            string ErrorMessage = string.Empty;

            bool foundDuplicate = false;
            var invoiceCheckingDTO = new InvoiceCheckingDTO();
            invoiceCheckingDTO.CaseId = caseInvoice.CaseId;
            invoiceCheckingDTO.InvoiceDate = caseInvoice.InvoiceDate;

            if (ModelState.IsValid)
            {
                //Checnking Already Exists Invoice

                if (caseInvoice.FeeId != null)
                {
                    foreach (var FeeItem in caseInvoice.FeeId)
                    {
                        if (!foundDuplicate)
                        {
                            invoiceCheckingDTO.FeeTypeId = Convert.ToInt32(FeeItem.FeeTypeId);
                            invoiceCheckingDTO.FeeAmount = FeeItem.FeeAmount;

                            DataTable _result = Helper.CheckInvoiceDuplicate(invoiceCheckingDTO);
                            foundDuplicate = Convert.ToInt32(_result.Rows[0]["DupExists"].ToString()) == 0 ? false : true;
                        }
                        
                    }
                }

                if (!foundDuplicate)
                {
                    var emp = db.Employees.Where(w => w.EmployeeNumber == User.Identity.Name).FirstOrDefault();

                    //Generating Invoice Number

                    string InvoiceNumber = Helper.GenerateINVNumber(emp.LocationCode);
                    caseInvoice.InvoiceNumber = string.Format(@"{0}{1}", User.Identity.Name, InvoiceNumber);

                    db.CaseInvoice.Add(caseInvoice);
                    db.SaveChanges();

                    courtCase.Subject = caseInvoice.Subject;
                    db.Entry(courtCase).State = EntityState.Modified;
                    db.SaveChanges();



                    if (upload != null && upload.ContentLength > 0)
                    {
                        string FileExtension = Path.GetExtension(upload.FileName);

                        string FileName = caseInvoice.InvoiceId + FileExtension;

                        string UploadPath = Path.Combine(UploadRoot, "INVDocuments", FileName);

                        upload.SaveAs(UploadPath);
                    }
                    Session["Message"] = new MessageVM
                    {
                        Category = "Success",
                        Message = "Data Save Successfully"
                    };

                    return RedirectToAction("InvoiceModify", new RouteValueDictionary(new { id = caseInvoice.InvoiceId, ViewType = caseInvoice.buttonName }));
                }
                else
                    ErrorMessage = "Duplicate Invoice Exists";
            }



            ViewBag.BeforeCourtStage = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.BeforeCourtStage), "Mst_Value", "Mst_Desc");
            ViewBag.EnforcementStage = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.EnforcementStage), "Mst_Value", "Mst_Desc");
            ViewBag.CounsultingFeeType = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CounsultingFeeType), "Mst_Value", "Mst_Desc");

            ViewBag.CaseAgreementDoc = Helper.GetCaseAgreementDoc((int)caseInvoice.CaseId);



            ViewBag.CourtType = new SelectList(Helper.GetCaseLevelList("C"), "Mst_Value", "Mst_Desc");
            ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", caseInvoice.Credit_Account);
            ViewBag.FeeTypeId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FeesType), "Mst_Value", "Mst_Desc");
            ViewBag.CourtRefNo = new SelectList(GetListForInvoiceCaseCourtRefNo(caseInvoice.CaseId), "Mst_Value", "Mst_Desc");
            ViewBag.CaseLevel = new SelectList(Helper.GetCaseLevelList("C"), "Mst_Value", "Mst_Desc");
            ViewBag.InvClassification = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FeeClassification), "Mst_Value", "Mst_Desc");
            ViewBag.PV_No = new SelectList(Helper.GetPVList(), "Mst_Value", "Mst_Desc");


            ViewBag.CaseId = caseInvoice.CaseId;
            ViewBag.InvoiceDate = DateTime.Now.ToString("dd/MM/yyyy");

            var CaseCreatedBy = EmployeesController.GetEmployeeInfoId(courtCase.CreatedBy);
            ViewBag.BranchName = CaseCreatedBy.LocationName;
            ViewBag.SpecialInstructions = courtCase.SpecialInstructions;

            ViewBag.CollectionCount = caseInvoice.FeeId.Count;

            ViewBag.InvClassification = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FeeClassification), "Mst_Value", "Mst_Desc");
            ViewBag.FeeTypeId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FeesType), "Mst_Value", "Mst_Desc");
            ViewBag.CaseLevel = new SelectList(Helper.GetCaseLevelList("C"), "Mst_Value", "Mst_Desc");
            ViewBag.FeeTypeCascadeDetail = new SelectList(Helper.GetFeeTypeCascadeDetail(invoiceCheckingDTO.FeeTypeId.ToString()), "Mst_Value", "Mst_Desc");

            if (caseInvoice.IsLastInvoice)
                @ViewBag.rdoLastInvoice2 = "checked";
            else
                @ViewBag.rdoLastInvoice1 = "checked";

            if (string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray());

            Session["Message"] = new MessageVM
            {
                Category = "Error",
                Message = ErrorMessage
            };
            return View(caseInvoice);
        }
        public ActionResult InvoiceEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CaseInvoice caseInvoice = db.CaseInvoice.Where(w => w.InvoiceId == id).Include(s => s.FeeId).Include(s => s.FeeCaclId).FirstOrDefault();
            if (caseInvoice == null)
            {
                return HttpNotFound();
            }

            var courtCase = db.CourtCase.Find(caseInvoice.CaseId);

            caseInvoice.Defendant = courtCase.Defendant;
            caseInvoice.OfficeFileNo = courtCase.OfficeFileNo;
            caseInvoice.AccountContractNo = courtCase.AccountContractNo;
            caseInvoice.ClientFileNo = courtCase.ClientFileNo;
            caseInvoice.ClaimAmount = courtCase.ClaimAmount;
            caseInvoice.ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == courtCase.ClientCode).FirstOrDefault().Mst_Desc;
            caseInvoice.CaseRegisterDate = courtCase.ReceptionDate?.ToString("dd/MM/yyyy");

            ViewBag.InvoiceAmount = db.CaseInvoiceFee.Where(w => w.InvoiceId == id).Select(s => s.FeeAmount).Sum();
            ViewBag.CourtType = new SelectList(Helper.GetCaseLevelList("C"), "Mst_Value", "Mst_Desc", caseInvoice.CourtType);

            ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", caseInvoice.Credit_Account);
            ViewBag.InvoiceStatus = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.InvoiceStatus), "Mst_Value", "Mst_Desc");

            ViewBag.CourtRefNo = caseInvoice.CourtRefNo;

            ViewBag.HidTotalFeeDetail = caseInvoice.CourtRefNo;
            ViewBag.HidTotalFeeCalcDetail = caseInvoice.CourtRefNo;

            var CaseCreatedBy = EmployeesController.GetEmployeeInfoId(courtCase.CreatedBy);
            ViewBag.BranchName = CaseCreatedBy.LocationName;
            ViewBag.FeeTypeId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FeesType), "Mst_Value", "Mst_Desc");
            ViewBag.CaseLevel = new SelectList(Helper.GetCaseLevelList("C"), "Mst_Value", "Mst_Desc");
            ViewBag.InvClassification = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FeeClassification), "Mst_Value", "Mst_Desc");

            ViewBag.SpecialInstructions = courtCase.SpecialInstructions;

            return View(caseInvoice);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InvoiceEdit(CaseInvoice caseInvoice)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            bool processSuccess = false;
            string processMessage = string.Empty;
            CourtCases courtCase = new CourtCases();
            string CourtTypes = string.Empty;

            if (ModelState.IsValid)
            {
                if (caseInvoice.IsLastInvoice && caseInvoice.TransferDate.HasValue)
                {
                    var UPDModalFinanceClosureDate = db.CourtCase.Find(caseInvoice.CaseId);
                    if (UPDModalFinanceClosureDate != null)
                    {
                        processSuccess = true;
                        processMessage = "UPDATE";

                    }
                    else
                        processMessage = "There is some error in Updating Finance Closure Date [CourtCases is NULL]";

                    if (processSuccess && !string.IsNullOrEmpty(processMessage))
                    {
                        db.Entry(caseInvoice).State = EntityState.Modified;
                        db.SaveChanges();

                        db.Entry(UPDModalFinanceClosureDate).Entity.FinanceFileClosureDate = caseInvoice.TransferDate;
                        db.Entry(UPDModalFinanceClosureDate).State = EntityState.Modified;
                        db.SaveChanges();

                        Session["Message"] = new MessageVM
                        {
                            Category = "Success",
                            Message = "Data Save Successfully"
                        };

                        return RedirectToAction("InvoiceManageIndex");
                    }
                }
                else
                {
                    db.Entry(caseInvoice).State = EntityState.Modified;
                    db.SaveChanges();

                    Session["Message"] = new MessageVM
                    {
                        Category = "Success",
                        Message = "Data Save Successfully"
                    };

                    return RedirectToAction("InvoiceManageIndex");
                }
            }

            if (!processSuccess)
            {
                courtCase = db.CourtCase.Find(caseInvoice.CaseId);

                caseInvoice.Defendant = courtCase.Defendant;
                caseInvoice.OfficeFileNo = courtCase.OfficeFileNo;
                caseInvoice.AccountContractNo = courtCase.AccountContractNo;
                caseInvoice.ClientFileNo = courtCase.ClientFileNo;
                caseInvoice.ClaimAmount = courtCase.ClaimAmount;
                caseInvoice.ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == courtCase.ClientCode).FirstOrDefault().Mst_Desc;
                caseInvoice.CaseRegisterDate = courtCase.ReceptionDate?.ToString("dd/MM/yyyy");

                ViewBag.InvoiceAmount = db.CaseInvoiceFee.Where(w => w.InvoiceId == caseInvoice.InvoiceId).Select(s => s.FeeAmount).Sum();
                ViewBag.CourtType = new SelectList(Helper.GetCaseLevelList("C"), "Mst_Value", "Mst_Desc", caseInvoice.CourtType);

                ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", caseInvoice.Credit_Account);
                ViewBag.InvoiceStatus = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.InvoiceStatus), "Mst_Value", "Mst_Desc");

                ViewBag.CourtRefNo = caseInvoice.CourtRefNo;

                ViewBag.HidTotalFeeDetail = caseInvoice.CourtRefNo;
                ViewBag.HidTotalFeeCalcDetail = caseInvoice.CourtRefNo;

                var CaseCreatedBy = EmployeesController.GetEmployeeInfoId(courtCase.CreatedBy);
                ViewBag.BranchName = CaseCreatedBy.LocationName;
                ViewBag.FeeTypeId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FeesType), "Mst_Value", "Mst_Desc");
                ViewBag.CaseLevel = new SelectList(Helper.GetCaseLevelList("C"), "Mst_Value", "Mst_Desc");
                ViewBag.InvClassification = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FeeClassification), "Mst_Value", "Mst_Desc");

                ViewBag.SpecialInstructions = courtCase.SpecialInstructions;

                if (string.IsNullOrEmpty(processMessage))
                {
                    Session["Message"] = new MessageVM
                    {
                        Category = "Error",
                        Message = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray())
                    };
                }
                else
                {
                    Session["Message"] = new MessageVM
                    {
                        Category = "Error",
                        Message = processMessage
                    };
                }
            }
            return View(caseInvoice);
        }

        public ActionResult InvoiceModify(int id, string ViewType = null)
        {
            var caseInvoice = db.CaseInvoice.Where(w => w.InvoiceId == id).Include(s => s.FeeId).Include(s => s.FeeCaclId).FirstOrDefault();

            if (caseInvoice == null)
            {
                return HttpNotFound();
            }

            var courtCase = db.CourtCase.Find(caseInvoice.CaseId);

            caseInvoice.CaseId = courtCase.CaseId;
            caseInvoice.Defendant = courtCase.Defendant;
            caseInvoice.OfficeFileNo = courtCase.OfficeFileNo;
            caseInvoice.AccountContractNo = courtCase.AccountContractNo;
            caseInvoice.ClientFileNo = courtCase.ClientFileNo;
            caseInvoice.ClaimAmount = courtCase.ClaimAmount;
            caseInvoice.Subject = courtCase.Subject;
            caseInvoice.ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == courtCase.ClientCode).FirstOrDefault().Mst_Desc;
            caseInvoice.CaseRegisterDate = courtCase.ReceptionDate?.ToString("dd/MM/yyyy");

            //string strExpectedFees = db.CaseInvoice.Where(w => w.CaseId == id && w.ExpectedFees != "").OrderByDescending(o => o.InvoiceId).FirstOrDefault().ExpectedFees;

            //caseInvoice.ExpectedFees = strExpectedFees;

            //ViewBag.CourtType = new SelectList(GetListForInvoiceCaseLevel(caseInvoice.CaseId), "Mst_Value", "Mst_Desc");
            ViewBag.CourtType = new SelectList(Helper.GetCaseLevelList("C"), "Mst_Value", "Mst_Desc", caseInvoice.CourtType);
            ViewBag.Credit_Account = new SelectList(Helper.GetBankList(caseInvoice.OfficeFileNo.Substring(0, 1)), "Mst_Value", "Mst_Desc", caseInvoice.Credit_Account);

            ViewBag.CaseId = caseInvoice.CaseId;
            ViewBag.InvoiceDate = caseInvoice.InvoiceDate;

            //var LoggedInUserInfo = EmployeesController.GetEmployeeInfoId(User.Identity.GetUserId());
            var CaseCreatedBy = EmployeesController.GetEmployeeInfoId(courtCase.CreatedBy);
            ViewBag.BranchName = CaseCreatedBy.LocationName;
            ViewBag.SpecialInstructions = courtCase.SpecialInstructions;

            ViewBag.BeforeCourtStage = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.BeforeCourtStage), "Mst_Value", "Mst_Desc", caseInvoice.BeforeCourtStage);
            ViewBag.EnforcementStage = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.EnforcementStage), "Mst_Value", "Mst_Desc", caseInvoice.EnforcementStage);
            ViewBag.CounsultingFeeType = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CounsultingFeeType), "Mst_Value", "Mst_Desc", caseInvoice.CounsultingFeeType);

            ViewBag.SupportingDoc = Helper.GetInvoiceDoc((int)id);
            ViewBag.CaseAgreementDoc = Helper.GetCaseAgreementDoc((int)caseInvoice.CaseId);

            ViewBag.CollectionCount = caseInvoice.FeeId.Count;

            ViewBag.InvClassification = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FeeClassification), "Mst_Value", "Mst_Desc");
            ViewBag.FeeTypeId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FeesType), "Mst_Value", "Mst_Desc");
            ViewBag.CaseLevel = new SelectList(Helper.GetCaseLevelList("C"), "Mst_Value", "Mst_Desc");
            ViewBag.FeeTypeCascadeDetail = new SelectList(Helper.GetFeeTypeCascadeDetail(""), "Mst_Value", "Mst_Desc");

            ViewBag.ViewType = ViewType;

            ViewBag.MstParentId = (int)MASTER_S.FeeTypeCascadeDetail;

            return View(caseInvoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InvoiceModify(CaseInvoice caseInvoice, HttpPostedFileBase upload)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string UploadRoot = Helper.GetStorageRoot;

            var courtCase = db.CourtCase.Find(caseInvoice.CaseId);

            if (ModelState.IsValid)
            {
                if (caseInvoice.buttonName != null)
                {
                    return RedirectToAction("InvoiceModify", new RouteValueDictionary(new { id = caseInvoice.InvoiceId, ViewType = caseInvoice.buttonName }));
                }

                var originalParent = db.CaseInvoice
                    .Where(p => p.InvoiceId == caseInvoice.InvoiceId)
                    .Include(p => p.FeeId)
                    .Include(p => p.FeeCaclId)
                    .SingleOrDefault();

                var parentEntry = db.Entry(originalParent);
                parentEntry.CurrentValues.SetValues(caseInvoice);

                if (originalParent.FeeId != null && caseInvoice.FeeId == null)
                {
                    foreach (var originalChildItem in originalParent.FeeId.Where(c => c.FeeId != 0).ToList())
                    {
                        db.CaseInvoiceFee.Remove(originalChildItem);
                    }
                }
                else
                {
                    foreach (var childItem in caseInvoice.FeeId)
                    {
                        var originalChildItem = originalParent.FeeId
                            .Where(c => c.FeeId == childItem.FeeId && c.FeeId != 0)
                            .SingleOrDefault();
                        if (originalChildItem != null)
                        {
                            var childEntry = db.Entry(originalChildItem);
                            childEntry.CurrentValues.SetValues(childItem);
                        }
                        else
                        {
                            childItem.FeeId = 0;
                            originalParent.FeeId.Add(childItem);
                        }
                    }


                    foreach (var originalChildItem in
                                 originalParent.FeeId.Where(c => c.FeeId != 0).ToList())
                    {
                        if (!caseInvoice.FeeId.Any(c => c.FeeId == originalChildItem.FeeId))
                            db.CaseInvoiceFee.Remove(originalChildItem);
                    }
                }

                if (originalParent.FeeCaclId != null && caseInvoice.FeeCaclId == null)
                {
                    foreach (var originalChildItem in originalParent.FeeCaclId.Where(c => c.FeeCaclId != 0).ToList())
                    {
                        db.CaseInvoiceFeeCalculation.Remove(originalChildItem);
                    }
                }
                else
                {
                    foreach (var childItem in caseInvoice.FeeCaclId)
                    {
                        var originalChildItem = originalParent.FeeCaclId
                            .Where(c => c.FeeCaclId == childItem.FeeCaclId && c.FeeCaclId != 0)
                            .SingleOrDefault();
                        if (originalChildItem != null)
                        {
                            var childEntry = db.Entry(originalChildItem);
                            childEntry.CurrentValues.SetValues(childItem);
                        }
                        else
                        {
                            childItem.FeeCaclId = 0;
                            originalParent.FeeCaclId.Add(childItem);
                        }
                    }


                    foreach (var originalChildItem in
                                 originalParent.FeeCaclId.Where(c => c.FeeCaclId != 0).ToList())
                    {
                        if (!caseInvoice.FeeCaclId.Any(c => c.FeeCaclId == originalChildItem.FeeCaclId))
                            db.CaseInvoiceFeeCalculation.Remove(originalChildItem);
                    }
                }
                db.SaveChanges();


                courtCase.Subject = caseInvoice.Subject;
                db.Entry(courtCase).State = EntityState.Modified;
                db.SaveChanges();

                #region RnD Code for Future Reference

                /*
                 * BELOW CODE NOT WORKING WITH DELETE
                 var OriginalCaseInvoice = db.CaseInvoice.Include("FeeId").Include("FeeCaclId")
                     .Where(x => x.InvoiceId == caseInvoice.InvoiceId).SingleOrDefault();

                 db.Entry(OriginalCaseInvoice).CurrentValues.SetValues(caseInvoice);

                 var addedFeeList = caseInvoice.FeeId
                     .Where(y => y.FeeId == 0).ToList();

                 var addedFeeCalList = caseInvoice.FeeCaclId
                     .Where(y => y.FeeCaclId == 0).ToList();

                 var deletedFeeList = OriginalCaseInvoice.FeeId
                     .Where
                     (
                         x => !caseInvoice.FeeId.Select(y => y.FeeId)
                             .Contains(x.FeeId)
                     )
                     .ToList();

                 var deletedFeeCalList = OriginalCaseInvoice.FeeCaclId
                     .Where
                     (
                         x => !caseInvoice.FeeCaclId.Select(y => y.FeeCaclId)
                             .Contains(x.FeeCaclId)
                     )
                     .ToList();

                 var editedFeeList = caseInvoice.FeeId
                     .Where
                     (
                         y => OriginalCaseInvoice.FeeId.Select(z => z.FeeId)
                             .Contains(y.FeeId)
                     )
                     .ToList();

                 var editedFeeCalList = caseInvoice.FeeCaclId
                     .Where
                     (
                         y => OriginalCaseInvoice.FeeCaclId.Select(z => z.FeeCaclId)
                             .Contains(y.FeeCaclId)
                     )
                     .ToList();

                 deletedFeeList.ForEach(deletedDetail =>
                 {
                     OriginalCaseInvoice.FeeId.Remove(deletedDetail);
                     //db.Entry(caseInvoice).State = EntityState.Deleted;
                 });

                 deletedFeeCalList.ForEach(deletedDetail =>
                 {
                     OriginalCaseInvoice.FeeCaclId.Remove(deletedDetail);
                     //db.Entry(caseInvoice).State = EntityState.Deleted;
                 });

                 editedFeeList.ForEach(editDetail =>
                 {
                     var OriginalFeeDetail = OriginalCaseInvoice.FeeId
                     .Where(x => x.FeeId == editDetail.FeeId)
                     .FirstOrDefault();

                     db.Entry(OriginalFeeDetail).CurrentValues.SetValues(editDetail);
                 });

                 editedFeeCalList.ForEach(editDetail =>
                 {
                     var OriginalFeeCalDetail = OriginalCaseInvoice.FeeCaclId
                     .Where(x => x.FeeCaclId == editDetail.FeeCaclId)
                     .FirstOrDefault();

                     db.Entry(OriginalFeeCalDetail).CurrentValues.SetValues(editDetail);
                 });

                 addedFeeList.ForEach(addedDetail =>
                 {
                     OriginalCaseInvoice.FeeId.Add(addedDetail);
                 });

                 addedFeeCalList.ForEach(addedDetail =>
                 {
                     OriginalCaseInvoice.FeeCaclId.Add(addedDetail);
                 });

                 db.Entry(OriginalCaseInvoice).State = EntityState.Modified;

                 db.SaveChanges();
                 */

                /*
                using(var context = new RBACDbContext())
                {
                    var OriginalCaseInvoice = context.CaseInvoice.Include("FeeId").Include("FeeCaclId")
                        .Where(x => x.InvoiceId == caseInvoice.InvoiceId).FirstOrDefault();

                    context.Entry(OriginalCaseInvoice).CurrentValues.SetValues(caseInvoice);

                    var addedFeeList = caseInvoice.FeeId
                        .Where(y => y.FeeId == 0).ToList();

                    var addedFeeCalList = caseInvoice.FeeCaclId
                        .Where(y => y.FeeCaclId == 0).ToList();

                    var deletedFeeList = OriginalCaseInvoice.FeeId
                        .Where
                        (
                            x => !caseInvoice.FeeId.Select(y => y.FeeId)
                                .Contains(x.FeeId)
                        )
                        .ToList();

                    var deletedFeeCalList = OriginalCaseInvoice.FeeCaclId
                        .Where
                        (
                            x => !caseInvoice.FeeCaclId.Select(y => y.FeeCaclId)
                                .Contains(x.FeeCaclId)
                        )
                        .ToList();

                    var editedFeeList = caseInvoice.FeeId
                        .Where
                        (
                            y => OriginalCaseInvoice.FeeId.Select(z => z.FeeId)
                                .Contains(y.FeeId)
                        )
                        .ToList();

                    var editedFeeCalList = caseInvoice.FeeCaclId
                        .Where
                        (
                            y => OriginalCaseInvoice.FeeCaclId.Select(z => z.FeeCaclId)
                                .Contains(y.FeeCaclId)
                        )
                        .ToList();

                    deletedFeeList.ForEach(deletedDetail =>
                    {
                        OriginalCaseInvoice.FeeId.Remove(deletedDetail);
                        context.Entry(caseInvoice).State = EntityState.Deleted;
                    });

                    deletedFeeCalList.ForEach(deletedDetail =>
                    {
                        OriginalCaseInvoice.FeeCaclId.Remove(deletedDetail);
                        context.Entry(caseInvoice).State = EntityState.Deleted;
                    });

                    editedFeeList.ForEach(editDetail =>
                    {
                        var OriginalFeeDetail = OriginalCaseInvoice.FeeId
                        .Where(x => x.FeeId == editDetail.FeeId)
                        .FirstOrDefault();

                        context.Entry(OriginalFeeDetail).CurrentValues.SetValues(editDetail);
                    });

                    editedFeeCalList.ForEach(editDetail =>
                    {
                        var OriginalFeeCalDetail = OriginalCaseInvoice.FeeCaclId
                        .Where(x => x.FeeCaclId == editDetail.FeeCaclId)
                        .FirstOrDefault();

                        context.Entry(OriginalFeeCalDetail).CurrentValues.SetValues(editDetail);
                    });

                    addedFeeList.ForEach(addedDetail =>
                    {
                        OriginalCaseInvoice.FeeId.Add(addedDetail);
                    });

                    addedFeeCalList.ForEach(addedDetail =>
                    {
                        OriginalCaseInvoice.FeeCaclId.Add(addedDetail);
                    });

                    context.Entry(OriginalCaseInvoice).State = EntityState.Modified;

                    context.SaveChanges();

                }
                */
                /*
                 * BELOW CODE WORKING ONLY IF MODIFIED
                db.Entry(caseInvoice).State = EntityState.Modified;
                db.SaveChanges();

                if(caseInvoice.FeeId != null)
                {
                    foreach (CaseInvoiceFee FeeId in caseInvoice.FeeId)
                    {
                        if (FeeId.FeeId == 0)
                            db.CaseInvoiceFee.Add(FeeId);
                        else
                            db.Entry(FeeId).State = EntityState.Modified;
                    }
                    //db.SaveChanges();
                }

                if (caseInvoice.FeeCaclId != null)
                {
                    foreach (CaseInvoiceFeeCalculation FeeCaclId in caseInvoice.FeeCaclId)
                    {
                        if (FeeCaclId.FeeCaclId == 0)
                            db.CaseInvoiceFeeCalculation.Add(FeeCaclId);
                        else
                            db.Entry(FeeCaclId).State = EntityState.Modified;
                    }
                    //db.SaveChanges();
                }

                db.SaveChanges();
                */


                #endregion

                if (upload != null && upload.ContentLength > 0)
                {
                    string FileExtension = Path.GetExtension(upload.FileName);

                    string FileName = caseInvoice.InvoiceId + FileExtension;

                    string UploadPath = Path.Combine(UploadRoot, "INVDocuments", FileName);

                    upload.SaveAs(UploadPath);
                }
                Session["Message"] = new MessageVM
                {
                    Category = "Success",
                    Message = "Data Save Successfully"
                };

                return RedirectToAction("InvoiceModify", new RouteValueDictionary(new { id = caseInvoice.InvoiceId, ViewType = caseInvoice.buttonName }));

                //if (caseInvoice.buttonName == "ViewInvoice")
                //    return RedirectToAction("InvoiceView", new RouteValueDictionary(new { id = caseInvoice.InvoiceId }));

                //else if (caseInvoice.buttonName == "PrintInvoice")
                //    return RedirectToAction("InvoicePrint", new RouteValueDictionary(new { id = caseInvoice.InvoiceId }));
                //else
                //    return RedirectToAction("InvoiceManageIndex");

            }

            ViewBag.CourtType = new SelectList(Helper.GetCaseLevelList("C"), "Mst_Value", "Mst_Desc");
            ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc", caseInvoice.Credit_Account);
            ViewBag.FeeTypeId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FeesType), "Mst_Value", "Mst_Desc");
            ViewBag.CourtRefNo = new SelectList(GetListForInvoiceCaseCourtRefNo(caseInvoice.CaseId), "Mst_Value", "Mst_Desc");
            ViewBag.CaseLevel = new SelectList(Helper.GetCaseLevelList("C"), "Mst_Value", "Mst_Desc");
            ViewBag.InvClassification = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FeeClassification), "Mst_Value", "Mst_Desc");
            ViewBag.PV_No = new SelectList(Helper.GetPVList(), "Mst_Value", "Mst_Desc");


            ViewBag.CaseId = caseInvoice.CaseId;
            ViewBag.InvoiceDate = DateTime.Now.ToString("dd/MM/yyyy");

            var CaseCreatedBy = EmployeesController.GetEmployeeInfoId(courtCase.CreatedBy);
            ViewBag.BranchName = CaseCreatedBy.LocationName;
            ViewBag.SpecialInstructions = courtCase.SpecialInstructions;
            Session["Message"] = new MessageVM
            {
                Category = "Error",
                Message = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray())
            };
            return View(caseInvoice);
        }


        public ActionResult InvoiceDelete(int? id)
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
        [HttpPost, ActionName("InvoiceDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult InvoiceDeleteConfirmed(int id)
        {
            CaseInvoice caseInvoice = db.CaseInvoice.Find(id);
            caseInvoice.InvoiceStatus = "-1";

            db.Entry(caseInvoice).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("InvoiceManageIndex");

        }
        public ActionResult InvoicePrint(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CaseInvoice caseInvoice = db.CaseInvoice.Where(w => w.InvoiceId == id).Include(s => s.FeeId).Include(s => s.FeeCaclId).FirstOrDefault();

            if (caseInvoice == null)
            {
                return HttpNotFound();
            }

            // GET INVOICE DATATABLE FROM PROCEDURE
            var Dt = Helper.GetInvoiceDetailDt(caseInvoice.InvoiceId);

            caseInvoice.ClientName = Dt.Rows[FIRST_ROW]["ClientName"].ToString();
            caseInvoice.OfficeFileNo = Dt.Rows[FIRST_ROW]["OfficeFileNo"].ToString();
            caseInvoice.Defendant = Dt.Rows[FIRST_ROW]["Defendant"].ToString();
            caseInvoice.ClientCaseTypeName = Dt.Rows[FIRST_ROW]["ClientCaseTypeName"].ToString();


            caseInvoice.ODBBranchName = Dt.Rows[FIRST_ROW]["ODBBranchName"].ToString();
            caseInvoice.AccountContractNo = Dt.Rows[FIRST_ROW]["AccountContractNo"].ToString();
            caseInvoice.ClientFileNo = Dt.Rows[FIRST_ROW]["ClientFileNo"].ToString();
            caseInvoice.CaseTypeName = Dt.Rows[FIRST_ROW]["CaseTypeName"].ToString();
            caseInvoice.CaseSubjectName = Dt.Rows[FIRST_ROW]["CaseSubjectName"].ToString();
            caseInvoice.CaseAgainst = Dt.Rows[FIRST_ROW]["CaseAgainst"].ToString();
            caseInvoice.CaseAgainstCode = Dt.Rows[FIRST_ROW]["CaseAgainstCode"].ToString();
            caseInvoice.ClaimAmount = decimal.Parse(Dt.Rows[FIRST_ROW]["ClaimAmount"].ToString());

            caseInvoice.CreditAccountName = Dt.Rows[FIRST_ROW]["CreditAccountName"].ToString();
            caseInvoice.Subject = Dt.Rows[FIRST_ROW]["Subject"].ToString();
            caseInvoice.EnforcementStageName = Dt.Rows[FIRST_ROW]["EnforcementStageName"].ToString();
            caseInvoice.ClientClassificationCode = Dt.Rows[FIRST_ROW]["ClientClassificationCode"].ToString();
            caseInvoice.ClientClassificationName = Dt.Rows[FIRST_ROW]["ClientClassificationName"].ToString();

            #region Disabled Code ONE
            /*
            var courtCase = db.CourtCase.Find(caseInvoice.CaseId);

            string UserLocation = string.Empty;

            if (User.IsInRole("CourtCasesViewAll") || User.IsSysAdmin())
                UserLocation = string.Empty;
            else
                UserLocation = Helper.GetEmployeeLocation(User.Identity.Name);

            List<CaseInvoice> caseInvoiceList = GetCaseInvoiceList(UserLocation);

            CaseInvoice caseInvoiceDetail = caseInvoiceList.Where(i => i.InvoiceId == id).FirstOrDefault();

            var UserReccrt = db.Users.Where(w => w.Id == caseInvoice.CreatedBy).FirstOrDefault();
            var CaseCreatedBy = EmployeesController.GetEmployeeInfoId(courtCase.CreatedBy);
            ViewBag.BranchName = CaseCreatedBy.LocationName.ToUpper();

            caseInvoice.ClientName = caseInvoiceDetail.ClientName;

            //caseInvoice.InvoiceDate = caseInvoiceDetail.InvoiceDate;
            //caseInvoice.InvoiceNumber = caseInvoiceDetail.InvoiceNumber;
            caseInvoice.OfficeFileNo = caseInvoiceDetail.OfficeFileNo;

            caseInvoice.Defendant = caseInvoiceDetail.Defendant;
            caseInvoice.ClientCaseTypeName = caseInvoiceDetail.ClientCaseTypeName;
            caseInvoice.ODBBranchName = caseInvoiceDetail.ODBBranchName;
            caseInvoice.AccountContractNo = caseInvoiceDetail.AccountContractNo;
            caseInvoice.ClientFileNo = caseInvoiceDetail.ClientFileNo;

            //CASE LOCATION AND COURT
            //ViewBag.CourtType = GetLocationNameForInvoiceCaseLevel(caseInvoice.CaseId, caseInvoice.CourtType);

            */
            #endregion

            string CurrentCaseLevel = string.Empty;

            if (caseInvoice.CourtType == "1")
                CurrentCaseLevel = "TOBE REGISTER";
            else if (caseInvoice.CourtType == "2")
                CurrentCaseLevel = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.BeforeCourtStage && w.Mst_Value == caseInvoice.BeforeCourtStage).FirstOrDefault().Mst_Desc;
            else if (caseInvoice.CourtType == "3")
                CurrentCaseLevel = "PRIMARY";
            else if (caseInvoice.CourtType == "4")
                CurrentCaseLevel = "APPEAL";
            else if (caseInvoice.CourtType == "5")
                CurrentCaseLevel = "SUPREME";
            else if (caseInvoice.CourtType == "6")
                CurrentCaseLevel = "ENFORCEMENT";

            ViewBag.CourtType = caseInvoice.CourtLocation + " " + CurrentCaseLevel;

            #region Disabled Code
            /*
            caseInvoice.CaseTypeName = caseInvoiceDetail.CaseTypeName;
            caseInvoice.CaseSubjectName = caseInvoiceDetail.CaseSubjectName;
            caseInvoice.CaseAgainst = caseInvoiceDetail.CaseAgainst;
            caseInvoice.CaseAgainstCode = courtCase.AgainstCode;

            //caseInvoice.EnforcementStage = caseInvoiceDetail.EnforcementStage;
            //caseInvoice.EnforcementStageNo = caseInvoiceDetail.EnforcementStageNo;

            caseInvoice.ClaimAmount = caseInvoiceDetail.ClaimAmount;

            caseInvoice.CreatedByBranchName = CaseCreatedBy.LocationName;
            //caseInvoice.CaseId = caseInvoiceDetail.CaseId;
            caseInvoice.CreditAccountName = caseInvoiceDetail.CreditAccountName;
            caseInvoice.Subject = courtCase.Subject;
            caseInvoice.EnforcementStageName = caseInvoiceDetail.EnforcementStageName;
            caseInvoice.ClientClassificationCode = caseInvoiceDetail.ClientClassificationCode;
            caseInvoice.ClientClassificationName = caseInvoiceDetail.ClientClassificationName;
            */
            #endregion

            if (!caseInvoice.ClientCaseTypeName.Contains("PLEASE SELECT"))
                ViewBag.ClientCaseTypeName = caseInvoice.ClientCaseTypeName;

            if (!caseInvoice.ClientCaseTypeName.Contains("PLEASE SELECT") && !caseInvoice.ODBBranchName.Contains("PLEASE SELECT"))
                ViewBag.ODBBranchName = " - " +  caseInvoice.ODBBranchName;
            else
                ViewBag.ODBBranchName = caseInvoice.ODBBranchName;

            if (!caseInvoice.CaseTypeName.Contains("PLEASE SELECT"))
                ViewBag.CaseTypeName = caseInvoice.CaseTypeName;

            if (!caseInvoice.CaseTypeName.Contains("PLEASE SELECT") && !caseInvoice.CaseSubjectName.Contains("PLEASE SELECT"))
                ViewBag.CaseSubjectName = " - " +  caseInvoice.CaseSubjectName;
            else
                ViewBag.CaseSubjectName = caseInvoice.CaseSubjectName;



            if (Convert.ToInt32(Dt.Rows[FIRST_ROW]["ClientCaseType"].ToString()) > 0)
            {
                ViewBag.ClientName = string.Format("{0} - {1}", caseInvoice.ClientName, caseInvoice.ClientCaseTypeName);
            }
            else
            {
                ViewBag.ClientName = caseInvoice.ClientName;
            }
            //if (Convert.ToInt32(courtCase.ClientCaseType) > 0)
            //{
            //    ViewBag.ClientName = string.Format("{0} - {1}", caseInvoice.ClientName, caseInvoice.ClientCaseTypeName);
            //}
            //else
            //{
            //    ViewBag.ClientName = caseInvoice.ClientName;
            //}

            //double InvAmt = Convert.ToDouble(db.CaseInvoiceFee.Where(w => w.InvoiceId == id).Select(s => s.FeeAmount).Sum());
            //string InvAmtinWords = Helper.NumberToWords(InvAmt);

            var CalcTotalFee = db.CaseInvoiceFee.Where(w => w.InvoiceId == id).ToList();
            decimal decInvoiceAmount= 0;

            foreach (var item in CalcTotalFee)
            {
                if (item.VATPct > 0 )
                {
                    decimal FeeAmVAT = (decimal)(item.FeeAmount + (item.FeeAmount * (item.VATPct / 100)));
                    decimal FeeAmVATTrunc = Math.Truncate(1000 * FeeAmVAT) / 1000;
                    decInvoiceAmount += FeeAmVAT;
                }
                else
                {
                    decInvoiceAmount += item.FeeAmount;
                }
            }

            double InvAmt = Convert.ToDouble(decInvoiceAmount);
            string InvAmtinWords = Helper.NumberToWords(InvAmt);

            //decimal decInvoiceAmount = db.CaseInvoiceFee.Where(w => w.InvoiceId == id).Select(s => s.FeeAmount).Sum();
            //ViewBag.InvoiceAmount = decInvoiceAmount.ToString("0.###");
            //ViewBag.InvAmtinWords = InvAmtinWords;

            //decimal decInvoiceAmount = item.TotalAmount;
            ViewBag.InvoiceAmount = decInvoiceAmount.ToString("0.###");
            ViewBag.InvAmtinWords = InvAmtinWords;

            ViewBag.CaseTypeCode = Dt.Rows[FIRST_ROW]["CaseTypeCode"].ToString();
            //ViewBag.CaseTypeCode = courtCase.CaseTypeCode;

            if(caseInvoice.ClaimAmount > 0)
            {
                string decClaimAmount = caseInvoice.ClaimAmount.Value.ToString("0.###");
                ViewBag.ClaimAmount = decClaimAmount;
            }

            if (caseInvoice.ClientClassificationCode == "1")
                ViewBag.ACCOUNTDETAILS = "ACCOUNT DETAIL";
            else if (caseInvoice.ClientClassificationCode == "2")
                ViewBag.ACCOUNTDETAILS = "CONTRACT DETAIL";
            else if (caseInvoice.ClientClassificationCode == "3")
                ViewBag.ACCOUNTDETAILS = "PREMISES DETAIL";
            else if (caseInvoice.ClientClassificationCode == "3")
                ViewBag.ACCOUNTDETAILS = "CLAIM DETAIL";
            else 
                ViewBag.ACCOUNTDETAILS = "ACCOUNT DETAIL";


            return View(caseInvoice);
        }

        public ActionResult InvoiceView(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CaseInvoice caseInvoice = db.CaseInvoice.Where(w => w.InvoiceId == id).Include(s => s.FeeId).Include(s => s.FeeCaclId).FirstOrDefault();

            if (caseInvoice == null)
            {
                return HttpNotFound();
            }

            // GET INVOICE DATATABLE FROM PROCEDURE
            var Dt = Helper.GetInvoiceDetailDt(caseInvoice.InvoiceId);

            caseInvoice.ClientName = Dt.Rows[FIRST_ROW]["ClientName"].ToString();
            caseInvoice.OfficeFileNo = Dt.Rows[FIRST_ROW]["OfficeFileNo"].ToString();
            caseInvoice.Defendant = Dt.Rows[FIRST_ROW]["Defendant"].ToString();
            caseInvoice.ClientCaseTypeName = Dt.Rows[FIRST_ROW]["ClientCaseTypeName"].ToString();


            caseInvoice.ODBBranchName = Dt.Rows[FIRST_ROW]["ODBBranchName"].ToString();
            caseInvoice.AccountContractNo = Dt.Rows[FIRST_ROW]["AccountContractNo"].ToString();
            caseInvoice.ClientFileNo = Dt.Rows[FIRST_ROW]["ClientFileNo"].ToString();
            caseInvoice.CaseTypeName = Dt.Rows[FIRST_ROW]["CaseTypeName"].ToString();
            caseInvoice.CaseSubjectName = Dt.Rows[FIRST_ROW]["CaseSubjectName"].ToString();
            caseInvoice.CaseAgainst = Dt.Rows[FIRST_ROW]["CaseAgainst"].ToString();
            caseInvoice.CaseAgainstCode = Dt.Rows[FIRST_ROW]["CaseAgainstCode"].ToString();
            caseInvoice.ClaimAmount = decimal.Parse(Dt.Rows[FIRST_ROW]["ClaimAmount"].ToString());

            caseInvoice.CreditAccountName = Dt.Rows[FIRST_ROW]["CreditAccountName"].ToString();
            caseInvoice.Subject = Dt.Rows[FIRST_ROW]["Subject"].ToString();
            caseInvoice.EnforcementStageName = Dt.Rows[FIRST_ROW]["EnforcementStageName"].ToString();
            caseInvoice.ClientClassificationCode = Dt.Rows[FIRST_ROW]["ClientClassificationCode"].ToString();
            caseInvoice.ClientClassificationName = Dt.Rows[FIRST_ROW]["ClientClassificationName"].ToString();

            #region Disabled Code ONE
            /*
            var courtCase = db.CourtCase.Find(caseInvoice.CaseId);

            string UserLocation = string.Empty;

            if (User.IsInRole("CourtCasesViewAll") || User.IsSysAdmin())
                UserLocation = string.Empty;
            else
                UserLocation = Helper.GetEmployeeLocation(User.Identity.Name);

            List<CaseInvoice> caseInvoiceList = GetCaseInvoiceList(UserLocation);

            CaseInvoice caseInvoiceDetail = caseInvoiceList.Where(i => i.InvoiceId == id).FirstOrDefault();

            var UserReccrt = db.Users.Where(w => w.Id == caseInvoice.CreatedBy).FirstOrDefault();
            var CaseCreatedBy = EmployeesController.GetEmployeeInfoId(courtCase.CreatedBy);
            ViewBag.BranchName = CaseCreatedBy.LocationName.ToUpper();

            caseInvoice.ClientName = caseInvoiceDetail.ClientName;

            //caseInvoice.InvoiceDate = caseInvoiceDetail.InvoiceDate;
            //caseInvoice.InvoiceNumber = caseInvoiceDetail.InvoiceNumber;
            caseInvoice.OfficeFileNo = caseInvoiceDetail.OfficeFileNo;

            caseInvoice.Defendant = caseInvoiceDetail.Defendant;
            caseInvoice.ClientCaseTypeName = caseInvoiceDetail.ClientCaseTypeName;
            caseInvoice.ODBBranchName = caseInvoiceDetail.ODBBranchName;
            caseInvoice.AccountContractNo = caseInvoiceDetail.AccountContractNo;
            caseInvoice.ClientFileNo = caseInvoiceDetail.ClientFileNo;

            //CASE LOCATION AND COURT
            //ViewBag.CourtType = GetLocationNameForInvoiceCaseLevel(caseInvoice.CaseId, caseInvoice.CourtType);

            */
            #endregion

            string CurrentCaseLevel = string.Empty;

            if (caseInvoice.CourtType == "1")
                CurrentCaseLevel = "TOBE REGISTER";
            else if (caseInvoice.CourtType == "2")
                CurrentCaseLevel = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.BeforeCourtStage && w.Mst_Value == caseInvoice.BeforeCourtStage).FirstOrDefault().Mst_Desc;
            else if (caseInvoice.CourtType == "3")
                CurrentCaseLevel = "PRIMARY";
            else if (caseInvoice.CourtType == "4")
                CurrentCaseLevel = "APPEAL";
            else if (caseInvoice.CourtType == "5")
                CurrentCaseLevel = "SUPREME";
            else if (caseInvoice.CourtType == "6")
                CurrentCaseLevel = "ENFORCEMENT";

            ViewBag.CourtType = caseInvoice.CourtLocation + " " + CurrentCaseLevel;

            #region Disabled Code
            /*
            caseInvoice.CaseTypeName = caseInvoiceDetail.CaseTypeName;
            caseInvoice.CaseSubjectName = caseInvoiceDetail.CaseSubjectName;
            caseInvoice.CaseAgainst = caseInvoiceDetail.CaseAgainst;
            caseInvoice.CaseAgainstCode = courtCase.AgainstCode;

            //caseInvoice.EnforcementStage = caseInvoiceDetail.EnforcementStage;
            //caseInvoice.EnforcementStageNo = caseInvoiceDetail.EnforcementStageNo;

            caseInvoice.ClaimAmount = caseInvoiceDetail.ClaimAmount;

            caseInvoice.CreatedByBranchName = CaseCreatedBy.LocationName;
            //caseInvoice.CaseId = caseInvoiceDetail.CaseId;
            caseInvoice.CreditAccountName = caseInvoiceDetail.CreditAccountName;
            caseInvoice.Subject = courtCase.Subject;
            caseInvoice.EnforcementStageName = caseInvoiceDetail.EnforcementStageName;
            caseInvoice.ClientClassificationCode = caseInvoiceDetail.ClientClassificationCode;
            caseInvoice.ClientClassificationName = caseInvoiceDetail.ClientClassificationName;
            */
            #endregion

            if (!caseInvoice.ClientCaseTypeName.Contains("PLEASE SELECT"))
                ViewBag.ClientCaseTypeName = caseInvoice.ClientCaseTypeName;

            if (!caseInvoice.ClientCaseTypeName.Contains("PLEASE SELECT") && !caseInvoice.ODBBranchName.Contains("PLEASE SELECT"))
                ViewBag.ODBBranchName = " - " +  caseInvoice.ODBBranchName;
            else
                ViewBag.ODBBranchName = caseInvoice.ODBBranchName;

            if (!caseInvoice.CaseTypeName.Contains("PLEASE SELECT"))
                ViewBag.CaseTypeName = caseInvoice.CaseTypeName;

            if (!caseInvoice.CaseTypeName.Contains("PLEASE SELECT") && !caseInvoice.CaseSubjectName.Contains("PLEASE SELECT"))
                ViewBag.CaseSubjectName = " - " +  caseInvoice.CaseSubjectName;
            else
                ViewBag.CaseSubjectName = caseInvoice.CaseSubjectName;



            if (Convert.ToInt32(Dt.Rows[FIRST_ROW]["ClientCaseType"].ToString()) > 0)
            {
                ViewBag.ClientName = string.Format("{0} - {1}", caseInvoice.ClientName, caseInvoice.ClientCaseTypeName);
            }
            else
            {
                ViewBag.ClientName = caseInvoice.ClientName;
            }
            //if (Convert.ToInt32(courtCase.ClientCaseType) > 0)
            //{
            //    ViewBag.ClientName = string.Format("{0} - {1}", caseInvoice.ClientName, caseInvoice.ClientCaseTypeName);
            //}
            //else
            //{
            //    ViewBag.ClientName = caseInvoice.ClientName;
            //}

            //double InvAmt = Convert.ToDouble(db.CaseInvoiceFee.Where(w => w.InvoiceId == id).Select(s => s.FeeAmount).Sum());
            //string InvAmtinWords = Helper.NumberToWords(InvAmt);

            var CalcTotalFee = db.CaseInvoiceFee.Where(w => w.InvoiceId == id).ToList();
            decimal decInvoiceAmount= 0;

            foreach (var item in CalcTotalFee)
            {
                if (item.VATPct > 0 )
                {
                    decimal FeeAmVAT = (decimal)(item.FeeAmount + (item.FeeAmount * (item.VATPct / 100)));
                    decimal FeeAmVATTrunc = Math.Truncate(1000 * FeeAmVAT) / 1000;
                    decInvoiceAmount += FeeAmVAT;
                }
                else
                {
                    decInvoiceAmount += item.FeeAmount;
                }
            }

            double InvAmt = Convert.ToDouble(decInvoiceAmount);
            string InvAmtinWords = Helper.NumberToWords(InvAmt);

            //decimal decInvoiceAmount = db.CaseInvoiceFee.Where(w => w.InvoiceId == id).Select(s => s.FeeAmount).Sum();
            //ViewBag.InvoiceAmount = decInvoiceAmount.ToString("0.###");
            //ViewBag.InvAmtinWords = InvAmtinWords;

            //decimal decInvoiceAmount = item.TotalAmount;
            ViewBag.InvoiceAmount = decInvoiceAmount.ToString("0.###");
            ViewBag.InvAmtinWords = InvAmtinWords;

            ViewBag.CaseTypeCode = Dt.Rows[FIRST_ROW]["CaseTypeCode"].ToString();
            //ViewBag.CaseTypeCode = courtCase.CaseTypeCode;

            if(caseInvoice.ClaimAmount > 0)
            {
                string decClaimAmount = caseInvoice.ClaimAmount.Value.ToString("0.###");
                ViewBag.ClaimAmount = decClaimAmount;
            }

            if (caseInvoice.ClientClassificationCode == "1")
                ViewBag.ACCOUNTDETAILS = "ACCOUNT DETAIL";
            else if (caseInvoice.ClientClassificationCode == "2")
                ViewBag.ACCOUNTDETAILS = "CONTRACT DETAIL";
            else if (caseInvoice.ClientClassificationCode == "3")
                ViewBag.ACCOUNTDETAILS = "PREMISES DETAIL";
            else if (caseInvoice.ClientClassificationCode == "3")
                ViewBag.ACCOUNTDETAILS = "CLAIM DETAIL";
            else 
                ViewBag.ACCOUNTDETAILS = "ACCOUNT DETAIL";


            return View(caseInvoice);
        }


        [HttpPost]
        public ActionResult GetCaseNumber(string CaseLevelId, int CaseId)
        {
            string CourtRefNo = string.Empty;
            string LocName = string.Empty;
            try
            {
                if (Convert.ToInt32(CaseLevelId) > 2)
                {
                    if (Convert.ToInt32(CaseLevelId) <= 5) //(PRIMARY / APEAL / SUPREME)
                    {
                        try
                        {
                            CourtRefNo = db.CourtCasesDetail.Where(w => w.CaseId == CaseId && w.CaseLevelCode == CaseLevelId).FirstOrDefault().CourtRefNo;
                        }
                        catch (Exception e)
                        {
                            CourtRefNo = "TO BE REGISTER";

                        }
                    }
                    else //ENFORCEMENT
                    {
                        try
                        {
                            var vRdoEnfInvoiceInfo = db.CourtCasesEnforcement.Where(w=> w.CaseId == CaseId).FirstOrDefault();

                            if (vRdoEnfInvoiceInfo.EnforcementInfoInvoice == "1")
                                CourtRefNo = vRdoEnfInvoiceInfo.ArrestOrderNo;
                            else if (vRdoEnfInvoiceInfo.EnforcementInfoInvoice == "2")
                            {
                                LocName = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Location && m.Mst_Value == vRdoEnfInvoiceInfo.PrimaryObjectionCourtLocationid).FirstOrDefault().Mst_Desc;
                                CourtRefNo = string.Format(@"{0} - {1}",vRdoEnfInvoiceInfo.PrimaryObjectionNo, LocName);
                            }
                            else if (vRdoEnfInvoiceInfo.EnforcementInfoInvoice == "3")
                            {
                                LocName = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Location && m.Mst_Value == vRdoEnfInvoiceInfo.ApealObjectionCourtLocationid).FirstOrDefault().Mst_Desc;
                                CourtRefNo = string.Format(@"{0} - {1}", vRdoEnfInvoiceInfo.ApealObjectionNo, LocName);
                            }
                            else if (vRdoEnfInvoiceInfo.EnforcementInfoInvoice == "4")
                            {
                                LocName = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Location && m.Mst_Value == vRdoEnfInvoiceInfo.PrimaryPlaintCourtLocationid).FirstOrDefault().Mst_Desc;
                                CourtRefNo = string.Format(@"{0} - {1}", vRdoEnfInvoiceInfo.PrimaryPlaintNo, LocName);
                            }
                            else if (vRdoEnfInvoiceInfo.EnforcementInfoInvoice == "5")
                            {
                                LocName = db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Location && m.Mst_Value == vRdoEnfInvoiceInfo.ApealPlaintCourtLocationid).FirstOrDefault().Mst_Desc;
                                CourtRefNo = string.Format(@"{0} - {1}", vRdoEnfInvoiceInfo.ApealPlaintNo, LocName);
                            }
                        }
                        catch (Exception e)
                        {
                            CourtRefNo = "TO BE REGISTER";

                        }
                    }
                }
                else //BEFORE COURT
                {
                    try
                    {
                        var vRdoBeforeCourt = db.CourtCase.Find(CaseId);

                        if (vRdoBeforeCourt.SelectedBeforeCourt == "1")
                            CourtRefNo = vRdoBeforeCourt.PoliceNo;
                        else if (vRdoBeforeCourt.SelectedBeforeCourt == "2")
                            CourtRefNo = vRdoBeforeCourt.PublicProsecutionNo;
                        else if (vRdoBeforeCourt.SelectedBeforeCourt == "3")
                            CourtRefNo = vRdoBeforeCourt.PAPCNo;
                        else if (vRdoBeforeCourt.SelectedBeforeCourt == "4")
                            CourtRefNo = vRdoBeforeCourt.LaborConflicNo;
                    }
                    catch (Exception e)
                    {
                        CourtRefNo = " ";
                    }
                }


                return new JsonResult()
                {
                    Data = new { Message = "Success", RetResult = CourtRefNo }
                };


            }
            catch (Exception e)
            {
                return new JsonResult()
                {
                    Data = new { Message = "Error", ErrorMessage = e.Message }
                };

            }


        }

        #endregion


        public static List<CaseInvoice> GetCaseInvoiceList(string UserLocation)
        {
            RBACDbContext db = new RBACDbContext();
            List<CaseInvoice> ResultList = new List<CaseInvoice>();

            var CreditList = Helper.GetBankList();

            #region Join CourtCases with CaseInvoice

            var ListCaseInvoice = db.CaseInvoice.ToList();


            var ListCourtCases = CourtCasesController.GetCourtCasesList(UserLocation);

            ResultList = (from Cases in ListCourtCases
                          join Invoices in ListCaseInvoice
                          on Cases.CaseId equals Invoices.CaseId
                          join court_mas in db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseLevel)
                          on Invoices.CourtType equals court_mas.Mst_Value
                          join InvStatusMaster in db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.InvoiceStatus)
                          on Invoices.InvoiceStatus equals InvStatusMaster.Mst_Value
                          join Enforcement in db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.EnforcementStage)
                          on Invoices.EnforcementStage equals Enforcement.Mst_Value
                          join PayTo in CreditList
                          on Invoices.Credit_Account equals Convert.ToInt32(PayTo.Mst_Value)
                          select new CaseInvoice
                          {
                              InvoiceId = Invoices.InvoiceId,
                              InvoiceDate = Invoices.InvoiceDate,
                              InvoiceNumber = Invoices.InvoiceNumber,
                              CaseId = Invoices.CaseId,
                              CourtType = Invoices.CourtType,
                              CourtName = court_mas.Mst_Desc,
                              InvoiceStatus = Invoices.InvoiceStatus,
                              InvoiceStatusName = InvStatusMaster.Mst_Desc,
                              TransferType = Invoices.TransferType,
                              TransferNumber = Invoices.TransferNumber,
                              TransferDate = Invoices.TransferDate,
                              Credit_Account = Invoices.Credit_Account,
                              CreditAccountName = PayTo.Mst_Desc,
                              Defendant = Cases.Defendant,
                              OfficeFileNo = Cases.OfficeFileNo,
                              AccountContractNo = Cases.AccountContractNo,
                              ClientFileNo = Cases.ClientFileNo,
                              ClaimAmount = Cases.ClaimAmount,
                              ClientName = Cases.ClientName,
                              CreatedBy = Invoices.CreatedBy,
                              CreatedOn = Invoices.CreatedOn,
                              UpdatedBy = Invoices.UpdatedBy,
                              UpdatedOn = Invoices.UpdatedOn,
                              ClientCaseTypeName = Cases.ClientCaseTypeName,
                              CaseTypeName = Cases.CaseTypeName,
                              CaseAgainst = Cases.AgainstName,
                              CaseAgainstCode = Cases.AgainstCode,
                              ClientClassificationCode = Cases.ClientClassificationCode,
                              ClientClassificationName = Cases.ClientClassificationName,
                              CaseSubjectName = Cases.CaseSubjectName,
                              EnforcementStage = Invoices.EnforcementStage,
                              EnforcementStageName = Enforcement.Mst_Desc,
                              Subject = Cases.Subject,
                              ODBBranchName = Cases.ODBBankBranchName
                              
                          }).ToList();

            #endregion

            return ResultList;
        }
        public static string GetLocationNameForInvoiceCaseLevel(int CaseId, string Courtid)
        {
            RBACDbContext db = new RBACDbContext();
            var CourtTypes = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.CaseLevel && w.Mst_Value == Courtid).FirstOrDefault();

            string Result = string.Empty;
            string LocationName = string.Empty;

            List<MasterSetups> ResultList = new List<MasterSetups>();
            var CourtsInDetails = db.CourtCasesDetail.Where(w => w.CaseId == CaseId && w.CaseLevelCode == Courtid).ToList();

            ResultList = (from Clist in CourtsInDetails
                          join loc_mas in db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Location)
                          on Clist.CourtLocationid equals loc_mas.Mst_Value
                          select new MasterSetups
                          {
                              Mst_Value = loc_mas.Mst_Value,
                              Mst_Desc = loc_mas.Mst_Desc
                          }).ToList();

            LocationName = ResultList.Select(s => s.Mst_Desc).FirstOrDefault();

            if (string.IsNullOrEmpty(LocationName))
                Result = CourtTypes.Mst_Desc;
            else
                Result = string.Format(@"{0} - {1}", CourtTypes.Mst_Desc, LocationName);


            return Result;
        }
        public static List<MasterSetups> GetCourtListforCase(int CaseId)
        {
            RBACDbContext db = new RBACDbContext();
            List<MasterSetups> ResultList = new List<MasterSetups>();
            var CourtsInDetails = db.CourtCasesDetail.Where(w => w.CaseId == CaseId).ToList();

            ResultList = (from Clist in CourtsInDetails
                          join court_mas in db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CourtType)
                          on Clist.Courtid equals court_mas.Mst_Value
                          join loc_mas in db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Location)
                          on Clist.CourtLocationid equals loc_mas.Mst_Value
                          select new MasterSetups
                          {
                              Mst_Value = court_mas.Mst_Value,
                              Mst_Desc = string.Format(@"{0} - {1}", court_mas.Mst_Desc, loc_mas.Mst_Desc)
                          }).ToList();
            return ResultList;
        }
        public static List<MasterSetups> GetListForInvoiceCaseLevel(int CaseId)
        {
            using (var context = new RBACDbContext())
            {
                List<MasterSetups> ResultList = new List<MasterSetups>();
                SqlParameter pCaseId = new SqlParameter("@CaseId", CaseId);

                ResultList = context.Database.SqlQuery<MasterSetups>("GetCaseLevelForInvoice @CaseId", pCaseId).OrderBy(o => o.DisplaySeq).ToList();
                return ResultList;
            }
        }
        public static List<MasterSetups> GetListBankTransferAuth()
        {
            using (var context = new RBACDbContext())
            {
                List<MasterSetups> ResultList = new List<MasterSetups>();
                ResultList = context.Database.SqlQuery<MasterSetups>("GetListBankTransferAuth").ToList();
                return ResultList;
            }
        }
        public static List<MasterSetups> GetListForPVInvoicesByCase(int CaseId)
        {
            RBACDbContext db = new RBACDbContext();
            List<MasterSetups> ResultList = new List<MasterSetups>();
            var Invoices = db.CaseInvoice.Where(w => w.CaseId == CaseId).ToList();

            ResultList = (from Clist in Invoices
                          select new MasterSetups
                          {
                              Mst_Value = Clist.InvoiceNumber,
                              Mst_Desc = Clist.InvoiceNumber
                          }).ToList();
            return ResultList;

        }
        public static List<MasterSetups> GetListForInvoiceCaseCourtRefNo(int CaseId)
        {
            RBACDbContext db = new RBACDbContext();
            List<MasterSetups> ResultList = new List<MasterSetups>();
            var CourtsInDetails = db.CourtCasesDetail.Where(w => w.CaseId == CaseId).ToList();

            ResultList = (from Clist in CourtsInDetails
                          select new MasterSetups
                          {
                              Mst_Value = Clist.CourtRefNo,
                              Mst_Desc = Clist.CourtRefNo
                          }).ToList();
            return ResultList;

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