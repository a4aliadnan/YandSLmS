namespace YandS.UI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SessionsRollVM
    {
        public int SessionRollId { get; set; }
        public int CaseId { get; set; }

        public string SessionClientId { get; set; }
        public string SessionRollClientName { get; set; }
        public string SessionRollDefendentName { get; set; }
        public string SessionLevel { get; set; }
        public string SessionLevelName { get; set; }
        public string CountLocationId { get; set; }
        public string CountLocationName { get; set; }
        public string CaseType { get; set; }
        public string LawyerId { get; set; }
        public string SessionRemarks { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CurrentHearingDate { get; set; }
        public string CourtDecision { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NextHearingDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PrintDateFrom { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PrintDateTo { get; set; }
        public string Requirements { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeforeDate { get; set; }
        public string FollowerId { get; set; }
        public string FollowerName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PrimaryJudgementsDate { get; set; }
        public string PrimaryJudgements { get; set; }
        public string PrimaryIsFavorable { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PrimaryJDReceiveDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal? PrimaryFinalJDAmount { get; set; }
        [StringLength(255)]
        public string PrimaryFinalJudgedInterests { get; set; }
        public string PrimaryAllJudgements { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AppealJudgementsDate { get; set; }
        public string AppealJudgements { get; set; }
        public string AppealIsFavorable { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AppealJDReceiveDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal? AppealFinalJDAmount { get; set; }
        [StringLength(255)]
        public string AppealFinalJudgedInterests { get; set; }
        public string AppealAllJudgements { get; set; }
        public DateTime? EnforcementJudgementsDate { get; set; }
        public string EnforcementJudgements { get; set; }
        public string EnforcementIsFavorable { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EnforcementJDReceiveDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastDate { get; set; }
        public string WorkRequired { get; set; }
        public string SessionNotes { get; set; }
        public string SuspendedWorkRequired { get; set; }
        public string SuspendedSessionNotes { get; set; }
        public string FollowNotes { get; set; }
        public string CurrentCaseLevel { get; set; }
        public string AccountContractNo { get; set; }
        public string ClientFileNo { get; set; }
        public string OfficeFileNo { get; set; }
        public string ClientName { get; set; }
        public string Defendant { get; set; }
        public string RegistrationLevel { get; set; }
        public string DisplaySentence { get; set; }
        [Display(Name = "SOURCE مصدر التحديث")]
        public string TransportationSource { get; set; }
        public string TransportationFee { get; set; }
        [Display(Name = "CLIENT REPLY رد الموكل")]
        public string ClientReply { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "FIRST EMAIL DATE تاريخ الايميل الأول")]
        public DateTime? FirstEmailDate { get; set; }

        #region PRIMARY, APPEAL and SUPREME CASE LEVEL

        public int DetailId { get; set; }
        public string Courtid { get; set; } //Dropdown Court
        public string CourtRefNo { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RegistrationDate { get; set; }
        public string PASCourtLocationid { get; set; } //Dropdown Location
        public string ApealByWho { get; set; }
        public string CaseLevelCode { get; set; } //Dropdown 
        public string ClaimSummary { get; set; }
        public string RealEstateYesNo { get; set; }
        public string RealEstateDetail { get; set; }
        public string CourtDepartment { get; set; } //Dropdown 

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CLOSURE DATE")]
        public DateTime? ClosureDate { get; set; }

        [Display(Name = "NEXT CASE LEVEL")]
        public string NextCaseLevel { get; set; } //Dropdown 

        [Display(Name = "NEXT CASE LEVEL CODE")]
        public string NextCaseLevelCode { get; set; }

        public bool ShowFollowup { get; set; }
        public bool ShowSuspend { get; set; }
        public string DifferentPanelYesSet { get; set; }
        public string DifferentPanelNotes { get; set; }
        public string AllJudgement { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SupremeJudgementsDate { get; set; }
        public string SupremeJudgements { get; set; }
        public string SupremeIsFavorable { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SupremeJDReceiveDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal? SupremeFinalJDAmount { get; set; }
        public string SupremeFinalJudgedInterests { get; set; }
        public string PartialViewName { get; set; }


        #endregion

        //Feb Sprint New Column
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SuspendedLastDate { get; set; }
        public string SuspendedFollowerId { get; set; }
        public string SuspendedFollowerName { get; set; }
        public string SessionOnHold { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SessionOnHoldUntill { get; set; }
        public string SessionFileStatus { get; set; }
        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        public decimal? ClaimAmount { get; set; }
        public string AgainstCode { get; set; }
        public string AgainstName { get; set; }
        public string EnforcementStageName { get; set; }
        public bool FinalJudgement { get; set; }

        #region PRINT FORM VARIABLES
        public string FormName { get; set; }
        public string DEFENDANT_PLAINTIFF { get; set; }
        public string DEFENDANT_PLAINTIFF_AR { get; set; }
        public string ACCOUNTDETAILS { get; set; }
        public string ACCOUNTDETAILSTEXT { get; set; }
        public string ACCOUNTDETAILS_AR { get; set; }
        public string UserId { get; set; }
        public string PrintDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReceivedDate { get; set; }
        public string FileStatusName { get; set; }
        public string AppealByName { get; set; }
        public string SupremeStageName { get; set; }

        #endregion
        public int? CaseRegistrationId { get; set; }

        public string Update_Follow { get; set; }
        public string Update_Suspend { get; set; }
        public string Update_CourtFollow { get; set; }
        public string Update_CourtTransfer { get; set; }
        public string Update_Addreass { get; set; }
        public string Update_PV { get; set; }
        [Display(Name = "COURT FOLLOW مراجعة المحكمة")]
        public string CourtFollow { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = " COMMISSIONING DATE تاريخ التكليف")]
        public DateTime? CommissioningDate { get; set; }
        public string CourtFollow_LawyerId { get; set; }
        public string CourtFollowRequirement { get; set; }
        public string SessionNote_Remark { get; set; }
        [Display(Name = "ANNOUNCEMENT TYPE طريقة الإعلان")]
        public string AnnouncementTypeId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DEF_DateOfContact { get; set; }
        public string DEF_MobileNo { get; set; }
        public string DEF_Corresponding { get; set; }
        public string DEF_CallerName { get; set; }
        public string DEF_LawyerId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DEF_VisitDate { get; set; }
        [Display(Name = "CONTACT TYPE طريقة التواصل")]
        public string DEF_ContactType { get; set; }
        #region Payment Voucher
        public int Voucher_No { get; set; }

        [Display(Name = "PAYMENT TYPE")]
        public string Payment_Type { get; set; }
        [Display(Name = "PAYMENT MODE")]
        public string Payment_Mode { get; set; }
        [Display(Name = "VOUCHER TYPE")]
        public string VoucherType { get; set; }
        [Display(Name = "Office File No")]
        public string OfficeFileNoSessionRollPV { get; set; }
        public string CourtType { get; set; }
        public string LocationCode { get; set; }
        public string Status { get; set; }
        public string PV_No { get; set; }// Format : Muscat-0001-2020 (Genrate after approval)
        public string VoucherStatus { get; set; }
        public int Credit_Account { get; set; }
        public int Debit_Account { get; set; }
        public string TransTypeCode { get; set; }
        public string TransReasonCode { get; set; }
        public string CaseInvoices { get; set; }
        public string SpecialNotification { get; set; }
        public string PDCRefNo { get; set; }
        public DateTime? FutureChequeDate { get; set; }
        [Display(Name = "REFERENCE NO. رقم الدعوى")]
        public string BillNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        [Display(Name = "Amount المبلغ")]
        public decimal Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        [Display(Name = "Vat الضريبة")]
        public decimal? VatAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total amounts مبلغ الفاتورة")]
        public decimal? TotalAmount { get; set; }
        [Display(Name = "PAY TO يحول المبلغ إلى")]
        public string Payment_To { get; set; }
        public string Remarks { get; set; }
        [Display(Name = "PAY TYPE")]
        public string Payment_Head { get; set; }
        public string PaymentHeadDetail { get; set; }
        public string PaymentToBenificry { get; set; }
        public string PayToMstValue { get; set; }
        public string PayToMstDesc { get; set; }
        public string PayToBankName { get; set; }
        public string PayToAccountNumber { get; set; }
        public string PayToEmail { get; set; }
        public string PayToContactNo { get; set; }
        [Display(Name = "BANK البنك")]
        public string PayToBankNameDisp { get; set; }
        [Display(Name = "ACC الحساب")]
        public string PayToAccountNumberDisp { get; set; }
        public string PayToMessageLang { get; set; }
        #endregion

        #region REGISTRATION IN PROGRESS
        [Display(Name = "ACTION LEVEL")]
        public string ActionLevel { get; set; }
        public string ActionLevelName { get; set; }

        [Display(Name = "INVESTMENT تبسيط الإجراءات")]
        public string DepartmentType { get; set; }
        [Display(Name = "ADVISOR المستشار")]
        public string ConsultantId { get; set; }
        public string FormPrintWorkRequired { get; set; }
        public string OfficeProcedure { get; set; } //NOTES
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FormPrintLastDate { get; set; }
        [Display(Name = "FILE STATUS")]
        public string FileStatus { get; set; }
        public string FileStatusRemarks { get; set; }
        public string OnHoldReasonDDL { get; set; }
        [Display(Name = "ACTION DATE")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ActionDate { get; set; }
        public string DisputeLevel { get; set; }
        public DateTime? DisputrRegisterDate { get; set; }
        #endregion

        #region MONEY TRANSFER

        public int DefendentTransferId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MoneyTrRequestDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TransferDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        public decimal? TransferAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MoneyTrCompleteDate { get; set; }
        public string DataFor { get; set; }
        public string MoneyWith { get; set; }
        #endregion
        [Display(Name = "JUDGMENT الحكم")]
        public string JudgementLevel { get; set; }
        public string StopEnfRequest { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal? FinalJDAmount { get; set; }
        [StringLength(255)]
        public string FinalJudgedInterests { get; set; }
        public string SessionOnHoldName { get; set; }
        public string SessionFileStatusName { get; set; }

        [Display(Name = "DELETE FROM SESSION ROLL حذف من الجلسات")]
        public string DeleteFromSessionDDL { get; set; }
        public string UpdatedOn { get; set; }
        public string IsDelete { get; set; }
        public string buttonToGo { get; set; }
        public string UpdatedBy { get; set; }
        public string IsAfterJudgementReceived { get; set; }
        public string EnforcementAdmin { get; set; }
        public string SavePV_Data { get; set; }
        public SessionsRollVM()
        {
            SessionRollDefendentName = "";
            SessionClientId = "0";
            CountLocationId = "0";
            CaseType = "0";
            LawyerId = "0";
            FollowerId = "0";
            SuspendedFollowerId = "0";
            SessionOnHold = "0";
            SessionFileStatus = "0";
            JudgementLevel = "0";
            IsDelete = "N";
            IsAfterJudgementReceived = "N";
        }
    }
}