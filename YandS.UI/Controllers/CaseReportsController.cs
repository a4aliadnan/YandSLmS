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
    public class CaseReportsController : Controller
    {
        private RBACDbContext db = new RBACDbContext();
        private string OfficeFileFilterENF = Helper.getOfficeFileFilterENF();

        public ActionResult CourtCasesDetail()
        {
            var modal = new RepCaseParameterForm();
            ViewBag.ViewToLoad = "_beforeCourt";
            return View(modal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CourtCasesDetail(RepCaseParameterForm p_modal)
        {
            string[] StatusCodes = new[] { "1", "2" };
            string PartialViewName = "";
            if (ModelState.IsValid)
            {
                
                try
                {
                    string MIMETYPE = string.Empty;
                    string FileExt = string.Empty;

                    if (p_modal.ClaimAmountFrom == null)
                        p_modal.ClaimAmountFrom = 0;

                    if (p_modal.ClaimAmountTo == null)
                        p_modal.ClaimAmountTo = 9999999999999;

                    if (p_modal.ReceptionDateFrom == null || p_modal.ReceptionDateTo == null)
                    {
                        p_modal.ReceptionDateFrom = DateTime.ParseExact("01-01-1800", "dd-MM-yyyy", null);
                        p_modal.ReceptionDateTo = DateTime.ParseExact("01-01-9999", "dd-MM-yyyy", null);
                    }


                    if (p_modal.RegistrationDateFrom == null || p_modal.RegistrationDateTo == null)
                    {
                        p_modal.RegistrationDateFrom = DateTime.ParseExact("01-01-1800", "dd-MM-yyyy", null);
                        p_modal.RegistrationDateTo = DateTime.ParseExact("01-01-9999", "dd-MM-yyyy", null);
                    }

                    if (p_modal.JudgmentDateFrom == null || p_modal.JudgmentDateTo == null)
                    {
                        p_modal.JudgmentDateFrom = DateTime.ParseExact("01-01-1800", "dd-MM-yyyy", null);
                        p_modal.JudgmentDateTo = DateTime.ParseExact("01-01-9999", "dd-MM-yyyy", null);
                    }


                    ReportCourtCases objDAL = new ReportCourtCases();

                    var RetResult = objDAL.getCourtCaseDetail(p_modal);

                    if (RetResult.Tables[0].Rows.Count > 0)
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

                        string TemplateName = string.Empty;
                        string FileName = string.Empty;
                        string ExcelResult = string.Empty;
                        string InvoiceREFNO = string.Empty;

                        if (p_modal.ClickedButtonName.In("btnNBORep", "btnBDRep", "btnUFRep", "btnODBShRep", "btnOMASCOShRep"))
                        {
                            if (p_modal.ClickedButtonName == "btnBDRep")
                            {
                                TemplateName = "BD_REPORT.xlsx";
                                FileName = "BD_REPORT";
                            }
                            else if (p_modal.ClickedButtonName == "btnNBORep")
                            {
                                TemplateName = "NBO_REPORT.xlsx";
                                FileName = "NBO_REPORT";
                            }
                            else if (p_modal.ClickedButtonName == "btnUFRep")
                            {
                                TemplateName = "UF_REPORT.xlsx";
                                FileName = "UF_REPORT";
                            }
                            else if (p_modal.ClickedButtonName == "btnODBShRep")
                            {
                                TemplateName = "ODB_REPORT.xlsx";
                                FileName = "ODB_REPORT";
                            }
                            else if (p_modal.ClickedButtonName == "btnOMASCOShRep")
                            {
                                TemplateName = "OMASCO_REPORT.xlsx";
                                FileName = "OMASCO_REPORT";
                            }
                            TemplateName = Path.Combine(Helper.GetTemplateRoot, TemplateName);

                            using (FileStream file = new FileStream(TemplateName, FileMode.Open, FileAccess.Read))
                                file.CopyTo(ResultStream);

                            ExcelResult = objDAL.GenerateExcelStream(FileName, "Court Case Detail Report", RetResult, ref ResultStream);

                        }
                        else
                        {
                            if (p_modal.PartialViewName == "_beforeCourt")
                            {
                                if (p_modal.ClickedButtonName == "btnENRep")
                                {
                                    TemplateName = "ShortDataEN.xlsx";
                                    TemplateName = Path.Combine(Helper.GetTemplateRoot, TemplateName);

                                    using (FileStream file = new FileStream(TemplateName, FileMode.Open, FileAccess.Read))
                                        file.CopyTo(ResultStream);

                                    ExcelResult = objDAL.GenerateExcelStream("ShortDataEN", "Court Case Detail Report", RetResult, ref ResultStream);

                                }
                                else if(p_modal.ClickedButtonName == "btnARRep")
                                {
                                    TemplateName = "ShortDataAR.xlsx";
                                    TemplateName = Path.Combine(Helper.GetTemplateRoot, TemplateName);

                                    using (FileStream file = new FileStream(TemplateName, FileMode.Open, FileAccess.Read))
                                        file.CopyTo(ResultStream);

                                    ExcelResult = objDAL.GenerateExcelStream("ShortDataAR", "Court Case Detail Report", RetResult, ref ResultStream);

                                }
                            }
                            else
                                ExcelResult = objDAL.GenerateExcelStream("", "Court Case Detail Report", RetResult, ref ResultStream);

                        }

                        byte[] fileContents = null;
                        using (ResultStream)
                        {
                            fileContents = ResultStream.ToArray();
                        }

                        return File(fileContents, MIMETYPE, "ExcelReport.xlsx");

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
                catch (Exception ex)
                {
                    Session["Message"] = new MessageVM
                    {
                        Category = "Error",
                        Message = ex.Message
                    };

                    PartialViewName = p_modal.PartialViewName;
                    StatusCodes = new[] { "1", "2" };

                    if (PartialViewName == "_beforeCourt")
                    {
                        ViewBag.Location = this.ListLocation();
                        ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                        ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
                        //ViewBag.EnforcementlevelId = new SelectList(Helper.GetCurEnfcLevel(), "Mst_Value", "Mst_Desc");
                        ViewBag.EnforcementlevelId = new SelectList(Helper.GetOfficeFileStatus(), "Mst_Value", "Mst_Desc");
                        ViewBag.CourtLocationid = new SelectList(Helper.GetCourtLocationList("1"), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc", "1");
                        ViewBag.ClientCaseType = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ClientCaseType).OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc");
                        ViewBag.ODBBankBranch = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ODBBankBranch), "Mst_Value", "Mst_Desc");
                        ViewBag.LoanManager = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.LoanManager), "Mst_Value", "Mst_Desc");

                    }
                    else if (PartialViewName == "_litigation")
                    {
                        StatusCodes = new[] { "1" };
                        ViewBag.Location = this.ListLocation();
                        ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                        ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc", "1");
                        ViewBag.ParentCourtId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ParentCourt), "Mst_Value", "Mst_Desc");


                    }
                    else if (PartialViewName == "_enforcement")
                    {
                        ViewBag.Location = this.ListLocation();
                        ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                        ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("6"), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc");

                        ViewBag.ApealByWho = new SelectList(Helper.GetByWho(), "Mst_Value", "Mst_Desc");
                        ViewBag.GovernorateId = new SelectList(Helper.GetGovernorate(), "Mst_Value", "Mst_Desc");
                        ViewBag.AgainstInsurance = new SelectList(Helper.GetYesNoForSelect(), "Mst_Value", "Mst_Desc");

                        ViewBag.ODBBankBranch = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ODBBankBranch), "Mst_Value", "Mst_Desc");
                        ViewBag.ClientCaseType = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ClientCaseType).OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc", "0"); // Blank
                        ViewBag.OmaniExp = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.OmaniExp), "Mst_Value", "Mst_Desc");

                        //ViewBag.EnforcementlevelId = new SelectList(Helper.GetCurEnfcLevel(), "Mst_Value", "Mst_Desc");
                        ViewBag.EnforcementlevelId = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterENF), "Mst_Value", "Mst_Desc");
                        ViewBag.ReOpenEnforcement = new SelectList(Helper.GetYesNoForSelect(), "Mst_Value", "Mst_Desc");
                        ViewBag.CourtLocationid = new SelectList(Helper.GetCourtLocationList("1"), "Mst_Value", "Mst_Desc");
                    }
                    else if (PartialViewName == "_consultants")
                    {
                        ViewBag.Location = this.ListLocation();
                        ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                        ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc");
                        ViewBag.ParentCourtId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ParentCourt), "Mst_Value", "Mst_Desc");


                    }
                    else if (PartialViewName == "_arabicReport")
                    {
                        ViewBag.Location = this.ListLocation();
                        ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                        ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc");
                        ViewBag.ParentCourtId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ParentCourt), "Mst_Value", "Mst_Desc");


                    }
                    else if (PartialViewName == "_closed")
                    {
                        StatusCodes = new[] { "2" };
                        ViewBag.Location = this.ListLocation();
                        ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                        ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
                        ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc", "2");
                        ViewBag.ParentCourtId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ParentCourt), "Mst_Value", "Mst_Desc");
                        ViewBag.ReasonCode = new SelectList(Helper.GetCaseClosingReasons(), "Mst_Value", "Mst_Desc");


                    }

                    ViewBag.ViewToLoad = p_modal.PartialViewName;
                    return View();
                }

            }

            PartialViewName = p_modal.PartialViewName;
            StatusCodes = new[] { "1", "2" };

            if (PartialViewName == "_beforeCourt")
            {
                ViewBag.Location = this.ListLocation();
                ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
                //ViewBag.EnforcementlevelId = new SelectList(Helper.GetCurEnfcLevel(), "Mst_Value", "Mst_Desc");
                ViewBag.EnforcementlevelId = new SelectList(Helper.GetOfficeFileStatus(), "Mst_Value", "Mst_Desc");
                ViewBag.CourtLocationid = new SelectList(Helper.GetCourtLocationList("1"), "Mst_Value", "Mst_Desc");
                ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc", "1");
                ViewBag.ClientCaseType = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ClientCaseType).OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc");
                ViewBag.ODBBankBranch = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ODBBankBranch), "Mst_Value", "Mst_Desc");
                ViewBag.LoanManager = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.LoanManager), "Mst_Value", "Mst_Desc");
            }
            else if (PartialViewName == "_litigation")
            {
                StatusCodes = new[] { "1" };
                ViewBag.Location = this.ListLocation();
                ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
                ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc", "1");
                ViewBag.ParentCourtId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ParentCourt), "Mst_Value", "Mst_Desc");


            }
            else if (PartialViewName == "_enforcement")
            {
                ViewBag.Location = this.ListLocation();
                ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("6"), "Mst_Value", "Mst_Desc");
                ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc");

                ViewBag.ApealByWho = new SelectList(Helper.GetByWho(), "Mst_Value", "Mst_Desc");
                ViewBag.GovernorateId = new SelectList(Helper.GetGovernorate(), "Mst_Value", "Mst_Desc");
                ViewBag.AgainstInsurance = new SelectList(Helper.GetYesNoForSelect(), "Mst_Value", "Mst_Desc");

                ViewBag.ODBBankBranch = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ODBBankBranch), "Mst_Value", "Mst_Desc");
                ViewBag.ClientCaseType = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ClientCaseType).OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc", "0"); // Blank
                ViewBag.OmaniExp = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.OmaniExp), "Mst_Value", "Mst_Desc");

                //ViewBag.EnforcementlevelId = new SelectList(Helper.GetCurEnfcLevel(), "Mst_Value", "Mst_Desc");
                ViewBag.EnforcementlevelId = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterENF), "Mst_Value", "Mst_Desc");
                ViewBag.ReOpenEnforcement = new SelectList(Helper.GetYesNoForSelect(), "Mst_Value", "Mst_Desc");
                ViewBag.CourtLocationid = new SelectList(Helper.GetCourtLocationList("1"), "Mst_Value", "Mst_Desc");
            }
            else if (PartialViewName == "_consultants")
            {
                ViewBag.Location = this.ListLocation();
                ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
                ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc");
                ViewBag.ParentCourtId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ParentCourt), "Mst_Value", "Mst_Desc");


            }
            else if (PartialViewName == "_arabicReport")
            {
                ViewBag.Location = this.ListLocation();
                ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
                ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc");
                ViewBag.ParentCourtId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ParentCourt), "Mst_Value", "Mst_Desc");


            }
            else if (PartialViewName == "_closed")
            {
                StatusCodes = new[] { "2" };
                ViewBag.Location = this.ListLocation();
                ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
                ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc", "2");
                ViewBag.ParentCourtId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ParentCourt), "Mst_Value", "Mst_Desc");
                ViewBag.ReasonCode = new SelectList(Helper.GetCaseClosingReasons(), "Mst_Value", "Mst_Desc");


            }

            ViewBag.ViewToLoad = p_modal.PartialViewName;
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

        [HttpGet]
        public ActionResult GetPartialView(string PartialViewName)
        {
            string[] StatusCodes = new[] { "1", "2" };
            var modal = new RepCaseParameterForm();

            if (PartialViewName == "_beforeCourt")
            {
                ViewBag.Location = this.ListLocation();
                ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
                //ViewBag.EnforcementlevelId = new SelectList(Helper.GetCurEnfcLevel(), "Mst_Value", "Mst_Desc");
                ViewBag.EnforcementlevelId = new SelectList(Helper.GetOfficeFileStatus(), "Mst_Value", "Mst_Desc");
                ViewBag.CourtLocationid = new SelectList(Helper.GetCourtLocationList("1"), "Mst_Value", "Mst_Desc");
                ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc", "1");
                ViewBag.ClientCaseType = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ClientCaseType).OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc", "0");
                ViewBag.ODBBankBranch = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ODBBankBranch), "Mst_Value", "Mst_Desc", "0");
                ViewBag.LoanManager = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.LoanManager), "Mst_Value", "Mst_Desc", "0");
            }
            else if (PartialViewName == "_litigation")
            {
                StatusCodes = new[] { "1" };
                ViewBag.Location = this.ListLocation();
                ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
                ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc","1");
                ViewBag.ParentCourtId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ParentCourt), "Mst_Value", "Mst_Desc");

                
            }
            else if (PartialViewName == "_enforcement")
            {
                ViewBag.Location = this.ListLocation();
                ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("6"), "Mst_Value", "Mst_Desc");
                ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc");

                ViewBag.ApealByWho = new SelectList(Helper.GetByWho(), "Mst_Value", "Mst_Desc");
                ViewBag.GovernorateId = new SelectList(Helper.GetGovernorate(), "Mst_Value", "Mst_Desc");
                ViewBag.AgainstInsurance = new SelectList(Helper.GetYesNoForSelect(), "Mst_Value", "Mst_Desc");

                ViewBag.ODBBankBranch = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ODBBankBranch), "Mst_Value", "Mst_Desc");
                ViewBag.ClientCaseType = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ClientCaseType).OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc", "0"); // Blank
                ViewBag.OmaniExp = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.OmaniExp), "Mst_Value", "Mst_Desc");

                //ViewBag.EnforcementlevelId = new SelectList(Helper.GetCurEnfcLevel(), "Mst_Value", "Mst_Desc");
                ViewBag.EnforcementlevelId = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterENF), "Mst_Value", "Mst_Desc");
                ViewBag.ReOpenEnforcement = new SelectList(Helper.GetYesNoForSelect(), "Mst_Value", "Mst_Desc");
                ViewBag.CourtLocationid = new SelectList(Helper.GetCourtLocationList("1"), "Mst_Value", "Mst_Desc");
            }
            else if (PartialViewName == "_consultants")
            {
                ViewBag.Location = this.ListLocation();
                ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
                ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc");
                ViewBag.ParentCourtId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ParentCourt), "Mst_Value", "Mst_Desc");

                
            }
            else if (PartialViewName == "_arabicReport")
            {
                ViewBag.Location = this.ListLocation();
                ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
                ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc");
                ViewBag.ParentCourtId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ParentCourt), "Mst_Value", "Mst_Desc");

                
            }
            else if (PartialViewName == "_closed")
            {
                StatusCodes = new[] { "2" };
                ViewBag.Location = this.ListLocation();
                ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                ViewBag.AgainstCode = new SelectList(Helper.GetCaseAgainst(), "Mst_Value", "Mst_Desc");
                ViewBag.CaseTypeCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.CaseType), "Mst_Value", "Mst_Desc");
                ViewBag.CaseLevelCode = new SelectList(Helper.GetCaseLevelList("A"), "Mst_Value", "Mst_Desc");
                ViewBag.CaseStatus = new SelectList(Helper.GetStatusCodeList(false, StatusCodes), "Mst_Value", "Mst_Desc","2");
                ViewBag.ParentCourtId = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ParentCourt), "Mst_Value", "Mst_Desc");
                ViewBag.ReasonCode = new SelectList(Helper.GetCaseClosingReasons(), "Mst_Value", "Mst_Desc");


            }
            else
                return PartialView(PartialViewName);

            return PartialView(PartialViewName, modal);
        }
    }
}