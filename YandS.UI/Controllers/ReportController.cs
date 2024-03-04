using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YandS.UI.Models;
using YandS.DAL;
using System.IO;

namespace YandS.UI.Controllers
{
    public class ReportController : Controller
    {
        private RBACDbContext db = new RBACDbContext();
        public FilePathResult DownloadArchieve(string fileName)
        {
            return new FilePathResult(string.Format(@"{0}\{1}", Helper.GetTemplateRoot, fileName + ".xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        public ActionResult CourtCasesDetail()
        {
            ViewBag.Location = this.ListLocation();
            ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
            ViewBag.AgainstCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseAgainst), "Mst_Value", "Mst_Desc");
            ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
            ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
            ViewBag.CaseStatus = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseStatus), "Mst_Value", "Mst_Desc");
            ViewBag.ParentCourtId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ParentCourt), "Mst_Value", "Mst_Desc");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CourtCasesDetail(RepCaseParameterForm p_modal)
        {
            if (ModelState.IsValid)
            {
                string MIMETYPE = string.Empty;
                string FileExt = string.Empty;

                if (p_modal.ClaimAmount == null)
                    p_modal.ClaimAmount = 0;

                if (p_modal.ReceptionDateFrom == null|| p_modal.ReceptionDateTo == null)
                {
                    p_modal.ReceptionDateFrom = DateTime.Today.AddYears(-100);
                    p_modal.ReceptionDateTo = DateTime.MaxValue;
                }
                    
                
                ReportCourtCases objDAL = new ReportCourtCases();

                var RetResult = objDAL.getCourtCaseDetail(p_modal);

                if(RetResult.Tables[0].Rows.Count > 0)
                {

                    if (p_modal.ReportFormat == "EXCEL")
                    {
                        MIMETYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        FileExt = ".xlsx";
                    }
                    else if (p_modal.ReportFormat == "PDF")
                    {
                        MIMETYPE = "application/pdf";
                        FileExt = ".pdf";
                    }
                    else if (p_modal.ReportFormat == "WORD")
                    {
                        MIMETYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        FileExt = ".docx";
                    }

                }

                MemoryStream ResultStream = new MemoryStream();

                string ExcelResult = objDAL.GenerateExcelStream("", "Court Case Detail Report", RetResult, ref ResultStream);

                byte[] fileContents = null;
                using (ResultStream)
                {
                    fileContents = ResultStream.ToArray();
                }

                return File(fileContents, MIMETYPE, "ExcelReport.xlsx");
            }

            ViewBag.Location = this.ListLocation();
            ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
            ViewBag.AgainstCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseAgainst), "Mst_Value", "Mst_Desc");
            ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
            ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
            ViewBag.CaseStatus = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseStatus), "Mst_Value", "Mst_Desc");
            ViewBag.ParentCourtId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ParentCourt), "Mst_Value", "Mst_Desc");

            return View();
        }

        public ActionResult CourtCasesFollowup()
        {
            ViewBag.Location = this.ListLocation();
            ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CourtCasesFollowup(RepCaseParameterForm p_modal)
        {
            if (ModelState.IsValid)
            {
                string MIMETYPE = string.Empty;
                string FileExt = string.Empty;

                if (p_modal.ClaimAmount == null)
                    p_modal.ClaimAmount = 0;

                if (p_modal.ReceptionDateFrom == null|| p_modal.ReceptionDateTo == null)
                {
                    p_modal.ReceptionDateFrom = DateTime.Today.AddYears(-100);
                    p_modal.ReceptionDateTo = DateTime.MaxValue;
                }
                    
                
                ReportCourtCases objDAL = new ReportCourtCases();

                var RetResult = objDAL.getCaseFollowup(p_modal);

                if(RetResult.Tables[0].Rows.Count > 0)
                {

                    if (p_modal.ReportFormat == "EXCEL")
                    {
                        MIMETYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        FileExt = ".xlsx";
                    }
                    else if (p_modal.ReportFormat == "PDF")
                    {
                        MIMETYPE = "application/pdf";
                        FileExt = ".pdf";
                    }
                    else if (p_modal.ReportFormat == "WORD")
                    {
                        MIMETYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        FileExt = ".docx";
                    }

                }

                MemoryStream ResultStream = new MemoryStream();

                string ExcelResult = objDAL.GenerateExcelStream("", "Case Followup Schedule", RetResult, ref ResultStream);

                byte[] fileContents = null;
                using (ResultStream)
                {
                    fileContents = ResultStream.ToArray();
                }

                return File(fileContents, MIMETYPE, "CaseFollowupReport.xlsx");
            }

            ViewBag.Location = this.ListLocation();
            ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");

            return View();
        }

        public ActionResult CourtCasesByStatus()
        {
            ViewBag.Location = this.ListLocation();
            ViewBag.CaseStatus = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseStatus), "Mst_Value", "Mst_Desc");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CourtCasesByStatus(RepCaseParameterForm p_modal)
        {
            if (ModelState.IsValid)
            {
                string MIMETYPE = string.Empty;
                string FileExt = string.Empty;

                if (p_modal.ClaimAmount == null)
                    p_modal.ClaimAmount = 0;

                if (p_modal.ReceptionDateFrom == null|| p_modal.ReceptionDateTo == null)
                {
                    p_modal.ReceptionDateFrom = DateTime.Today.AddYears(-100);
                    p_modal.ReceptionDateTo = DateTime.MaxValue;
                }
                    
                
                ReportCourtCases objDAL = new ReportCourtCases();

                var RetResult = objDAL.getCourtCasesByStatus(p_modal);

                if(RetResult.Tables[0].Rows.Count > 0)
                {


                }

                if (p_modal.ReportFormat == "EXCEL")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    FileExt = ".xlsx";
                }
                else if (p_modal.ReportFormat == "PDF")
                {
                    MIMETYPE = "application/pdf";
                    FileExt = ".pdf";
                }
                else if (p_modal.ReportFormat == "WORD")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    FileExt = ".docx";
                }

                MemoryStream ResultStream = new MemoryStream();

                string StatusName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.CaseStatus && w.Mst_Value == p_modal.CaseStatus).FirstOrDefault().Mst_Desc;

                string ReportHeading = string.Format(@"Muscat and other Regions {0} cases [{1}]", p_modal.CaseStatus == "0" ? "ALL" : StatusName, DateTime.Now.ToString("MMMM").ToUpper());

                string ExcelResult = objDAL.GenerateExcelStream("", ReportHeading, RetResult, ref ResultStream);

                byte[] fileContents = null;
                using (ResultStream)
                {
                    fileContents = ResultStream.ToArray();
                }

                return File(fileContents, MIMETYPE, "CourtCasesByStatus.xlsx");
            }

            ViewBag.Location = this.ListLocation();
            ViewBag.CaseStatus = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseStatus), "Mst_Value", "Mst_Desc");

            return View();
        }

        public ActionResult ClientWiseInvRep()
        {
            ViewBag.Location = this.ListLocation();
            ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientWiseInvRep(RepCaseParameterForm p_modal)
        {
            if (ModelState.IsValid)
            {
                string MIMETYPE = string.Empty;
                string FileExt = string.Empty;

                if (p_modal.ClaimAmount == null)
                    p_modal.ClaimAmount = 0;

                if (p_modal.ReceptionDateFrom == null|| p_modal.ReceptionDateTo == null)
                {
                    p_modal.ReceptionDateFrom = DateTime.Today.AddYears(-100);
                    p_modal.ReceptionDateTo = DateTime.MaxValue;
                }
                    
                string ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == p_modal.ClientCode).FirstOrDefault().Mst_Desc;
                ReportCourtCases objDAL = new ReportCourtCases();

                var RetResult = objDAL.getClientWiseInvRep(p_modal);

                if (p_modal.ReportFormat == "EXCEL")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    FileExt = ".xlsx";
                }
                else if (p_modal.ReportFormat == "PDF")
                {
                    MIMETYPE = "application/pdf";
                    FileExt = ".pdf";
                }
                else if (p_modal.ReportFormat == "WORD")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    FileExt = ".docx";
                }

                MemoryStream ResultStream = new MemoryStream();
                string ReportHeading = string.Format(@"Report for Client : {0}{1} Date of Issue of Report : {2}", p_modal.ClientCode == "0" ? "ALL" : ClientName, Environment.NewLine, DateTime.Today.ToString("dd-MM-yyyy"));

                string ExcelResult = objDAL.GenerateExcelStream("", ReportHeading, RetResult, ref ResultStream);

                byte[] fileContents = null;
                using (ResultStream)
                {
                    fileContents = ResultStream.ToArray();
                }

                return File(fileContents, MIMETYPE, "ClientWiseInvRep.xlsx");
            }

            ViewBag.Location = this.ListLocation();
            ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");

            return View();
        }

        public ActionResult PVReportPivot()
        {
            ViewBag.Location = this.ListLocation();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PVReportPivot(RepCaseParameterForm p_modal)
        {
            if (ModelState.IsValid)
            {
                string MIMETYPE = string.Empty;
                string FileExt = string.Empty;

                if (p_modal.ClaimAmount == null)
                    p_modal.ClaimAmount = 0;

                if (p_modal.ReceptionDateFrom == null|| p_modal.ReceptionDateTo == null)
                {
                    p_modal.ReceptionDateFrom = DateTime.Today.AddYears(-100);
                    p_modal.ReceptionDateTo = DateTime.MaxValue;
                }
                    
                string BranchName = this.ListLocation().Where(w => w.Value == p_modal.Location).FirstOrDefault().Text;
                ReportCourtCases objDAL = new ReportCourtCases();

                var RetResult = objDAL.getPVReportPivot(p_modal);

                if (p_modal.ReportFormat == "EXCEL")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    FileExt = ".xlsx";
                }
                else if (p_modal.ReportFormat == "PDF")
                {
                    MIMETYPE = "application/pdf";
                    FileExt = ".pdf";
                }
                else if (p_modal.ReportFormat == "WORD")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    FileExt = ".docx";
                }

                MemoryStream ResultStream = new MemoryStream();
                string ReportHeading = string.Format(@"{0}{1}PAYMENT HEAD WISE PV REPORT", BranchName, Environment.NewLine);

                string ExcelResult = objDAL.GenerateExcelStream("", ReportHeading, RetResult, ref ResultStream);

                byte[] fileContents = null;
                using (ResultStream)
                {
                    fileContents = ResultStream.ToArray();
                }

                return File(fileContents, MIMETYPE, "PaymentHeadWisePVReport.xlsx");
            }

            ViewBag.Location = this.ListLocation();

            return View();
        }

        public ActionResult PVListReport()
        {
            ViewBag.Location = this.ListLocation();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PVListReport(RepCaseParameterForm p_modal)
        {
            if (ModelState.IsValid)
            {
                string MIMETYPE = string.Empty;
                string FileExt = string.Empty;

                if (p_modal.ClaimAmount == null)
                    p_modal.ClaimAmount = 0;

                if (p_modal.ReceptionDateFrom == null|| p_modal.ReceptionDateTo == null)
                {
                    p_modal.ReceptionDateFrom = DateTime.Today.AddYears(-100);
                    p_modal.ReceptionDateTo = DateTime.MaxValue;
                }
                    
                string BranchName = this.ListLocation().Where(w => w.Value == p_modal.Location).FirstOrDefault().Text;
                ReportCourtCases objDAL = new ReportCourtCases();

                var RetResult = objDAL.getPVListReport(p_modal);

                if (p_modal.ReportFormat == "EXCEL")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    FileExt = ".xlsx";
                }
                else if (p_modal.ReportFormat == "PDF")
                {
                    MIMETYPE = "application/pdf";
                    FileExt = ".pdf";
                }
                else if (p_modal.ReportFormat == "WORD")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    FileExt = ".docx";
                }

                MemoryStream ResultStream = new MemoryStream();
                string ReportHeading = string.Format(@"{0}{1}PAYMENT VOUCHER LIST", BranchName, Environment.NewLine);

                string ExcelResult = objDAL.GenerateExcelStream("", ReportHeading, RetResult, ref ResultStream);

                byte[] fileContents = null;
                using (ResultStream)
                {
                    fileContents = ResultStream.ToArray();
                }

                return File(fileContents, MIMETYPE, "PVListReport.xlsx");
            }

            ViewBag.Location = this.ListLocation();

            return View();
        }

        public ActionResult InvoiceReport()
        {
            ViewBag.Location = this.ListLocation();
            ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client && m.Active_Flag == true).OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc");
            ViewBag.AgainstCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseAgainst && m.Active_Flag == true).OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc");
            ViewBag.ODBBankBranch = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ODBBankBranch && m.Active_Flag == true).OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc");
            ViewBag.InvoiceStatus = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.InvoiceStatus && m.Active_Flag == true).OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc");
            ViewBag.ClientCaseType = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ClientCaseType && m.Active_Flag == true).OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc", "0"); // Blank
            ViewBag.FeeTypeId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FeesType && m.Active_Flag == true).OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InvoiceReport(RepCaseParameterForm p_modal)
        {
            if (ModelState.IsValid)
            {
                string MIMETYPE = string.Empty;
                string FileExt = string.Empty;

                if (p_modal.ClaimAmount == null)
                    p_modal.ClaimAmount = 0;

                if (p_modal.ReceptionDateFrom == null || p_modal.ReceptionDateTo == null)
                {
                    p_modal.ReceptionDateFrom = DateTime.Today.AddYears(-100);
                    p_modal.ReceptionDateTo = DateTime.MaxValue;
                }

                string BranchName = this.ListLocation().Where(w => w.Value == p_modal.Location).FirstOrDefault().Text;
                ReportCourtCases objDAL = new ReportCourtCases();

                var RetResult = objDAL.getInvoiceReport(p_modal);

                if(RetResult.Tables[0].Rows.Count > 0)
                {
                    if (p_modal.ReportFormat == "EXCEL")
                    {
                        MIMETYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        FileExt = ".xlsx";
                    }
                    else if (p_modal.ReportFormat == "PDF")
                    {
                        MIMETYPE = "application/pdf";
                        FileExt = ".pdf";
                    }
                    else if (p_modal.ReportFormat == "WORD")
                    {
                        MIMETYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        FileExt = ".docx";
                    }

                    MemoryStream ResultStream = new MemoryStream();

                    string InvTemplateName = string.Empty;
                    string InvName = string.Empty;
                    string TemplateName = string.Empty;
                    string ExcelResult = string.Empty;
                    string InvoiceREFNO = string.Empty;

                    if (p_modal.ClientCode == "7")
                    {
                        InvTemplateName = "ReportAhliBank.xlsx";
                        InvName = "INVOICEREPORTAHLI";
                    }
                    else
                    {
                        InvTemplateName = "INVOICE_REP_TEMPLATE.xlsx";
                        InvName = "INVOICEREPORT";
                    }

                    TemplateName = Path.Combine(Helper.GetTemplateRoot, InvTemplateName);

                    using (FileStream file = new FileStream(TemplateName, FileMode.Open, FileAccess.Read))
                        file.CopyTo(ResultStream);

                    string ReportHeading = string.Format(@"{0}{1}INVOICE REPORT", BranchName, Environment.NewLine);

                    if (InvName == "INVOICEREPORT")
                    {
                        string UserLocationCode = Helper.GetEmployeeLocation(User.Identity.Name).Split('-')[1];
                        string UserLocationName = this.ListLocation().Where(w => w.Value == UserLocationCode).FirstOrDefault().Text;
                        InvoiceREFNO = Helper.ProcessInvoiceREFNO(UserLocationName);
                        ExcelResult = objDAL.GenerateExcelStream(InvName, ReportHeading, RetResult, ref ResultStream, InvoiceREFNO);
                    }
                    else
                        ExcelResult = objDAL.GenerateExcelStream(InvName, ReportHeading, RetResult, ref ResultStream);

                    //ExcelResult = objDAL.GenerateExcelStream(InvName, ReportHeading, RetResult, ref ResultStream);

                    byte[] fileContents = null;
                    using (ResultStream)
                    {
                        fileContents = ResultStream.ToArray();
                    }

                    if (InvName == "INVOICEREPORT")
                        return File(fileContents, MIMETYPE, InvoiceREFNO + ".xlsx");
                    else
                        return File(fileContents, MIMETYPE, "InvoiceReport.xlsx");

                    //return File(fileContents, MIMETYPE, "InvoiceReport.xlsx");

                }
                else
                {
                    Session["Message"] = new MessageVM
                    {
                        Category = "Error",
                        Message = "NO DATA FOUND FOR SELECTED CRITERIA"
                    };
                }
            }

            ViewBag.Location = this.ListLocation();

            return View();
        }

        public ActionResult PVIntraTrans()
        {
            ViewBag.Location = this.ListLocation();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PVIntraTrans(RepCaseParameterForm p_modal)
        {
            if (ModelState.IsValid)
            {
                string MIMETYPE = string.Empty;
                string FileExt = string.Empty;

                if (p_modal.ClaimAmount == null)
                    p_modal.ClaimAmount = 0;

                if (p_modal.ReceptionDateFrom == null || p_modal.ReceptionDateTo == null)
                {
                    p_modal.ReceptionDateFrom = DateTime.Today.AddYears(-100);
                    p_modal.ReceptionDateTo = DateTime.MaxValue;
                }

                string BranchName = this.ListLocation().Where(w => w.Value == p_modal.Location).FirstOrDefault().Text;
                ReportCourtCases objDAL = new ReportCourtCases();

                var RetResult = objDAL.getPVIntraTransReport(p_modal);

                if (p_modal.ReportFormat == "EXCEL")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    FileExt = ".xlsx";
                }
                else if (p_modal.ReportFormat == "PDF")
                {
                    MIMETYPE = "application/pdf";
                    FileExt = ".pdf";
                }
                else if (p_modal.ReportFormat == "WORD")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    FileExt = ".docx";
                }

                MemoryStream ResultStream = new MemoryStream();
                string ReportHeading = string.Format(@"{0}{1}INTRA-TRANSACTION REPORT", BranchName, Environment.NewLine);

                string ExcelResult = objDAL.GenerateExcelStream("", ReportHeading, RetResult, ref ResultStream);

                byte[] fileContents = null;
                using (ResultStream)
                {
                    fileContents = ResultStream.ToArray();
                }

                return File(fileContents, MIMETYPE, "IntraTransaction.xlsx");
            }

            ViewBag.Location = this.ListLocation();

            return View();
        }

        public ActionResult ClientAnnualAuditReport()
        {
            ViewBag.Location = this.ListLocation();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientAnnualAuditReport(RepCaseParameterForm p_modal)
        {
            if (ModelState.IsValid)
            {
                string MIMETYPE = string.Empty;
                string FileExt = string.Empty;

                if (p_modal.ClaimAmount == null)
                    p_modal.ClaimAmount = 0;

                if (p_modal.ReceptionDateFrom == null || p_modal.ReceptionDateTo == null)
                {
                    p_modal.ReceptionDateFrom = DateTime.Today.AddYears(-100);
                    p_modal.ReceptionDateTo = DateTime.MaxValue;
                }

                string BranchName = this.ListLocation().Where(w => w.Value == p_modal.Location).FirstOrDefault().Text;
                ReportCourtCases objDAL = new ReportCourtCases();

                var RetResult = objDAL.getPVIntraTransReport(p_modal);

                if (p_modal.ReportFormat == "EXCEL")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    FileExt = ".xlsx";
                }
                else if (p_modal.ReportFormat == "PDF")
                {
                    MIMETYPE = "application/pdf";
                    FileExt = ".pdf";
                }
                else if (p_modal.ReportFormat == "WORD")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    FileExt = ".docx";
                }

                MemoryStream ResultStream = new MemoryStream();
                string ReportHeading = string.Format(@"{0}{1}INTRA-TRANSACTION REPORT", BranchName, Environment.NewLine);

                string ExcelResult = objDAL.GenerateExcelStream("", ReportHeading, RetResult, ref ResultStream);

                byte[] fileContents = null;
                using (ResultStream)
                {
                    fileContents = ResultStream.ToArray();
                }

                return File(fileContents, MIMETYPE, "IntraTransaction.xlsx");
            }

            ViewBag.Location = this.ListLocation();

            return View();
        }

        public ActionResult REFPVReport()
        {
            ViewBag.Location = this.ListLocation();
            ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(), "Mst_Value", "Mst_Desc", "1901");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult REFPVReport(RepCaseParameterForm p_modal)
        {
            if (ModelState.IsValid)
            {
                string MIMETYPE = string.Empty;
                string FileExt = string.Empty;

                if (p_modal.ClaimAmount == null)
                    p_modal.ClaimAmount = 0;

                if (p_modal.ReceptionDateFrom == null || p_modal.ReceptionDateTo == null)
                {
                    p_modal.ReceptionDateFrom = DateTime.Today.AddYears(-100);
                    p_modal.ReceptionDateTo = DateTime.MaxValue;
                }

                string BranchName = this.ListLocation().Where(w => w.Value == p_modal.Location).FirstOrDefault().Text;
                ReportCourtCases objDAL = new ReportCourtCases();

                var RetResult = objDAL.getPVIntraTransReport(p_modal);

                if (p_modal.ReportFormat == "EXCEL")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    FileExt = ".xlsx";
                }
                else if (p_modal.ReportFormat == "PDF")
                {
                    MIMETYPE = "application/pdf";
                    FileExt = ".pdf";
                }
                else if (p_modal.ReportFormat == "WORD")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    FileExt = ".docx";
                }

                MemoryStream ResultStream = new MemoryStream();
                string ReportHeading = string.Format(@"{0}{1}REFUNDABLE PV REPORT", BranchName, Environment.NewLine);

                string ExcelResult = objDAL.GenerateExcelStream("", ReportHeading, RetResult, ref ResultStream);

                byte[] fileContents = null;
                using (ResultStream)
                {
                    fileContents = ResultStream.ToArray();
                }

                return File(fileContents, MIMETYPE, "RefundablePVReport.xlsx");
            }

            ViewBag.Location = this.ListLocation();

            return View();
        }

        public ActionResult NONREFPVReport()
        {
            ViewBag.Location = this.ListLocation();
            ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(), "Mst_Value", "Mst_Desc", "1901");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NONREFPVReport(RepCaseParameterForm p_modal)
        {
            if (ModelState.IsValid)
            {
                string MIMETYPE = string.Empty;
                string FileExt = string.Empty;

                if (p_modal.ClaimAmount == null)
                    p_modal.ClaimAmount = 0;

                if (p_modal.ReceptionDateFrom == null || p_modal.ReceptionDateTo == null)
                {
                    p_modal.ReceptionDateFrom = DateTime.Today.AddYears(-100);
                    p_modal.ReceptionDateTo = DateTime.MaxValue;
                }

                string BranchName = this.ListLocation().Where(w => w.Value == p_modal.Location).FirstOrDefault().Text;
                ReportCourtCases objDAL = new ReportCourtCases();

                var RetResult = objDAL.getPVIntraTransReport(p_modal);

                if (p_modal.ReportFormat == "EXCEL")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    FileExt = ".xlsx";
                }
                else if (p_modal.ReportFormat == "PDF")
                {
                    MIMETYPE = "application/pdf";
                    FileExt = ".pdf";
                }
                else if (p_modal.ReportFormat == "WORD")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    FileExt = ".docx";
                }

                MemoryStream ResultStream = new MemoryStream();
                string ReportHeading = string.Format(@"{0}{1}NON-REFUNDABLE PV REPORT", BranchName, Environment.NewLine);

                string ExcelResult = objDAL.GenerateExcelStream("", ReportHeading, RetResult, ref ResultStream);

                byte[] fileContents = null;
                using (ResultStream)
                {
                    fileContents = ResultStream.ToArray();
                }

                return File(fileContents, MIMETYPE, "NonRefundablePVReport.xlsx");
            }

            ViewBag.Location = this.ListLocation();

            return View();
        }

        public ActionResult LawreFeeReport()
        {
            ViewBag.Location = this.ListLocation();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LawreFeeReport(RepCaseParameterForm p_modal)
        {
            if (ModelState.IsValid)
            {
                string MIMETYPE = string.Empty;
                string FileExt = string.Empty;

                if (p_modal.ClaimAmount == null)
                    p_modal.ClaimAmount = 0;

                if (p_modal.ReceptionDateFrom == null || p_modal.ReceptionDateTo == null)
                {
                    p_modal.ReceptionDateFrom = DateTime.Today.AddYears(-100);
                    p_modal.ReceptionDateTo = DateTime.MaxValue;
                }

                string BranchName = this.ListLocation().Where(w => w.Value == p_modal.Location).FirstOrDefault().Text;
                ReportCourtCases objDAL = new ReportCourtCases();

                var RetResult = objDAL.getPVIntraTransReport(p_modal);

                if (p_modal.ReportFormat == "EXCEL")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    FileExt = ".xlsx";
                }
                else if (p_modal.ReportFormat == "PDF")
                {
                    MIMETYPE = "application/pdf";
                    FileExt = ".pdf";
                }
                else if (p_modal.ReportFormat == "WORD")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    FileExt = ".docx";
                }

                MemoryStream ResultStream = new MemoryStream();
                string ReportHeading = string.Format(@"{0}{1}NON-REFUNDABLE PV REPORT", BranchName, Environment.NewLine);

                string ExcelResult = objDAL.GenerateExcelStream("", ReportHeading, RetResult, ref ResultStream);

                byte[] fileContents = null;
                using (ResultStream)
                {
                    fileContents = ResultStream.ToArray();
                }

                return File(fileContents, MIMETYPE, "NonRefundablePVReport.xlsx");
            }

            ViewBag.Location = this.ListLocation();

            return View();
        }

        public ActionResult EmployeeInformation()
        {
            ViewBag.Location = this.ListLocation();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeInformation(RepCaseParameterForm p_modal)
        {
            if (ModelState.IsValid)
            {
                string MIMETYPE = string.Empty;
                string FileExt = string.Empty;

                if (p_modal.ClaimAmount == null)
                    p_modal.ClaimAmount = 0;

                if (p_modal.ReceptionDateFrom == null || p_modal.ReceptionDateTo == null)
                {
                    p_modal.ReceptionDateFrom = DateTime.Today.AddYears(-100);
                    p_modal.ReceptionDateTo = DateTime.MaxValue;
                }

                string BranchName = this.ListLocation().Where(w => w.Value == p_modal.Location).FirstOrDefault().Text;
                ReportCourtCases objDAL = new ReportCourtCases();

                var RetResult = objDAL.getPVIntraTransReport(p_modal);

                if (p_modal.ReportFormat == "EXCEL")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    FileExt = ".xlsx";
                }
                else if (p_modal.ReportFormat == "PDF")
                {
                    MIMETYPE = "application/pdf";
                    FileExt = ".pdf";
                }
                else if (p_modal.ReportFormat == "WORD")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    FileExt = ".docx";
                }

                MemoryStream ResultStream = new MemoryStream();
                string ReportHeading = string.Format(@"{0}{1}NON-REFUNDABLE PV REPORT", BranchName, Environment.NewLine);

                string ExcelResult = objDAL.GenerateExcelStream("", ReportHeading, RetResult, ref ResultStream);

                byte[] fileContents = null;
                using (ResultStream)
                {
                    fileContents = ResultStream.ToArray();
                }

                return File(fileContents, MIMETYPE, "NonRefundablePVReport.xlsx");
            }

            ViewBag.Location = this.ListLocation();

            return View();
        }

        public ActionResult FinancialDetails()
        {
            ViewBag.Location = this.ListLocation();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinancialDetails(RepCaseParameterForm p_modal)
        {
            if (ModelState.IsValid)
            {
                string MIMETYPE = string.Empty;
                string FileExt = string.Empty;

                if (p_modal.ClaimAmount == null)
                    p_modal.ClaimAmount = 0;

                if (p_modal.ReceptionDateFrom == null || p_modal.ReceptionDateTo == null)
                {
                    p_modal.ReceptionDateFrom = DateTime.Today.AddYears(-100);
                    p_modal.ReceptionDateTo = DateTime.MaxValue;
                }

                string BranchName = this.ListLocation().Where(w => w.Value == p_modal.Location).FirstOrDefault().Text;
                ReportCourtCases objDAL = new ReportCourtCases();

                var RetResult = objDAL.getPVIntraTransReport(p_modal);

                if (p_modal.ReportFormat == "EXCEL")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    FileExt = ".xlsx";
                }
                else if (p_modal.ReportFormat == "PDF")
                {
                    MIMETYPE = "application/pdf";
                    FileExt = ".pdf";
                }
                else if (p_modal.ReportFormat == "WORD")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    FileExt = ".docx";
                }

                MemoryStream ResultStream = new MemoryStream();
                string ReportHeading = string.Format(@"{0}{1}NON-REFUNDABLE PV REPORT", BranchName, Environment.NewLine);

                string ExcelResult = objDAL.GenerateExcelStream("", ReportHeading, RetResult, ref ResultStream);

                byte[] fileContents = null;
                using (ResultStream)
                {
                    fileContents = ResultStream.ToArray();
                }

                return File(fileContents, MIMETYPE, "NonRefundablePVReport.xlsx");
            }

            ViewBag.Location = this.ListLocation();

            return View();
        }

        public ActionResult FinancialReport()
        {
            ViewBag.Location = this.ListLocation();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinancialReport(RepCaseParameterForm p_modal)
        {
            if (ModelState.IsValid)
            {
                string MIMETYPE = string.Empty;
                string FileExt = string.Empty;

                if (p_modal.ClaimAmount == null)
                    p_modal.ClaimAmount = 0;

                if (p_modal.ReceptionDateFrom == null || p_modal.ReceptionDateTo == null)
                {
                    p_modal.ReceptionDateFrom = DateTime.Today.AddYears(-100);
                    p_modal.ReceptionDateTo = DateTime.MaxValue;
                }

                string BranchName = this.ListLocation().Where(w => w.Value == p_modal.Location).FirstOrDefault().Text;
                ReportCourtCases objDAL = new ReportCourtCases();

                var RetResult = objDAL.getPVIntraTransReport(p_modal);

                if (p_modal.ReportFormat == "EXCEL")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    FileExt = ".xlsx";
                }
                else if (p_modal.ReportFormat == "PDF")
                {
                    MIMETYPE = "application/pdf";
                    FileExt = ".pdf";
                }
                else if (p_modal.ReportFormat == "WORD")
                {
                    MIMETYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    FileExt = ".docx";
                }

                MemoryStream ResultStream = new MemoryStream();
                string ReportHeading = string.Format(@"{0}{1}NON-REFUNDABLE PV REPORT", BranchName, Environment.NewLine);

                string ExcelResult = objDAL.GenerateExcelStream("", ReportHeading, RetResult, ref ResultStream);

                byte[] fileContents = null;
                using (ResultStream)
                {
                    fileContents = ResultStream.ToArray();
                }

                return File(fileContents, MIMETYPE, "NonRefundablePVReport.xlsx");
            }

            ViewBag.Location = this.ListLocation();

            return View();
        }

        public List<SelectListItem> ListLocation()
        {
            var _retVal = new List<SelectListItem>();
            try
            {
                _retVal.Add(new SelectListItem { Text = "ALL", Value = "A" });
                _retVal.Add(new SelectListItem { Text = "Muscat", Value = "M" });
                _retVal.Add(new SelectListItem { Text = "Salalah", Value = "S" });
            }
            catch { }
            return _retVal;
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
    }
}