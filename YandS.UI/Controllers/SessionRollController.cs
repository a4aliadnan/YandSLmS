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
using YandS.DAL;
using YandS.UI.Models;

namespace YandS.UI.Controllers
{
    [RBAC]
    public class SessionRollController : Controller
    {
        private RBACDbContext db = new RBACDbContext();
        private SessionsRoll ExistingModelData = new SessionsRoll();
        private string PartialViewName = "";
        private string OfficeFileFilterTBR = Helper.getOfficeFileFilterTBR();
        private string OfficeFileFilterSR = Helper.getOfficeFileFilterSR();
        private string OfficeFileFilterENF = Helper.getOfficeFileFilterENF();
        private string OfficeFileFilterSUP  = Helper.getOfficeFileFilterSUP();
        private string OfficeFileFilterAJP = Helper.getOfficeFileFilterAJP();
        private string[] FileStatusCodesSR = Helper.getFileStatusCodesSR();
        private string[] FileStatusCodesTBR = Helper.getFileStatusCodesTBR(); 

        public ActionResult Index(int? id,string ViewToLoad = null)
        {
            if (User.IsInRole("VoucherApproval") || User.IsSysAdmin())
            {
                ViewBag.LocationId = "M";
                ViewBag.UserRole = "VoucherApproval";
                ViewBag.VoucherApproval = "checked";
            }
            else
                ViewBag.LocationId = Helper.GetEmployeeLocation(User.Identity.Name).Split('-')[1];

            ViewBag.LawyerUser = "N";

            if (!string.IsNullOrEmpty(Helper.GetLawyerId(User.Identity.Name)))
                ViewBag.LawyerUser = "Y";

            ViewBag.OfficeFileNo = "";
            ViewBag.IsEdit = "";

            ViewBag.SessionRollRegisterActive = "";
            ViewBag.AllSessionsActive = "";
            ViewBag.SessionPrintActive = "";
            ViewBag.SessionFollowActive = "";
            ViewBag.SessionTobeUpdatedActive = "";
            ViewBag.SessionSuspendedActive = "";
            ViewBag.SessionJudgementActive = "";
            ViewBag.SessionDiffPanelActive = "";
            ViewBag.SessionOnHoldActive = "";
            ViewBag.ViewSessionRollTabs = "";
            ViewBag.ViewFollowUpTabs = "";

            ViewBag.SessionRollId = "0";
            ViewBag.HFCaseId = "0";
            ViewBag.Div_OfficeFileNoMain = "";
            ViewBag.ContainerHeading = "RUNNING CASE";
            ViewBag.Div_OfficeFileNo = "AppHidden";

            if (id == null)
            {
                ViewBag.ViewFollowUpTabs = "AppHidden";
                ViewBag.FormMode = "CREATE";
                ViewBag.ViewContainer = "#PartialViewContainer";

                if (User.Identity.Name.In("10", "51", "54"))
                {
                    ViewBag.SessionSuspendedActive = "SessionSuspendedActive";
                    ViewBag.ViewToLoad = "_SessionSuspended";
                }
                else
                {
                    ViewBag.AllSessionsActive = "AllSessionsActive";
                    ViewBag.ViewToLoad = "_AllSessions";

                }
            }
            else if (id == -1)
            {
                ViewBag.ViewFollowUpTabs = "AppHidden";
                ViewBag.StartTAB = "btn_CaseRegNewCase";
                ViewBag.CaseRegNewCaseActive = "CaseRegNewCaseActive";
                ViewBag.LoadTable = "tableNewCases";
            }
            else if (id == -2)
            {
                ViewBag.ViewSessionRollTabs = "AppHidden";
                ViewBag.FormMode = "CREATE";
                ViewBag.ViewContainer = "#PartialViewContainer";
                ViewBag.SessionJudgementFollowupActive = "SessionJudgementFollowupActive";
                ViewBag.ViewToLoad = "_SessionJudgementFollowup";
                ViewBag.ContainerHeading = "AFTER JUDGMENT PROCEEDINGS";
            }
            else if (id == -3)
            {
                ViewBag.ViewSessionRollTabs = "AppHidden";
                ViewBag.FormMode = "CREATE";
                ViewBag.ViewContainer = "#PartialViewContainer";

                if (ViewToLoad == OfficeFileStatus.RedStamp.ToString())
                {
                    ViewBag.SessionRedStampActive = "SessionRedStampActive";
                    ViewBag.ViewToLoad = "_SessionRedStamp";
                }

                if (ViewToLoad == OfficeFileStatus.PeriodOfAppeal.ToString())
                {
                    ViewBag.SessionPeriodOfAppealActive = "SessionPeriodOfAppealActive";
                    ViewBag.ViewToLoad = "_SessionPeriodOfAppeal";
                }

                ViewBag.ContainerHeading = "AFTER JUDGMENT PROCEEDINGS";
            }
            else
            {
                ViewBag.ViewFollowUpTabs = "AppHidden";
                ViewBag.ViewContainer = "#PartialViewContainer";
                ViewBag.AllSessionsActive = "AllSessionsActive";
                ViewBag.ViewToLoad = "_AllSessions";
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SessionsRollVM modal, HttpPostedFileBase upload, HttpPostedFileBase uploadAddress, HttpPostedFileBase uploadPVSupDocs)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string UploadRoot = Helper.GetStorageRoot;
            var skipIDs = "1,2,3,13,25";

            MessageVM ProcessMessage = new MessageVM { Category = "Success", Message = "Data Save Successfully" };

            try
            {
                #region VALID MODAL PROCESSING

                if (ModelState.IsValid)
                {
                    SessionsRoll ModelToSave = new SessionsRoll();

                    if (modal.SessionRollId > 0) //EDIT
                    {
                        ModelToSave = db.SessionsRoll.Find(modal.SessionRollId);
                        UpdateModel(ref modal, ref ModelToSave);

                        if (upload != null && upload.ContentLength > 0)
                        {
                            string FileExtension = Path.GetExtension(upload.FileName);

                            string FileName = ModelToSave.CaseId + FileExtension;

                            string UploadPath = Path.Combine(UploadRoot, "DEF_Lawyer_Docs", FileName);

                            upload.SaveAs(UploadPath);
                        }

                        if (uploadAddress != null && uploadAddress.ContentLength > 0)
                        {
                            string FileExtension = Path.GetExtension(uploadAddress.FileName);

                            string FileName = ModelToSave.CaseId + FileExtension;

                            string UploadPath = Path.Combine(UploadRoot, "DEF_Address_Docs", FileName);

                            uploadAddress.SaveAs(UploadPath);
                        }

                        if (uploadPVSupDocs != null && uploadPVSupDocs.ContentLength > 0)
                        {
                            string FileExtension = Path.GetExtension(uploadPVSupDocs.FileName);

                            string FileName = modal.Voucher_No + FileExtension;

                            string UploadPath = Path.Combine(UploadRoot, "PVDocuments", FileName);

                            uploadPVSupDocs.SaveAs(UploadPath);
                        }

                    }
                    else
                    {
                        if (modal.SavePV_Data == "_AfterJDAddNewFile")
                            Helper.ProcessSessionRollDetail(modal, "SessionsRollVM");
                        else
                        {
                            if (modal.PartialViewName == "_SessionPauseEnfReqDIV")
                                UpdateStopEnforcement(modal.CaseId, modal.StopEnfRequest);
                            else if (modal.PartialViewName == "_AfterJudgementFavorable")
                                UpdateAfterJudgementFavorable(modal);
                            else
                                SaveModel(modal, ref ModelToSave);
                        }
                    }

                    if (modal.SessionRollId > 0) //EDIT
                    {
                        if (modal.PartialViewName == "_AfterJudgementEditForm")
                            PartialViewName = "_AfterJudgementEditForm";
                        else if (modal.PartialViewName == "_AfterJudgementReceived")
                            PartialViewName = "_AfterJudgementReceived";
                        else if (modal.PartialViewName == "_AfterJudgementFavorable")
                            PartialViewName = "_AfterJudgementFavorable";
                        else
                            PartialViewName = "_AllSessionsEditForm";

                    }
                    else
                    {
                        if (modal.PartialViewName == "_SessionPauseEnfReqDIV")
                            PartialViewName = "_SessionPauseEnfReqDIV";
                        else if (modal.PartialViewName == "_AfterJudgementFavorable")
                            PartialViewName = "_AfterJudgementFavorable";
                        else
                            PartialViewName = "_SessionRollRegister";
                    }

                    mapSessionRollVM(modal.CaseId, modal.SessionRollId, ref modal);

                    ViewBag.CaseType = new SelectList(Helper.GetSessionCaseType(), "Mst_Value", "Mst_Desc", modal.CaseType);
                    ViewBag.LawyerId = new SelectList(Helper.LoadDependentDDL("1901"), "Mst_Value", "Mst_Desc", modal.LawyerId);
                    ViewBag.SessionFileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterSR, modal.CaseLevelCode == "6" ? null : FileStatusCodesSR), "Mst_Value", "Mst_Desc", modal.SessionFileStatus);


                    ViewBag.FollowerId = new SelectList(Helper.GetSessionFollowers(), "Mst_Value", "Mst_Desc", modal.FollowerId);
                    ViewBag.SuspendedFollowerId = new SelectList(Helper.GetSessionFollowers(), "Mst_Value", "Mst_Desc", modal.SuspendedFollowerId);

                    ViewBag.CourtFollow = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.CourtFollow);
                    ViewBag.CourtFollow_LawyerId = new SelectList(Helper.GetSessionLawyers(), "Mst_Value", "Mst_Desc", modal.CourtFollow_LawyerId);
                    ViewBag.MoneyWith = new SelectList(Helper.GetMoneyWith(), "Mst_Value", "Mst_Desc");
                    #region ADDRESS

                    ViewBag.DEF_CallerName = new SelectList(Helper.GetCallerNames(), "Mst_Value", "Mst_Desc", modal.DEF_CallerName);
                    ViewBag.AnnouncementTypeId = new SelectList(Helper.GetAnnouncementType(), "Mst_Value", "Mst_Desc", modal.AnnouncementTypeId);

                    string LawyerDoc = Helper.GetDEF_Lawyer_Doc(modal.CaseId);
                    string AddressDoc = Helper.GetDEF_Address_Doc(modal.CaseId);

                    if (LawyerDoc == "#")
                    {
                        ViewBag.ViewDEF_LawyerDocs = "AppHidden";
                    }
                    else
                    {
                        ViewBag.ViewDEF_LawyerDocs = "";
                        ViewBag.DEF_Lawyer_Docs = LawyerDoc;
                    }

                    if (AddressDoc == "#")
                    {
                        ViewBag.ViewDEF_AddressDocs = "AppHidden";
                    }
                    else
                    {
                        ViewBag.ViewDEF_AddressDocs = "";
                        ViewBag.DEF_Address_Docs = AddressDoc;
                    }

                    #endregion

                    #region Pay Voucher

                    
                    ViewBag.CourtType = new SelectList(Helper.GetCaseLevelList("D"), "Mst_Value", "Mst_Desc", modal.CaseLevelCode);
                    ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
                    ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
                    ViewBag.Payment_Head = new SelectList(Helper.LoadPayForRefunableOnly(), "Mst_Value", "Mst_Desc", "10007");
                    ViewBag.PaymentHeadDetail = new SelectList(Helper.LoadDependentDDL("10007"), "Mst_Value", "Mst_Desc");
                    ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(), "Mst_Value", "Mst_Desc", "1901");
                    ViewBag.PaymentToBenificry = new SelectList(Helper.LoadDependentDDL("1901", skipIDs), "Mst_Value", "Mst_Desc", User.Identity.Name);
                    ViewBag.PayToBankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc");
                    ViewBag.PayToMessageLang = new SelectList(Helper.GetLangForSelect(), "Mst_Value", "Mst_Desc");
                    ViewBag.UpdatedOn = modal.UpdatedOn;
                    #endregion

