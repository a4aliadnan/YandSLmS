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
    public class CaseRegistrationController : Controller
    {
        private RBACDbContext db = new RBACDbContext();
        private string OfficeFileFilterTBR = Helper.getOfficeFileFilterTBR();
        private string OfficeFileFilterSR = Helper.getOfficeFileFilterSR();
        private string OfficeFileFilterENF = Helper.getOfficeFileFilterENF();
        private string OfficeFileFilterSUP = Helper.getOfficeFileFilterSUP();
        private string[] FileStatusCodesSR = Helper.getFileStatusCodesSR();
        private string[] FileStatusCodesTBR = Helper.getFileStatusCodesTBR();
        public ActionResult Index()
        {
            if (User.IsInRole("VoucherApproval") || User.IsSysAdmin())
            {
                ViewBag.LocationId = "A";
                ViewBag.UserRole = "VoucherApproval";
                ViewBag.VoucherApproval = "checked";
            }
            else
                ViewBag.LocationId = Helper.GetEmployeeLocation(User.Identity.Name).Split('-')[1];

            return View();
        }
        public ActionResult IndexMain(int? id)
        {
            if (User.IsInRole("VoucherApproval") || User.IsSysAdmin())
            {
                ViewBag.LocationId = "M";
                ViewBag.UserRole = "VoucherApproval";
                ViewBag.VoucherApproval = "checked";
            }
            else
                ViewBag.LocationId = Helper.GetEmployeeLocation(User.Identity.Name).Split('-')[1];

            ViewBag.OfficeFileNo = "";
            ViewBag.IsEdit = "";

            ViewBag.CaseRegisterActive = "";
            ViewBag.CasePaymentActive = "";
            ViewBag.CaseRegNewCaseActive = "";
            ViewBag.CaseRegAppealActive = "";
            ViewBag.CaseRegSupremeActive = "";

            ViewBag.CaseRegisterId = "0";
            ViewBag.HFCaseId = "0";
            ViewBag.HFClaimAmount = "0";
            ViewBag.Div_OfficeFileNoMain = "";
            ViewBag.Div_OfficeFileNo = "AppHidden";

            if (id == null)
            {
                ViewBag.FormMode = "CREATE";
                ViewBag.StartTAB = "btn_CasePayment";
                ViewBag.CasePaymentActive = "CasePaymentActive";
                ViewBag.LoadTable = "tablePayment";
            }
            else if (id == -1)
            {
                ViewBag.StartTAB = "btn_CaseRegNewCase";
                ViewBag.CaseRegNewCaseActive = "CaseRegNewCaseActive";
                ViewBag.LoadTable = "tableNewCases";
            }
            else
            {
                var caseRegistration = db.CaseRegistration.Find(id);
                var courtCases = db.CourtCase.Find(caseRegistration.CaseId);

                ViewBag.FormMode = "EDIT";
                ViewBag.IsEdit = "disabled";
                ViewBag.HFCaseId = caseRegistration.CaseId;
                ViewBag.HFClaimAmount = courtCases.ClaimAmount;
                ViewBag.OfficeFileNo = courtCases.OfficeFileNo;
                ViewBag.CaseRegisterId = id;
                ViewBag.StartTAB = "btn_CaseRegister";

                ViewBag.CaseRegisterActive = "CaseRegisterActive";
                ViewBag.LoadTable = "btn_CaseRegister";
                ViewBag.Div_OfficeFileNoMain = "AppHidden";
                ViewBag.Div_OfficeFileNo = "";

            }
            return View();
        }
        [HttpPost]
        public ActionResult IndexMain(CaseRegistrationVM modal, HttpPostedFileBase upload, HttpPostedFileBase uploadPVSupDocs, HttpPostedFileBase uploadPVBTDocs)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string UploadRoot = Helper.GetStorageRoot;

            MessageVM ProcessMessage = new MessageVM { Category = "Success", Message = "Data Save Successfully" };

            try
            {
                #region VALID MODAL PROCESSING

                if (ModelState.IsValid)
                {
                    CaseRegistration ModelToSave = new CaseRegistration();
                    if (!string.IsNullOrEmpty(modal.PartialViewName))
                    {
                        if (modal.CaseRegistrationId > 0) //EDIT
                        {
                            ModelToSave = db.CaseRegistration.Find(modal.CaseRegistrationId);
                            UpdateCaseRegister(modal, ref ModelToSave);

                            UpdateCourtCaseUpdate(modal);
                        }
                    }
                    
                    #region UPLOADED DOCUMENT SAVING

                    if (modal.FileStatus == OfficeFileStatus.LegalNotice.ToString())
                    {
                        //Process Attachment Legal Notice (OMAN POST)
                        if (upload != null && upload.ContentLength > 0)
                        {
                            string FileExtension = Path.GetExtension(upload.FileName);

                            string FileName = ModelToSave.CaseRegistrationId + FileExtension;

                            string UploadPath = Path.Combine(UploadRoot, "OmanPost", FileName);

                            upload.SaveAs(UploadPath);
                        }
                    }
                    else if (modal.FileStatus == OfficeFileStatus.ForPayment.ToString())
                    {
                        //Process Attachment Payment Vouchers
                        if (uploadPVSupDocs != null && uploadPVSupDocs.ContentLength > 0)
                        {
                            string FileExtension = Path.GetExtension(uploadPVSupDocs.FileName);

                            string FileName = ModelToSave.Voucher_No + FileExtension;

                            string UploadPath = Path.Combine(UploadRoot, "PVDocuments", FileName);

                            uploadPVSupDocs.SaveAs(UploadPath);
                        }

                        if (uploadPVBTDocs != null && uploadPVBTDocs.ContentLength > 0)
                        {
                            string FileExtension = Path.GetExtension(uploadPVBTDocs.FileName);

                            string FileName = ModelToSave.Voucher_No + FileExtension;

                            string UploadPath = Path.Combine(UploadRoot, "PVTransfers", FileName);

                            uploadPVBTDocs.SaveAs(UploadPath);
                        }
                    }
                    else if (modal.FileStatus == OfficeFileStatus.Registered.ToString())
                    {
                        //Process Attachment Payment Vouchers
                        if (uploadPVSupDocs != null && uploadPVSupDocs.ContentLength > 0)
                        {
                            string FileExtension = Path.GetExtension(uploadPVSupDocs.FileName);

                            string FileName = ModelToSave.Voucher_No + FileExtension;

                            string UploadPath = Path.Combine(UploadRoot, "PVDocuments", FileName);

                            uploadPVSupDocs.SaveAs(UploadPath);
                        }
                    }
                    
                    #endregion

                    #region RETURN
                    if (!string.IsNullOrEmpty(modal.PartialViewName))
                    {
                        if (!string.IsNullOrEmpty(modal.redirectTo))
                            return Json(new { redirectTo = modal.FileStatus });
                        else
                        {
                            if (modal.PartialViewName == "TBR-REGISTERED" || modal.PartialViewName == "TBR-STOP_REGISTRATION")
                            {
                                if (!string.IsNullOrEmpty(modal.retMessageWHM_Email))
                                    return Json(new { sameDTLoad = "Y", MessageWHM_Email = modal.retMessageWHM_Email, WhatsAppMessage = modal.WhatsAppMessage });
                                else
                                    return Json(new { sameDTLoad = "Y" });
                            }

                            if (modal.PartialViewName == "TBR-PAYMENT" && modal.IsShowWithLawyer == "Y")
                            {
                                if (!string.IsNullOrEmpty(modal.retMessageWHM_Email))
                                    return Json(new { ManualTrigger = "Y", OfficeFileNo = modal.OfficeFileNo, MessageWHM_Email = modal.retMessageWHM_Email, WhatsAppMessage = modal.WhatsAppMessage });
                                else
                                    return Json(new { ManualTrigger = "Y", OfficeFileNo = modal.OfficeFileNo });
                            }

                            CaseRegistrationVM ViewModal = new CaseRegistrationVM();
                            Helper.GetCaseRegistrationVM(modal.CaseRegistrationId, ref ViewModal);

                            ViewBag.FileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterTBR), "Mst_Value", "Mst_Desc", ViewModal.FileStatus);
                            ViewBag.ClientReply = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", ViewModal.ClientReply);
                            ViewBag.TransportationSource = new SelectList(Helper.GetTransSourceSelect(), "Mst_Value", "Mst_Desc", ViewModal.TransportationSource);
                            ViewBag.DepartmentType = new SelectList(Helper.GetInvestmentYN(), "Mst_Value", "Mst_Desc", ViewModal.DepartmentType);
                            ViewBag.DisputeLevel = new SelectList(Helper.GetDisputeLevel(), "Mst_Value", "Mst_Desc", ViewModal.DisputeLevel);
                            ViewBag.DisputeType = new SelectList(Helper.GetDisputeType(), "Mst_Value", "Mst_Desc", ViewModal.DisputeType);
                            ViewBag.Session_CaseType = new SelectList(Helper.GetSessionCaseType(), "Mst_Value", "Mst_Desc");
                            ViewBag.Session_LawyerId = new SelectList(Helper.GetSessionLawyers(), "Mst_Value", "Mst_Desc");
                            ViewBag.IsFavorable = new SelectList(Helper.GetYNForSelectAR(), "Mst_Value", "Mst_Desc", ViewModal.IsFavorable);

                            ViewBag.CaseRegistrationId = modal.CaseRegistrationId;
                            ViewBag.hid_DetailId = ViewModal.DetailId;

                            return PartialView("_TBR_Modify", ViewModal);
                        }
                    }
                    else
                    {
                        ProcessMessage.IsShowForm = "Y";
                        ProcessMessage.CaseId = ModelToSave.CaseId.ToString();
                        Session["Message"] = ProcessMessage;

                        if (ProcessMessage.id == -1)
                            return RedirectToAction("IndexMain", new RouteValueDictionary(new { ProcessMessage.id }));
                        else
                            return RedirectToAction("IndexMain", new RouteValueDictionary(new { id = ModelToSave.CaseRegistrationId }));

                    }
                    #endregion
                }

                #endregion

                #region INVALID MODAL RETURN

                if (!string.IsNullOrEmpty(modal.PartialViewName))
                {
                    ViewBag.ConsultantId = new SelectList(Helper.GetCommonNameList(), "Mst_Value", "Mst_Desc", modal.ConsultantId);
                    ViewBag.OnHoldReasonDDL = new SelectList(Helper.GetOnHoldReason(), "Mst_Value", "Mst_Desc", modal.FileStatus == "4" ? modal.FileStatusRemarks : "0");
                    ViewBag.OnHoldDone = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.OnHoldDone);
                    ViewBag.CourtFollow = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.CourtFollow);
                    ViewBag.LawyerId = new SelectList(Helper.GetCommonNameList(), "Mst_Value", "Mst_Desc", modal.LawyerId);
                    ViewBag.GovernorateId = new SelectList(Helper.GetGovernorate(), "Mst_Value", "Mst_Desc", modal.GovernorateId);
                    ViewBag.CourtLocationid = new SelectList(Helper.GetCourtLocationList(modal.ActionLevel), "Mst_Value", "Mst_Desc", modal.CourtLocationid);

                    ViewBag.RealEstateYesNo = new SelectList(Helper.GetYNForSelect(), "Mst_Value", "Mst_Desc", modal.RealEstateYesNo);
                    ViewBag.StopEnfRequest = new SelectList(Helper.GetYesNoForSelect(), "Mst_Value", "Mst_Desc", modal.StopEnfRequest);

                    ViewBag.FileStatus = new SelectList(Helper.GetOfficeFileStatus(OfficeFileFilterTBR), "Mst_Value", "Mst_Desc", modal.FileStatus);
                    ViewBag.ClientReply = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.ClientReply);
                    ViewBag.TransportationSource = new SelectList(Helper.GetTransSourceSelect(), "Mst_Value", "Mst_Desc", modal.TransportationSource);
                    ViewBag.DepartmentType = new SelectList(Helper.GetInvestmentYN(), "Mst_Value", "Mst_Desc", modal.DepartmentType);

                    ViewBag.DisputeLevel = new SelectList(Helper.GetDisputeLevel(), "Mst_Value", "Mst_Desc", modal.DisputeLevel);
                    ViewBag.DisputeType = new SelectList(Helper.GetDisputeType(), "Mst_Value", "Mst_Desc", modal.DisputeType);
                    ViewBag.CourtDepartment = new SelectList(Helper.GetSupremeStage(), "Mst_Value", "Mst_Desc", modal.CourtDepartment);

                    ViewBag.CaseRegistrationId = modal.CaseRegistrationId;
                    ViewBag.hid_DetailId = modal.DetailId;



                    string OmanPostDoc = Helper.GetOmanPostDoc(modal.CaseRegistrationId);

                    if (OmanPostDoc == "#")
                    {
                        ViewBag.View_OmanPostDoc = "AppHidden";
                    }
                    else
                    {
                        ViewBag.View_OmanPostDoc = "";
                        ViewBag.OmanPostDoc = OmanPostDoc;
                    }

                    string StopRegEmailsDoc = Helper.GetStopRegEmails_Doc(modal.CaseRegistrationId);

                    if (StopRegEmailsDoc == "#")
                    {
                        ViewBag.View_StopRegEmailsDoc = "AppHidden";
                    }
                    else
                    {
                        ViewBag.View_StopRegEmailsDoc = "";
                        ViewBag.StopRegEmailsDoc = StopRegEmailsDoc;
                    }

                    return Json(new {
                        Category = "Error",
                        Message = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray())
                    });
                }
                else
                {
                    ViewBag.ActionLevel = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.ActionLevel).OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc");
                    ViewBag.EnforcementDispute = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.EnforcementDispute).OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc");
                    ViewBag.CourtRegistration = new SelectList(Helper.GetCourtLocationList("1").OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc");
                    ViewBag.FileStatus = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FileStatus).OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc");
                    ViewBag.CourtReg_RegCourt = new SelectList(Helper.GetCourtLocationList("1").OrderBy(o => o.DisplaySeq), "Mst_Value", "Mst_Desc");
                    ViewBag.CourtReg_Regby = new SelectList(Helper.GetByWho(), "Mst_Value", "Mst_Desc");
                    ViewBag.OnHoldReasonDDL = new SelectList(Helper.GetOnHoldReason(), "Mst_Value", "Mst_Desc");
                    ViewBag.DepartmentType = new SelectList(Helper.GetInvestmentYN(), "Mst_Value", "Mst_Desc");

                    ViewBag.CourtType = new SelectList(Helper.GetCaseLevelList("D"), "Mst_Value", "Mst_Desc");
                    ViewBag.Debit_Account = new SelectList(Helper.GetBankList(), "Mst_Value", "Mst_Desc");
                    ViewBag.Payment_Head = new SelectList(Helper.LoadPayForRefunableOnly(), "Mst_Value", "Mst_Desc", "10007");
                    ViewBag.PaymentHeadDetail = new SelectList(Helper.LoadDependentDDL("10007"), "Mst_Value", "Mst_Desc");
                    ViewBag.Payment_To = new SelectList(Helper.GetPayToNew(), "Mst_Value", "Mst_Desc", "1901");
                    ViewBag.PaymentToBenificry = new SelectList(Helper.LoadDependentDDL("1901"), "Mst_Value", "Mst_Desc", User.Identity.Name);
                    ViewBag.PayToBankName = new SelectList(Helper.GetBanks(), "Mst_Value", "Mst_Desc");
                    ViewBag.PayToMessageLang = new SelectList(Helper.GetLangForSelect(), "Mst_Value", "Mst_Desc");
                    Session["Message"] = new MessageVM
                    {
                        Category = "Error",
                        Message = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray())
                    };

                }

                return View(modal);

                #endregion
            }
            catch (Exception ex)
            {
                Session["Message"] = new MessageVM
                {
                    Category = "Error",
                    Message = ex.Message
                };
                return View(modal);
            }

        }
        private void UpdateCaseRegister(CaseRegistrationVM modal, ref CaseRegistration ModelToSave)
        {
            if (!string.IsNullOrEmpty(modal.PartialViewName))
            {
                if (modal.CurrentTableName.In("TBR-AppSubmission", "TBR-AplDispute", "TBR-SupSubmission", "TBR-SupDispute"))
                {
                    SessionsRoll sessionsRoll = db.SessionsRoll.Find(modal.JDSessionId);

                    bool IsRemoveCR = false;
                    if (sessionsRoll != null)
                    {
                        CaseRegistrationVM ViewModal = new CaseRegistrationVM();
                        Helper.GetCaseRegistrationVM(modal.CaseRegistrationId, ref ViewModal);

                        switch (modal.JDCaseLevelCode)
                        {
                            case "3":
                                db.Entry(sessionsRoll).Entity.PrimaryJudgementsDate = modal.JudgementsDate;
                                db.Entry(sessionsRoll).Entity.PrimaryJDReceiveDate = modal.JDReceiveDate;
                                db.Entry(sessionsRoll).Entity.PrimaryIsFavorable = modal.IsFavorable;
                                break;
                            case "4":
                                db.Entry(sessionsRoll).Entity.AppealJudgementsDate = modal.JudgementsDate;
                                db.Entry(sessionsRoll).Entity.AppealJDReceiveDate = modal.JDReceiveDate;
                                db.Entry(sessionsRoll).Entity.AppealIsFavorable = modal.IsFavorable;
                                break;
                            case "5":
                                db.Entry(sessionsRoll).Entity.SupremeJudgementsDate = modal.JudgementsDate;
                                db.Entry(sessionsRoll).Entity.SupremeJDReceiveDate = modal.JDReceiveDate;
                                db.Entry(sessionsRoll).Entity.SupremeIsFavorable = modal.IsFavorable;
                                break;
                            case "6":
                                db.Entry(sessionsRoll).Entity.EnforcementJudgementsDate = modal.JudgementsDate;
                                db.Entry(sessionsRoll).Entity.EnforcementJDReceiveDate = modal.JDReceiveDate;
                                db.Entry(sessionsRoll).Entity.EnforcementIsFavorable = modal.IsFavorable;

                                break;
                        }

                        if (ViewModal.IsFavorable == "N" && modal.IsFavorable == "Y")
                            IsRemoveCR = true;
                        else
                        {
                            if (modal.IsFavorable == "N" && modal.FileStatus.In(OfficeFileStatus.PeriodOfAppeal.ToString(), OfficeFileStatus.RedStamp.ToString()))
                            {
                                IsRemoveCR = true;
                                modal.redirectTo = "YES";
                            }
                        }
                        db.Entry(sessionsRoll).State = EntityState.Modified;
                        db.SaveChanges();

                        if (IsRemoveCR)
                        {
                            db.Entry(ModelToSave).Entity.IsDeleted = true;
                            db.Entry(ModelToSave).State = EntityState.Modified;
                            db.SaveChanges();

                            return;
                        }
                    }
                }
                else
                {
                    if (modal.FileStatus.In(OfficeFileStatus.PeriodOfAppeal.ToString(), OfficeFileStatus.RedStamp.ToString()))
                    {
                        SessionsRoll sessionsRoll = db.SessionsRoll.Find(modal.JDSessionId);

                        bool IsRemoveCR = true;
                        modal.redirectTo = "YES";
                        if (sessionsRoll != null)
                        {
                            CaseRegistrationVM ViewModal = new CaseRegistrationVM();
                            Helper.GetCaseRegistrationVM(modal.CaseRegistrationId, ref ViewModal);

                            switch (modal.JDCaseLevelCode)
                            {
                                case "3":
                                    db.Entry(sessionsRoll).Entity.PrimaryJudgementsDate = modal.JudgementsDate;
                                    db.Entry(sessionsRoll).Entity.PrimaryJDReceiveDate = modal.JDReceiveDate;
                                    db.Entry(sessionsRoll).Entity.PrimaryIsFavorable = modal.IsFavorable;
                                    break;
                                case "4":
                                    db.Entry(sessionsRoll).Entity.AppealJudgementsDate = modal.JudgementsDate;
                                    db.Entry(sessionsRoll).Entity.AppealJDReceiveDate = modal.JDReceiveDate;
                                    db.Entry(sessionsRoll).Entity.AppealIsFavorable = modal.IsFavorable;
                                    break;
                                case "5":
                                    db.Entry(sessionsRoll).Entity.SupremeJudgementsDate = modal.JudgementsDate;
                                    db.Entry(sessionsRoll).Entity.SupremeJDReceiveDate = modal.JDReceiveDate;
                                    db.Entry(sessionsRoll).Entity.SupremeIsFavorable = modal.IsFavorable;
                                    break;
                                case "6":
                                    db.Entry(sessionsRoll).Entity.EnforcementJudgementsDate = modal.JudgementsDate;
                                    db.Entry(sessionsRoll).Entity.EnforcementJDReceiveDate = modal.JDReceiveDate;
                                    db.Entry(sessionsRoll).Entity.EnforcementIsFavorable = modal.IsFavorable;

                                    break;
                            }

                            db.Entry(sessionsRoll).State = EntityState.Modified;
                            db.SaveChanges();

                            if (IsRemoveCR)
                            {
                                db.Entry(ModelToSave).Entity.IsDeleted = true;
                                db.Entry(ModelToSave).State = EntityState.Modified;
                                db.SaveChanges();

                                return;
                            }
                        }
                        else
                        {
                            db.Entry(ModelToSave).Entity.IsDeleted = true;
                        }
                    }
                }

                DateTime? CommissioningDate = null;

                db.Entry(ModelToSave).Entity.FormPrintWorkRequired = modal.FormPrintWorkRequired;
                db.Entry(ModelToSave).Entity.OfficeProcedure = modal.OfficeProcedure;
                db.Entry(ModelToSave).Entity.FormPrintLastDate = modal.FormPrintLastDate;
                if (modal.IsShowWithLawyer == "Y")
                    db.Entry(ModelToSave).Entity.FileStatus = OfficeFileStatus.WithLawyer.ToString();
                else
                    db.Entry(ModelToSave).Entity.FileStatus = modal.FileStatus;


                db.Entry(ModelToSave).Entity.ActionDate = modal.ActionDate;
                db.Entry(ModelToSave).Entity.Remarks = modal.MainRemarks;

                CourtCases courtCases = db.CourtCase.Find(modal.CaseId);

                if (modal.FileStatus == ModelToSave.FileStatus)
                {
                    if(modal.CourtFollow == "1")
                    {
                        db.Entry(ModelToSave).Entity.LawyerId = modal.LawyerId;

                        db.Entry(courtCases).Entity.CourtFollow = modal.CourtFollow;
                        db.Entry(courtCases).Entity.CourtFollowRequirement = modal.CourtFollowRequirement;
                        db.Entry(courtCases).Entity.CommissioningDate = modal.CommissioningDate;
                    }
                    else
                    {
                        db.Entry(ModelToSave).Entity.LawyerId = "0";
                        db.Entry(courtCases).Entity.CourtFollow = "0";
                        db.Entry(courtCases).Entity.CourtFollowRequirement = "";
                        db.Entry(courtCases).Entity.CommissioningDate = CommissioningDate;
                    }
                }
                else
                {
                    if (modal.IsShowWithLawyer == "Y")
                    {
                        if (modal.CourtFollow == "1")
                        {
                            db.Entry(ModelToSave).Entity.LawyerId = modal.LawyerId;

                            db.Entry(courtCases).Entity.CourtFollow = modal.CourtFollow;
                            db.Entry(courtCases).Entity.CourtFollowRequirement = modal.CourtFollowRequirement;
                            db.Entry(courtCases).Entity.CommissioningDate = modal.CommissioningDate;
                        }
                        else
                        {
                            db.Entry(ModelToSave).Entity.LawyerId = "0";
                            db.Entry(courtCases).Entity.CourtFollow = "0";
                            db.Entry(courtCases).Entity.CourtFollowRequirement = "";
                            db.Entry(courtCases).Entity.CommissioningDate = CommissioningDate;
                        }
                    }
                    else
                    {
                        db.Entry(ModelToSave).Entity.LawyerId = "0";
                        db.Entry(courtCases).Entity.CourtFollow = "0";
                        db.Entry(courtCases).Entity.CourtFollowRequirement = "";
                        db.Entry(courtCases).Entity.CommissioningDate = CommissioningDate;
                    }
                }

                db.Entry(courtCases).State = EntityState.Modified;
                db.SaveChanges();


                if (int.Parse(modal.ActionLevel) == 2)
                {
                    db.Entry(ModelToSave).Entity.DepartmentType = modal.DepartmentType;
                }

                if (modal.FileStatus == OfficeFileStatus.WritingSubmission.ToString() || modal.FileStatus == OfficeFileStatus.Scanned.ToString())
                {
                    db.Entry(ModelToSave).Entity.ActionDate = DateTime.UtcNow.AddHours(4);
                }

                if (int.Parse(modal.ActionLevel) == 4)
                {
                    db.Entry(ModelToSave).Entity.DisputeLevel = modal.DisputeLevel;
                    db.Entry(ModelToSave).Entity.DisputeType = modal.DisputeType;
                }

                switch (modal.PartialViewName)
                {
                    case "TBR-TRANSFER":
                        db.Entry(ModelToSave).Entity.ConsultantId = modal.ConsultantId;
                        break;
                    case "TBR-LEGAL_NOTICE":
                        db.Entry(ModelToSave).Entity.SendDate = modal.SendDate;
                        db.Entry(ModelToSave).Entity.OmanPostNo = modal.OmanPostNo;
                        db.Entry(ModelToSave).Entity.FirstReminderDate = modal.FirstReminderDate;
                        break;
                    case "TBR-ON_HOLD":
                        db.Entry(ModelToSave).Entity.FileStatusRemarks = modal.OnHoldReasonDDL;
                        db.Entry(ModelToSave).Entity.OnHoldDone = modal.OnHoldDone;
                        break;
                    case "TBR-ONLINE_SUBMISSION":
                        db.Entry(ModelToSave).Entity.CourtLocationid = modal.CourtLocationid;
                        db.Entry(ModelToSave).Entity.ElectronicNo = modal.ElectronicNo;
                        db.Entry(ModelToSave).Entity.ClaimSummary = modal.ClaimSummary;
                        db.Entry(ModelToSave).Entity.RegistrationDate = modal.RegistrationDate;

                        courtCases = db.CourtCase.Find(modal.CaseId);

                        if (int.Parse(modal.ActionLevel) == 1)
                        {
                            db.Entry(ModelToSave).Entity.DepartmentType = modal.DepartmentType;
                            db.Entry(ModelToSave).Entity.RealEstateYesNo = modal.RealEstateYesNo;
                            db.Entry(ModelToSave).Entity.RealEstateDetail = modal.RealEstateDetail;

                            
                            db.Entry(courtCases).Entity.AgainstCode = modal.AgainstCode;
                            db.Entry(courtCases).Entity.OmaniExp = modal.OmaniExp;
                            db.Entry(courtCases).Entity.GovernorateId = modal.GovernorateId;
                            db.Entry(courtCases).Entity.ClaimAmount = modal.CourtReg_ClaimAmount;
                            db.Entry(courtCases).Entity.RealEstateYesNo = modal.RealEstateYesNo;
                            db.Entry(courtCases).Entity.RealEstateDetail = modal.RealEstateDetail;
                        }
                        

                        db.Entry(courtCases).Entity.ClaimSummary = modal.ClaimSummary;

                        if (modal.AgainstCode == "3")
                            db.Entry(courtCases).Entity.StopEnfRequest = modal.StopEnfRequest;

                        db.Entry(courtCases).State = EntityState.Modified;
                        db.SaveChanges();

                        Process_TBR_REGISTERED(modal);
                        break;
                    case "TBR-COURT_NOTES":
                        db.Entry(ModelToSave).Entity.CourtMessage = modal.CourtMessage;
                        break;
                    case "TBR-PAYMENT":
                        db.Entry(ModelToSave).Entity.CourtFeeAmount = modal.CourtFeeAmount;
                        db.Entry(ModelToSave).Entity.PaymentDate = modal.PaymentDate;

                        if (modal.Voucher_No > 0)
                            UpdateVoucher(modal);
                        else
                            CreatePayVoucher(modal, ref ModelToSave);
                        break;
                    case "TBR-WITH_LAWYER":
                        break;
                    case "TBR-REGISTERED":
                        Process_TBR_REGISTERED(modal);

                        if (modal.CaseLevelCode != "5")
                        {
                            //CREATE OR UPDATE SESSION ROLL
                            ProcessSessionRollDetail(modal);
                            /*
                            SessionsRoll sessionRoll = db.SessionsRoll.Where(w => w.CaseId == modal.CaseId && w.DeletedOn == null).OrderByDescending(o => o.SessionRollId).FirstOrDefault();
                            if (sessionRoll == null)
                            {
                                sessionRoll = new SessionsRoll();
                                sessionRoll.CaseId = modal.CaseId;
                                sessionRoll.CountLocationId = modal.CourtLocationid;
                                sessionRoll.SessionLevel = modal.CaseLevelCode;
                                sessionRoll.CaseRegistrationId = modal.CaseRegistrationId;
                                sessionRoll.CaseType = modal.Session_CaseType;
                                sessionRoll.LawyerId = modal.Session_LawyerId;

                                db.SessionsRoll.Add(sessionRoll);
                                db.SaveChanges();
                            }
                            else
                            {
                                db.Entry(sessionRoll).Entity.CountLocationId = modal.CourtLocationid;
                                db.Entry(sessionRoll).Entity.SessionLevel = modal.CaseLevelCode;
                                db.Entry(sessionRoll).Entity.CaseRegistrationId = modal.CaseRegistrationId;
                                db.Entry(sessionRoll).Entity.CaseType = modal.Session_CaseType;
                                db.Entry(sessionRoll).Entity.LawyerId = modal.Session_LawyerId;
                                db.Entry(sessionRoll).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                            db.Entry(ModelToSave).Entity.SessionRollId = sessionRoll.SessionRollId;
                            */
                        }

                        if (modal.Update_Addreass == "Y")
                            UpdateSessionDEFAddress(modal);

                        if (modal.Update_CourtTransfer == "Y")
                            CreateCourtMoneyTransfer(modal);

                        if (modal.Update_PV == "Y")
                        {
                            int Voucher_No = CreatePayVoucher(modal);
                            modal.Voucher_No = Voucher_No;
                        }

                        db.Entry(ModelToSave).Entity.IsDeleted = true;
                        db.Entry(ModelToSave).Entity.CourtLocationid = modal.CourtLocationid;
                        db.Entry(ModelToSave).Entity.RegistrationDate = modal.RegistrationDate;
                        db.Entry(ModelToSave).Entity.FileStatusRemarks = modal.CourtRefNo;

                        courtCases = db.CourtCase.Find(modal.CaseId);

                        db.Entry(courtCases).Entity.CaseLevelCode = modal.CaseLevelCode;
                        db.Entry(courtCases).Entity.SessionRemarks = modal.SessionRemarks;

                        if (modal.AgainstCode == "3")
                            courtCases.StopEnfRequest = modal.StopEnfRequest;

                        db.Entry(courtCases).State = EntityState.Modified;
                        db.SaveChanges();

                        break;
                    case "TBR-STOP_REGISTRATION":
                        db.Entry(ModelToSave).Entity.ActionDate = modal.ActionDate;
                        db.Entry(ModelToSave).Entity.StopRegEmailDate = modal.StopRegEmailDate;
                        db.Entry(ModelToSave).Entity.StopRegUserName = modal.StopRegUserName ?? User.Identity.Name;
                        db.Entry(ModelToSave).Entity.Remarks = modal.Remarks;
                        break;
                    default:
                        db.Entry(ModelToSave).Entity.ActionDate = modal.ActionDate;
                        break;
                }
            }
            else
            {
                db.Entry(ModelToSave).Entity.CaseId = modal.CaseId;
                db.Entry(ModelToSave).Entity.ActionDate = modal.ActionDate;
                db.Entry(ModelToSave).Entity.ActionLevel = modal.ActionLevel;
                db.Entry(ModelToSave).Entity.JudgementDate = modal.JudgementDate;
                db.Entry(ModelToSave).Entity.UrgentCaseDays = modal.UrgentCaseDays;
                db.Entry(ModelToSave).Entity.EnforcementDispute = modal.EnforcementDispute;
                db.Entry(ModelToSave).Entity.CourtRegistration = modal.CourtRegistration;
                db.Entry(ModelToSave).Entity.FileStatus = modal.FileStatus;

                if (int.Parse(modal.FileStatus) == 4)
                    db.Entry(ModelToSave).Entity.FileStatusRemarks = modal.OnHoldReasonDDL;
                else if (int.Parse(modal.FileStatus) == 6)
                    db.Entry(ModelToSave).Entity.ElectronicNo = modal.FileStatusRemarks;
                else if (int.Parse(modal.FileStatus) == 8 && !string.IsNullOrEmpty(modal.FileStatusRemarks))
                    db.Entry(ModelToSave).Entity.CourtFeeAmount = decimal.Parse(modal.FileStatusRemarks);
                else
                    db.Entry(ModelToSave).Entity.FileStatusRemarks = modal.FileStatusRemarks;

                db.Entry(ModelToSave).Entity.CourtMessage = modal.CourtMessage;

                db.Entry(ModelToSave).Entity.SendDate = modal.SendDate;
                db.Entry(ModelToSave).Entity.FirstReminderDate = modal.FirstReminderDate;
                db.Entry(ModelToSave).Entity.ReminderNo = modal.ReminderNo;
                db.Entry(ModelToSave).Entity.CourtRequestDate = modal.CourtRequestDate;
                db.Entry(ModelToSave).Entity.OfficeProcedure = modal.OfficeProcedure;
                db.Entry(ModelToSave).Entity.PaymentDate = modal.PaymentDate;
                db.Entry(ModelToSave).Entity.CourtDetailRegistered = modal.CourtDetailRegistered; //THIS IS URGENT NOW BOOLEAN
                db.Entry(ModelToSave).Entity.AdminFile = modal.AdminFile;
                db.Entry(ModelToSave).Entity.OmanPostNo = modal.OmanPostNo;
                db.Entry(ModelToSave).Entity.DepartmentType = modal.DepartmentType;
                db.Entry(ModelToSave).Entity.Voucher_No = modal.Voucher_No;
                db.Entry(ModelToSave).Entity.FormPrintDefendant = modal.FormPrintDefendant;
                db.Entry(ModelToSave).Entity.FormPrintLastDate = modal.FormPrintLastDate;
                db.Entry(ModelToSave).Entity.FormPrintWorkRequired = modal.FormPrintWorkRequired;
                db.Entry(ModelToSave).Entity.Remarks = modal.MainRemarks;
                db.Entry(ModelToSave).Entity.RealEstateYesNo = modal.RealEstateYesNo;
                db.Entry(ModelToSave).Entity.RealEstateDetail = modal.RealEstateDetail;
                db.Entry(ModelToSave).Entity.ClaimSummary = modal.ClaimSummary;

            }

            db.Entry(ModelToSave).State = EntityState.Modified;
            db.SaveChanges();
        }
        private void Process_TBR_REGISTERED(CaseRegistrationVM modal)
        {
            if(modal.PartialViewName == "TBR-ONLINE_SUBMISSION")
            {
                if (modal.ActionLevel == "4")
                {
                    var ObjToUpdate = db.CourtCasesEnforcement.Where(w => w.CaseId == modal.CaseId).FirstOrDefault();

                    if (ObjToUpdate != null)
                    {
                        db.Entry(ObjToUpdate).Entity.EnforcementNo = modal.ElectronicNo;
                        db.Entry(ObjToUpdate).Entity.RegistrationDate = modal.ActionDate;
                        db.Entry(ObjToUpdate).Entity.CourtLocationid = modal.CourtLocationid;

                        db.Entry(ObjToUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        //CREATE NEW DETAIL
                        CourtCasesEnforcement _ModalToSave = new CourtCasesEnforcement();

                        _ModalToSave.CaseId = modal.CaseId;
                        _ModalToSave.Courtid = "4";
                        _ModalToSave.EnforcementBy = "0";
                        _ModalToSave.CourtLocationid = modal.CourtLocationid;
                        _ModalToSave.EnforcementNo = modal.ElectronicNo;
                        _ModalToSave.RegistrationDate = modal.ActionDate;

                        db.CourtCasesEnforcement.Add(_ModalToSave);
                        db.SaveChanges();
                    }
                }
                else
                {
                    var ObjToUpdate = db.CourtCasesDetail.Where(w => w.CaseId == modal.CaseId && w.Courtid == modal.ActionLevel).FirstOrDefault();

                    if (ObjToUpdate != null)
                    {

                        db.Entry(ObjToUpdate).Entity.CourtLocationid = modal.CourtLocationid;
                        db.Entry(ObjToUpdate).Entity.CourtRefNo = modal.ElectronicNo;
                        db.Entry(ObjToUpdate).Entity.RegistrationDate = modal.RegistrationDate;

                        db.Entry(ObjToUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        //CREATE NEW DETAIL
                        CourtCasesDetail _ModalToSave = new CourtCasesDetail();
                        string SelectCaseLevel = (int.Parse(modal.ActionLevel) + 2).ToString();

                        _ModalToSave.CaseId = modal.CaseId;
                        _ModalToSave.Courtid = modal.ActionLevel;
                        _ModalToSave.CourtLocationid = modal.CourtLocationid;
                        _ModalToSave.CourtRefNo = modal.ElectronicNo;
                        _ModalToSave.RegistrationDate = modal.RegistrationDate;
                        _ModalToSave.CaseLevelCode = SelectCaseLevel;

                        db.CourtCasesDetail.Add(_ModalToSave);
                        db.SaveChanges();
                    }

                }
            }
            else
            {
                if (modal.ActionLevel == "4")
                {
                    var ObjToUpdate = db.CourtCasesEnforcement.Where(w => w.CaseId == modal.CaseId).FirstOrDefault();

                    if (ObjToUpdate != null)
                    {
                        if(modal.DisputeType == "1")
                        {
                            if (modal.DisputeLevel == "1")
                            {
                                db.Entry(ObjToUpdate).Entity.PrimaryObjectionNo = modal.PrimaryObjectionNo;
                                db.Entry(ObjToUpdate).Entity.PrimaryObjectionCourt = modal.PrimaryObjectionCourt;

                            }
                            else if (modal.DisputeLevel == "2")
                            {
                                db.Entry(ObjToUpdate).Entity.ApealObjectionNo = modal.ApealObjectionNo;
                                db.Entry(ObjToUpdate).Entity.ApealObjectionCourt = modal.ApealObjectionCourt;

                            }
                            else if (modal.DisputeLevel == "3")
                            {
                                db.Entry(ObjToUpdate).Entity.SupremeObjectionNo = modal.SupremeObjectionNo;
                                db.Entry(ObjToUpdate).Entity.SupremeObjectionCourt = modal.SupremeObjectionCourt;

                            }
                        }
                        else if (modal.DisputeType == "2")
                        {
                            if (modal.DisputeLevel == "1")
                            {
                                db.Entry(ObjToUpdate).Entity.PrimaryPlaintNo = modal.PrimaryPlaintNo;
                                db.Entry(ObjToUpdate).Entity.PrimaryPlaintCourt = modal.PrimaryPlaintCourt;

                            }
                            else if (modal.DisputeLevel == "2")
                            {
                                db.Entry(ObjToUpdate).Entity.ApealPlaintNo = modal.ApealPlaintNo;
                                db.Entry(ObjToUpdate).Entity.ApealPlaintCourt = modal.ApealPlaintCourt;

                            }
                            else if (modal.DisputeLevel == "3")
                            {
                                db.Entry(ObjToUpdate).Entity.SupremePlaintNo = modal.SupremePlaintNo;
                                db.Entry(ObjToUpdate).Entity.SupremePlaintCourt = modal.SupremePlaintCourt;

                            }
                        }

                        if (modal.DisputeLevel == "3")
                        {
                            db.Entry(ObjToUpdate).Entity.CourtDepartment = modal.CourtDepartment;

                        }

                        db.Entry(ObjToUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        //CREATE NEW DETAIL
                        CourtCasesEnforcement _ModalToSave = new CourtCasesEnforcement();
                        string SelectCaseLevel = (int.Parse(modal.ActionLevel) + 2).ToString();

                        _ModalToSave.CaseId = modal.CaseId;
                        _ModalToSave.Courtid = modal.ActionLevel;
                        _ModalToSave.EnforcementBy = modal.ApealByWho;
                        _ModalToSave.CourtLocationid = modal.CourtLocationid;
                        _ModalToSave.EnforcementNo = modal.CourtRefNo;
                        _ModalToSave.RegistrationDate = modal.RegistrationDate;
                        _ModalToSave.CaseLevelCode = SelectCaseLevel;

                        db.CourtCasesEnforcement.Add(_ModalToSave);
                        db.SaveChanges();

                        //CourtCases courtCases = db.CourtCase.Find(_ModalToSave.CaseId);
                        //courtCases.CaseLevelCode = SelectCaseLevel;
                        //db.Entry(courtCases).State = EntityState.Modified;
                        //db.SaveChanges();
                    }
                }
                else
                {
                    var ObjToUpdate = db.CourtCasesDetail.Where(w => w.CaseId == modal.CaseId && w.CaseLevelCode == modal.CaseLevelCode).FirstOrDefault();

                    if (ObjToUpdate != null)
                    {
                        string SelectCaseLevel = modal.CaseLevelCode; // (int.Parse(modal.ActionLevel) + 2).ToString();
                        string Courtid = (int.Parse(modal.CaseLevelCode) - 2).ToString();
                        db.Entry(ObjToUpdate).Entity.ApealByWho = modal.ApealByWho;
                        db.Entry(ObjToUpdate).Entity.CourtDepartment = modal.CourtDepartment;
                        db.Entry(ObjToUpdate).Entity.CourtLocationid = modal.CourtLocationid;
                        db.Entry(ObjToUpdate).Entity.CourtRefNo = modal.CourtRefNo;
                        db.Entry(ObjToUpdate).Entity.RegistrationDate = modal.RegistrationDate;
                        db.Entry(ObjToUpdate).Entity.CaseLevelCode = SelectCaseLevel;

                        db.Entry(ObjToUpdate).State = EntityState.Modified;
                        db.SaveChanges();

                        //CourtCases courtCases = db.CourtCase.Find(ObjToUpdate.CaseId);
                        //courtCases.CaseLevelCode = SelectCaseLevel;
                        //db.Entry(courtCases).State = EntityState.Modified;
                        //db.SaveChanges();
                    }
                    else
                    {
                        //CREATE NEW DETAIL
                        CourtCasesDetail _ModalToSave = new CourtCasesDetail();
                        string SelectCaseLevel = modal.CaseLevelCode; // (int.Parse(modal.ActionLevel) + 2).ToString();
                        string Courtid = (int.Parse(modal.CaseLevelCode) - 2).ToString();

                        _ModalToSave.CaseId = modal.CaseId;
                        _ModalToSave.Courtid = Courtid;
                        _ModalToSave.ApealByWho = modal.ApealByWho;
                        _ModalToSave.CourtDepartment = modal.CourtDepartment;
                        _ModalToSave.CourtLocationid = modal.CourtLocationid;
                        _ModalToSave.CourtRefNo = modal.CourtRefNo;
                        _ModalToSave.RegistrationDate = modal.RegistrationDate;
                        _ModalToSave.CaseLevelCode = SelectCaseLevel;

                        db.CourtCasesDetail.Add(_ModalToSave);
                        db.SaveChanges();

                        //CourtCases courtCases = db.CourtCase.Find(_ModalToSave.CaseId);
                        //courtCases.CaseLevelCode = SelectCaseLevel;
                        //db.Entry(courtCases).State = EntityState.Modified;
                        //db.SaveChanges();
                    }

                }

            }
        }
        private void CreatePayVoucher(CaseRegistrationVM modal, ref CaseRegistration ModelToSave)
        {
            if (User.IsInRole("VoucherApproval") || User.Identity.Name.Equals("6"))
            {
                if (modal.PaymentDate != null && modal.Amount > 0)
                {
                    string UserLocation = Helper.GetEmployeeLocation(User.Identity.Name);
                    

                    PayVoucher payVoucher = new PayVoucher();

                    string strPVLocation = string.Empty;

                    if (modal.CaseId > 0)
                        strPVLocation = Helper.getPVLocation(modal.CaseId);
                    else
                        strPVLocation = UserLocation.Substring(0, 3).ToUpper();

                    payVoucher.PVLocation = strPVLocation;

                    payVoucher.Voucher_Date = DateTime.UtcNow.AddHours(4);
                    payVoucher.Payment_Type = "1";
                    payVoucher.Payment_Mode = "3";//3 BANK TRANSFER
                    payVoucher.VoucherType = "1"; //1 REFUNDABLE
                    payVoucher.Status = "1";
                    payVoucher.VoucherStatus = "1";
                    payVoucher.LocationCode = "0";
                    payVoucher.Credit_Account = 0;
                    payVoucher.TransTypeCode = "-2";
                    payVoucher.TransReasonCode = "0";
                    payVoucher.CaseId = modal.CaseId;

                    payVoucher.Cheque_Date = modal.Cheque_Date;
                    payVoucher.CourtType = modal.CourtType;
                    payVoucher.Payment_Head = modal.Payment_Head;
                    payVoucher.PaymentHeadDetail = modal.PaymentHeadDetail;
                    payVoucher.Remarks = modal.Remarks;
                    payVoucher.Payment_To = modal.Payment_To;
                    payVoucher.PaymentToBenificry = modal.PaymentToBenificry;
                    payVoucher.Amount = modal.Amount;
                    payVoucher.VatAmount = modal.VatAmount;
                    payVoucher.BillNumber = modal.BillNumber;
                    payVoucher.Debit_Account = modal.Debit_Account;
                    payVoucher.Cheque_Number = modal.Cheque_Number;
                    payVoucher.Cheque_Date = modal.Cheque_Date;

                    db.PayVouchers.Add(payVoucher);
                    db.SaveChanges();


                    payVoucher.AuthorizeBy = User.Identity.GetUserId();
                    payVoucher.AuthorizeDate = DateTime.Now;

                    string PV_Vooucher = Helper.GeneratePVNumber(payVoucher.Voucher_No);
                    payVoucher.PV_No = PV_Vooucher;
                    ModelToSave.Voucher_No = payVoucher.Voucher_No;

                    db.Entry(payVoucher).State = EntityState.Modified;
                    db.SaveChanges();

                    if (!string.IsNullOrEmpty(payVoucher.Cheque_Number))
                    {
                        string strMessage = string.Empty;

                        modal.WhatsAppMessage = Helper.GetWhatsApp(payVoucher.Voucher_No, User.Identity.Name, out strMessage);
                        modal.retMessageWHM_Email = strMessage;
                    }
                }
            }
        }
        private void UpdateVoucher(CaseRegistrationVM modal)
        {
            if (User.IsInRole("VoucherApproval") || User.Identity.Name.Equals("6"))
            {
                if (modal.PaymentDate != null && modal.Amount > 0)
                {
                    var payVoucher = db.PayVouchers.Find(modal.Voucher_No);

                    db.Entry(payVoucher).Entity.CaseId = modal.CaseId;

                    db.Entry(payVoucher).Entity.Cheque_Date = modal.Cheque_Date;
                    db.Entry(payVoucher).Entity.CourtType = modal.CourtType;
                    db.Entry(payVoucher).Entity.Payment_Head = modal.Payment_Head;
                    db.Entry(payVoucher).Entity.PaymentHeadDetail = modal.PaymentHeadDetail;
                    db.Entry(payVoucher).Entity.Remarks = modal.Remarks;
                    db.Entry(payVoucher).Entity.Payment_To = modal.Payment_To;
                    db.Entry(payVoucher).Entity.PaymentToBenificry = modal.PaymentToBenificry;
                    db.Entry(payVoucher).Entity.Amount = modal.Amount;
                    db.Entry(payVoucher).Entity.VatAmount = modal.VatAmount;
                    db.Entry(payVoucher).Entity.BillNumber = modal.BillNumber;
                    db.Entry(payVoucher).Entity.Debit_Account = modal.Debit_Account;
                    db.Entry(payVoucher).Entity.Cheque_Number = modal.Cheque_Number;
                    db.Entry(payVoucher).Entity.Cheque_Date = modal.Cheque_Date;

                    db.Entry(payVoucher).State = EntityState.Modified;
                    db.SaveChanges();

                    if (!string.IsNullOrEmpty(payVoucher.Cheque_Number))
                    {
                        string strMessage = string.Empty;
                        modal.WhatsAppMessage = Helper.GetWhatsApp(payVoucher.Voucher_No, User.Identity.Name, out strMessage);
                        modal.retMessageWHM_Email = strMessage;
                    }
                }
            }
        }
        private void SoftDeleteCaseRegister(ref CaseRegistration ModelToSave)
        {
            db.Entry(ModelToSave).Entity.IsDeleted = true;
            db.Entry(ModelToSave).State = EntityState.Modified;
            db.SaveChanges();

        }
        private void UpdateCourtCaseUpdate(CaseRegistrationVM modal)
        {
            Helper.ProcessCourtDecisionHistory(modal, HttpContext.User.Identity.GetUserId(), "CaseRegistrationVM");
            bool IsUpdate = Helper.IsUpdate(modal, "CaseRegistrationVM");
            var CourtCase = db.CourtCase.Find(modal.CaseId);

            db.Entry(CourtCase).Entity.CurrentHearingDate = modal.CurrentHearingDate;
            db.Entry(CourtCase).Entity.CourtDecision = modal.CourtDecision;
            db.Entry(CourtCase).Entity.Requirements = modal.Requirements;
            db.Entry(CourtCase).Entity.ClientReply = modal.ClientReply;
            db.Entry(CourtCase).Entity.TransportationSource = modal.TransportationSource;
            db.Entry(CourtCase).Entity.FirstEmailDate = modal.FirstEmailDate;
            db.Entry(CourtCase).Entity.NextHearingDate = modal.NextHearingDate;

            if (modal.FileStatus == OfficeFileStatus.Registered.ToString())
                db.Entry(CourtCase).Entity.OfficeFileStatus = "OFS-16";// modal.FileStatus;
            else
                db.Entry(CourtCase).Entity.OfficeFileStatus = modal.FileStatus;

            if (IsUpdate)
            {
                Helper.CreateTranslation(modal, "CaseRegistrationVM");
                db.Entry(CourtCase).Entity.UpdateBoxDate = DateTime.UtcNow.AddHours(4);
                db.Entry(CourtCase).Entity.UpdateBoxBy = User.Identity.GetUserId();
            }

            db.Entry(CourtCase).State = EntityState.Modified;
            db.SaveChanges();

        }
        private void ProcessSessionRollDetail(CaseRegistrationVM modal)
        {
            Helper.ProcessSessionRollDetail(modal, "CaseRegistrationVM");
        }
        private void UpdateSessionDEFAddress(CaseRegistrationVM modal)
        {
            Helper.UpdateSessionDEFAddress(modal, "CaseRegistrationVM");
        }
        private void CreateCourtMoneyTransfer(CaseRegistrationVM modal)
        {
            Helper.CreateCourtMoneyTransfer(modal, "CaseRegistrationVM");
        }
        private int CreatePayVoucher(CaseRegistrationVM modal)
        {
            string UserLocation = Helper.GetEmployeeLocation(User.Identity.Name);

            return Helper.CreatePaymentVoucher(modal, "CaseRegistrationVM", UserLocation);
        }
        public ActionResult AjaxIndexData(string DataFor)
        {
            var request = HttpContext.Request;
            List<CaseRegistrationListForIndex> data = new List<CaseRegistrationListForIndex>();

            var start = (Convert.ToInt32(Request["start"]));
            var Length = (Convert.ToInt32(Request["length"])) == 0 ? 10 : (Convert.ToInt32(Request["length"]));
            var searchvalue = Request["search[value]"] ?? "";
            var sortcoloumnIndex = Convert.ToInt32(Request["order[0][column]"]);
            var sortDirection = Request["order[0][dir]"] ?? "asc";
            var recordsTotal = 0;

            try
            {
                string LocationId = string.Empty;
                string FileStatus = string.Empty;
                DataFor = string.Empty;
                try
                {
                    DataFor = request.Params["DataTableName"].ToString();
                    LocationId = request.Params["LocationId"].ToString();
                    FileStatus = request.Params["FileStatus"].ToString();
                    data = Helper.GetCaseRegistrationList(sortcoloumnIndex, start, searchvalue, Length, sortDirection, User.Identity.Name, DataFor, LocationId, FileStatus).ToList();
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string UserLocation = string.Empty;

            var caseRegistration = db.CaseRegistration.Find(id);
            if (caseRegistration == null)
            {
                return HttpNotFound();
            }

            var courtCases = db.CourtCase.Find(caseRegistration.CaseId);

            var ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == courtCases.ClientCode).FirstOrDefault().Mst_Desc;
            ViewBag.OfficeFileNo = courtCases.OfficeFileNo;
            ViewBag.ClientName = ClientName;
            ViewBag.Defendant = courtCases.Defendant;

            return View(caseRegistration);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var caseRegistration = db.CaseRegistration.Find(id);
            SoftDeleteCaseRegister(ref caseRegistration);
            MessageVM ProcessMessage = new MessageVM { id = -1, Category = "Success", Message = "RECORD DELETED SUCCESSFULLY" };

            return Json(new { redirectTo = "#btn_PayVoucherPDC" });

        }
        public ActionResult PrintForm(int id)
        {
            if (id <= 0)
                return HttpNotFound();

            var caseRegistration = db.CaseRegistration.Find(id);
            CaseRegistrationVM modal = new CaseRegistrationVM();
            Helper.GetCaseRegistrationVM(id, ref modal);
            if (!caseRegistration.IsDeleted)
            {
                modal.FormPrintJugDate = caseRegistration.JudgementDate?.ToString("dd/MM/yyyy");

                ViewBag.UserId = User.Identity.Name;
                ViewBag.PrintDate = DateTime.Now.ToString("dd/MM/yyyy");
            }

            return View(modal);
        }
        [HttpPost]
        public ActionResult GetTableTotal(string DataFor, string LocationId)
        {
            try
            {
                int TableTotal = Helper.GetTableTotal(DataFor, LocationId);

                return new JsonResult()
                {
                    Data = new { Message = "Success", TableTotal }
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