                    if (PartialViewName == "_SessionRollRegister")
                        return Json(new { redirectTo = "#btn_AllSessions" });
                    else if (PartialViewName == "_SessionPauseEnfReqDIV")
                        return Json(new { message = "OK" });
                    else if (PartialViewName == "_AfterJudgementEditForm")
                    {
                        modal.PartialViewName = PartialViewName;

                        ViewBag.SessionRollId = modal.SessionRollId;
                        ViewBag.HFCaseId = modal.CaseId;
                        ViewBag.FrmMode = "E";

                        ViewBag.SessionFileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterAJP), "Mst_Value", "Mst_Desc", modal.SessionFileStatus);
                        ViewBag.PrimaryIsFavorable = new SelectList(Helper.GetYNForSelectAR(), "Mst_Value", "Mst_Desc", modal.PrimaryIsFavorable);
                        ViewBag.AppealIsFavorable = new SelectList(Helper.GetYNForSelectAR(), "Mst_Value", "Mst_Desc", modal.AppealIsFavorable);
                        ViewBag.EnforcementIsFavorable = new SelectList(Helper.GetYNForSelectAR(), "Mst_Value", "Mst_Desc", modal.EnforcementIsFavorable);

                        return PartialView(PartialViewName, modal);

                    }
                    else if (PartialViewName == "_AfterJudgementReceived")
                    {
                        modal.PartialViewName = PartialViewName;

                        ViewBag.SessionRollId = modal.SessionRollId;
                        ViewBag.HFCaseId = modal.CaseId;
                        ViewBag.FrmMode = "E";

                        modal.PartialViewName = PartialViewName;

                        ViewBag.JudgementLevel = new SelectList(Helper.GetJudgementLevel(), "Mst_Value", "Mst_Desc", modal.JudgementLevel);

                        ViewBag.ClientReply = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.ClientReply);
                        ViewBag.TransportationSource = new SelectList(Helper.GetTransSourceSelect(), "Mst_Value", "Mst_Desc", modal.TransportationSource);

                        ViewBag.HFCurrentHearingDate = modal.CurrentHearingDate?.ToString("dd/MM/yyyy");
                        ViewBag.HFCourtDecision = modal.CourtDecision;
                        var cases = db.CourtCase.Find(modal.CaseId);
                        string JudFilter = "Y";

                        if (cases != null)
                        {
                            if ((cases.ClientClassificationCode == "1" || cases.ClientClassificationCode == "2") && cases.CaseTypeCode == "1" && (cases.AgainstCode == "1" || cases.AgainstCode == "2"))
                                JudFilter = "N";
                        }

                        ViewBag.JudFilter = JudFilter;

                        return PartialView(PartialViewName, modal);

                    }
                    else if (PartialViewName == "_AfterJudgementFavorable")
                    {
                        modal.PartialViewName = PartialViewName;

                        ViewBag.SessionRollId = modal.SessionRollId;
                        ViewBag.HFCaseId = modal.CaseId;
                        ViewBag.FrmMode = "E";

                        modal.PartialViewName = PartialViewName;

                        ViewBag.SessionFileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterAJP), "Mst_Value", "Mst_Desc", modal.SessionFileStatus);
                        ViewBag.EnforcementAdmin = new SelectList(Helper.GetEnfcAdmin(), "Mst_Value", "Mst_Desc", modal.EnforcementAdmin);

                        ViewBag.ClientReply = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.ClientReply);
                        ViewBag.TransportationSource = new SelectList(Helper.GetTransSourceSelect(), "Mst_Value", "Mst_Desc", modal.TransportationSource);
                        ViewBag.CourtFollow = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.CourtFollow);
                        ViewBag.CourtFollow_LawyerId = new SelectList(Helper.GetSessionLawyers(), "Mst_Value", "Mst_Desc", modal.CourtFollow_LawyerId);

                        ViewBag.HFCurrentHearingDate = modal.CurrentHearingDate?.ToString("dd/MM/yyyy");
                        ViewBag.HFCourtDecision = modal.CourtDecision;
                        var cases = db.CourtCase.Find(modal.CaseId);
                        string JudFilter = "Y";

                        if (cases != null)
                        {
                            if ((cases.ClientClassificationCode == "1" || cases.ClientClassificationCode == "2") && cases.CaseTypeCode == "1" && (cases.AgainstCode == "1" || cases.AgainstCode == "2"))
                                JudFilter = "N";
                        }

                        ViewBag.JudFilter = JudFilter;

                        return PartialView(PartialViewName, modal);

                    }
                    else
                    {
                        if (modal.PartialViewName == "_SessionRollSupremeUpdate" || modal.PartialViewName == "_SessionJudgementSupreme" || modal.PartialViewName == "_SessionUpdateSupreme" || modal.PartialViewName == "_SessionUpdate" || modal.PartialViewName == "_SessionClose" || modal.PartialViewName == "_SessionFollowSupreme" || modal.PartialViewName == "_SessionJudgementSupreme_CaseDetail")
                        {
                            ViewBag.PASCourtLocationid = new SelectList(Helper.GetCourtLocationList("3"), "Mst_Value", "Mst_Desc");
                            ViewBag.ApealByWho = new SelectList(Helper.GetByWho(true), "Mst_Value", "Mst_Desc");
                            ViewBag.CourtDepartment = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterSUP), "Mst_Value", "Mst_Desc", modal.CourtDepartment);
                            ViewBag.MoneyWith = new SelectList(Helper.GetMoneyWith(), "Mst_Value", "Mst_Desc");
                            if (modal.PartialViewName == "_SessionJudgementSupreme_CaseDetail")
                                return PartialView("_CaseDetail_Modify", modal);
                            else
                                return PartialView("_SessionRollSupremeUpdate", modal);
                        }
                        else
                            return PartialView(PartialViewName, modal);

                    }
                }

                #endregion

                #region INVALID MODAL RETURN
                ViewBag.SessionLevel = new SelectList(Helper.GetSessionLevel(), "Mst_Value", "Mst_Desc", modal.SessionLevel);
                ViewBag.CountLocationId = new SelectList(Helper.GetCourtLocationList("1"), "Mst_Value", "Mst_Desc", modal.CountLocationId);
                ViewBag.CaseType = new SelectList(Helper.GetSessionCaseType(), "Mst_Value", "Mst_Desc", modal.CaseType);
                ViewBag.LawyerId = new SelectList(Helper.LoadDependentDDL("1901"), "Mst_Value", "Mst_Desc", modal.LawyerId);
                ViewBag.FollowerId = new SelectList(Helper.GetSessionFollowers(), "Mst_Value", "Mst_Desc", modal.FollowerId);

                //Session["Message"] = new MessageVM
                //{
                //    Category = "Error",
                //    Message = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray())
                //};
                return Json(new { errorMessage = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray()) });

                #endregion
            }
            catch (Exception ex)
            {
                return Json(new { errorMessage = ex.Message });
            }

        }

        #region Private Methods
        private void mapSessionRollVM(int CaseId, int SessionRollId, ref SessionsRollVM ModalToReturn)
        {
            string currentCaseLevelName = "";
            string AccountContractNo = "";
            string ClientFileNo = "";
            string OfficeFileNo = "";
            string OfficeFileStatus = "";
            string ClientName = "";
            string Defendant = "";
            string SessionClientId = "";
            string SessionRollClientName = "";
            string SessionRollDefendentName = "";
            DateTime? CurrentHearingDate = (DateTime?)null;
            string CourtDecision = "";
            string SessionRemarks = "";
            DateTime? NextHearingDate = (DateTime?)null;
            string Requirements = "";

            ModalToReturn.CaseId = CaseId;
            ModalToReturn.SessionRollId = SessionRollId;

            var courtCases = db.CourtCase.Find(CaseId);

            if (courtCases != null)
            {
                AccountContractNo = courtCases.AccountContractNo;
                ClientFileNo = courtCases.ClientFileNo;
                OfficeFileNo = courtCases.OfficeFileNo;
                OfficeFileStatus = courtCases.OfficeFileStatus;

                if (!string.IsNullOrEmpty(courtCases.ClientCode) && courtCases.ClientCode != "0")
                    ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == courtCases.ClientCode).FirstOrDefault().Mst_Desc;

                Defendant = courtCases.Defendant;

                if (!string.IsNullOrEmpty(courtCases.CaseLevelCode) && courtCases.CaseLevelCode != "0")
                    currentCaseLevelName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.CaseLevel && w.Mst_Value == courtCases.CaseLevelCode).FirstOrDefault().Mst_Desc;

                CurrentHearingDate = courtCases.CurrentHearingDate;
                CourtDecision = courtCases.CourtDecision;
                SessionRemarks = courtCases.SessionRemarks;
                NextHearingDate = courtCases.NextHearingDate;
                Requirements = courtCases.Requirements;
                SessionClientId = courtCases.SessionClientId;
                SessionRollDefendentName = courtCases.SessionRollDefendentName;

                if (!string.IsNullOrEmpty(courtCases.SessionClientId) && courtCases.SessionClientId != "0")
                    SessionRollClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.SessionClients && w.Mst_Value == courtCases.SessionClientId).FirstOrDefault().Mst_Desc;

                ModalToReturn.AccountContractNo = AccountContractNo;
                ModalToReturn.ClientFileNo = ClientFileNo;
                ModalToReturn.OfficeFileNo = OfficeFileNo;
                ModalToReturn.OfficeFileNoSessionRollPV = OfficeFileNo;
                ModalToReturn.ClientName = ClientName;
                ModalToReturn.Defendant = Defendant;
                ModalToReturn.CurrentCaseLevel = currentCaseLevelName;
                ModalToReturn.CurrentHearingDate = CurrentHearingDate;
                ModalToReturn.CourtDecision = CourtDecision;
                ModalToReturn.SessionRemarks = SessionRemarks;
                ModalToReturn.NextHearingDate = NextHearingDate;
                ModalToReturn.Requirements = Requirements;
                ModalToReturn.SessionClientId = SessionClientId;
                ModalToReturn.SessionRollDefendentName = SessionRollDefendentName;
                ModalToReturn.SessionRollClientName = SessionRollClientName;

                ModalToReturn.CaseLevelCode = courtCases.CaseLevelCode;
                ModalToReturn.AgainstCode = courtCases.AgainstCode;
                ModalToReturn.ClientReply = courtCases.ClientReply;
                ModalToReturn.TransportationSource = courtCases.TransportationSource;
                ModalToReturn.TransportationFee = courtCases.TransportationFee;
                ModalToReturn.FirstEmailDate = courtCases.FirstEmailDate;
                ModalToReturn.CourtFollow = courtCases.CourtFollow;
                ModalToReturn.CourtFollowRequirement = courtCases.CourtFollowRequirement;
                ModalToReturn.CommissioningDate = courtCases.CommissioningDate;
                ModalToReturn.StopEnfRequest = courtCases.StopEnfRequest;
                ModalToReturn.ClaimSummary = courtCases.ClaimSummary;
                ModalToReturn.UpdatedOn = courtCases.UpdatedOn?.ToString("dd/MM/yyyy HH:mm:ss") ?? courtCases.CreatedOn.ToString("dd/MM/yyyy HH:mm:ss");
                ModalToReturn.SessionFileStatus = OfficeFileStatus;

                CourtCasesEnforcement courtEnforcementDetail = new CourtCasesEnforcement();
                CourtCasesDetail courtCaseDetail = new CourtCasesDetail();

                if (int.Parse(ModalToReturn.CaseLevelCode) >= 3 && int.Parse(ModalToReturn.CaseLevelCode) <= 5)
                {
                    courtCaseDetail = db.CourtCasesDetail.Where(w => w.CaseId == CaseId && w.CaseLevelCode == courtCases.CaseLevelCode).FirstOrDefault();

                    if (courtCaseDetail != null)
                    {
                        ModalToReturn.CourtRefNo = courtCaseDetail.CourtRefNo;

                        if (!string.IsNullOrEmpty(courtCaseDetail.CourtLocationid))
                            ModalToReturn.CountLocationName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == courtCaseDetail.CourtLocationid).FirstOrDefault().Mst_Desc;

                        ModalToReturn.ClosureDate = courtCaseDetail.ClosureDate;
                        ModalToReturn.NextCaseLevel = courtCaseDetail.NextCaseLevel;
                        ModalToReturn.NextCaseLevelCode = courtCaseDetail.NextCaseLevelCode;
                        ModalToReturn.CountLocationId = courtCaseDetail.CourtLocationid;

                    }
                }
                else if (int.Parse(ModalToReturn.CaseLevelCode) == 6)
                {
                    courtEnforcementDetail = db.CourtCasesEnforcement.Where(w => w.CaseId == CaseId).FirstOrDefault();

                    if (courtEnforcementDetail != null)
                    {
                        ModalToReturn.CourtRefNo = courtEnforcementDetail.EnforcementNo;

                        if (!string.IsNullOrEmpty(courtEnforcementDetail.CourtLocationid))
                            ModalToReturn.CountLocationName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == courtEnforcementDetail.CourtLocationid).FirstOrDefault().Mst_Desc;

                        ModalToReturn.CountLocationId = courtEnforcementDetail.CourtLocationid;
                    }
                }

                courtEnforcementDetail = db.CourtCasesEnforcement.Where(w => w.CaseId == CaseId).FirstOrDefault();

                if (courtEnforcementDetail != null)
                {
                    ModalToReturn.AnnouncementTypeId = courtEnforcementDetail.AnnouncementTypeId;
                    ModalToReturn.DEF_DateOfContact = courtEnforcementDetail.DEF_DateOfContact;
                    ModalToReturn.DEF_MobileNo = courtEnforcementDetail.DEF_MobileNo;
                    ModalToReturn.DEF_Corresponding = courtEnforcementDetail.DEF_Corresponding;
                    ModalToReturn.DEF_CallerName = courtEnforcementDetail.DEF_CallerName;
                    ModalToReturn.DEF_LawyerId = courtEnforcementDetail.DEF_LawyerId;
                    ModalToReturn.DEF_VisitDate = courtEnforcementDetail.DEF_VisitDate;

                }

                if (ModalToReturn.PartialViewName == "_SessionJudgementPrimary")
                {
                    courtCaseDetail = db.CourtCasesDetail.Where(w => w.CaseId == CaseId && w.CaseLevelCode == "3").FirstOrDefault();
                    if (courtCaseDetail != null)
                    {
                        ModalToReturn.CourtRefNo = courtCaseDetail.CourtRefNo;

                        if (!string.IsNullOrEmpty(courtCaseDetail.CourtLocationid))
                            ModalToReturn.CountLocationName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == courtCaseDetail.CourtLocationid).FirstOrDefault().Mst_Desc;

                        ModalToReturn.CountLocationId = courtCaseDetail.CourtLocationid;

                    }
                    else
                    {
                        ModalToReturn.CourtRefNo = "";
                        ModalToReturn.CountLocationId = "";
                        ModalToReturn.CountLocationName = "";
                    }
                }
                else if (ModalToReturn.PartialViewName == "_SessionJudgementAppeal")
                {
                    courtCaseDetail = db.CourtCasesDetail.Where(w => w.CaseId == CaseId && w.CaseLevelCode == "4").FirstOrDefault();
                    if (courtCaseDetail != null)
                    {
                        ModalToReturn.CourtRefNo = courtCaseDetail.CourtRefNo;

                        if (!string.IsNullOrEmpty(courtCaseDetail.CourtLocationid))
                            ModalToReturn.CountLocationName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == courtCaseDetail.CourtLocationid).FirstOrDefault().Mst_Desc;

                        ModalToReturn.CountLocationId = courtCaseDetail.CourtLocationid;

                    }
                    else
                    {
                        ModalToReturn.CourtRefNo = "";
                        ModalToReturn.CountLocationId = "";
                        ModalToReturn.CountLocationName = "";
                    }
                }
                else if (ModalToReturn.PartialViewName == "_SessionJudgementSupremeModal" || ModalToReturn.PartialViewName == "_SessionJudgementSupreme_CaseDetail")
                {
                    courtCaseDetail = db.CourtCasesDetail.Where(w => w.CaseId == CaseId && w.CaseLevelCode == "5").FirstOrDefault();
                    if (courtCaseDetail != null)
                    {
                        ModalToReturn.CourtRefNo = courtCaseDetail.CourtRefNo;

                        if (!string.IsNullOrEmpty(courtCaseDetail.CourtLocationid))
                            ModalToReturn.CountLocationName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == courtCaseDetail.CourtLocationid).FirstOrDefault().Mst_Desc;

                        ModalToReturn.CountLocationId = courtCaseDetail.CourtLocationid;

                    }
                    else
                    {
                        ModalToReturn.CourtRefNo = "";
                        ModalToReturn.CountLocationId = "";
                        ModalToReturn.CountLocationName = "";
                    }
                }
                else if (ModalToReturn.PartialViewName.In("_SessionJudgementEnforcement", "_AfterJudgementFavorable"))
                {
                    courtEnforcementDetail = db.CourtCasesEnforcement.Where(w => w.CaseId == CaseId).OrderByDescending(O => O.EnforcementId).FirstOrDefault();

                    if (courtEnforcementDetail != null)
                    {
                        ModalToReturn.CourtRefNo = courtEnforcementDetail.EnforcementNo;
                        ModalToReturn.DetailId = courtEnforcementDetail.EnforcementId;
                        ModalToReturn.EnforcementAdmin = courtEnforcementDetail.EnforcementAdmin;

                        if (!string.IsNullOrEmpty(courtEnforcementDetail.CourtLocationid))
                            ModalToReturn.CountLocationName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == courtEnforcementDetail.CourtLocationid).FirstOrDefault().Mst_Desc;

                        ModalToReturn.CountLocationId = courtEnforcementDetail.CourtLocationid;

                    }
                    else
                    {
                        ModalToReturn.DetailId = 0;
                        ModalToReturn.CourtRefNo = "";
                        ModalToReturn.CountLocationId = "";
                        ModalToReturn.CountLocationName = "";
                    }
                }

                var modal = db.SessionsRoll.Find(SessionRollId);

                if (modal != null)
                {
                    ModalToReturn.SessionLevel = modal.SessionLevel;
                    ModalToReturn.CountLocationId = modal.CountLocationId;
                    ModalToReturn.CaseType = modal.CaseType;
                    ModalToReturn.LawyerId = modal.LawyerId;
                    ModalToReturn.BeforeDate = modal.BeforeDate;
                    ModalToReturn.FollowerId = modal.FollowerId;
                    ModalToReturn.PrimaryJudgementsDate = modal.PrimaryJudgementsDate;
                    ModalToReturn.PrimaryJudgements = modal.PrimaryJudgements;
                    ModalToReturn.PrimaryIsFavorable = modal.PrimaryIsFavorable;
                    ModalToReturn.PrimaryJDReceiveDate = modal.PrimaryJDReceiveDate;
                    ModalToReturn.AppealJudgementsDate = modal.AppealJudgementsDate;
                    ModalToReturn.AppealJudgements = modal.AppealJudgements;
                    ModalToReturn.AppealIsFavorable = modal.AppealIsFavorable;
                    ModalToReturn.AppealJDReceiveDate = modal.AppealJDReceiveDate;
                    ModalToReturn.EnforcementJudgementsDate = modal.EnforcementJudgementsDate;
                    ModalToReturn.EnforcementJudgements = modal.EnforcementJudgements;
                    ModalToReturn.EnforcementIsFavorable = modal.EnforcementIsFavorable;
                    ModalToReturn.EnforcementJDReceiveDate = modal.EnforcementJDReceiveDate;
                    ModalToReturn.LastDate = modal.LastDate;
                    ModalToReturn.WorkRequired = modal.WorkRequired;
                    ModalToReturn.SessionNotes = modal.SessionNotes;
                    ModalToReturn.SuspendedWorkRequired = modal.SuspendedWorkRequired;
                    ModalToReturn.SuspendedSessionNotes = modal.SuspendedSessionNotes;
                    ModalToReturn.SuspendedLastDate = modal.SuspendedLastDate;
                    ModalToReturn.SuspendedFollowerId = modal.SuspendedFollowerId;

                    ModalToReturn.PrimaryFinalJDAmount = modal.PrimaryFinalJDAmount;
                    ModalToReturn.PrimaryFinalJudgedInterests = modal.PrimaryFinalJudgedInterests;
                    //ModalToReturn.PrimaryAllJudgements = modal.PrimaryAllJudgements;
                    ModalToReturn.AppealFinalJDAmount = modal.AppealFinalJDAmount;
                    ModalToReturn.AppealFinalJudgedInterests = modal.AppealFinalJudgedInterests;
                    //ModalToReturn.AppealAllJudgements = modal.AppealAllJudgements;
                    ModalToReturn.FollowNotes = modal.FollowNotes;

                    ModalToReturn.ShowFollowup = modal.ShowFollowup;
                    ModalToReturn.Update_Follow = modal.ShowFollowup ? "Y" : "N";
                    ModalToReturn.ShowSuspend = modal.ShowSuspend;
                    ModalToReturn.Update_Suspend = modal.ShowSuspend ? "Y" : "N";

                    ModalToReturn.DifferentPanelYesSet = modal.DifferentPanelYesSet;
                    ModalToReturn.DifferentPanelNotes = modal.DifferentPanelNotes;
                    ModalToReturn.SupremeJudgementsDate = modal.SupremeJudgementsDate;
                    ModalToReturn.SupremeJudgements = modal.SupremeJudgements;
                    ModalToReturn.SupremeIsFavorable = modal.SupremeIsFavorable;
                    ModalToReturn.SupremeJDReceiveDate = modal.SupremeJDReceiveDate;
                    ModalToReturn.SupremeFinalJDAmount = modal.SupremeFinalJDAmount;
                    ModalToReturn.SupremeFinalJudgedInterests = modal.SupremeFinalJudgedInterests;
                    ModalToReturn.AllJudgement = string.Format(@"{0}{1}{2}{3}{4}", modal.PrimaryJudgements, string.IsNullOrEmpty(modal.PrimaryJudgements) ? "" : Environment.NewLine, modal.AppealJudgements, string.IsNullOrEmpty(modal.AppealJudgements) ? "" : Environment.NewLine, modal.SupremeJudgements);
                    ModalToReturn.SessionOnHold = modal.SessionOnHold;
                    ModalToReturn.SessionOnHoldUntill = modal.SessionOnHoldUntill;

                    //ModalToReturn.SessionFileStatus = modal.SessionFileStatus;

                    ModalToReturn.CourtFollow_LawyerId = modal.CourtFollow_LawyerId;
                    ModalToReturn.SessionNote_Remark = modal.SessionNote_Remark;
                    ModalToReturn.JudgementLevel = modal.JudgementLevel;
                    ModalToReturn.CaseRegistrationId = 0;

                    decimal? strFinalJDAmount = modal.SupremeFinalJDAmount > 0 ? modal.SupremeFinalJDAmount : modal.AppealFinalJDAmount;
                    strFinalJDAmount = strFinalJDAmount > 0 ? strFinalJDAmount : modal.PrimaryFinalJDAmount;
                    ModalToReturn.FinalJDAmount = Helper.GetTotalFinalJDAmount(modal.CaseId);// strFinalJDAmount;

                    string strFinalJudgedInterests = string.IsNullOrEmpty(modal.SupremeFinalJudgedInterests) ? modal.AppealFinalJudgedInterests : modal.SupremeFinalJudgedInterests;
                    strFinalJudgedInterests = string.IsNullOrEmpty(strFinalJudgedInterests) ? modal.PrimaryFinalJudgedInterests : strFinalJudgedInterests;

                    ModalToReturn.FinalJudgedInterests = Helper.GetFinalJudgedInterests(modal.CaseId);//strFinalJudgedInterests;

                    if (modal.CaseRegistrationId == null)
                    {
                        var objcaseReg = db.CaseRegistration.Where(w => w.CaseId == CaseId).OrderByDescending(o => o.CaseRegistrationId).FirstOrDefault();

                        if (objcaseReg != null)
                            ModalToReturn.CaseRegistrationId = objcaseReg.CaseRegistrationId;
                    }
                    else
                        ModalToReturn.CaseRegistrationId = modal.CaseRegistrationId;

                    var caseRegister = db.CaseRegistration.Where(w => w.SessionRollId == SessionRollId && !w.IsDeleted).FirstOrDefault();

                    if (caseRegister != null)
                    {
                        ModalToReturn.ActionLevel = caseRegister.ActionLevel;

                        if (!string.IsNullOrEmpty(caseRegister.ActionLevel) && caseRegister.ActionLevel != "0")
                            ModalToReturn.ActionLevelName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.ActionLevel && w.Mst_Value == caseRegister.ActionLevel).FirstOrDefault().Mst_Desc;

                        ModalToReturn.DepartmentType = caseRegister.DepartmentType;
                        ModalToReturn.ConsultantId = caseRegister.ConsultantId;
                        ModalToReturn.FormPrintWorkRequired = caseRegister.FormPrintWorkRequired;
                        ModalToReturn.OfficeProcedure = caseRegister.OfficeProcedure;
                        ModalToReturn.FormPrintLastDate = caseRegister.FormPrintLastDate;
                        //ModalToReturn.FileStatus = caseRegister.FileStatus;
                        ModalToReturn.FileStatus = OfficeFileStatus;
                        ModalToReturn.FileStatusRemarks = caseRegister.FileStatusRemarks;
                        ModalToReturn.ActionDate = caseRegister.ActionDate;
                        ModalToReturn.DisputeLevel = caseRegister.DisputeLevel;
                        ModalToReturn.DisputrRegisterDate = caseRegister.DisputrRegisterDate;
                        ModalToReturn.CaseRegistrationId = caseRegister.CaseRegistrationId;
                    }
                }

                if (ModalToReturn.PartialViewName.In("_AfterJudgementFavorable"))
                {
                    ModalToReturn.ActionDate = courtCases.ActionDate ?? DateTime.UtcNow.AddHours(4);
                    ModalToReturn.EnforcementAdmin = courtCases.EnforcementAdmin;
                    ModalToReturn.SessionNote_Remark = courtCases.SessionNote_Remark;
                    ModalToReturn.CourtFollow_LawyerId = courtCases.CourtFollow_LawyerId;
                }
            }
        }
        private void SaveModel(SessionsRollVM modal, ref SessionsRoll ModelToSave)
        {
            if(modal.PartialViewName == "_SessionJudgementSupreme" || modal.PartialViewName == "_SessionRollSupremeUpdate" || modal.PartialViewName == "_SessionFollowSupreme" || modal.PartialViewName == "_SessionUpdate"|| modal.PartialViewName == "_SessionUpdateSupreme" || modal.PartialViewName == "_SessionClose")
            {
                //ModelToSave.SessionClientId = modal.SessionClientId;
                //ModelToSave.SessionRollDefendentName = modal.SessionRollDefendentName;
                ModelToSave.CaseId = modal.CaseId;
                ModelToSave.CountLocationId = "1";

                ModelToSave.CaseType = "0";
                ModelToSave.LawyerId = "0";
                ModelToSave.FollowerId = "0";
                ModelToSave.SuspendedFollowerId = "0";
                ModelToSave.SessionOnHold = "0";
                ModelToSave.SessionLevel = "5";

                db.SessionsRoll.Add(ModelToSave);
                db.SaveChanges();

                modal.SessionRollId = ModelToSave.SessionRollId;
                modal.SessionLevel = ModelToSave.SessionLevel;

                PartialViewName = "_AllSessionsEditForm";

                if (modal.PartialViewName == "_SessionJudgementSupreme")
                {
                    UpdateSessionClientDefendent(modal.CaseId, modal.SessionClientId, modal.SessionRollDefendentName);

                    if (modal.SessionFileStatus == OfficeFileStatus.RunningCase.ToString()) // Running
                    {
                        UpdateNextHearingDate(modal.CaseId, modal.NextHearingDate);
                        db.Entry(ModelToSave).Entity.SessionOnHold = "0";
                        db.Entry(ModelToSave).Entity.SessionOnHoldUntill = null;

                    }
                    else if (modal.SessionFileStatus == OfficeFileStatus.JudgIssued.ToString()) // Judgement Issued
                    {
                        //UpdateJudgementDetail(modal, ref ModelToSave);
                    }
                    else if (modal.SessionFileStatus == OfficeFileStatus.ToKnowSessionDate.ToString()) // On Hold
                    {
                        UpdateNextHearingDate(modal.CaseId, (DateTime?)null);
                        db.Entry(ModelToSave).Entity.SessionOnHold = modal.SessionOnHold;
                        db.Entry(ModelToSave).Entity.SessionOnHoldUntill = modal.SessionOnHoldUntill;
                    }
                    

                    db.Entry(ModelToSave).Entity.SessionFileStatus = modal.SessionFileStatus;
                    db.Entry(ModelToSave).Entity.DifferentPanelYesSet = modal.DifferentPanelYesSet;
                    db.Entry(ModelToSave).Entity.DifferentPanelNotes = modal.DifferentPanelNotes;
                    db.Entry(ModelToSave).Entity.SupremeJudgementsDate = modal.SupremeJudgementsDate;
                    db.Entry(ModelToSave).Entity.SupremeJudgements = modal.SupremeJudgements;
                    db.Entry(ModelToSave).Entity.SupremeIsFavorable = modal.SupremeIsFavorable;
                    db.Entry(ModelToSave).Entity.SupremeJDReceiveDate = modal.SupremeJDReceiveDate;
                    db.Entry(ModelToSave).Entity.SupremeFinalJDAmount = modal.SupremeFinalJDAmount;
                    db.Entry(ModelToSave).Entity.SupremeFinalJudgedInterests = modal.SupremeFinalJudgedInterests;

                    db.Entry(ModelToSave).State = EntityState.Modified;
                    db.SaveChanges();


                }
                else if (modal.PartialViewName == "_SessionUpdate" || modal.PartialViewName == "_SessionUpdateSupreme")
                {
                    CourtCases courtCases = db.CourtCase.Find(modal.CaseId);

                    db.Entry(courtCases).Entity.CurrentHearingDate = modal.CurrentHearingDate;
                    db.Entry(courtCases).Entity.CourtDecision = modal.CourtDecision;
                    db.Entry(courtCases).Entity.SessionRemarks = modal.SessionRemarks;
                    db.Entry(courtCases).Entity.Requirements = modal.Requirements;
                    db.Entry(courtCases).Entity.ClientReply = modal.ClientReply;
                    db.Entry(courtCases).Entity.TransportationSource = modal.TransportationSource;
                    db.Entry(courtCases).Entity.FirstEmailDate = modal.FirstEmailDate;

                    db.Entry(courtCases).State = EntityState.Modified;
                    db.SaveChanges();


                }
                else if (modal.PartialViewName == "_SessionFollowSupreme")
                {
                    UpdateSessionClientDefendent(modal.CaseId, modal.SessionClientId, modal.SessionRollDefendentName);
                    //db.Entry(ModelToSave).Entity.SessionClientId = modal.SessionClientId;
                    //db.Entry(ModelToSave).Entity.SessionRollDefendentName = modal.SessionRollDefendentName;
                    db.Entry(ModelToSave).Entity.ShowFollowup = modal.ShowFollowup;
                    db.Entry(ModelToSave).Entity.ShowSuspend = modal.ShowSuspend;
                    db.Entry(ModelToSave).Entity.LastDate = modal.LastDate;
                    db.Entry(ModelToSave).Entity.WorkRequired = modal.WorkRequired;
                    db.Entry(ModelToSave).Entity.SessionNotes = modal.SessionNotes;
                    db.Entry(ModelToSave).Entity.SuspendedWorkRequired = modal.SuspendedWorkRequired;
                    db.Entry(ModelToSave).Entity.SuspendedSessionNotes = modal.SuspendedSessionNotes;
                    db.Entry(ModelToSave).Entity.FollowerId = modal.FollowerId;
                    db.Entry(ModelToSave).Entity.SuspendedFollowerId = modal.SuspendedFollowerId;
                    db.Entry(ModelToSave).Entity.SuspendedLastDate = modal.SuspendedLastDate;

                    db.Entry(ModelToSave).State = EntityState.Modified;
                    db.SaveChanges();

                }

            }
            else
            {
                UpdateSessionClientDefendent(modal.CaseId, modal.SessionClientId, modal.SessionRollDefendentName);
                ModelToSave.CaseId = modal.CaseId;

                ModelToSave.SessionLevel = modal.SessionLevel;
                ModelToSave.CountLocationId = modal.CountLocationId;
                ModelToSave.CaseType = modal.CaseType;
                ModelToSave.LawyerId = modal.LawyerId;
                ModelToSave.FollowerId = modal.FollowerId;
                ModelToSave.SuspendedFollowerId = modal.SuspendedFollowerId;
                ModelToSave.SessionOnHold = modal.SessionOnHold;

                db.SessionsRoll.Add(ModelToSave);
                db.SaveChanges();

                UpdateNextHearingDate(modal.CaseId, modal.NextHearingDate);
                UpdateOfficeFileStatus(modal.CaseId, modal.SessionFileStatus);
            }

            UpdateCourtCaseDetail(ref modal);

            if (int.Parse(modal.SessionLevel) >= 3 && int.Parse(modal.SessionLevel) <= 5)
            {
                if (modal.DetailId <= 0) // New Create
                    CreateCaseDetail(modal);
                else
                    UpdateCaseDetail(modal);
            }
            else if (int.Parse(modal.SessionLevel) == 6)
            {
                if (modal.DetailId <= 0) // New Create
                    CreateEnforcementDetail(modal);
                else
                    UpdateEnforcementDetail(modal);

            }

            SetDefendentName(modal.CaseId, modal.SessionRollDefendentName);
        }
        private void InitSessionRoll(ref SessionsRollVM modal)
        {
            SessionsRoll ModelToSave = new SessionsRoll();
            ModelToSave.CaseId = modal.CaseId;
            ModelToSave.SessionLevel = modal.CaseLevelCode;

            db.SessionsRoll.Add(ModelToSave);
            db.SaveChanges();

            modal.SessionRollId = ModelToSave.SessionRollId;
            modal.SessionLevel = ModelToSave.SessionLevel;
        }
        private void CreateSessionRoll(ref SessionsRoll ModelToSave)
        {
            db.SessionsRoll.Add(ModelToSave);
            db.SaveChanges();
        }
        private void CreateCaseDetail(SessionsRollVM courtCasesDetail)
        {
            string HeaderClass = string.Empty;
            string SelectCaseLevel = string.Empty;
            string CaseLevelName = string.Empty;
            DateTime? JudgementDate = null;
            DateTime? JudgementReceivingDate = null;
            string JudgementDetails = string.Empty;

            if (courtCasesDetail.Courtid == "1") {
                HeaderClass = "card-success";
                SelectCaseLevel = "3";
                CaseLevelName = "PRIMARY COURT";
                JudgementDate = courtCasesDetail.PrimaryJudgementsDate;
                JudgementReceivingDate = courtCasesDetail.PrimaryJDReceiveDate;
                JudgementDetails = courtCasesDetail.PrimaryJudgements;
            }
            else if (courtCasesDetail.Courtid == "2") {
                HeaderClass = "card-warning";
                SelectCaseLevel = "4";
                CaseLevelName = "APPEAL COURT";
                JudgementDate = courtCasesDetail.AppealJudgementsDate;
                JudgementReceivingDate = courtCasesDetail.AppealJDReceiveDate;
                JudgementDetails = courtCasesDetail.AppealJudgements;

            }
            else if (courtCasesDetail.Courtid == "3") {
                HeaderClass = "card-info";
                SelectCaseLevel = "5";
                CaseLevelName = "SUPREME COURT";
                JudgementDate = courtCasesDetail.SupremeJudgementsDate;
                JudgementReceivingDate = courtCasesDetail.SupremeJDReceiveDate;
                JudgementDetails = courtCasesDetail.SupremeJudgements;

            }
            else if (courtCasesDetail.Courtid == "4") {
                HeaderClass = "card-danger";
                SelectCaseLevel = "6";
                CaseLevelName = "ENFORCEMENT";
                JudgementDate = courtCasesDetail.EnforcementJudgementsDate;
                JudgementReceivingDate = courtCasesDetail.EnforcementJDReceiveDate;
                JudgementDetails = courtCasesDetail.EnforcementJudgements;
            }

            CourtCasesDetail _ModalToSave = new CourtCasesDetail();
            _ModalToSave.CaseId = courtCasesDetail.CaseId;
            _ModalToSave.Courtid = courtCasesDetail.Courtid;
            _ModalToSave.CourtRefNo = courtCasesDetail.CourtRefNo;
            _ModalToSave.CourtLocationid = courtCasesDetail.PASCourtLocationid;
            _ModalToSave.RegistrationDate = courtCasesDetail.RegistrationDate;
            _ModalToSave.CourtDepartment = courtCasesDetail.CourtDepartment;
            _ModalToSave.CaseLevelCode = courtCasesDetail.CaseLevelCode;
            _ModalToSave.ApealByWho = courtCasesDetail.ApealByWho;
            _ModalToSave.JudgementDate = JudgementDate;
            _ModalToSave.JudgementReceivingDate = JudgementReceivingDate;
            _ModalToSave.JudgementDetails = JudgementDetails;

            if (courtCasesDetail.Courtid == "3" && courtCasesDetail.PartialViewName == "_SessionClose")
            {
                _ModalToSave.ClosureDate = courtCasesDetail.ClosureDate;
                _ModalToSave.NextCaseLevel = courtCasesDetail.NextCaseLevel;
                _ModalToSave.NextCaseLevelCode = courtCasesDetail.NextCaseLevelCode;
            }


            db.CourtCasesDetail.Add(_ModalToSave);
            db.SaveChanges();

            CourtCases courtCases = db.CourtCase.Find(_ModalToSave.CaseId);
            db.Entry(courtCases).Entity.CaseLevelCode = SelectCaseLevel;
            db.Entry(courtCases).Entity.ClaimSummary = courtCasesDetail.ClaimSummary;
            db.Entry(courtCases).Entity.RealEstateYesNo = courtCasesDetail.RealEstateYesNo;
            db.Entry(courtCases).Entity.RealEstateDetail = courtCasesDetail.RealEstateDetail;

            db.Entry(courtCases).State = EntityState.Modified;
            db.SaveChanges();

            if (_ModalToSave.Courtid == "1")
            {
                var CaseRegistered = db.CaseRegistration.Where(w => w.CaseId == _ModalToSave.CaseId && w.ActionLevel == "1").FirstOrDefault();

                if (CaseRegistered != null)
                {
                    db.Entry(CaseRegistered).Entity.ClaimSummary = courtCasesDetail.ClaimSummary;
                    db.Entry(CaseRegistered).Entity.RealEstateYesNo = courtCasesDetail.RealEstateYesNo;
                    db.Entry(CaseRegistered).Entity.RealEstateDetail = courtCasesDetail.RealEstateDetail;

                    db.Entry(CaseRegistered).State = EntityState.Modified;
                    db.SaveChanges();

                }

            }

        }
        private void CreateEnforcementDetail(SessionsRollVM courtCasesDetail)
        {
            string HeaderClass = string.Empty;
            string SelectCaseLevel = string.Empty;
            string CaseLevelName = string.Empty;
            DateTime? JudgementDate = null;
            DateTime? JudgementReceivingDate = null;
            string JudgementDetails = string.Empty;

            if (courtCasesDetail.Courtid == "1")
            {
                HeaderClass = "card-success";
                SelectCaseLevel = "3";
                CaseLevelName = "PRIMARY COURT";
                JudgementDate = courtCasesDetail.PrimaryJudgementsDate;
                JudgementReceivingDate = courtCasesDetail.PrimaryJDReceiveDate;
                JudgementDetails = courtCasesDetail.PrimaryJudgements;
            }
            else if (courtCasesDetail.Courtid == "2")
            {
                HeaderClass = "card-warning";
                SelectCaseLevel = "4";
                CaseLevelName = "APPEAL COURT";
                JudgementDate = courtCasesDetail.AppealJudgementsDate;
                JudgementReceivingDate = courtCasesDetail.AppealJDReceiveDate;
                JudgementDetails = courtCasesDetail.AppealJudgements;

            }
            else if (courtCasesDetail.Courtid == "3")
            {
                HeaderClass = "card-info";
                SelectCaseLevel = "5";
                CaseLevelName = "SUPREME COURT";
                JudgementDate = courtCasesDetail.SupremeJudgementsDate;
                JudgementReceivingDate = courtCasesDetail.SupremeJDReceiveDate;
                JudgementDetails = courtCasesDetail.SupremeJudgements;

            }
            else if (courtCasesDetail.Courtid == "4")
            {
                HeaderClass = "card-danger";
                SelectCaseLevel = "6";
                CaseLevelName = "ENFORCEMENT";
                JudgementDate = courtCasesDetail.EnforcementJudgementsDate;
                JudgementReceivingDate = courtCasesDetail.EnforcementJDReceiveDate;
                JudgementDetails = courtCasesDetail.EnforcementJudgements;
            }

            CourtCasesEnforcement _ModalToSave = new CourtCasesEnforcement();

            _ModalToSave.CaseId = courtCasesDetail.CaseId;
            _ModalToSave.Courtid = courtCasesDetail.Courtid;
            _ModalToSave.EnforcementNo = courtCasesDetail.CourtRefNo;
            _ModalToSave.CourtLocationid = courtCasesDetail.PASCourtLocationid;
            _ModalToSave.RegistrationDate = courtCasesDetail.RegistrationDate;
            //_ModalToSave.JudgementDate = JudgementDate;
            //_ModalToSave.JudgementReceivingDate = JudgementReceivingDate;
            //_ModalToSave.JudgementDetails = JudgementDetails;

            db.CourtCasesEnforcement.Add(_ModalToSave);
            db.SaveChanges();

            CourtCases courtCases = db.CourtCase.Find(_ModalToSave.CaseId);
            courtCases.CaseLevelCode = SelectCaseLevel;
            db.Entry(courtCases).State = EntityState.Modified;
            db.SaveChanges();
        }
        private void UpdateCaseDetail(SessionsRollVM courtCasesDetail)
        {
            string HeaderClass = string.Empty;
            string SelectCaseLevel = string.Empty;
            string CaseLevelName = string.Empty;
            DateTime? JudgementDate = null;
            DateTime? JudgementReceivingDate = null;
             //default(DateTime);
            string JudgementDetails = string.Empty;

            if (courtCasesDetail.Courtid == "1")
            {
                HeaderClass = "card-success";
                SelectCaseLevel = "3";
                CaseLevelName = "PRIMARY COURT";
                JudgementDate = courtCasesDetail.PrimaryJudgementsDate;
                JudgementReceivingDate = courtCasesDetail.PrimaryJDReceiveDate;
                JudgementDetails = courtCasesDetail.PrimaryJudgements;
            }
            else if (courtCasesDetail.Courtid == "2")
            {
                HeaderClass = "card-warning";
                SelectCaseLevel = "4";
                CaseLevelName = "APPEAL COURT";
                JudgementDate = courtCasesDetail.AppealJudgementsDate;
                JudgementReceivingDate = courtCasesDetail.AppealJDReceiveDate;
                JudgementDetails = courtCasesDetail.AppealJudgements;

            }
            else if (courtCasesDetail.Courtid == "3")
            {
                HeaderClass = "card-info";
                SelectCaseLevel = "5";
                CaseLevelName = "SUPREME COURT";
                JudgementDate = courtCasesDetail.SupremeJudgementsDate;
                JudgementReceivingDate = courtCasesDetail.SupremeJDReceiveDate;
                JudgementDetails = courtCasesDetail.SupremeJudgements;

            }
            else if (courtCasesDetail.Courtid == "4")
            {
                HeaderClass = "card-danger";
                SelectCaseLevel = "6";
                CaseLevelName = "ENFORCEMENT";
                JudgementDate = courtCasesDetail.EnforcementJudgementsDate;
                JudgementReceivingDate = courtCasesDetail.EnforcementJDReceiveDate;
                JudgementDetails = courtCasesDetail.EnforcementJudgements;
            }

            CourtCasesDetail _ModalToSave = db.CourtCasesDetail.Find(courtCasesDetail.DetailId);
            db.Entry(_ModalToSave).Entity.DetailId = courtCasesDetail.DetailId;
            db.Entry(_ModalToSave).Entity.CaseId = courtCasesDetail.CaseId;
            db.Entry(_ModalToSave).Entity.Courtid = courtCasesDetail.Courtid;
            db.Entry(_ModalToSave).Entity.CourtRefNo = courtCasesDetail.CourtRefNo;
            db.Entry(_ModalToSave).Entity.CourtLocationid = courtCasesDetail.PASCourtLocationid;
            db.Entry(_ModalToSave).Entity.RegistrationDate = courtCasesDetail.RegistrationDate;
            db.Entry(_ModalToSave).Entity.CourtDepartment = courtCasesDetail.CourtDepartment;
            db.Entry(_ModalToSave).Entity.CaseLevelCode = courtCasesDetail.CaseLevelCode;
            db.Entry(_ModalToSave).Entity.ApealByWho = courtCasesDetail.ApealByWho;
            db.Entry(_ModalToSave).Entity.JudgementDate = JudgementDate;
            db.Entry(_ModalToSave).Entity.JudgementReceivingDate = JudgementReceivingDate;
            db.Entry(_ModalToSave).Entity.JudgementDetails = JudgementDetails;


            if (courtCasesDetail.Courtid == "3" && courtCasesDetail.PartialViewName == "_SessionClose")
            {
                db.Entry(_ModalToSave).Entity.ClosureDate = courtCasesDetail.ClosureDate;
                db.Entry(_ModalToSave).Entity.NextCaseLevel = courtCasesDetail.NextCaseLevel;
                db.Entry(_ModalToSave).Entity.NextCaseLevelCode = courtCasesDetail.NextCaseLevelCode;
            }

            db.Entry(_ModalToSave).State = EntityState.Modified;
            db.SaveChanges();

            _ModalToSave = db.CourtCasesDetail.Find(_ModalToSave.DetailId);

            CourtCases courtCases = db.CourtCase.Find(_ModalToSave.CaseId);
            db.Entry(courtCases).Entity.CaseLevelCode = SelectCaseLevel;
            db.Entry(courtCases).Entity.ClaimSummary = courtCasesDetail.ClaimSummary;
            db.Entry(courtCases).Entity.RealEstateYesNo = courtCasesDetail.RealEstateYesNo;
            db.Entry(courtCases).Entity.RealEstateDetail = courtCasesDetail.RealEstateDetail;
            db.Entry(courtCases).State = EntityState.Modified;
            db.SaveChanges();

            if (_ModalToSave.Courtid == "1")
            {
                var CaseRegistered = db.CaseRegistration.Where(w => w.CaseId == _ModalToSave.CaseId && w.ActionLevel == "1").FirstOrDefault();

                if (CaseRegistered != null)
                {
                    db.Entry(CaseRegistered).Entity.ClaimSummary = courtCasesDetail.ClaimSummary;
                    db.Entry(CaseRegistered).Entity.RealEstateYesNo = courtCasesDetail.RealEstateYesNo;
                    db.Entry(CaseRegistered).Entity.RealEstateDetail = courtCasesDetail.RealEstateDetail;

                    db.Entry(CaseRegistered).State = EntityState.Modified;
                    db.SaveChanges();

                }

            }

        }
        private void UpdateEnforcementDetail(SessionsRollVM courtCasesDetail)
        {
            string HeaderClass = string.Empty;
            string SelectCaseLevel = string.Empty;
            string CaseLevelName = string.Empty;
            DateTime? JudgementDate = null;
            DateTime? JudgementReceivingDate = null;
            string JudgementDetails = string.Empty;

            if (courtCasesDetail.Courtid == "1")
            {
                HeaderClass = "card-success";
                SelectCaseLevel = "3";
                CaseLevelName = "PRIMARY COURT";
                JudgementDate = courtCasesDetail.PrimaryJudgementsDate;
                JudgementReceivingDate = courtCasesDetail.PrimaryJDReceiveDate;
                JudgementDetails = courtCasesDetail.PrimaryJudgements;
            }
            else if (courtCasesDetail.Courtid == "2")
            {
                HeaderClass = "card-warning";
                SelectCaseLevel = "4";
                CaseLevelName = "APPEAL COURT";
                JudgementDate = courtCasesDetail.AppealJudgementsDate;
                JudgementReceivingDate = courtCasesDetail.AppealJDReceiveDate;
                JudgementDetails = courtCasesDetail.AppealJudgements;

            }
            else if (courtCasesDetail.Courtid == "3")
            {
                HeaderClass = "card-info";
                SelectCaseLevel = "5";
                CaseLevelName = "SUPREME COURT";
                JudgementDate = courtCasesDetail.SupremeJudgementsDate;
                JudgementReceivingDate = courtCasesDetail.SupremeJDReceiveDate;
                JudgementDetails = courtCasesDetail.SupremeJudgements;

            }
            else if (courtCasesDetail.Courtid == "4")
            {
                HeaderClass = "card-danger";
                SelectCaseLevel = "6";
                CaseLevelName = "ENFORCEMENT";
                JudgementDate = courtCasesDetail.EnforcementJudgementsDate;
                JudgementReceivingDate = courtCasesDetail.EnforcementJDReceiveDate;
                JudgementDetails = courtCasesDetail.EnforcementJudgements;
            }
            CourtCasesEnforcement _ModalToSave = db.CourtCasesEnforcement.Find(courtCasesDetail.DetailId);
            db.Entry(_ModalToSave).Entity.EnforcementId = courtCasesDetail.DetailId;
            db.Entry(_ModalToSave).Entity.CaseId = courtCasesDetail.CaseId;
            db.Entry(_ModalToSave).Entity.Courtid = courtCasesDetail.Courtid;
            db.Entry(_ModalToSave).Entity.EnforcementNo = courtCasesDetail.CourtRefNo;
            db.Entry(_ModalToSave).Entity.CourtLocationid = courtCasesDetail.PASCourtLocationid;
            db.Entry(_ModalToSave).Entity.RegistrationDate = courtCasesDetail.RegistrationDate;

            db.Entry(_ModalToSave).State = EntityState.Modified;
            db.SaveChanges();

            _ModalToSave = db.CourtCasesEnforcement.Find(_ModalToSave.EnforcementId);

            CourtCases courtCases = db.CourtCase.Find(_ModalToSave.CaseId);
            courtCases.CaseLevelCode = SelectCaseLevel;
            db.Entry(courtCases).State = EntityState.Modified;
            db.SaveChanges();

        }
        private void UpdateModel(ref SessionsRollVM modal, ref SessionsRoll ModelToSave)
        {

            if (modal.PartialViewName == "_SessionJudgementSupreme")
            {
                if (modal.DetailId > 0)
                    UpdateCaseDetail(modal);
                else
                    CreateCaseDetail(modal);

                if (modal.SessionFileStatus == OfficeFileStatus.RunningCase.ToString()) // Running
                {
                    UpdateNextHearingDate(modal.CaseId, modal.NextHearingDate);
                    db.Entry(ModelToSave).Entity.SessionOnHold = "0";
                    db.Entry(ModelToSave).Entity.SessionOnHoldUntill = null;

                }
                else if (modal.SessionFileStatus == OfficeFileStatus.JudgIssued.ToString()) // Judgement Issued
                {
                    UpdateNextHearingDate(modal.CaseId, (DateTime?)null);
                    UpdateJudhmentIssued(modal, ref ModelToSave);
                    db.Entry(ModelToSave).Entity.SessionOnHold = "0";
                    db.Entry(ModelToSave).Entity.SessionOnHoldUntill = null;
                }
                else if (modal.SessionFileStatus == OfficeFileStatus.ToKnowSessionDate.ToString()) // On Hold
                {
                    UpdateNextHearingDate(modal.CaseId, (DateTime?)null);
                    db.Entry(ModelToSave).Entity.SessionOnHold = modal.SessionOnHold;
                    db.Entry(ModelToSave).Entity.SessionOnHoldUntill = modal.SessionOnHoldUntill;
                }

                UpdateOfficeFileStatus(modal.CaseId, modal.SessionFileStatus);

                db.Entry(ModelToSave).Entity.SupremeJudgementsDate = modal.SupremeJudgementsDate;
                db.Entry(ModelToSave).Entity.SupremeJudgements = modal.SupremeJudgements;
                db.Entry(ModelToSave).Entity.SupremeIsFavorable = modal.SupremeIsFavorable;
                db.Entry(ModelToSave).Entity.SupremeJDReceiveDate = modal.SupremeJDReceiveDate;
                db.Entry(ModelToSave).Entity.SupremeFinalJDAmount = modal.SupremeFinalJDAmount;
                db.Entry(ModelToSave).Entity.SupremeFinalJudgedInterests = modal.SupremeFinalJudgedInterests;

                UpdateSessionClientDefendent(modal.CaseId, modal.SessionClientId, modal.SessionRollDefendentName);
            }
            else if (modal.PartialViewName == "_SessionRollSupremeUpdate" || modal.PartialViewName == "_SessionJudgementSupreme_CaseDetail" || modal.PartialViewName == "_SessionUpdate_CaseDetail" || modal.PartialViewName == "_SessionOnHoldDIV_CaseDetail")
            {
                if (modal.DetailId > 0)
                    UpdateCaseDetail(modal);
                else
                    CreateCaseDetail(modal);

                modal.SessionFileStatus = modal.CourtDepartment;
                db.Entry(ModelToSave).Entity.SessionFileStatus = modal.SessionFileStatus;

                if (modal.CourtDepartment == OfficeFileStatus.RunningCase.ToString() || modal.CourtDepartment == OfficeFileStatus.AssigningJudge.ToString())
                {
                    db.Entry(ModelToSave).Entity.SessionOnHold = "0";
                    db.Entry(ModelToSave).Entity.SessionOnHoldUntill = null;

                }
                else if (modal.CourtDepartment == OfficeFileStatus.JudgIssued.ToString()) // Judgement Issued
                {
                    modal.JudgementLevel = "3";

                    db.Entry(ModelToSave).Entity.SessionOnHold = "0";
                    db.Entry(ModelToSave).Entity.SessionOnHoldUntill = null;
                    db.Entry(ModelToSave).Entity.JudgementLevel = modal.JudgementLevel;

                    UpdateJudgementIssued(modal, ref ModelToSave);
                    //CreateCaseRegistration(modal, ref ModelToSave); // NO CaseRegistration for SPREME
                    UpdateNextHearingDate(modal.CaseId, (DateTime?)null);

                    if (modal.IsDelete == "Y")
                    {
                        db.Entry(ModelToSave).Entity.DeletedOn = DateTime.UtcNow.AddHours(4);
                        db.Entry(ModelToSave).Entity.DeletedBy = User.Identity.GetUserId();
                    }
                }
                else if (modal.CourtDepartment == OfficeFileStatus.ToKnowSessionDate.ToString() || modal.CourtDepartment == OfficeFileStatus.DifferentPanel.ToString()) // On Hold
                {
                    UpdateNextHearingDate(modal.CaseId, (DateTime?)null);
                    db.Entry(ModelToSave).Entity.SessionOnHold = modal.SessionOnHold;
                    db.Entry(ModelToSave).Entity.SessionOnHoldUntill = modal.SessionOnHoldUntill;

                }

                db.Entry(ModelToSave).Entity.CourtFollow_LawyerId = modal.CourtFollow_LawyerId;
                db.Entry(ModelToSave).Entity.SessionNote_Remark = modal.SessionNote_Remark;

                UpdateFollowupDetail(modal, ref ModelToSave);

                if (modal.Update_Addreass == "Y")
                    UpdateSessionDEFAddress(modal);

                if (modal.Update_PV == "Y")
                {
                    int Voucher_No = CreatePayVoucher(modal);
                    modal.Voucher_No = Voucher_No;
                }

                ProcessCourtDecisionHistory(modal);
                UpdateSessionUpdateDetail(modal);
            }
            else if (modal.PartialViewName == "_SessionUpdate" || modal.PartialViewName == "_SessionUpdateSupreme")
            {
                if (modal.DetailId > 0)
                    UpdateCaseDetail(modal);
                else
                    CreateCaseDetail(modal);


                ProcessCourtDecisionHistory(modal);

                CourtCases courtCases = db.CourtCase.Find(modal.CaseId);

                db.Entry(courtCases).Entity.CurrentHearingDate = modal.CurrentHearingDate;
                db.Entry(courtCases).Entity.CourtDecision = modal.CourtDecision;
                db.Entry(courtCases).Entity.SessionRemarks = modal.SessionRemarks;
                db.Entry(courtCases).Entity.Requirements = modal.Requirements;
                db.Entry(courtCases).Entity.ClientReply = modal.ClientReply;
                db.Entry(courtCases).Entity.TransportationSource = modal.TransportationSource;
                db.Entry(courtCases).Entity.FirstEmailDate = modal.FirstEmailDate;

                db.Entry(courtCases).State = EntityState.Modified;
                db.SaveChanges();

            }
            else if (modal.PartialViewName == "_SessionClose")
            {
                if (modal.DetailId > 0)
                    UpdateCaseDetail(modal);
                else
                    CreateCaseDetail(modal);
            }
            else if (modal.PartialViewName == "_AfterJudgementEditForm")
            {
                db.Entry(ModelToSave).Entity.SessionOnHold = "0";
                db.Entry(ModelToSave).Entity.SessionOnHoldUntill = null;
                db.Entry(ModelToSave).Entity.SessionFileStatus = modal.SessionFileStatus;

                UpdateAfterJudgementReceived(modal, ref ModelToSave);
                UpdateNextHearingDate(modal.CaseId, (DateTime?)null);

                if (modal.IsDelete == "Y")
                {
                    db.Entry(ModelToSave).Entity.DeletedOn = DateTime.UtcNow.AddHours(4);
                    db.Entry(ModelToSave).Entity.DeletedBy = User.Identity.GetUserId();
                }

                UpdateOfficeFileStatus(modal.CaseId, modal.SessionFileStatus);
            }
            else if (modal.PartialViewName == "_SessionFollowSupreme")
            {
                if (modal.DetailId > 0)
                    UpdateCaseDetail(modal);
                else
                    CreateCaseDetail(modal);

                //db.Entry(ModelToSave).Entity.SessionClientId = modal.SessionClientId;
                //db.Entry(ModelToSave).Entity.SessionRollDefendentName = modal.SessionRollDefendentName;

                UpdateSessionClientDefendent(modal.CaseId, modal.SessionClientId, modal.SessionRollDefendentName);

                UpdateFollowupDetail(modal, ref ModelToSave);

                SetDefendentName(modal.CaseId, modal.SessionRollDefendentName);
            }
            else
            {
                bool IsRemoveCR = false;

                if (modal.IsAfterJudgementReceived == "Y")
                {
                    if (modal.SavePV_Data == "_AfterJudgementReceived")
                    {
                        db.Entry(ModelToSave).Entity.SessionOnHold = "0";
                        db.Entry(ModelToSave).Entity.SessionOnHoldUntill = null;
                        db.Entry(ModelToSave).Entity.SessionFileStatus = modal.SessionFileStatus;

                        UpdateJudgementIssued(modal, ref ModelToSave);
                        CreateCaseRegistration(modal, ref ModelToSave);
                        UpdateNextHearingDate(modal.CaseId, (DateTime?)null);

                        if (modal.IsDelete == "Y")
                        {
                            db.Entry(ModelToSave).Entity.DeletedOn = DateTime.UtcNow.AddHours(4);
                            db.Entry(ModelToSave).Entity.DeletedBy = User.Identity.GetUserId();
                        }

                        UpdateOfficeFileStatus(modal.CaseId, modal.SessionFileStatus);
                    }
                    else
                    {
                        if (modal.PartialViewName == "_AfterJudgementFavorable")
                        {
                            UpdateAfterJudgementFavorable(modal);
                        }
                    }
                }
                else
                {
                    db.Entry(ModelToSave).Entity.CaseType = modal.CaseType;
                    db.Entry(ModelToSave).Entity.LawyerId = modal.LawyerId;
                    db.Entry(ModelToSave).Entity.SessionFileStatus = modal.SessionFileStatus;

                    if (modal.SessionFileStatus == OfficeFileStatus.RunningCase.ToString()) // Running
                    {
                        db.Entry(ModelToSave).Entity.SessionOnHold = "0";
                        db.Entry(ModelToSave).Entity.SessionOnHoldUntill = null;

                    }
                    else if (modal.SessionFileStatus == OfficeFileStatus.JudgIssued.ToString()) // Judgement Issued
                    {
                        db.Entry(ModelToSave).Entity.SessionOnHold = "0";
                        db.Entry(ModelToSave).Entity.SessionOnHoldUntill = null;
                        db.Entry(ModelToSave).Entity.JudgementLevel = modal.JudgementLevel;

                        UpdateJudgementIssued(modal, ref ModelToSave);
                        CreateCaseRegistration(modal, ref ModelToSave);
                        UpdateNextHearingDate(modal.CaseId, (DateTime?)null);

                        if (modal.IsDelete == "Y")
                        {
                            db.Entry(ModelToSave).Entity.DeletedOn = DateTime.UtcNow.AddHours(4);
                            db.Entry(ModelToSave).Entity.DeletedBy = User.Identity.GetUserId();
                        }
                    }
                    else if (modal.SessionFileStatus == OfficeFileStatus.ToKnowSessionDate.ToString()) // On Hold
                    {
                        UpdateNextHearingDate(modal.CaseId, (DateTime?)null);
                        db.Entry(ModelToSave).Entity.SessionOnHold = modal.SessionOnHold;
                        db.Entry(ModelToSave).Entity.SessionOnHoldUntill = modal.SessionOnHoldUntill;

                    }

                    db.Entry(ModelToSave).Entity.CourtFollow_LawyerId = modal.CourtFollow_LawyerId;
                    db.Entry(ModelToSave).Entity.SessionNote_Remark = modal.SessionNote_Remark;

                    UpdateFollowupDetail(modal, ref ModelToSave);

                    if (modal.Update_Addreass == "Y")
                        UpdateSessionDEFAddress(modal);

                    if (modal.Update_CourtTransfer == "Y")
                        CreateCourtMoneyTransfer(modal);

                    if (modal.Update_PV == "Y")
                    {
                        int Voucher_No = CreatePayVoucher(modal);
                        modal.Voucher_No = Voucher_No;
                    }
                }

                ProcessCourtDecisionHistory(modal);
                UpdateSessionUpdateDetail(modal);

                if (ModelToSave.JudgementLevel == "1" && ModelToSave.PrimaryIsFavorable == "N" && modal.PrimaryIsFavorable == "Y")
                    IsRemoveCR = true;
                else if (ModelToSave.JudgementLevel == "2" && ModelToSave.AppealIsFavorable == "N" && modal.AppealIsFavorable == "Y")
                    IsRemoveCR = true;
                else if (ModelToSave.JudgementLevel == "4" && ModelToSave.EnforcementIsFavorable == "N" && modal.EnforcementIsFavorable == "Y")
                    IsRemoveCR = true;

                if (IsRemoveCR)
                    RemoveCaseRegistration(modal);
            }

            db.Entry(ModelToSave).State = EntityState.Modified;
            db.SaveChanges();
        }
        private string GetDefendentName(int Caseid)
        {
            try
            {
                string DefendentName = db.CaseRegistration.Where(w => w.CaseId == Caseid).OrderByDescending(o => o.CaseRegistrationId).FirstOrDefault().FormPrintDefendant;
                return DefendentName;

            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
        private void SetDefendentName(int Caseid, string DefendentName)
        {
            CaseRegistration caseRegistration = db.CaseRegistration.Where(w => w.CaseId == Caseid).OrderByDescending(o => o.CaseRegistrationId).FirstOrDefault();
            if(caseRegistration != null)
            {
                caseRegistration.FormPrintDefendant = DefendentName;
                db.Entry(caseRegistration).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        private void UpdateSessionUpdateDetail(SessionsRollVM modal)
        {
            Helper.UpdateSessionUpdateDetail(modal, "SessionsRollVM");
        }
        private void ProcessCourtDecisionHistory(SessionsRollVM modal)
        {
            Helper.ProcessCourtDecisionHistory(modal, HttpContext.User.Identity.GetUserId());
        }
        private void UpdateNextHearingDate(int CaseId, DateTime? NextHearingDate)
        {
            Helper.UpdateNextHearingDate(CaseId, NextHearingDate);
        }
        private void UpdateStopEnforcement(int CaseId,string StopEnfRequest)
        {
            Helper.UpdateStopEnforcement(CaseId, StopEnfRequest);
        }
        private void UpdateRequirement(int CaseId, string strRequirement = null)
        {
            Helper.UpdateRequirement(CaseId, strRequirement);
        }
        private void UpdateJudgementIssued(SessionsRollVM modal, ref SessionsRoll ModelToSave)
        {
            if (modal.JudgementLevel == "1")
            {
                db.Entry(ModelToSave).Entity.PrimaryJudgementsDate = modal.PrimaryJudgementsDate;
                db.Entry(ModelToSave).Entity.PrimaryJudgements = modal.PrimaryJudgements;
                db.Entry(ModelToSave).Entity.PrimaryIsFavorable = modal.PrimaryIsFavorable;
                db.Entry(ModelToSave).Entity.PrimaryJDReceiveDate = modal.PrimaryJDReceiveDate;
                db.Entry(ModelToSave).Entity.PrimaryFinalJDAmount = modal.PrimaryFinalJDAmount;
                db.Entry(ModelToSave).Entity.PrimaryFinalJudgedInterests = modal.PrimaryFinalJudgedInterests;
            }
            else if (modal.JudgementLevel == "2")
            {
                db.Entry(ModelToSave).Entity.AppealJudgementsDate = modal.AppealJudgementsDate;
                db.Entry(ModelToSave).Entity.AppealJudgements = modal.AppealJudgements;
                db.Entry(ModelToSave).Entity.AppealIsFavorable = modal.AppealIsFavorable;
                db.Entry(ModelToSave).Entity.AppealJDReceiveDate = modal.AppealJDReceiveDate;
                db.Entry(ModelToSave).Entity.AppealFinalJDAmount = modal.AppealFinalJDAmount;
                db.Entry(ModelToSave).Entity.AppealFinalJudgedInterests = modal.AppealFinalJudgedInterests;
            }
            else if (modal.JudgementLevel == "3")
            {
                db.Entry(ModelToSave).Entity.SupremeJudgementsDate = modal.SupremeJudgementsDate;
                db.Entry(ModelToSave).Entity.SupremeJudgements = modal.SupremeJudgements;
                db.Entry(ModelToSave).Entity.SupremeJDReceiveDate = modal.SupremeJDReceiveDate;
                db.Entry(ModelToSave).Entity.SupremeFinalJDAmount = modal.SupremeFinalJDAmount;
                db.Entry(ModelToSave).Entity.SupremeFinalJudgedInterests = modal.SupremeFinalJudgedInterests;
            }
            else if (modal.JudgementLevel == "4")
            {
                modal.DisputrRegisterDate = modal.EnforcementJudgementsDate;

                db.Entry(ModelToSave).Entity.EnforcementJudgements = modal.EnforcementJudgements;
                db.Entry(ModelToSave).Entity.EnforcementJDReceiveDate = modal.EnforcementJDReceiveDate;
                db.Entry(ModelToSave).Entity.EnforcementJudgementsDate = modal.EnforcementJudgementsDate;
                db.Entry(ModelToSave).Entity.EnforcementIsFavorable = modal.EnforcementIsFavorable;
            }
        }
        private void UpdateAfterJudgementReceived(SessionsRollVM modal, ref SessionsRoll ModelToSave)
        {
            if (modal.JudgementLevel == "1")
            {
                db.Entry(ModelToSave).Entity.PrimaryJudgementsDate = modal.PrimaryJudgementsDate;
                db.Entry(ModelToSave).Entity.PrimaryIsFavorable = modal.PrimaryIsFavorable;
                db.Entry(ModelToSave).Entity.PrimaryJDReceiveDate = modal.PrimaryJDReceiveDate;
            }
            else if (modal.JudgementLevel == "2")
            {
                db.Entry(ModelToSave).Entity.AppealJudgementsDate = modal.AppealJudgementsDate;
                db.Entry(ModelToSave).Entity.AppealIsFavorable = modal.AppealIsFavorable;
                db.Entry(ModelToSave).Entity.AppealJDReceiveDate = modal.AppealJDReceiveDate;
            }
            else if (modal.JudgementLevel == "3")
            {
                db.Entry(ModelToSave).Entity.SupremeJudgementsDate = modal.SupremeJudgementsDate;
                db.Entry(ModelToSave).Entity.SupremeJDReceiveDate = modal.SupremeJDReceiveDate;
            }
            else if (modal.JudgementLevel == "4")
            {
                db.Entry(ModelToSave).Entity.EnforcementJDReceiveDate = modal.EnforcementJDReceiveDate;
                db.Entry(ModelToSave).Entity.EnforcementJudgementsDate = modal.EnforcementJudgementsDate;
                db.Entry(ModelToSave).Entity.EnforcementIsFavorable = modal.EnforcementIsFavorable;
            }
        }
        private void UpdateSessionClientDefendent(int CaseId, string SessionClientId, string SessionRollDefendentName)
        {
            CourtCases courtCases = db.CourtCase.Find(CaseId);

            db.Entry(courtCases).Entity.SessionClientId = SessionClientId;
            db.Entry(courtCases).Entity.SessionRollDefendentName = SessionRollDefendentName;

            db.Entry(courtCases).State = EntityState.Modified;
            db.SaveChanges();

        }
        private void UpdateJudhmentIssued(SessionsRollVM modal, ref SessionsRoll ModelToSave)
        {
            db.Entry(ModelToSave).Entity.DifferentPanelYesSet = modal.DifferentPanelYesSet;
        }
        private void UpdateFollowupDetail(SessionsRollVM modal, ref SessionsRoll ModelToSave)
        {
            db.Entry(ModelToSave).Entity.ShowFollowup = modal.ShowFollowup;
            db.Entry(ModelToSave).Entity.ShowSuspend = modal.ShowSuspend;
            db.Entry(ModelToSave).Entity.LastDate = modal.LastDate;
            db.Entry(ModelToSave).Entity.SuspendedLastDate = modal.SuspendedLastDate;
            db.Entry(ModelToSave).Entity.WorkRequired = modal.WorkRequired;
            db.Entry(ModelToSave).Entity.SessionNotes = modal.SessionNotes;
            db.Entry(ModelToSave).Entity.SuspendedWorkRequired = modal.SuspendedWorkRequired;
            db.Entry(ModelToSave).Entity.SuspendedSessionNotes = modal.SuspendedSessionNotes;
            db.Entry(ModelToSave).Entity.FollowerId = modal.FollowerId;
            db.Entry(ModelToSave).Entity.SuspendedFollowerId = modal.SuspendedFollowerId;
        }
        private void UpdateCourtCaseDetail(ref SessionsRollVM modal)
        {
            int intCaseID = modal.CaseId;
            string strCaseLevelCode = modal.SessionLevel;

            if (int.Parse(strCaseLevelCode) >= 3 && int.Parse(strCaseLevelCode) < 6)
            {
                var courtCases = db.CourtCase.Where(w => w.CaseId == intCaseID).FirstOrDefault();
                var courtCaseDetail = db.CourtCasesDetail.Where(w => w.CaseId == intCaseID && w.CaseLevelCode == strCaseLevelCode).FirstOrDefault();

                if (courtCaseDetail != null)
                {
                    modal.CourtRefNo = courtCaseDetail.CourtRefNo;
                    modal.CountLocationName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == courtCaseDetail.CourtLocationid).FirstOrDefault().Mst_Desc;
                    modal.ClosureDate = courtCaseDetail.ClosureDate;
                    modal.NextCaseLevel = courtCaseDetail.NextCaseLevel;
                    modal.NextCaseLevelCode = courtCaseDetail.NextCaseLevelCode;
                    modal.Courtid = courtCaseDetail.Courtid;
                    modal.DetailId = courtCaseDetail.DetailId;
                    modal.CountLocationId = courtCaseDetail.CourtLocationid;
                    modal.PASCourtLocationid = courtCaseDetail.CourtLocationid;
                    modal.RegistrationDate = courtCaseDetail.RegistrationDate;
                    modal.CourtDepartment = courtCaseDetail.CourtDepartment;
                    modal.CaseLevelCode = courtCaseDetail.CaseLevelCode;
                    modal.ApealByWho = courtCaseDetail.ApealByWho;
                    modal.ClaimSummary = courtCases.ClaimSummary;
                    modal.RealEstateYesNo = courtCases.RealEstateYesNo;
                    modal.RealEstateDetail = courtCases.RealEstateDetail;
                    modal.CourtDepartment = courtCaseDetail.CourtDepartment;
                }
            }
            else if (int.Parse(strCaseLevelCode) == 6)
            {
                var courtCaseDetail = db.CourtCasesEnforcement.Where(w => w.CaseId == intCaseID).FirstOrDefault();

                if (courtCaseDetail != null)
                {
                    modal.CourtRefNo = courtCaseDetail.EnforcementNo;
                    modal.CountLocationName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == courtCaseDetail.CourtLocationid).FirstOrDefault().Mst_Desc;
                    modal.ClosureDate = courtCaseDetail.ClosureDate;
                    modal.NextCaseLevel = courtCaseDetail.NextCaseLevel;
                    modal.NextCaseLevelCode = courtCaseDetail.NextCaseLevelCode;
                    modal.Courtid = courtCaseDetail.Courtid;
                    modal.DetailId = courtCaseDetail.EnforcementId;
                    modal.CountLocationId = courtCaseDetail.CourtLocationid;
                    modal.PASCourtLocationid = courtCaseDetail.CourtLocationid;
                    modal.RegistrationDate = courtCaseDetail.RegistrationDate;
                    modal.CaseLevelCode = courtCaseDetail.CaseLevelCode;
                }
            }

        }
        private SessionsRollVM GetPrintFormData(int id, string formName,int? sessionId = null)
        {
            SessionsRollVM modal = new SessionsRollVM();
            modal.CaseId = id;
            int CaseId = id;
            var courtCases = db.CourtCase.Find(modal.CaseId);
            var ClientName = ExtensionMethods.IsNull(courtCases.ClientCode, "0") == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == courtCases.ClientCode).FirstOrDefault().Mst_Desc;
            var currentCaseLevelName = ExtensionMethods.IsNull(courtCases.CaseLevelCode, "0") == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.CaseLevel && w.Mst_Value == courtCases.CaseLevelCode).FirstOrDefault().Mst_Desc;
            modal.CurrentCaseLevel = currentCaseLevelName;
            modal.OfficeFileNo = courtCases.OfficeFileNo;
            modal.CurrentHearingDate = courtCases.CurrentHearingDate;
            modal.NextHearingDate = courtCases.NextHearingDate;
            modal.Requirements = courtCases.Requirements;
            modal.ReceivedDate = courtCases.ReceptionDate;
            modal.ClaimSummary = courtCases.ClaimSummary;

            string AppealByName = string.Empty;
            string replaceWith = Environment.NewLine;
            modal.AccountContractNo = courtCases.AccountContractNo;
            modal.ClientFileNo = courtCases.ClientFileNo;
            modal.ClaimAmount = courtCases.ClaimAmount;
            modal.AgainstCode = courtCases.AgainstCode;
            modal.AgainstName = ExtensionMethods.IsNull(courtCases.AgainstCode, "0") == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.CaseAgainst && w.Mst_Value == courtCases.AgainstCode).FirstOrDefault().Mst_Desc;

            modal.CourtDecision = courtCases.CourtDecision;
            modal.Defendant = courtCases.Defendant;
            modal.SessionRollDefendentName = courtCases.SessionRollDefendentName;

            modal.SessionFileStatus = courtCases.OfficeFileStatus;
            modal.SessionFileStatusName = ExtensionMethods.IsNull(courtCases.OfficeFileStatus, "0") == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.OfficeFileStatus && w.Mst_Value == courtCases.OfficeFileStatus).FirstOrDefault().Mst_Desc;

            if (courtCases.AgainstCode == "3")
            {
                modal.DEFENDANT_PLAINTIFF = "PLAINTIFF";
                modal.DEFENDANT_PLAINTIFF_AR = "مدعى";
            }
            else
            {
                modal.DEFENDANT_PLAINTIFF = "DEFENDANT";
                modal.DEFENDANT_PLAINTIFF_AR = "الخصم";
            }

            string AccountContractNo = modal.AccountContractNo;
            string ClientFileNo = string.IsNullOrEmpty(modal.ClientFileNo) ? "" : string.Format(@"    - {0}", modal.ClientFileNo);
            string LoanManagerName = string.Empty;

            LoanManagerName = ExtensionMethods.IsNull(courtCases.LoanManager, "0") == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.LoanManager && w.Mst_Value == courtCases.LoanManager).FirstOrDefault().Mst_Desc;

            LoanManagerName = string.IsNullOrEmpty(LoanManagerName) ? "" : string.Format(@"    - {0}", LoanManagerName);

            modal.ACCOUNTDETAILSTEXT = string.Format(@"{0}{1}{2}", AccountContractNo, ClientFileNo, LoanManagerName);

            if (courtCases.ClientClassificationCode == "1")
            {
                modal.ACCOUNTDETAILS = "ACCOUNT NO.";
                modal.ACCOUNTDETAILS_AR = "رقم الحساب";
            }
            else if (courtCases.ClientClassificationCode == "2")
            {
                modal.ACCOUNTDETAILS = "CONTRACT NO.";
                modal.ACCOUNTDETAILS_AR = "رقم العقد";
            }
            else if (courtCases.ClientClassificationCode == "3")
            {
                modal.ACCOUNTDETAILS = "PREMISES DETAIL";
                modal.ACCOUNTDETAILS_AR = "تفاصيل أماكن الإقامة";
            }
            else if (courtCases.ClientClassificationCode == "4")
            {
                modal.ACCOUNTDETAILS = "CLAIM DETAIL";
                modal.ACCOUNTDETAILS_AR = "تفاصيل المطالبة";
            }
            else
            {
                modal.ACCOUNTDETAILS = "ACCOUNT NO.";
                modal.ACCOUNTDETAILS_AR = "رقم الحساب";
            }

            bool sessionDataExists = false;
            bool IsJudgementIsued = false;

            if (sessionId != null && sessionId > 0)
            {
                var sessionData = db.SessionsRoll.Where(w => w.SessionRollId == sessionId && w.DeletedOn == null).FirstOrDefault();

                if (sessionData != null )
                {
                    sessionDataExists = true;

                    modal.SessionOnHold = sessionData.SessionOnHold;
                    modal.SessionOnHoldUntill = sessionData.SessionOnHoldUntill;
                    modal.SessionOnHoldName = ExtensionMethods.IsNull(sessionData.SessionOnHold, "0") == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.SessionOnHold && w.Mst_Value == sessionData.SessionOnHold).FirstOrDefault().Mst_Desc;

                    string templateSuffix = string.Empty;

                    if (sessionData.SessionFileStatus == OfficeFileStatus.RunningCase.ToString())
                        templateSuffix = "_RUNN";
                    else if (sessionData.SessionFileStatus == OfficeFileStatus.JudgIssued.ToString()) {
                        templateSuffix = "_JUDG";
                        IsJudgementIsued = true;
                    }
                    else if (sessionData.SessionFileStatus == OfficeFileStatus.ToKnowSessionDate.ToString() || sessionData.SessionFileStatus == OfficeFileStatus.DifferentPanel.ToString())
                        templateSuffix = "_HOLD";
                    else if (sessionData.SessionFileStatus == "4")
                        templateSuffix = "_TEMP";

                    formName = formName + templateSuffix;

                    if (courtCases.CaseLevelCode == "1")
                    {
                        formName = "_PrintRequirementFormTobeREG";

                        var caseReg = db.CaseRegistration.Where(w => w.CaseId == CaseId).OrderByDescending(o => o.CaseRegistrationId).FirstOrDefault();

                        if (caseReg != null)
                        {
                            modal.FileStatusName = caseReg.FileStatus == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.OfficeFileStatus && w.Mst_Value == courtCases.OfficeFileStatus).FirstOrDefault().Mst_Desc;
                        }

                    }
                    else
                    {
                        if (int.Parse(courtCases.CaseLevelCode) >= 3 && int.Parse(courtCases.CaseLevelCode) < 6)
                        {
                            var courtCaseDetail = db.CourtCasesDetail.Where(w => w.CaseId == CaseId && w.CaseLevelCode == courtCases.CaseLevelCode).FirstOrDefault();

                            if (courtCaseDetail != null)
                            {
                                if (int.Parse(courtCases.CaseLevelCode) == 5)
                                {
                                    formName = "_PrintRequirementFormSUP" + templateSuffix;
                                    modal.SupremeStageName = ExtensionMethods.IsNull(courtCaseDetail.CourtDepartment, "0") == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.OfficeFileStatus && w.Mst_Value == courtCaseDetail.CourtDepartment).FirstOrDefault().Mst_Desc;
                                }

                                AppealByName = ExtensionMethods.IsNull(courtCaseDetail.ApealByWho, "0") == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.ApealByWho && w.Mst_Value == courtCaseDetail.ApealByWho).FirstOrDefault().Mst_Desc;

                                modal.CourtRefNo = courtCaseDetail.CourtRefNo;
                                modal.RegistrationDate = courtCaseDetail.RegistrationDate;
                                modal.CountLocationName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == courtCaseDetail.CourtLocationid).FirstOrDefault().Mst_Desc;
                            }
                        }
                        else if (courtCases.CaseLevelCode == "6")
                        {
                            var courtEnforcementDetail = db.CourtCasesEnforcement.Where(w => w.CaseId == CaseId).FirstOrDefault();

                            var _caseRegistration = db.CaseRegistration.Where(w => w.CaseId == modal.CaseId && w.ActionLevel == "4" && !w.IsDeleted).OrderByDescending(o => o.CaseRegistrationId).FirstOrDefault();

                            if (_caseRegistration != null)
                                formName = "_PrintRequirementFormENF2" + templateSuffix;
                            else
                                formName = "_PrintRequirementFormENF" + templateSuffix;

                            modal.RealEstateDetail = courtCases.RealEstateDetail;


                            if (courtEnforcementDetail != null)
                            {
                                modal.CourtRefNo = courtEnforcementDetail.EnforcementNo;
                                modal.RegistrationDate = courtEnforcementDetail.RegistrationDate;
                                modal.CountLocationName = courtEnforcementDetail.CourtLocationid == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == courtEnforcementDetail.CourtLocationid).FirstOrDefault().Mst_Desc;
                                modal.EnforcementStageName = courtEnforcementDetail.EnforcementlevelId == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.OfficeFileStatus && w.Mst_Value == courtCases.OfficeFileStatus).FirstOrDefault().Mst_Desc;
                            }


                            // modal.CurrentHearingDate = sessionData.EnforcementJudgementsDate;

                        }

                        if (IsJudgementIsued)
                        {
                            if (sessionData.JudgementLevel == "1")
                                modal.CurrentHearingDate = sessionData.PrimaryJudgementsDate;
                            else if (sessionData.JudgementLevel == "2")
                                modal.CurrentHearingDate = sessionData.AppealJudgementsDate;
                            else if (sessionData.JudgementLevel == "3")
                                modal.CurrentHearingDate = sessionData.SupremeJudgementsDate;
                        }
                    } 
                }
            }

            if(!sessionDataExists)
            {
                if (courtCases.CaseLevelCode == "1")
                {
                    formName = "_PrintRequirementFormTobeREG";

                    var caseReg = db.CaseRegistration.Where(w => w.CaseId == CaseId).OrderByDescending(o => o.CaseRegistrationId).FirstOrDefault();

                    if (caseReg != null)
                    {
                        modal.FileStatusName = caseReg.FileStatus == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.OfficeFileStatus && w.Mst_Value == courtCases.OfficeFileStatus).FirstOrDefault().Mst_Desc;
                    }

                }
                else if (int.Parse(courtCases.CaseLevelCode) >= 3 && int.Parse(courtCases.CaseLevelCode) < 6)
                {
                    //modal.CurrentHearingDate = null;

                    var courtCaseDetail = db.CourtCasesDetail.Where(w => w.CaseId == CaseId && w.CaseLevelCode == courtCases.CaseLevelCode).FirstOrDefault();

                    if (courtCaseDetail != null)
                    {
                        if (int.Parse(courtCases.CaseLevelCode) == 5)
                        {
                            formName = "_PrintRequirementFormSUP";
                            modal.SupremeStageName = ExtensionMethods.IsNull(courtCaseDetail.CourtDepartment, "0") == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.OfficeFileStatus && w.Mst_Value == courtCaseDetail.CourtDepartment).FirstOrDefault().Mst_Desc;
                        }

                        AppealByName = ExtensionMethods.IsNull(courtCaseDetail.ApealByWho, "0") == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.ApealByWho && w.Mst_Value == courtCaseDetail.ApealByWho).FirstOrDefault().Mst_Desc;

                        modal.CourtRefNo = courtCaseDetail.CourtRefNo;
                        modal.RegistrationDate = courtCaseDetail.RegistrationDate;
                        modal.CountLocationName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == courtCaseDetail.CourtLocationid).FirstOrDefault().Mst_Desc;
                    }
                }
                else if (courtCases.CaseLevelCode == "6")
                {
                    //modal.CurrentHearingDate = null;

                    var courtEnforcementDetail = db.CourtCasesEnforcement.Where(w => w.CaseId == CaseId).FirstOrDefault();

                    var _caseRegistration = db.CaseRegistration.Where(w => w.CaseId == modal.CaseId && w.ActionLevel == "4" && !w.IsDeleted).OrderByDescending(o => o.CaseRegistrationId).FirstOrDefault();

                    if (_caseRegistration != null)
                        formName = "_PrintRequirementFormENF2";
                    else
                        formName = "_PrintRequirementFormENF";

                    modal.RealEstateDetail = courtCases.RealEstateDetail;

                    if (courtEnforcementDetail != null)
                    {
                        modal.CourtRefNo = courtEnforcementDetail.EnforcementNo;
                        modal.RegistrationDate = courtEnforcementDetail.RegistrationDate;
                        modal.CountLocationName = courtEnforcementDetail.CourtLocationid == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == courtEnforcementDetail.CourtLocationid).FirstOrDefault().Mst_Desc;
                        modal.EnforcementStageName = courtEnforcementDetail.EnforcementlevelId == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.OfficeFileStatus && w.Mst_Value == courtCases.OfficeFileStatus).FirstOrDefault().Mst_Desc;
                    }
                }
            }

            modal.AppealByName = AppealByName;
            modal.UserId = User.Identity.Name;
            modal.PrintDate = DateTime.Now.ToString("dd/MM/yyyy");
            modal.FormName = formName;


            return modal;
        }
        private void UpdateSessionDEFAddress(SessionsRollVM modal)
        {
            Helper.UpdateSessionDEFAddress(modal);
        }
        private int CreatePayVoucher(SessionsRollVM modal)
        {
            string UserLocation = Helper.GetEmployeeLocation(User.Identity.Name);

            return Helper.CreatePaymentVoucher(modal, "SessionsRollVM", UserLocation); 
        }
        private void CreateCaseRegistration(SessionsRollVM modal, ref SessionsRoll ModelToSave)
        {
            bool RedirectToCaseRegister = false;
            CaseRegistration caseRegistration = new CaseRegistration();

            caseRegistration = db.CaseRegistration.Where(w => w.SessionRollId == modal.SessionRollId && !w.IsDeleted).FirstOrDefault();

            if(caseRegistration == null)
            {
                caseRegistration = new CaseRegistration();
                if (modal.PrimaryIsFavorable == "N")
                {
                    if (string.IsNullOrEmpty(ModelToSave.PrimaryCrtInRegInProgress))
                    {
                        RedirectToCaseRegister = true;
                        caseRegistration.JudgementDate = modal.PrimaryJudgementsDate;
                        caseRegistration.EnforcementDispute = "0";
                        caseRegistration.CourtRegistration = "0";

                    }
                }

                if (modal.AppealIsFavorable == "N")
                {
                    if (string.IsNullOrEmpty(ModelToSave.AppealCrtInRegInProgress))
                    {
                        RedirectToCaseRegister = true;
                        caseRegistration.JudgementDate = modal.AppealJudgementsDate;
                        caseRegistration.EnforcementDispute = "0";
                        caseRegistration.CourtRegistration = "0";
                    }
                }

                if (modal.EnforcementIsFavorable == "N")
                {
                    RedirectToCaseRegister = true;
                    caseRegistration.JudgementDate = modal.SupremeJudgementsDate;
                    caseRegistration.EnforcementDispute = "1";
                    caseRegistration.CourtRegistration = "0";
                    caseRegistration.DisputeLevel = modal.DisputeLevel;
                    caseRegistration.DisputrRegisterDate = modal.DisputrRegisterDate;
                }

                if (RedirectToCaseRegister)
                {
                    caseRegistration.CaseId = ModelToSave.CaseId;
                    caseRegistration.SessionRollId = ModelToSave.SessionRollId;
                    caseRegistration.ActionLevel = modal.ActionLevel;
                    caseRegistration.ActionDate = modal.ActionDate;
                    caseRegistration.FileStatus = modal.FileStatus;
                    caseRegistration.FileStatusRemarks = modal.FileStatus == "4" ? modal.OnHoldReasonDDL : "";
                    caseRegistration.DepartmentType = modal.DepartmentType;
                    caseRegistration.ConsultantId = modal.ConsultantId;
                    caseRegistration.FormPrintWorkRequired = modal.FormPrintWorkRequired;
                    caseRegistration.OfficeProcedure = modal.OfficeProcedure;
                    caseRegistration.FormPrintLastDate = modal.FormPrintLastDate;
                    caseRegistration.CourtDetailRegistered = false;
                    caseRegistration.IsDeleted = false;

                    db.CaseRegistration.Add(caseRegistration);
                    db.SaveChanges();

                    db.Entry(ModelToSave).Entity.CaseRegistrationId = caseRegistration.CaseRegistrationId;

                    if (caseRegistration.ActionLevel == "2")
                        db.Entry(ModelToSave).Entity.PrimaryCrtInRegInProgress = "Y";
                    else if (caseRegistration.ActionLevel == "3")
                        db.Entry(ModelToSave).Entity.AppealCrtInRegInProgress = "Y";
                    else if (caseRegistration.ActionLevel == "4")
                        db.Entry(ModelToSave).Entity.EnforcementCrtInRegInProgress = "Y";

                    db.Entry(ModelToSave).State = EntityState.Modified;
                    db.SaveChanges();
                    modal.buttonToGo = "CR";
                }
            }
            else
            {
                if (modal.PrimaryIsFavorable == "Y" || modal.AppealIsFavorable == "Y" || modal.EnforcementIsFavorable == "Y")
                {
                    db.Entry(caseRegistration).Entity.IsDeleted = true;
                    db.Entry(caseRegistration).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    if (modal.PrimaryIsFavorable == "N")
                    {
                        RedirectToCaseRegister = true;
                        db.Entry(caseRegistration).Entity.JudgementDate = modal.PrimaryJudgementsDate;
                        db.Entry(caseRegistration).Entity.EnforcementDispute = "0";
                        db.Entry(caseRegistration).Entity.CourtRegistration = "0";

                    }

                    if (modal.AppealIsFavorable == "N")
                    {
                        RedirectToCaseRegister = true;
                        db.Entry(caseRegistration).Entity.JudgementDate = modal.AppealJudgementsDate;
                        db.Entry(caseRegistration).Entity.EnforcementDispute = "0";
                        db.Entry(caseRegistration).Entity.CourtRegistration = "0";
                    }

                    if (modal.EnforcementIsFavorable == "N")
                    {
                        RedirectToCaseRegister = true;
                        db.Entry(caseRegistration).Entity.JudgementDate = modal.SupremeJudgementsDate;
                        db.Entry(caseRegistration).Entity.EnforcementDispute = "1";
                        db.Entry(caseRegistration).Entity.CourtRegistration = "0";
                        db.Entry(caseRegistration).Entity.DisputeLevel = modal.DisputeLevel;
                        db.Entry(caseRegistration).Entity.DisputrRegisterDate = modal.DisputrRegisterDate;
                    }

                    if (RedirectToCaseRegister)
                    {
                        db.Entry(caseRegistration).Entity.ActionLevel = modal.ActionLevel;
                        db.Entry(caseRegistration).Entity.ActionDate = modal.ActionDate;
                        db.Entry(caseRegistration).Entity.FileStatus = modal.FileStatus;
                        db.Entry(caseRegistration).Entity.DepartmentType = modal.DepartmentType;
                        db.Entry(caseRegistration).Entity.ConsultantId = modal.ConsultantId;
                        db.Entry(caseRegistration).Entity.FormPrintWorkRequired = modal.FormPrintWorkRequired;
                        db.Entry(caseRegistration).Entity.OfficeProcedure = modal.OfficeProcedure;
                        db.Entry(caseRegistration).Entity.FormPrintLastDate = modal.FormPrintLastDate;
                        db.Entry(caseRegistration).Entity.CourtDetailRegistered = false;
                        db.Entry(caseRegistration).Entity.IsDeleted = false;

                        db.Entry(caseRegistration).State = EntityState.Modified;
                        db.SaveChanges();

                        if (caseRegistration.ActionLevel == "2")
                            db.Entry(ModelToSave).Entity.PrimaryCrtInRegInProgress = "Y";
                        else if (caseRegistration.ActionLevel == "3")
                            db.Entry(ModelToSave).Entity.AppealCrtInRegInProgress = "Y";
                        else if (caseRegistration.ActionLevel == "4")
                            db.Entry(ModelToSave).Entity.EnforcementCrtInRegInProgress = "Y";

                        db.Entry(ModelToSave).State = EntityState.Modified;
                        db.SaveChanges();
                        modal.buttonToGo = "CR";
                    }
                }
            }

        }
        private void UpdateOfficeFileStatus(int CaseId, string OfficeFileStatus)
        {
            Helper.UpdateOfficeFileStatus(CaseId, OfficeFileStatus);
        }
        private void CreateCourtMoneyTransfer(SessionsRollVM modal)
        {
            Helper.CreateCourtMoneyTransfer(modal, "SessionsRollVM");
        }
        private void RemoveCaseRegistration(SessionsRollVM modal)
        {
            var caseRegistration = db.CaseRegistration.Where(w => w.SessionRollId == modal.SessionRollId && !w.IsDeleted).OrderByDescending(O => O.CaseRegistrationId).FirstOrDefault();

            if (caseRegistration != null)
            {
                db.Entry(caseRegistration).Entity.IsDeleted = true;
                db.Entry(caseRegistration).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        private void UpdateAfterJudgementFavorable(SessionsRollVM modal)
        {
            CourtCases courtCases = db.CourtCase.Find(modal.CaseId);

            db.Entry(courtCases).Entity.OfficeFileStatus = modal.SessionFileStatus;
            db.Entry(courtCases).Entity.ActionDate = modal.ActionDate;
            db.Entry(courtCases).Entity.EnforcementAdmin = modal.EnforcementAdmin;
            db.Entry(courtCases).Entity.SessionNote_Remark = modal.SessionNote_Remark;
            db.Entry(courtCases).Entity.CurrentHearingDate = modal.CurrentHearingDate;
            db.Entry(courtCases).Entity.CourtDecision = modal.CourtDecision;
            db.Entry(courtCases).Entity.ClientReply = modal.ClientReply;
            db.Entry(courtCases).Entity.Requirements = modal.Requirements;
            db.Entry(courtCases).Entity.FirstEmailDate = modal.FirstEmailDate;
            db.Entry(courtCases).Entity.TransportationSource = modal.TransportationSource;
            db.Entry(courtCases).Entity.CourtFollow = modal.CourtFollow;
            db.Entry(courtCases).Entity.CourtFollow_LawyerId = modal.CourtFollow_LawyerId;
            db.Entry(courtCases).Entity.CourtFollowRequirement = modal.CourtFollowRequirement;
            db.Entry(courtCases).Entity.CommissioningDate = modal.CommissioningDate;

            db.Entry(courtCases).State = EntityState.Modified;
            db.SaveChanges();

            if (courtCases.CaseLevelCode == "5")
            {
                CourtCasesDetail CCD = db.CourtCasesDetail.Where(w => w.CaseId == courtCases.CaseId && w.CaseLevelCode == "5").OrderByDescending(o => o.DetailId).FirstOrDefault();
                if (CCD != null)
                {
                    db.Entry(CCD).Entity.CourtDepartment = modal.SessionFileStatus;
                    db.Entry(CCD).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            CourtCasesEnforcement courtCasesEnforcement = db.CourtCasesEnforcement.Where(w => w.CaseId == modal.CaseId).OrderByDescending(o => o.EnforcementId).FirstOrDefault();

            if (courtCasesEnforcement != null)
            {
                db.Entry(courtCasesEnforcement).Entity.ActionDate = modal.ActionDate;
                db.Entry(courtCasesEnforcement).Entity.EnforcementAdmin = modal.EnforcementAdmin;
                db.Entry(courtCasesEnforcement).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        #endregion

        public ActionResult AjaxIndexData()
        {
            var request = HttpContext.Request;
            List<SessionRollListForIndex> data = new List<SessionRollListForIndex>();

            var start = (Convert.ToInt32(Request["start"]));
            var Length = (Convert.ToInt32(Request["length"])) == 0 ? 10 : (Convert.ToInt32(Request["length"]));
            var searchvalue = Request["search[value]"] ?? "";
            var sortcoloumnIndex = Convert.ToInt32(Request["order[0][column]"]);
            var sortDirection = Request["order[0][dir]"] ?? "asc";
            var recordsTotal = 0;

            try
            {
                string LocationId = string.Empty;
                string DataFor = string.Empty;
                string SessionLevel = string.Empty;
                string ClientCode = string.Empty;
                DateTime DateFrom = DateTime.Now;
                DateTime DateTo = DateTime.Now;
                string CountLocationId = string.Empty;
                string LawyerId = string.Empty;
                string LawyerUser = string.Empty;
                int MCTRecords = 0;
                int SLLRecords = 0;
                string ProcedureName = string.Empty;

                try
                {
                    DataFor = request.Params["DataTableName"].ToString();
                    LocationId = request.Params["LocationId"].ToString();

                    if (DataFor.In("TO_RECEIVE_JUDGEMENT","SUPREME", "OFS-63", "OFS-33", "OFS-34", "OFS-35", "OFS-36", "OFS-37", "OFS-38", "OFS-39"))
                    {
                        List<string> parName = Helper.getDefaultParNames();
                        List<object> parValues = Helper.getDefaultParValues();
                        ProcedureName = request.Params["ProcedureName"].ToString();
                        parName.AddRange(new[] { "@UserName", "@DataFor", "@Location" });
                        parValues.AddRange(new[] { User.Identity.Name, DataFor, LocationId });

                        var ds = Helper.getDataSet(parName.ToArray(), parValues.ToArray(), TableNames: new string[] { "data", "summary" }, procedureName: ProcedureName);
                        DataTable dt = ds.Tables["data"];
                        DataTable Summarydt = ds.Tables["summary"];

                        var jsondata = dt.ToDictionary();

                        recordsTotal = Summarydt.Rows.Count > 0 ? int.Parse(Summarydt.Rows[0]["recordsTotal"].ToString()) : 0;
                        MCTRecords = Summarydt.Rows.Count > 0 ? int.Parse(Summarydt.Rows[0]["MCTRecords"].ToString()) : 0;
                        SLLRecords = Summarydt.Rows.Count > 0 ? int.Parse(Summarydt.Rows[0]["SLLRecords"].ToString()) : 0;

                        return Json(new { data = jsondata, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, MuscatTotal = MCTRecords, SalalahTotal = SLLRecords }, JsonRequestBehavior.AllowGet);

                    }

                    if (DataFor == "PRINT")
                    {
                        if (request.Params["buttonDate"].ToString() == "N")
                        {
                            SessionLevel = request.Params["SessionLevel"].ToString();
                            DateFrom = request.Params["DateFrom"].ToString() == "" ? DateTime.Now.AddYears(-100) : DateTime.ParseExact(request.Params["DateFrom"].ToString(), "dd/MM/yyyy", null);
                            DateTo = request.Params["DateTo"].ToString() == "" ? DateTime.Now.AddYears(100) : DateTime.ParseExact(request.Params["DateTo"].ToString(), "dd/MM/yyyy", null);
                            CountLocationId = request.Params["CountLocationId"].ToString();
                            LawyerId = request.Params["LawyerId"].ToString();
                            ClientCode = request.Params["ClientCode"].ToString();
                        }
                        else
                        {
                            SessionLevel = request.Params["SessionLevel"].ToString();
                            DateFrom = request.Params["buttonDate"].ToString() == "" ? DateTime.Now.AddYears(-100) : DateTime.ParseExact(request.Params["buttonDate"].ToString(), "dd-MM-yyyy", null);
                            DateTo = request.Params["buttonDate"].ToString() == "" ? DateTime.Now.AddYears(100) : DateTime.ParseExact(request.Params["buttonDate"].ToString(), "dd-MM-yyyy", null);
                            CountLocationId = request.Params["CountLocationId"].ToString();
                            LawyerId = request.Params["LawyerId"].ToString();
                            ClientCode = request.Params["ClientCode"].ToString();
                        }

                    }

                    if (DataFor == "SUSPENDED")
                    {
                        string SuspendedFollowerId = Helper.GetLawyerId(User.Identity.Name) ?? "0";

                        LawyerId = request.Params["LawyerId"].ToString();
                        LawyerUser = request.Params["LawyerUser"].ToString();
                        if (LawyerUser == "Y" && LawyerId == SuspendedFollowerId)
                            LocationId = "A";
                    }

                    data = Helper.GetSessionRollList(sortcoloumnIndex, start, searchvalue, Length, sortDirection, User.Identity.Name, DataFor, LocationId, SessionLevel, DateFrom, DateTo, CountLocationId, LawyerId, ClientCode).ToList();
                    recordsTotal = data.Count > 0 ? data[0].TotalRecords : 0;

                }
                catch (Exception e)
                {
                }

            }
            catch (Exception ex)
            {
            }
            return Json(new { data = data, recordsTotal = recordsTotal, recordsFiltered = recordsTotal }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult GetTab(string Mode, string PartialViewName, int CaseId, int SessionRollId, string CaseLevel = null)
        {
            if (PartialViewName == "_AllSessionsEditForm" || PartialViewName == "_SessionRollRegister")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.FrmMode = "";

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                modal.PartialViewName = PartialViewName;
                mapSessionRollVM(CaseId, SessionRollId, ref modal);

                if (Mode == "C")
                {
                    InitSessionRoll(ref modal);
                    ViewBag.SessionRollId = modal.SessionRollId;
                    ViewBag.HFCaseId = modal.CaseId;

                    return Json(new { SessionRollId = modal.SessionRollId, CaseId = modal.CaseId });
                }

                ViewBag.FrmMode = "E";
                ViewBag.SessionRollId = modal.SessionRollId;
                ViewBag.HFCaseId = modal.CaseId;


                ViewBag.CaseType = new SelectList(Helper.GetSessionCaseType(), "Mst_Value", "Mst_Desc", modal.CaseType);
                ViewBag.LawyerId = new SelectList(Helper.LoadDependentDDL("1901"), "Mst_Value", "Mst_Desc", modal.LawyerId);
                ViewBag.SessionFileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterSR, modal.CaseLevelCode == "6" ? null : FileStatusCodesSR), "Mst_Value", "Mst_Desc", modal.SessionFileStatus);

                ViewBag.FollowerId = new SelectList(Helper.GetSessionFollowers(), "Mst_Value", "Mst_Desc", modal.FollowerId);
                ViewBag.SuspendedFollowerId = new SelectList(Helper.GetSessionFollowers(), "Mst_Value", "Mst_Desc", modal.SuspendedFollowerId);

                ViewBag.CourtFollow = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.CourtFollow);
                ViewBag.CourtFollow_LawyerId = new SelectList(Helper.GetSessionLawyers(), "Mst_Value", "Mst_Desc", modal.CourtFollow_LawyerId);
                ViewBag.MoneyWith = new SelectList(Helper.GetMoneyWith(), "Mst_Value", "Mst_Desc");

                #region ADDRESS

                ViewBag.DEF_CallerName = new SelectList(Helper.GetCallerNames(), "Mst_Value", "Mst_Desc", modal.DEF_CallerName);
                ViewBag.AnnouncementTypeId = new SelectList(Helper.GetAnnouncementType(), "Mst_Value", "Mst_Desc", modal.AnnouncementTypeId);

                string LawyerDoc = Helper.GetDEF_Lawyer_Doc(modal.CaseId);
                string AddressDoc = Helper.GetDEF_Address_Doc(modal.CaseId);

                if (LawyerDoc == "#")
                {
                    ViewBag.ViewDEF_LawyerDocs = "AppHidden";
                }
                else
                {
                    ViewBag.ViewDEF_LawyerDocs = "";
                    ViewBag.DEF_Lawyer_Docs = LawyerDoc;
                }

                if (AddressDoc == "#")
                {
                    ViewBag.ViewDEF_AddressDocs = "AppHidden";
                }
                else
                {
                    ViewBag.ViewDEF_AddressDocs = "";
                    ViewBag.DEF_Address_Docs = AddressDoc;
                }

                #endregion

                #region Pay Voucher

                var skipIDs = "1,2,3,13,25";
                ViewBag.CourtType = new SelectList(Helper.GetCaseLevelList("D"), "Mst_Value", "Mst_Desc", modal.CaseLevelCode);
                ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
                ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
                ViewBag.Payment_Head = new SelectList(Helper.LoadPayForRefunableOnly(), "Mst_Value", "Mst_Desc", "10007");
                ViewBag.PaymentHeadDetail = new SelectList(Helper.LoadDependentDDL("10007"), "Mst_Value", "Mst_Desc");
                ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(), "Mst_Value", "Mst_Desc", "1901");
                ViewBag.PaymentToBenificry = new SelectList(Helper.LoadDependentDDL("1901", skipIDs), "Mst_Value", "Mst_Desc", User.Identity.Name);
                ViewBag.PayToBankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc");
                ViewBag.PayToMessageLang = new SelectList(Helper.GetLangForSelect(), "Mst_Value", "Mst_Desc");
                #endregion



                ViewBag.UpdatedOn = modal.UpdatedOn;

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_SessionPrint")
            {
                ViewBag.SessionLevel = new SelectList(Helper.GetCaseLevelList("C"), "Mst_Value", "Mst_Desc");
                ViewBag.CountLocationId = new SelectList(Helper.GetCourtLocationList("1"), "Mst_Value", "Mst_Desc");
                ViewBag.LawyerId = new SelectList(Helper.LoadDependentDDL("1901"), "Mst_Value", "Mst_Desc");
                ViewBag.ClientCode = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.Client), "Mst_Value", "Mst_Desc");
                return PartialView(PartialViewName);
            }
            else if (PartialViewName == "_SessionRollRegisterPAS")
            {
                CourtCasesDetail _courtCasesDetail = new CourtCasesDetail();
                SessionsRollVM modal = new SessionsRollVM();

                string CaseLevelName = string.Empty;
                string HeaderClass = string.Empty;
                string SelectCaseLevel = string.Empty;
                string Courtid = string.Empty;

                if (CaseLevel == "3") { Courtid = "1"; ViewBag.CourtRefNo = "PRIMARY NO"; ViewBag.HeadingText = "PRIMARY REGISTRATION"; HeaderClass = "CaseRegisterCaseLevelPrimary"; SelectCaseLevel = "3"; CaseLevelName = "PRIMARY COURT"; }
                else if (CaseLevel == "4") { Courtid = "2"; ViewBag.CourtRefNo = "APPEAL NO"; ViewBag.HeadingText = "APPEAL REGISTRATION"; HeaderClass = "CaseRegisterCaseLevelAppeal"; SelectCaseLevel = "4"; CaseLevelName = "APPEAL COURT"; }
                else if (CaseLevel == "5") { Courtid = "3"; ViewBag.CourtRefNo = "SUPREME NO"; ViewBag.HeadingText = "SUPREME REGISTRATION"; HeaderClass = "CaseRegisterCaseLevelSupreme"; SelectCaseLevel = "5"; CaseLevelName = "SUPREME COURT"; }

                _courtCasesDetail = db.CourtCasesDetail.Where(w => w.CaseId == CaseId && w.Courtid == Courtid).FirstOrDefault();
                var CourtCases = db.CourtCase.Find(CaseId);
                ViewBag.ClaimAmount = CourtCases.ClaimAmount;
                if (_courtCasesDetail == null)
                {
                    modal = new SessionsRollVM();
                    modal.Courtid = Courtid;
                    modal.CaseLevelCode = SelectCaseLevel;

                    if (Courtid == "1")
                    {
                        var CaseRegistered = db.CaseRegistration.Where(w => w.CaseId == CaseId && w.ActionLevel == "1" && !w.IsDeleted).OrderByDescending(o => o.CaseRegistrationId).FirstOrDefault();

                        if (CaseRegistered != null)
                        {
                            modal.ClaimSummary = CaseRegistered.ClaimSummary;
                            modal.RealEstateYesNo = CaseRegistered.RealEstateYesNo;
                            modal.RealEstateDetail = CaseRegistered.RealEstateDetail;

                        }
                    }

                    ViewBag.PASCourtLocationid = new SelectList(Helper.GetCourtLocationList(Courtid), "Mst_Value", "Mst_Desc");
                    ViewBag.ApealByWho = new SelectList(Helper.GetByWho(), "Mst_Value", "Mst_Desc");
                    ViewBag.hid_DetailId = "0";

                }
                else
                {
                    modal = new SessionsRollVM();

                    modal.DetailId = _courtCasesDetail.DetailId;
                    modal.CaseId = _courtCasesDetail.CaseId;
                    modal.Courtid = _courtCasesDetail.Courtid;
                    modal.CourtRefNo = _courtCasesDetail.CourtRefNo;
                    modal.PASCourtLocationid = _courtCasesDetail.CourtLocationid;
                    modal.RegistrationDate = _courtCasesDetail.RegistrationDate;
                    modal.CourtDepartment = _courtCasesDetail.CourtDepartment;
                    modal.CaseLevelCode = _courtCasesDetail.CaseLevelCode;
                    modal.ApealByWho = _courtCasesDetail.ApealByWho;
                    modal.ClaimSummary = CourtCases.ClaimSummary;
                    modal.RealEstateYesNo = CourtCases.RealEstateYesNo;
                    modal.RealEstateDetail = CourtCases.RealEstateDetail;

                    ViewBag.hid_DetailId = _courtCasesDetail.DetailId;
                    ViewBag.PASCourtLocationid = new SelectList(Helper.GetCourtLocationList(Courtid), "Mst_Value", "Mst_Desc", modal.PASCourtLocationid);
                    ViewBag.ApealByWho = new SelectList(Helper.GetByWho(), "Mst_Value", "Mst_Desc", modal.ApealByWho);

                }

                ViewBag.hid_Courtid = Courtid;

                ViewBag.CourtClass = HeaderClass;
                ViewBag.CaseLevelCode = CaseLevelName;


                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_SessionRollRegisterENF")
            {
                CourtCasesEnforcement _courtCasesDetail = new CourtCasesEnforcement();
                SessionsRollVM modal = new SessionsRollVM();

                string CaseLevelName = string.Empty;
                string HeaderClass = string.Empty;
                string SelectCaseLevel = string.Empty;
                string Courtid = string.Empty;

                if (CaseLevel == "3") { Courtid = "1"; ViewBag.CourtRefNo = "PRIMARY NO"; HeaderClass = "CaseRegisterCaseLevelPrimary"; SelectCaseLevel = "3"; CaseLevelName = "PRIMARY COURT"; }
                else if (CaseLevel == "4") { Courtid = "2"; ViewBag.CourtRefNo = "APPEAL NO"; HeaderClass = "CaseRegisterCaseLevelAppeal"; SelectCaseLevel = "4"; CaseLevelName = "APPEAL COURT"; }
                else if (CaseLevel == "5") { Courtid = "3"; ViewBag.CourtRefNo = "SUPREME NO"; HeaderClass = "CaseRegisterCaseLevelSupreme"; SelectCaseLevel = "5"; CaseLevelName = "SUPREME COURT"; }
                else if (CaseLevel == "6") { Courtid = "4"; ViewBag.CourtRefNo = "ENFORCEMENT NO"; ViewBag.HeadingText = "ENFORCEMENT REGISTRATION"; HeaderClass = "CaseRegisterCaseLevelEnforcement"; SelectCaseLevel = "6"; CaseLevelName = "ENFORCEMENT"; }

                _courtCasesDetail = db.CourtCasesEnforcement.Where(w => w.CaseId == CaseId && w.Courtid == Courtid).FirstOrDefault();

                var CourtCases = db.CourtCase.Find(CaseId);
                var AgainstName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.CaseAgainst && w.Mst_Value == CourtCases.AgainstCode).FirstOrDefault().Mst_Desc;
                ViewBag.ClaimAmount = CourtCases.ClaimAmount;

                ViewBag.AgainstName = AgainstName;
                ViewBag.CaseLevelName = "ENFORCEMENT";

                if (_courtCasesDetail == null)
                {
                    modal = new SessionsRollVM();
                    modal.Courtid = Courtid;
                    modal.CaseLevelCode = SelectCaseLevel;

                    ViewBag.PASCourtLocationid = new SelectList(Helper.GetCourtLocationList(Courtid), "Mst_Value", "Mst_Desc");
                    ViewBag.ApealByWho = new SelectList(Helper.GetByWho(), "Mst_Value", "Mst_Desc");

                    ViewBag.hid_DetailId = "0";

                }
                else
                {
                    modal = new SessionsRollVM();

                    modal.DetailId = _courtCasesDetail.EnforcementId;
                    modal.CaseId = _courtCasesDetail.CaseId;
                    modal.CourtRefNo = _courtCasesDetail.EnforcementNo;
                    modal.PASCourtLocationid = _courtCasesDetail.CourtLocationid;
                    modal.RegistrationDate = _courtCasesDetail.RegistrationDate;
                    modal.CaseLevelCode = "6";
                    modal.ApealByWho = _courtCasesDetail.EnforcementBy;

                    ViewBag.hid_DetailId = _courtCasesDetail.EnforcementId;
                    ViewBag.PASCourtLocationid = new SelectList(Helper.GetCourtLocationList(Courtid), "Mst_Value", "Mst_Desc", modal.PASCourtLocationid);
                    ViewBag.ApealByWho = new SelectList(Helper.GetByWho(), "Mst_Value", "Mst_Desc", modal.ApealByWho);

                }

                ViewBag.hid_Courtid = Courtid;

                ViewBag.CourtClass = HeaderClass;
                ViewBag.CaseLevelCode = CaseLevelName;


                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_PayVoucherCreate")
            {
                var skipIDs = "1,2,3,13,25";
                PayVoucher payVoucher = new PayVoucher();
                ViewBag.CourtType = new SelectList(FinanceController.GetListForInvoiceCaseLevel(0), "Mst_Value", "Mst_Desc");
                ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
                ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
                ViewBag.Payment_Head = new SelectList(Helper.LoadPayForRefunableOnly(), "Mst_Value", "Mst_Desc", "10007");
                ViewBag.PaymentHeadDetail = new SelectList(Helper.LoadDependentDDL("10007"), "Mst_Value", "Mst_Desc");
                ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(), "Mst_Value", "Mst_Desc", "1901");
                ViewBag.PaymentToBenificry = new SelectList(Helper.LoadDependentDDL("1901", skipIDs), "Mst_Value", "Mst_Desc", User.Identity.Name);
                ViewBag.PayToBankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc");
                ViewBag.VoucherDate = DateTime.Now.ToString("dd/MM/yyyy");
                ViewBag.PayToMessageLang = new SelectList(Helper.GetLangForSelect(), "Mst_Value", "Mst_Desc");

                return PartialView(PartialViewName, payVoucher);
            }
            else if (PartialViewName == "_SessionFollowDIV")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.FrmMode = "E";

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                modal.PartialViewName = PartialViewName;
                mapSessionRollVM(CaseId, SessionRollId, ref modal);
                ViewBag.SessionRollId = modal.SessionRollId;
                ViewBag.HFCaseId = modal.CaseId;

                ViewBag.FollowerId = new SelectList(Helper.GetSessionFollowers(), "Mst_Value", "Mst_Desc", modal.FollowerId);
                ViewBag.SuspendedFollowerId = new SelectList(Helper.GetSessionFollowers(), "Mst_Value", "Mst_Desc", modal.SuspendedFollowerId);

                if (string.IsNullOrEmpty(modal.WorkRequired) || string.IsNullOrEmpty(modal.SessionNotes) || !modal.LastDate.HasValue)
                    ViewBag.PrintFormButton = "AppHidden";
                else
                    ViewBag.PrintFormButton = "";

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_SessionJudgementDIV")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.FrmMode = "E";

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                modal.PartialViewName = PartialViewName;
                mapSessionRollVM(CaseId, SessionRollId, ref modal);
                ViewBag.SessionRollId = modal.SessionRollId;
                ViewBag.HFCaseId = modal.CaseId;

                ViewBag.JudgementLevel = new SelectList(Helper.GetJudgementLevel(), "Mst_Value", "Mst_Desc", modal.JudgementLevel);

                ViewBag.ClientReply = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.ClientReply);
                ViewBag.TransportationSource = new SelectList(Helper.GetTransSourceSelect(), "Mst_Value", "Mst_Desc", modal.TransportationSource);

                ViewBag.HFCurrentHearingDate = modal.CurrentHearingDate?.ToString("dd/MM/yyyy");
                ViewBag.HFCourtDecision = modal.CourtDecision;
                var cases = db.CourtCase.Find(CaseId);
                string JudFilter = "Y";

                if (cases != null)
                {
                    if ((cases.ClientClassificationCode == "1" || cases.ClientClassificationCode == "2") && cases.CaseTypeCode == "1" && (cases.AgainstCode == "1" || cases.AgainstCode == "2"))
                        JudFilter = "N";
                }

                ViewBag.JudFilter = JudFilter;

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_AfterJudgementReceived")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.FrmMode = "E";

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                modal.PartialViewName = PartialViewName;
                mapSessionRollVM(CaseId, SessionRollId, ref modal);
                ViewBag.SessionRollId = modal.SessionRollId;
                ViewBag.HFCaseId = modal.CaseId;

                ViewBag.JudgementLevel = new SelectList(Helper.GetJudgementLevel(), "Mst_Value", "Mst_Desc", modal.JudgementLevel);

                ViewBag.ClientReply = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.ClientReply);
                ViewBag.TransportationSource = new SelectList(Helper.GetTransSourceSelect(), "Mst_Value", "Mst_Desc", modal.TransportationSource);

                ViewBag.HFCurrentHearingDate = modal.CurrentHearingDate?.ToString("dd/MM/yyyy");
                ViewBag.HFCourtDecision = modal.CourtDecision;
                var cases = db.CourtCase.Find(CaseId);
                string JudFilter = "Y";

                if (cases != null)
                {
                    if ((cases.ClientClassificationCode == "1" || cases.ClientClassificationCode == "2") && cases.CaseTypeCode == "1" && (cases.AgainstCode == "1" || cases.AgainstCode == "2"))
                        JudFilter = "N";
                }

                ViewBag.JudFilter = JudFilter;

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_AfterJudgementFavorable")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.FrmMode = "E";

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                modal.PartialViewName = PartialViewName;
                mapSessionRollVM(CaseId, SessionRollId, ref modal);
                ViewBag.SessionRollId = modal.SessionRollId;
                ViewBag.HFCaseId = modal.CaseId;

                ViewBag.SessionFileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterAJP), "Mst_Value", "Mst_Desc", modal.SessionFileStatus);
                ViewBag.EnforcementAdmin = new SelectList(Helper.GetEnfcAdmin(), "Mst_Value", "Mst_Desc", modal.EnforcementAdmin);

                ViewBag.ClientReply = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.ClientReply);
                ViewBag.TransportationSource = new SelectList(Helper.GetTransSourceSelect(), "Mst_Value", "Mst_Desc", modal.TransportationSource);
                ViewBag.CourtFollow = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.CourtFollow);
                ViewBag.CourtFollow_LawyerId = new SelectList(Helper.GetSessionLawyers(), "Mst_Value", "Mst_Desc", modal.CourtFollow_LawyerId);

                ViewBag.HFCurrentHearingDate = modal.CurrentHearingDate?.ToString("dd/MM/yyyy");
                ViewBag.HFCourtDecision = modal.CourtDecision;
                var cases = db.CourtCase.Find(CaseId);
                string JudFilter = "Y";

                if (cases != null)
                {
                    if ((cases.ClientClassificationCode == "1" || cases.ClientClassificationCode == "2") && cases.CaseTypeCode == "1" && (cases.AgainstCode == "1" || cases.AgainstCode == "2"))
                        JudFilter = "N";
                }

                ViewBag.JudFilter = JudFilter;

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName.In( "_SessionJudgementPrimary", "_AfterJudgementReceivedPrimary"))
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.SessionRollId = SessionRollId;
                ViewBag.HFCaseId = CaseId;
                ViewBag.FrmMode = "E";
                modal.PartialViewName = PartialViewName;

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);

                ViewBag.PrimaryIsFavorable = new SelectList(Helper.GetYNForSelectAR(), "Mst_Value", "Mst_Desc", modal.PrimaryIsFavorable);
                ViewBag.DepartmentType = new SelectList(Helper.GetInvestmentYN(), "Mst_Value", "Mst_Desc", modal.DepartmentType);

                if (PartialViewName == "_AfterJudgementReceivedPrimary")
                {
                    if (modal.PrimaryIsFavorable == "Y")
                        ViewBag.SessionFileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterAJP), "Mst_Value", "Mst_Desc", modal.SessionFileStatus);
                    else
                        ViewBag.SessionFileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterTBR), "Mst_Value", "Mst_Desc", modal.SessionFileStatus);
                }


                return PartialView(PartialViewName, modal);

            }
            else if (PartialViewName.In("_SessionJudgementAppeal", "_AfterJudgementReceivedAppeal"))
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.SessionRollId = SessionRollId;
                ViewBag.HFCaseId = CaseId;
                ViewBag.FrmMode = "E";
                modal.PartialViewName = PartialViewName;

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);

                ViewBag.AppealIsFavorable = new SelectList(Helper.GetYNForSelectAR(), "Mst_Value", "Mst_Desc", modal.AppealIsFavorable);
                ViewBag.DepartmentType = new SelectList(Helper.GetInvestmentYN(), "Mst_Value", "Mst_Desc", modal.DepartmentType);

                if (PartialViewName == "_AfterJudgementReceivedAppeal")
                {
                    if (modal.AppealIsFavorable == "Y")
                        ViewBag.SessionFileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterAJP), "Mst_Value", "Mst_Desc", modal.SessionFileStatus);
                    else
                        ViewBag.SessionFileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterTBR), "Mst_Value", "Mst_Desc", modal.SessionFileStatus);
                }

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName.In("_SessionJudgementSupremeModal", "_AfterJudgementReceivedSupreme"))
            {

                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.SessionRollId = SessionRollId;
                ViewBag.HFCaseId = CaseId;
                ViewBag.FrmMode = "E";
                modal.PartialViewName = PartialViewName;

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);

                if (PartialViewName == "_AfterJudgementReceivedSupreme")
                {
                    ViewBag.SessionFileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterAJP), "Mst_Value", "Mst_Desc", modal.SessionFileStatus);
                }

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName.In("_SessionJudgementSupreme_CaseDetail"))
            {

                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.SessionRollId = SessionRollId;
                ViewBag.HFCaseId = CaseId;
                ViewBag.FrmMode = "E";

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);
                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_SessionJudgementSupreme")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.SessionRollId = SessionRollId;
                ViewBag.HFCaseId = CaseId;
                ViewBag.FrmMode = "E";
                modal.PartialViewName = PartialViewName;

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);

                ViewBag.SessionClientId = new SelectList(Helper.GetSessionClients(), "Mst_Value", "Mst_Desc", modal.SessionClientId);
                ViewBag.SessionFileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterSR, modal.CaseLevelCode == "6" ? null : FileStatusCodesSR), "Mst_Value", "Mst_Desc", modal.SessionFileStatus);
                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName.In("_SessionJudgementEnforcement", "_AfterJudgementReceivedEnforcement"))
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.SessionRollId = SessionRollId;
                ViewBag.HFCaseId = CaseId;
                ViewBag.FrmMode = "E";
                modal.PartialViewName = PartialViewName;

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);
                FileStatusCodesTBR = Helper.getFileStatusCodesTBR(true);
                ViewBag.EnforcementIsFavorable = new SelectList(Helper.GetYNForSelectAR(), "Mst_Value", "Mst_Desc", modal.EnforcementIsFavorable);
                ViewBag.DisputeLevel = new SelectList(Helper.GetDisputeLevel(), "Mst_Value", "Mst_Desc", modal.DisputeLevel);
                ViewBag.ConsultantId = new SelectList(Helper.GetCommonNameList(), "Mst_Value", "Mst_Desc", modal.ConsultantId);
                ViewBag.FileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterTBR, FileStatusCodesTBR), "Mst_Value", "Mst_Desc", modal.FileStatus);

                if (PartialViewName == "_AfterJudgementReceivedEnforcement")
                {
                    if (modal.EnforcementIsFavorable == "Y")
                        ViewBag.SessionFileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterENF), "Mst_Value", "Mst_Desc", modal.SessionFileStatus);
                    else
                        ViewBag.SessionFileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterTBR), "Mst_Value", "Mst_Desc", modal.SessionFileStatus);
                }

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_SessionRollSupremeUpdate")
            {
                CourtCasesDetail _courtCasesDetail = new CourtCasesDetail();
                string Courtid = "3";

                _courtCasesDetail = db.CourtCasesDetail.Where(w => w.CaseId == CaseId && w.CaseLevelCode == CaseLevel).FirstOrDefault();

                SessionsRollVM modal = new SessionsRollVM();
                modal.PartialViewName = PartialViewName;

                mapSessionRollVM(CaseId, SessionRollId, ref modal);

                string CaseLevelName = string.Empty;
                string HeaderClass = string.Empty;
                string SelectCaseLevel = string.Empty;

                ViewBag.CourtRefNo = "SUPREME NO";
                ViewBag.HeadingText = "SUPREME REGISTRATION";
                HeaderClass = "CaseRegisterCaseLevelSupreme";
                SelectCaseLevel = "5";
                CaseLevelName = "SUPREME COURT";

                var CourtCases = db.CourtCase.Find(CaseId);

                var AgainstName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.CaseAgainst && w.Mst_Value == CourtCases.AgainstCode).FirstOrDefault().Mst_Desc;
                var ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == CourtCases.ClientCode).FirstOrDefault().Mst_Desc;

                ViewBag.CourtClass = HeaderClass;
                ViewBag.AgainstName = AgainstName;
                ViewBag.ClaimAmount = CourtCases.ClaimAmount;
                modal.OfficeFileNo = CourtCases.OfficeFileNo;
                modal.ClientName = ClientName;
                modal.Defendant = CourtCases.Defendant;

                if (_courtCasesDetail == null)
                {
                    modal.Courtid = Courtid;
                    modal.CaseLevelCode = SelectCaseLevel;
                    modal.SessionLevel = SelectCaseLevel;

                    modal.DetailId = 0;

                    ViewBag.PASCourtLocationid = new SelectList(Helper.GetCourtLocationList(Courtid), "Mst_Value", "Mst_Desc");
                    ViewBag.ApealByWho = new SelectList(Helper.GetByWho(true), "Mst_Value", "Mst_Desc");
                    ViewBag.CourtDepartment = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterSUP), "Mst_Value", "Mst_Desc");
                }
                else
                {
                    //modal = new SessionsRollVM();

                    modal.DetailId = _courtCasesDetail.DetailId;
                    modal.CaseId = _courtCasesDetail.CaseId;
                    modal.Courtid = _courtCasesDetail.Courtid;
                    modal.CourtRefNo = _courtCasesDetail.CourtRefNo;
                    modal.PASCourtLocationid = _courtCasesDetail.CourtLocationid;
                    modal.RegistrationDate = _courtCasesDetail.RegistrationDate;
                    modal.CourtDepartment = modal.SessionFileStatus;
                    modal.CaseLevelCode = _courtCasesDetail.CaseLevelCode;
                    modal.ApealByWho = _courtCasesDetail.ApealByWho;
                    modal.ClaimSummary = CourtCases.ClaimSummary;
                    modal.ClosureDate = _courtCasesDetail.ClosureDate;
                    modal.NextCaseLevel = _courtCasesDetail.NextCaseLevel;
                    modal.NextCaseLevelCode = _courtCasesDetail.NextCaseLevelCode;
                    modal.SessionLevel = SelectCaseLevel;

                    ViewBag.PASCourtLocationid = new SelectList(Helper.GetCourtLocationList(Courtid), "Mst_Value", "Mst_Desc", modal.PASCourtLocationid);
                    ViewBag.ApealByWho = new SelectList(Helper.GetByWho(true), "Mst_Value", "Mst_Desc", modal.ApealByWho);
                    ViewBag.CourtDepartment = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterSUP), "Mst_Value", "Mst_Desc", modal.CourtDepartment);
                }

                ViewBag.hid_Courtid = Courtid;

                ViewBag.CourtClass = HeaderClass;
                ViewBag.CaseLevelCode = CaseLevelName;

                ViewBag.FollowerId = new SelectList(Helper.GetSessionFollowers(), "Mst_Value", "Mst_Desc", modal.FollowerId);
                ViewBag.SuspendedFollowerId = new SelectList(Helper.GetSessionFollowers(), "Mst_Value", "Mst_Desc", modal.SuspendedFollowerId);

                ViewBag.CourtFollow = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.CourtFollow);
                ViewBag.CourtFollow_LawyerId = new SelectList(Helper.GetSessionLawyers(), "Mst_Value", "Mst_Desc", modal.CourtFollow_LawyerId);
                ViewBag.MoneyWith = new SelectList(Helper.GetMoneyWith(), "Mst_Value", "Mst_Desc");
                #region ADDRESS

                ViewBag.DEF_CallerName = new SelectList(Helper.GetCallerNames(), "Mst_Value", "Mst_Desc", modal.DEF_CallerName);
                ViewBag.AnnouncementTypeId = new SelectList(Helper.GetAnnouncementType(), "Mst_Value", "Mst_Desc", modal.AnnouncementTypeId);

                string LawyerDoc = Helper.GetDEF_Lawyer_Doc(modal.CaseId);
                string AddressDoc = Helper.GetDEF_Address_Doc(modal.CaseId);

                if (LawyerDoc == "#")
                {
                    ViewBag.ViewDEF_LawyerDocs = "AppHidden";
                }
                else
                {
                    ViewBag.ViewDEF_LawyerDocs = "";
                    ViewBag.DEF_Lawyer_Docs = LawyerDoc;
                }

                if (AddressDoc == "#")
                {
                    ViewBag.ViewDEF_AddressDocs = "AppHidden";
                }
                else
                {
                    ViewBag.ViewDEF_AddressDocs = "";
                    ViewBag.DEF_Address_Docs = AddressDoc;
                }

                #endregion

                #region Pay Voucher

                var skipIDs = "1,2,3,13,25";
                ViewBag.CourtType = new SelectList(Helper.GetCaseLevelList("D"), "Mst_Value", "Mst_Desc");
                ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
                ViewBag.Credit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
                ViewBag.Payment_Head = new SelectList(Helper.LoadPayForRefunableOnly(), "Mst_Value", "Mst_Desc", "10007");
                ViewBag.PaymentHeadDetail = new SelectList(Helper.LoadDependentDDL("10007"), "Mst_Value", "Mst_Desc");
                ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(), "Mst_Value", "Mst_Desc", "1901");
                ViewBag.PaymentToBenificry = new SelectList(Helper.LoadDependentDDL("1901", skipIDs), "Mst_Value", "Mst_Desc", User.Identity.Name);
                ViewBag.PayToBankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc");
                ViewBag.PayToMessageLang = new SelectList(Helper.GetLangForSelect(), "Mst_Value", "Mst_Desc");

                #endregion



                ViewBag.UpdatedOn = modal.UpdatedOn;

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_SessionFollowSupreme")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.SessionRollId = SessionRollId;
                ViewBag.HFCaseId = CaseId;
                ViewBag.FrmMode = "E";
                modal.PartialViewName = PartialViewName;

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);


                ViewBag.SessionClientId = new SelectList(Helper.GetSessionClients(), "Mst_Value", "Mst_Desc", modal.SessionClientId);
                ViewBag.FollowerId = new SelectList(Helper.GetSessionFollowers(), "Mst_Value", "Mst_Desc", modal.FollowerId);
                ViewBag.SuspendedFollowerId = new SelectList(Helper.GetSessionFollowers(), "Mst_Value", "Mst_Desc", modal.SuspendedFollowerId);

                if (string.IsNullOrEmpty(modal.WorkRequired) || string.IsNullOrEmpty(modal.SessionNotes) || !modal.LastDate.HasValue)
                    ViewBag.PrintFormButton = "AppHidden";
                else
                    ViewBag.PrintFormButton = "";

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_SessionUpdate" || PartialViewName == "_SessionUpdateSupreme")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.SessionRollId = SessionRollId;
                ViewBag.HFCaseId = CaseId;
                ViewBag.FrmMode = "E";
                modal.PartialViewName = PartialViewName;

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);

                ViewBag.ClientReply = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.ClientReply);
                ViewBag.TransportationSource = new SelectList(Helper.GetTransSourceSelect(), "Mst_Value", "Mst_Desc", modal.TransportationSource);

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_SessionUpdate_CaseDetail")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.SessionRollId = SessionRollId;
                ViewBag.HFCaseId = CaseId;
                ViewBag.FrmMode = "E";

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);

                ViewBag.ClientReply = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.ClientReply);
                ViewBag.TransportationSource = new SelectList(Helper.GetTransSourceSelect(), "Mst_Value", "Mst_Desc", modal.TransportationSource);

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_SessionClose")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.SessionRollId = SessionRollId;
                ViewBag.HFCaseId = CaseId;
                ViewBag.FrmMode = "E";
                modal.PartialViewName = PartialViewName;

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);

                ViewBag.PrintFormButton = "";
                ViewBag.NextCaseLevel = new SelectList(Helper.GetNextCaseLevel(), "Mst_Value", "Mst_Desc", modal.NextCaseLevel);

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_SessionRunningDIV")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.FrmMode = "E";
                modal.PartialViewName = PartialViewName;

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);
                ViewBag.SessionRollId = modal.SessionRollId;
                ViewBag.HFCaseId = modal.CaseId;

                ViewBag.ClientReply = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.ClientReply);
                ViewBag.TransportationSource = new SelectList(Helper.GetTransSourceSelect(), "Mst_Value", "Mst_Desc", modal.TransportationSource);

                ViewBag.CourtFollow = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.CourtFollow);
                ViewBag.CourtFollow_LawyerId = new SelectList(Helper.GetSessionLawyers(), "Mst_Value", "Mst_Desc", modal.CourtFollow_LawyerId);

                ViewBag.HFCurrentHearingDate = modal.CurrentHearingDate?.ToString("dd/MM/yyyy");
                ViewBag.HFCourtDecision = modal.CourtDecision;

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_SessionOnHoldDIV")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.FrmMode = "E";
                modal.PartialViewName = PartialViewName;

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);
                ViewBag.SessionRollId = modal.SessionRollId;
                ViewBag.HFCaseId = modal.CaseId;

                ViewBag.SessionOnHold = new SelectList(Helper.GetSessionOnHold(), "Mst_Value", "Mst_Desc", modal.SessionOnHold);
                ViewBag.ClientReply = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.ClientReply);
                ViewBag.TransportationSource = new SelectList(Helper.GetTransSourceSelect(), "Mst_Value", "Mst_Desc", modal.TransportationSource);

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_SessionOnHoldDIV_CaseDetail")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.FrmMode = "E";

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);
                ViewBag.SessionRollId = modal.SessionRollId;
                ViewBag.HFCaseId = modal.CaseId;

                ViewBag.SessionOnHold = new SelectList(Helper.GetSessionOnHold(), "Mst_Value", "Mst_Desc", modal.SessionOnHold);
                ViewBag.ClientReply = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.ClientReply);
                ViewBag.TransportationSource = new SelectList(Helper.GetTransSourceSelect(), "Mst_Value", "Mst_Desc", modal.TransportationSource);

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_SessionPauseEnfReqDIV")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.SessionRollId = SessionRollId;
                ViewBag.HFCaseId = CaseId;
                ViewBag.FrmMode = "E";
                modal.PartialViewName = PartialViewName;

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);
                ViewBag.StopEnfRequest = new SelectList(Helper.GetYNForSelect(), "Mst_Value", "Mst_Desc");

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_TBRAppeal")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.FrmMode = "E";
                modal.PartialViewName = PartialViewName;

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);
                ViewBag.SessionRollId = modal.SessionRollId;
                ViewBag.HFCaseId = modal.CaseId;

                FileStatusCodesTBR = Helper.getFileStatusCodesTBR();

                ViewBag.ConsultantId = new SelectList(Helper.GetCommonNameList(), "Mst_Value", "Mst_Desc", modal.ConsultantId);
                ViewBag.FileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterTBR, FileStatusCodesTBR), "Mst_Value", "Mst_Desc", modal.FileStatus);

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_TBRSupreme")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.FrmMode = "E";
                modal.PartialViewName = PartialViewName;

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);
                ViewBag.SessionRollId = modal.SessionRollId;
                ViewBag.HFCaseId = modal.CaseId;
                FileStatusCodesTBR = Helper.getFileStatusCodesTBR();
                ViewBag.ConsultantId = new SelectList(Helper.GetCommonNameList(), "Mst_Value", "Mst_Desc", modal.ConsultantId);
                ViewBag.FileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterTBR, FileStatusCodesTBR), "Mst_Value", "Mst_Desc", modal.FileStatus);

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_JudgIssuedControl")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.SessionRollId = SessionRollId;
                ViewBag.HFCaseId = CaseId;
                ViewBag.FrmMode = "E";
                modal.PartialViewName = PartialViewName;

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);
                ViewBag.DifferentPanelYesSet = new SelectList(Helper.GetJudgementIssued(), "Mst_Value", "Mst_Desc", modal.DifferentPanelYesSet);

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_SessionTemporaryDIV")
            {
                SessionsRollVM modal = new SessionsRollVM();

                ViewBag.SessionRollId = SessionRollId;
                ViewBag.HFCaseId = CaseId;
                ViewBag.FrmMode = "E";
                modal.PartialViewName = PartialViewName;

                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                mapSessionRollVM(CaseId, SessionRollId, ref modal);

                ViewBag.ClientReply = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.ClientReply);
                ViewBag.TransportationSource = new SelectList(Helper.GetTransSourceSelect(), "Mst_Value", "Mst_Desc", modal.TransportationSource);
                ViewBag.DeleteFromSessionDDL = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.DeleteFromSessionDDL);

                ViewBag.HFCurrentHearingDate = modal.CurrentHearingDate?.ToString("dd/MM/yyyy");
                ViewBag.HFCourtDecision = modal.CourtDecision;

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "SaveResult")
            {
                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                
                SessionsRollVM modal = GetPrintFormData(CaseId, "_PrintRequirementForm", SessionRollId);
                ViewBag.SessionRollId = modal.SessionRollId;
                ViewBag.HFCaseId = modal.CaseId;

                return PartialView(modal.FormName, modal);
            }
            else if (PartialViewName == "_SessionSuspended")
            {
                string SuspendedFollowerId = Helper.GetLawyerId(User.Identity.Name) ?? "0";

                ViewBag.Search_SuspendedFollowerId = new SelectList(Helper.GetSessionFollowers(), "Mst_Value", "Mst_Desc", SuspendedFollowerId);
                return PartialView(PartialViewName);
            }
            else if (PartialViewName == "_AfterJudgementEditForm")
            {
                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                SessionsRollVM modal = new SessionsRollVM();
                modal.PartialViewName = PartialViewName;
                mapSessionRollVM(CaseId, SessionRollId, ref modal);

                ViewBag.SessionRollId = modal.SessionRollId;
                ViewBag.HFCaseId = modal.CaseId;
                ViewBag.FrmMode = "E";

                if(CaseLevel != null)
                {
                    if (CaseLevel == "Y")
                        ViewBag.SessionFileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterAJP), "Mst_Value", "Mst_Desc", modal.SessionFileStatus);
                    else
                        ViewBag.SessionFileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterTBR), "Mst_Value", "Mst_Desc", modal.SessionFileStatus);
                }
                else
                    ViewBag.SessionFileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterAJP), "Mst_Value", "Mst_Desc", modal.SessionFileStatus);
                
                ViewBag.PrimaryIsFavorable = new SelectList(Helper.GetYNForSelectAR(), "Mst_Value", "Mst_Desc", modal.PrimaryIsFavorable);
                ViewBag.AppealIsFavorable = new SelectList(Helper.GetYNForSelectAR(), "Mst_Value", "Mst_Desc", modal.AppealIsFavorable);
                ViewBag.EnforcementIsFavorable = new SelectList(Helper.GetYNForSelectAR(), "Mst_Value", "Mst_Desc", modal.EnforcementIsFavorable);

                return PartialView(PartialViewName, modal);
            }
            else if (PartialViewName == "_AfterJDAddNewFile")
            {
                if (CaseId == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                SessionsRollVM modal = new SessionsRollVM();
                modal.PartialViewName = PartialViewName;
                modal.SavePV_Data = PartialViewName;
                mapSessionRollVM(CaseId, SessionRollId, ref modal);

                int CurrentJudgementLevel = int.Parse(modal.CaseLevelCode) - 2;
                modal.JudgementLevel = CurrentJudgementLevel.ToString();
                modal.SessionLevel = modal.CaseLevelCode;

                return PartialView(PartialViewName, modal);
            }
            else
                return PartialView(PartialViewName);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            SessionsRoll sessionRoll = new SessionsRoll();
            sessionRoll = db.SessionsRoll.Find(id);

            sessionRoll.DeletedOn = DateTime.UtcNow.AddHours(4);
            sessionRoll.DeletedBy = User.Identity.GetUserId();

            db.Entry(sessionRoll).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { redirectTo = "#btn_PayVoucherPDC" });
        }
        public ActionResult PrintForm(int id, string formName)
        {
            var sessionsRoll = db.SessionsRoll.Find(id);
            SessionsRollVM modal = new SessionsRollVM();
            if (sessionsRoll != null)
            {
                if (sessionsRoll.DeletedOn == null)
                {
                    modal.SessionRollId = sessionsRoll.SessionRollId;
                    modal.CaseId = sessionsRoll.CaseId;
                    int CaseId = modal.CaseId;
                    modal.FormName = formName;

                    var courtCases = db.CourtCase.Find(modal.CaseId);
                    var ClientName = ExtensionMethods.IsNull(courtCases.ClientCode, "0") == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == courtCases.ClientCode).FirstOrDefault().Mst_Desc;
                    var currentCaseLevelName = ExtensionMethods.IsNull(courtCases.CaseLevelCode, "0") == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.CaseLevel && w.Mst_Value == courtCases.CaseLevelCode).FirstOrDefault().Mst_Desc;


                    modal.OfficeFileNo = courtCases.OfficeFileNo;
                    modal.SessionRollClientName = ExtensionMethods.IsNull(courtCases.SessionClientId, "0") == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.SessionClients && w.Mst_Value == courtCases.SessionClientId).FirstOrDefault().Mst_Desc;
                    modal.SessionRollDefendentName = courtCases.SessionRollDefendentName;
                    modal.CurrentHearingDate = courtCases.CurrentHearingDate;
                    modal.NextHearingDate = courtCases.NextHearingDate;
                    modal.SessionLevel = sessionsRoll.SessionLevel;
                    modal.SessionLevelName = ExtensionMethods.IsNull(sessionsRoll.SessionLevel, "0") == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.SessionLevel && w.Mst_Value == sessionsRoll.SessionLevel).FirstOrDefault().Mst_Desc;
                    modal.CurrentCaseLevel = currentCaseLevelName;
                    modal.BeforeDate = sessionsRoll.BeforeDate;
                    modal.Requirements = courtCases.Requirements;
                    modal.FollowNotes = sessionsRoll.FollowNotes;
                    modal.CountLocationName = ExtensionMethods.IsNull(sessionsRoll.CountLocationId, "0") == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == sessionsRoll.CountLocationId).FirstOrDefault().Mst_Desc;
                    modal.ClientName = ClientName;
                    modal.Defendant = courtCases.Defendant;

                    var LawyerList = Helper.GetSessionFollowers();

                    if (int.Parse(courtCases.CaseLevelCode) >= 3 && int.Parse(courtCases.CaseLevelCode) < 6)
                    {
                        var courtCaseDetail = db.CourtCasesDetail.Where(w => w.CaseId == CaseId && w.CaseLevelCode == courtCases.CaseLevelCode).FirstOrDefault();

                        if (courtCaseDetail != null)
                        {

                            modal.CountLocationName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == courtCaseDetail.CourtLocationid).FirstOrDefault().Mst_Desc;
                        }
                    }
                    else if (courtCases.CaseLevelCode == "6")
                    {
                        var courtEnforcementDetail = db.CourtCasesEnforcement.Where(w => w.CaseId == CaseId).FirstOrDefault();

                        if (courtEnforcementDetail != null)
                        {
                            modal.CountLocationName = courtEnforcementDetail.CourtLocationid == "0" ? "" : db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == courtEnforcementDetail.CourtLocationid).FirstOrDefault().Mst_Desc;
                        }
                    }

                    if (modal.FormName == "_SuspendedPrintWorkForm")
                    {
                        modal.SuspendedWorkRequired = sessionsRoll.SuspendedWorkRequired;
                        modal.SuspendedSessionNotes = sessionsRoll.SuspendedSessionNotes;
                        modal.SuspendedFollowerName = ExtensionMethods.IsNull(sessionsRoll.SuspendedFollowerId, "0") == "0" ? "" : LawyerList.Where(w => w.Mst_Value == sessionsRoll.SuspendedFollowerId).FirstOrDefault().Mst_Desc;
                        modal.SuspendedLastDate = sessionsRoll.SuspendedLastDate;
                    }
                    else if (modal.FormName == "_PrintWorkForm")
                    {
                        modal.WorkRequired = sessionsRoll.WorkRequired;
                        modal.SessionNotes = sessionsRoll.SessionNotes;
                        modal.FollowerName = ExtensionMethods.IsNull(sessionsRoll.FollowerId, "0") == "0" ? "" : LawyerList.Where(w => w.Mst_Value == sessionsRoll.FollowerId).FirstOrDefault().Mst_Desc;
                        modal.LastDate = sessionsRoll.LastDate;
                    }
                    else if (modal.FormName == "_PrintRequirementForm")
                    {
                        modal = GetPrintFormData(CaseId, modal.FormName, id);
                    }

                    ViewBag.UserId = User.Identity.Name;
                    ViewBag.PrintDate = DateTime.Now.ToString("dd/MM/yyyy");
                    ViewBag.FormName = modal.FormName;
                }
            }
            return View(modal);
        }
        public ActionResult PrintRequirementForm(int id, string formName)
        {
            SessionsRollVM modal = GetPrintFormData(id, formName);
            return View(modal);
        }
        [HttpPost]
        public ActionResult GetCaseDetail(string OfficeFileNo, string ReturnDetail = null)
        {
            SessionsRollVM RetResult = new SessionsRollVM();
            string RegistrationLevel = string.Empty;
            bool foundRecord = false;
            RetResult.SessionRollId = 0;
            RetResult.CaseId = 0;
            string ClientName = string.Empty;
            string SessionRollClientName = string.Empty;

            var courtCase = db.CourtCase.Where(w => w.OfficeFileNo == OfficeFileNo && w.CaseStatus == "1").FirstOrDefault();


            if (courtCase != null)
            {
                if (!string.IsNullOrEmpty(courtCase.ClientCode) && courtCase.ClientCode != "0")
                    ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == courtCase.ClientCode).FirstOrDefault().Mst_Desc;


                if (!string.IsNullOrEmpty(courtCase.SessionClientId) && courtCase.SessionClientId != "0")
                    SessionRollClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.SessionClients && w.Mst_Value == courtCase.SessionClientId).FirstOrDefault().Mst_Desc;

                RetResult.CaseId = courtCase.CaseId;
                RetResult.ClientName = ClientName;
                RetResult.Defendant = courtCase.Defendant;
                RetResult.OfficeFileNo = courtCase.OfficeFileNo;
                RetResult.SessionRollClientName = SessionRollClientName;
                RetResult.SessionRollDefendentName = courtCase.SessionRollDefendentName;
                RetResult.AccountContractNo = courtCase.AccountContractNo;
                RetResult.ClientFileNo = courtCase.ClientFileNo;
                RetResult.buttonToGo = "";

                var NextHearingDateEx = db.CourtCase.Where(w => w.CaseId == courtCase.CaseId && w.CaseStatus == "1" && w.NextHearingDate != null).Count();

                var sessionRollExists = db.SessionsRoll.Where(w => w.CaseId == courtCase.CaseId && w.DeletedOn == null).Count();

                if (sessionRollExists > 0)
                {
                    if (sessionRollExists > 1)
                    {

                        var sessionsRoll = db.SessionsRoll.Where(w => w.CaseId == courtCase.CaseId && w.DeletedOn == null).ToList();
                        string RegistrationLevelDetail = @"<ul>";

                        foreach (var items in sessionsRoll)
                        {
                            string ActionLevelName = "";
                            DateTime? NextHearingDate = db.CourtCase.Where(w => w.CaseId == items.CaseId).FirstOrDefault().NextHearingDate;
                            bool blnReturnResult = false;

                            string CaseLevelCode = courtCase.CaseLevelCode;
                            RetResult.SessionRollId = items.SessionRollId;
                            RetResult.SessionFileStatus = items.SessionFileStatus;


                            if (items.SessionFileStatus == OfficeFileStatus.RunningCase.ToString())
                            {
                                if (NextHearingDate >= DateTime.UtcNow.AddHours(4).Date)
                                    blnReturnResult = true;
                            }
                            

                            if (blnReturnResult)
                            {
                                RetResult.RegistrationLevel = "SameTable";

                                return this.Json(RetResult, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (items.ShowFollowup)
                                {
                                    ActionLevelName = ActionLevelName == "" ? "REQUIREMENT" : ActionLevelName + ", " + "REQUIREMENT";
                                    foundRecord = true;
                                    RetResult.buttonToGo = "_SessionFollow";
                                }

                                if (items.ShowSuspend)
                                {
                                    ActionLevelName = ActionLevelName == "" ? "WRITING PLEADINGS" : ActionLevelName + ", " + "WRITING PLEADINGS";
                                    foundRecord = true;
                                    RetResult.buttonToGo = "_SessionSuspended";
                                }

                                if (items.SessionFileStatus == OfficeFileStatus.ToKnowSessionDate.ToString())
                                {
                                    ActionLevelName = ActionLevelName == "" ? "SESSION ON HOLD" : ActionLevelName + ", " + "SESSION ON HOLD";
                                    foundRecord = true;
                                    RetResult.buttonToGo = "_SessionOnHold";
                                }
                                else if (items.SessionFileStatus == OfficeFileStatus.JudgIssued.ToString())
                                {
                                    if ((items.PrimaryJudgementsDate.HasValue && items.PrimaryIsFavorable == "N") || (items.AppealJudgementsDate.HasValue && items.AppealIsFavorable == "N") || (items.SupremeJudgementsDate.HasValue) || (items.EnforcementJudgementsDate.HasValue && items.EnforcementIsFavorable == "N"))
                                    {
                                        string maxJudgementDate = Helper.GetGreatestJudgementDate(items.SessionRollId);
                                        if (!string.IsNullOrEmpty(maxJudgementDate))
                                        {
                                            DateTime MAX_DATE = new DateTime(2022, 11, 29);
                                            DateTime MAX_JUD_DATE = DateTime.ParseExact(maxJudgementDate, "dd/MM/yyyy", null);

                                            if (MAX_JUD_DATE > MAX_DATE)
                                            {
                                                ActionLevelName = ActionLevelName == "" ? "JUDGEMENT" : ActionLevelName + ", " + "JUDGEMENT";
                                                foundRecord = true;
                                                RetResult.buttonToGo = "_SessionJudgement";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (NextHearingDate.HasValue)
                                    {
                                        if (NextHearingDate >= DateTime.UtcNow.AddHours(4).Date)
                                        {
                                            ActionLevelName = ActionLevelName == "" ? "SESSION" : ActionLevelName + ", " + "SESSION";
                                            foundRecord = true;
                                        }
                                        else
                                        {
                                            ActionLevelName = ActionLevelName == "" ? "TO BE UPDATE" : ActionLevelName + ", " + "TO BE UPDATE";
                                            foundRecord = true;
                                            RetResult.buttonToGo = "_SessionTobeUpdated";
                                        }
                                    }
                                }
                            }

                            RegistrationLevelDetail += string.Format(@"<li>{0}</li>", ActionLevelName);
                        }
                        RegistrationLevelDetail += @"</ul>";

                        if (foundRecord)
                        {
                            RegistrationLevel = string.Format(@"<p>PLEASE NOTE FILE NO {0} IS ALREADY REGISTERED AND AVAILABLE IN FOLLOWING LEVEL</p>", courtCase.OfficeFileNo);
                            RegistrationLevel += RegistrationLevelDetail;
                        }
                    }
                    else
                    {
                        var items = db.SessionsRoll.Where(w => w.CaseId == courtCase.CaseId && w.DeletedOn == null).FirstOrDefault();
                        RetResult.SessionRollId = items.SessionRollId;
                        RetResult.SessionFileStatus = items.SessionFileStatus;
                        string ActionLevelName = "";

                        DateTime? NextHearingDate = db.CourtCase.Where(w => w.CaseId == items.CaseId).FirstOrDefault().NextHearingDate;
                        bool blnReturnResult = false;

                        string CaseLevelCode = courtCase.CaseLevelCode;


                        if (items.SessionFileStatus == OfficeFileStatus.RunningCase.ToString())
                        {
                            if (NextHearingDate >= DateTime.UtcNow.AddHours(4).Date)
                                blnReturnResult = true;
                        }

                        if (blnReturnResult)
                        {
                            RetResult.RegistrationLevel = "SameTable";
                            return this.Json(RetResult, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (items.ShowFollowup)
                            {
                                ActionLevelName = ActionLevelName == "" ? "REQUIREMENT" : ActionLevelName + ", " + "REQUIREMENT";
                                foundRecord = true;
                                RetResult.buttonToGo = "_SessionFollow";
                            }
                            else
                            {
                                if (items.ShowSuspend)
                                {
                                    ActionLevelName = ActionLevelName == "" ? "WRITING PLEADINGS" : ActionLevelName + ", " + "WRITING PLEADINGS";
                                    foundRecord = true;
                                    RetResult.buttonToGo = "_SessionSuspended";
                                }
                                else
                                {
                                    if (items.SessionFileStatus == OfficeFileStatus.ToKnowSessionDate.ToString())
                                    {
                                        ActionLevelName = ActionLevelName == "" ? "SESSION ON HOLD" : ActionLevelName + ", " + "SESSION ON HOLD";
                                        foundRecord = true;
                                        RetResult.buttonToGo = "_SessionOnHold";
                                    }
                                    else if (items.SessionFileStatus == OfficeFileStatus.JudgIssued.ToString())
                                    {
                                        //ActionLevelName = ActionLevelName == "" ? "JUDGEMENT" : ActionLevelName + ", " + "JUDGEMENT";
                                        if ((items.PrimaryJudgementsDate.HasValue && items.PrimaryIsFavorable == "N") || (items.AppealJudgementsDate.HasValue && items.AppealIsFavorable == "N") || (items.SupremeJudgementsDate.HasValue) || (items.EnforcementJudgementsDate.HasValue && items.EnforcementIsFavorable == "N"))
                                        {
                                            string maxJudgementDate = Helper.GetGreatestJudgementDate(items.SessionRollId);
                                            if (!string.IsNullOrEmpty(maxJudgementDate))
                                            {
                                                DateTime MAX_DATE = new DateTime(2022, 11, 29);
                                                DateTime MAX_JUD_DATE = DateTime.ParseExact(maxJudgementDate, "dd/MM/yyyy", null);

                                                if (MAX_JUD_DATE > MAX_DATE)
                                                {
                                                    ActionLevelName = ActionLevelName == "" ? "JUDGEMENT" : ActionLevelName + ", " + "JUDGEMENT";
                                                    foundRecord = true;
                                                    RetResult.buttonToGo = "_SessionJudgement";
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (NextHearingDate.HasValue)
                                        {
                                            if (NextHearingDate >= DateTime.UtcNow.AddHours(4).Date)
                                            {
                                                ActionLevelName = ActionLevelName == "" ? "SESSION" : ActionLevelName + ", " + "SESSION";
                                                foundRecord = true;
                                            }
                                            else
                                            {
                                                ActionLevelName = ActionLevelName == "" ? "TO BE UPDATE" : ActionLevelName + ", " + "TO BE UPDATE";
                                                foundRecord = true;
                                                RetResult.buttonToGo = "_SessionTobeUpdated";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (foundRecord)
                            RegistrationLevel = string.Format(@"PLEASE NOTE FILE NO {0} IS ALREADY REGISTERED IN {1} LEVEL", courtCase.OfficeFileNo, ActionLevelName);
                    }
                }
            }

            RetResult.RegistrationLevel = RegistrationLevel;

            return this.Json(RetResult, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult GetCaseDetailAfterJudgement(string OfficeFileNo, string ReturnDetail = null)
        {
            SessionsRollVM RetResult = new SessionsRollVM();
            string RegistrationLevel = string.Empty;
            bool foundRecord = false;
            RetResult.SessionRollId = 0;
            RetResult.CaseId = 0;
            string ClientName = string.Empty;
            string SessionRollClientName = string.Empty;
            string ActionLevelName = "";

            var courtCase = db.CourtCase.Where(w => w.OfficeFileNo == OfficeFileNo && w.CaseStatus == "1").FirstOrDefault();


            if (courtCase != null)
            {
                if (!string.IsNullOrEmpty(courtCase.ClientCode) && courtCase.ClientCode != "0")
                    ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == courtCase.ClientCode).FirstOrDefault().Mst_Desc;


                if (!string.IsNullOrEmpty(courtCase.SessionClientId) && courtCase.SessionClientId != "0")
                    SessionRollClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.SessionClients && w.Mst_Value == courtCase.SessionClientId).FirstOrDefault().Mst_Desc;

                RetResult.CaseId = courtCase.CaseId;
                RetResult.ClientName = ClientName;
                RetResult.Defendant = courtCase.Defendant;
                RetResult.OfficeFileNo = courtCase.OfficeFileNo;
                RetResult.SessionRollClientName = SessionRollClientName;
                RetResult.SessionRollDefendentName = courtCase.SessionRollDefendentName;
                RetResult.AccountContractNo = courtCase.AccountContractNo;
                RetResult.ClientFileNo = courtCase.ClientFileNo;
                RetResult.buttonToGo = "";

                var NextHearingDateEx = db.CourtCase.Where(w => w.CaseId == courtCase.CaseId && w.CaseStatus == "1" && w.NextHearingDate != null).Count();

                var sessionRollExists = db.SessionsRoll.Where(w => w.CaseId == courtCase.CaseId && w.DeletedOn == null).Count();
                var sessionsRoll = db.SessionsRoll.Where(w => w.CaseId == courtCase.CaseId && w.DeletedOn == null).ToList();
                int sessionsID = 0;

                try
                {
                    sessionsID = db.SessionsRoll.Where(w => w.CaseId == courtCase.CaseId && w.DeletedOn == null).OrderByDescending(o => o.SessionRollId).FirstOrDefault().SessionRollId;
                }
                catch(Exception ex)
                {
                    string Error = ex.Message;
                }

                if (Helper.IsExistsInAfterJD(courtCase.CaseId) == "Y")
                {
                    RetResult.SessionRollId = sessionsID;
                    RetResult.RegistrationLevel = "SameTable";
                    return this.Json(RetResult, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    RetResult.SessionRollId = sessionsID;
                    string[] values = new[] { OfficeFileStatus.CorrectingJudg.ToString(), OfficeFileStatus.PeriodOfAppeal.ToString(), OfficeFileStatus.RedStamp.ToString(), OfficeFileStatus.EnfcApproval.ToString(), OfficeFileStatus.MissingDocuments.ToString(), OfficeFileStatus.FileReview.ToString(), OfficeFileStatus.Refundable3_4.ToString() };

                    var AFJExists = values.Contains(courtCase.OfficeFileStatus);

                    if (AFJExists)
                    {
                        foundRecord = true;

                        switch (courtCase.OfficeFileStatus)
                        {
                            #region CorrectingJudg
                            case "OFS-34":
                                RetResult.buttonToGo = "_SessionJudgementCorrected";
                                ActionLevelName = "JUDGMENT CORRECTED";
                                break;
                            #endregion
                            #region PeriodOfAppeal
                            case "OFS-35":
                                RetResult.buttonToGo = "_SessionPeriodOfAppeal";
                                ActionLevelName = "PERIOD OF APPEAL";
                                break;
                            #endregion
                            #region RedStamp
                            case "OFS-36":
                                RetResult.buttonToGo = "_SessionRedStamp";
                                ActionLevelName = "RED STAMP";
                                break;
                            #endregion
                            #region EnfcApproval
                            case "OFS-37":
                                RetResult.buttonToGo = "_SessionEnfcApproval";
                                ActionLevelName = "ENFORCEMENT APPROVALS";
                                break;
                            #endregion
                            #region MissingDocuments
                            case "OFS-63":
                                RetResult.buttonToGo = "_SessionMissingDocuments";
                                ActionLevelName = "MISSING DOCUMENT";
                                break;
                            #endregion
                            #region FileReview
                            case "OFS-39":
                                RetResult.buttonToGo = "_SessionFileReview";
                                ActionLevelName = "FILE REVIEW";
                                break;
                            #endregion
                            #region Refundable3_4
                            case "OFS-38":
                                RetResult.buttonToGo = "_SessionRefund34";
                                ActionLevelName = "REFUND 3/4";
                                break;
                                #endregion
                        }

                        RegistrationLevel = string.Format(@"PLEASE NOTE FILE NO {0} IS ALREADY EXISTS IN {1} ", courtCase.OfficeFileNo, ActionLevelName);
                        RetResult.RegistrationLevel = RegistrationLevel;
                        return this.Json(RetResult, JsonRequestBehavior.AllowGet);
                    }
                }

                if (sessionRollExists > 0)
                {
                    if (sessionRollExists > 1)
                    {

                        string RegistrationLevelDetail = @"<ul>";

                        foreach (var items in sessionsRoll)
                        {
                            ActionLevelName = "";
                            DateTime? NextHearingDate = db.CourtCase.Where(w => w.CaseId == items.CaseId).FirstOrDefault().NextHearingDate;
                            bool blnReturnResult = false;

                            string CaseLevelCode = courtCase.CaseLevelCode;
                            RetResult.SessionRollId = items.SessionRollId;
                            RetResult.SessionFileStatus = items.SessionFileStatus;


                            if (items.SessionFileStatus == OfficeFileStatus.RunningCase.ToString())
                            {
                                if (NextHearingDate >= DateTime.UtcNow.AddHours(4).Date)
                                    blnReturnResult = true;
                            }


                            if (blnReturnResult)
                            {
                                ActionLevelName = ActionLevelName == "" ? "SESSION UPDATE" : ActionLevelName + ", " + "SESSION UPDATE";
                                
                                RetResult.RegistrationLevel = string.Format(@"PLEASE NOTE FILE NO {0} IS ALREADY REGISTERED IN {1} LEVEL", courtCase.OfficeFileNo, ActionLevelName);
                                RetResult.buttonToGo = "_AllSessions";
                                return this.Json(RetResult, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (items.ShowFollowup)
                                {
                                    ActionLevelName = ActionLevelName == "" ? "REQUIREMENT" : ActionLevelName + ", " + "REQUIREMENT";
                                    foundRecord = true;
                                    RetResult.buttonToGo = "_SessionFollow";
                                    
                                }

                                if (items.ShowSuspend)
                                {
                                    ActionLevelName = ActionLevelName == "" ? "WRITING PLEADINGS" : ActionLevelName + ", " + "WRITING PLEADINGS";
                                    foundRecord = true;
                                    RetResult.buttonToGo = "_SessionSuspended";
                                }

                                if (items.SessionFileStatus == OfficeFileStatus.ToKnowSessionDate.ToString())
                                {
                                    ActionLevelName = ActionLevelName == "" ? "SESSION ON HOLD" : ActionLevelName + ", " + "SESSION ON HOLD";
                                    foundRecord = true;
                                    RetResult.buttonToGo = "_SessionOnHold";
                                }
                                else if (items.SessionFileStatus == OfficeFileStatus.JudgIssued.ToString())
                                {
                                    if ((items.PrimaryJudgementsDate.HasValue && items.PrimaryIsFavorable == "N") || (items.AppealJudgementsDate.HasValue && items.AppealIsFavorable == "N") || (items.SupremeJudgementsDate.HasValue) || (items.EnforcementJudgementsDate.HasValue && items.EnforcementIsFavorable == "N"))
                                    {
                                        string maxJudgementDate = Helper.GetGreatestJudgementDate(items.SessionRollId);
                                        if (!string.IsNullOrEmpty(maxJudgementDate))
                                        {
                                            DateTime MAX_DATE = new DateTime(2022, 11, 29);
                                            DateTime MAX_JUD_DATE = DateTime.ParseExact(maxJudgementDate, "dd/MM/yyyy", null);

                                            if (MAX_JUD_DATE > MAX_DATE)
                                            {
                                                ActionLevelName = ActionLevelName == "" ? "JUDGEMENT" : ActionLevelName + ", " + "JUDGEMENT";
                                                foundRecord = true;
                                                RetResult.buttonToGo = "_SessionJudgement";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (NextHearingDate.HasValue)
                                    {
                                        if (NextHearingDate >= DateTime.UtcNow.AddHours(4).Date)
                                        {
                                            ActionLevelName = ActionLevelName == "" ? "SESSION" : ActionLevelName + ", " + "SESSION";
                                            foundRecord = true;
                                        }
                                        else
                                        {
                                            ActionLevelName = ActionLevelName == "" ? "TO BE UPDATE" : ActionLevelName + ", " + "TO BE UPDATE";
                                            foundRecord = true;
                                            RetResult.buttonToGo = "_SessionTobeUpdated";
                                        }
                                    }
                                }
                            }

                            RegistrationLevelDetail += string.Format(@"<li>{0}</li>", ActionLevelName);
                        }
                        RegistrationLevelDetail += @"</ul>";

                        if (foundRecord)
                        {
                            RegistrationLevel = string.Format(@"<p>PLEASE NOTE FILE NO {0} IS ALREADY REGISTERED AND AVAILABLE IN FOLLOWING LEVEL</p>", courtCase.OfficeFileNo);
                            RegistrationLevel += RegistrationLevelDetail;
                        }
                    }
                    else
                    {
                        var items = db.SessionsRoll.Where(w => w.CaseId == courtCase.CaseId && w.DeletedOn == null).FirstOrDefault();
                        RetResult.SessionRollId = items.SessionRollId;
                        RetResult.SessionFileStatus = items.SessionFileStatus;

                        DateTime? NextHearingDate = db.CourtCase.Where(w => w.CaseId == items.CaseId).FirstOrDefault().NextHearingDate;
                        bool blnReturnResult = false;

                        string CaseLevelCode = courtCase.CaseLevelCode;

                        if (items.SessionFileStatus == OfficeFileStatus.RunningCase.ToString())
                        {
                            if (NextHearingDate >= DateTime.UtcNow.AddHours(4).Date)
                                blnReturnResult = true;
                        }

                        if (blnReturnResult)
                        {
                            ActionLevelName = ActionLevelName == "" ? "SESSION UPDATE" : ActionLevelName + ", " + "SESSION UPDATE";

                            RetResult.RegistrationLevel = string.Format(@"PLEASE NOTE FILE NO {0} IS ALREADY REGISTERED IN {1} LEVEL", courtCase.OfficeFileNo, ActionLevelName);
                            RetResult.buttonToGo = "_AllSessions";
                            return this.Json(RetResult, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (items.ShowFollowup)
                            {
                                ActionLevelName = ActionLevelName == "" ? "REQUIREMENT" : ActionLevelName + ", " + "REQUIREMENT";
                                foundRecord = true;
                                RetResult.buttonToGo = "_SessionFollow";
                            }
                            else
                            {
                                if (items.ShowSuspend)
                                {
                                    ActionLevelName = ActionLevelName == "" ? "WRITING PLEADINGS" : ActionLevelName + ", " + "WRITING PLEADINGS";
                                    foundRecord = true;
                                    RetResult.buttonToGo = "_SessionSuspended";
                                }
                                else
                                {
                                    if (items.SessionFileStatus == OfficeFileStatus.ToKnowSessionDate.ToString())
                                    {
                                        ActionLevelName = ActionLevelName == "" ? "SESSION ON HOLD" : ActionLevelName + ", " + "SESSION ON HOLD";
                                        foundRecord = true;
                                        RetResult.buttonToGo = "_SessionOnHold";
                                    }
                                    else if (items.SessionFileStatus == OfficeFileStatus.JudgIssued.ToString())
                                    {
                                        //ActionLevelName = ActionLevelName == "" ? "JUDGEMENT" : ActionLevelName + ", " + "JUDGEMENT";
                                        if ((items.PrimaryJudgementsDate.HasValue && items.PrimaryIsFavorable == "N") || (items.AppealJudgementsDate.HasValue && items.AppealIsFavorable == "N") || (items.SupremeJudgementsDate.HasValue) || (items.EnforcementJudgementsDate.HasValue && items.EnforcementIsFavorable == "N"))
                                        {
                                            string maxJudgementDate = Helper.GetGreatestJudgementDate(items.SessionRollId);
                                            if (!string.IsNullOrEmpty(maxJudgementDate))
                                            {
                                                DateTime MAX_DATE = new DateTime(2022, 11, 29);
                                                DateTime MAX_JUD_DATE = DateTime.ParseExact(maxJudgementDate, "dd/MM/yyyy", null);

                                                if (MAX_JUD_DATE > MAX_DATE)
                                                {
                                                    ActionLevelName = ActionLevelName == "" ? "JUDGEMENT" : ActionLevelName + ", " + "JUDGEMENT";
                                                    foundRecord = true;
                                                    RetResult.buttonToGo = "_SessionJudgement";
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (NextHearingDate.HasValue)
                                        {
                                            if (NextHearingDate >= DateTime.UtcNow.AddHours(4).Date)
                                            {
                                                ActionLevelName = ActionLevelName == "" ? "SESSION" : ActionLevelName + ", " + "SESSION";
                                                foundRecord = true;
                                            }
                                            else
                                            {
                                                ActionLevelName = ActionLevelName == "" ? "TO BE UPDATE" : ActionLevelName + ", " + "TO BE UPDATE";
                                                foundRecord = true;
                                                RetResult.buttonToGo = "_SessionTobeUpdated";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (foundRecord)
                            RegistrationLevel = string.Format(@"PLEASE NOTE FILE NO {0} IS ALREADY REGISTERED IN {1} LEVEL", courtCase.OfficeFileNo, ActionLevelName);
                    }
                }
            }

            RetResult.RegistrationLevel = RegistrationLevel;

            return this.Json(RetResult, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult GetPrintSessionData(SessionsRollVM modal)
        {
            return View(modal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PayVoucherCreate(PayVoucher payVoucher, HttpPostedFileBase uploadPVSupDocs)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            string UploadRoot = Helper.GetStorageRoot;

            if (ModelState.IsValid)
            {
                if(payVoucher.VoucherType == "1")
                {
                    if (string.IsNullOrEmpty(payVoucher.CaseId.ToString()) || payVoucher.CaseId == 0)
                    {
                        var courtCase = db.CourtCase.Where(w => w.OfficeFileNo == payVoucher.OfficeFileNoSessionRollPV).FirstOrDefault();
                        payVoucher.CaseId = courtCase.CaseId;
                    }
                }

                payVoucher.Voucher_Date = DateTime.UtcNow.AddHours(4);
                string UserLocation = Helper.GetEmployeeLocation(User.Identity.Name);

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

                return Json(new { redirectTo = "#btnPVCreate" });
            }

            return PartialView(payVoucher);
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
