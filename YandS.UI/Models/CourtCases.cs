namespace YandS.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CourtCases : Base
    {
        [Key]
        public int CaseId { get; set; }

        [Display(Name = "FILE NO")]
        [StringLength(10)]
        public string OfficeFileNo { get; set; }

        [Display(Name = "CLIENT NAME")] //Dropdown Client Master
        [Required]
        [SelectZeroVal(ErrorMessage = "PLEASE SELECT CLIENT NAME")]
        [StringLength(6)]
        public string ClientCode { get; set; }

        [Required]
        [Display(Name = "DEFENDANT NAME")]
        [StringLength(255)]
        public string Defendant { get; set; }

        [Display(Name = "AGAINST")]
        [StringLength(2)]
        public string AgainstCode { get; set; } //Dropdown 

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RestrictFutureDate(ErrorMessage = "FUTURE DATE NOT ALLOWED FOR RECEIVED DATE")]
        [Display(Name = "RECEIVED DATE")]
        [Column(TypeName = "datetime2")]
        public DateTime? ReceptionDate { get; set; }

        [Display(Name = "RECEIVE LEVEL")]
        [StringLength(2)]
        public string ReceiveLevelCode { get; set; } //Dropdown 

        [Display(Name = "A/C CONTRACT NO")]
        [StringLength(100)]
        public string AccountContractNo { get; set; }

        [Display(Name = "CLIENT FILE NO")]
        [StringLength(100)]
        public string ClientFileNo { get; set; }

        //[DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "CLAIM AMOUNT")]
        public decimal? ClaimAmount { get; set; }

        [Display(Name = "CASE TYPE")]
        [StringLength(2)]
        public string CaseTypeCode { get; set; } //Dropdown 

        [Display(Name = "CASE LEVEL")]
        [StringLength(2)]
        public string CaseLevelCode { get; set; } //Dropdown 

        [Display(Name = "PARENT COURT")]
        [StringLength(2)]
        public string ParentCourtId { get; set; }  //Dropdown 

        [Display(Name = "LOAN MANAGER")]
        [StringLength(2)]
        public string LoanManager { get; set; }  //Dropdown 

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RestrictFutureDate(ErrorMessage = "LEGAL NOTICE DATE NOT ALLOWED FOR RECEIVED DATE")]
        [Display(Name = "LEGAL NOTICE DATE")]
        [Column(TypeName = "datetime2")]
        public DateTime? LegalNoticeDate { get; set; }

        [Display(Name = "ODB LOAN")]
        [StringLength(3)]
        public string ODBLoanCode { get; set; } //Dropdown 

        [Display(Name = "ODB BANK BRANCH")]
        [StringLength(3)]
        public string ODBBankBranch { get; set; } //Dropdown 

        [Display(Name = "POLICE NO")]
        [StringLength(100)]
        public string PoliceNo { get; set; }

        [Display(Name = "POLICE STATION")]
        [StringLength(5)]
        public string PoliceStation { get; set; }//Dropdown 

        [Display(Name = "PUBLIC PROSECUTION NO")]
        [StringLength(100)]
        public string PublicProsecutionNo { get; set; }

        [Display(Name = "PUBLIC PROSECUTION PLACE")]
        [StringLength(5)]
        public string PublicPlace { get; set; }//Dropdown 

        [Display(Name = "PAPC NO")]
        [StringLength(100)]
        public string PAPCNo { get; set; }

        [Display(Name = "PAPC PLACE")]
        [StringLength(5)]
        public string PAPCPlace { get; set; }//Dropdown 

        [Display(Name = "LABOR CONFLIC NO")]
        [StringLength(100)]
        public string LaborConflicNo { get; set; }

        [Display(Name = "LABOR CONFLIC PLACE")]
        [StringLength(5)]
        public string LaborConflicPlace { get; set; }//Dropdown 
        [StringLength(5)]
        public string SelectedBeforeCourt { get; set; }//Radio 

        [Display(Name = "Y&S NOTE")]
        public string YandSNotes { get; set; }

        [StringLength(2)]
        public string CaseStatus { get; set; }

        [Display(Name = "SPECIAL INSTRUCTIONS")]
        public string SpecialInstructions { get; set; }
        [Display(Name = "CLIENT CASE TYPE")]
        [StringLength(3)]
        public string ClientCaseType { get; set; }

        [Display(Name = "PRIVATE FEES?")]
        public bool IsPrivateFee { get; set; }

        [Display(Name = "COMMISSION")]
        public bool IsCommission { get; set; }
        [Display(Name = "ID/CIVIL  NO. ")]
        [StringLength(50)]
        public string IdRegistrationNo { get; set; }

        [Display(Name = "CR / REGISTRATION NO.")]
        [StringLength(50)]
        public string CRRegistrationNo { get; set; }
        [Display(Name = "NATIONALITY")]
        [StringLength(1)]
        public string OmaniExp { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RestrictFutureDate(ErrorMessage = "FUTURE DATE NOT ALLOWED FOR FINAL CLOSURE DATE")]
        [Display(Name = "FINAL CLOSURE DATE")]
        [Column(TypeName = "datetime2")]
        public DateTime? ClosureDate { get; set; }

        [Display(Name = "CLOSED BY")]
        [StringLength(20)]
        public string ClosedbyStaff { get; set; }

        [Display(Name = "CLASSIFICATION OF CLIENT")] //Dropdown Client Master
        [Required]
        [SelectZeroVal(ErrorMessage = "PLEASE SELECT CLASSIFICATION OF CLIENT")]
        [StringLength(2)]
        public string ClientClassificationCode { get; set; }

        [Display(Name = "CASE SUBJECT")]
        [StringLength(3)]
        public string CaseSubject { get; set; }  //Dropdown 

        [Display(Name = "SUBJECT")]
        public string Subject { get; set; }
        [Display(Name = "NOTES")]
        public string ClosingNotes { get; set; }
        public DateTime? ClosingNotesDate { get; set; }
        public string SessionRemarks { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? CurrentHearingDate { get; set; }
        public string CourtDecision { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? NextHearingDate { get; set; }
        public string Requirements { get; set; }
        [StringLength(3)]
        public string RealEstateYesNo { get; set; }
        [StringLength(4000)]
        public string RealEstateDetail { get; set; }
        [StringLength(50)]
        public string ReconciliationNo { get; set; }
        [StringLength(2)]
        public string ReconciliationDep { get; set; }
        [StringLength(2)]
        public string GovernorateId { get; set; }
        public string ClaimSummary { get; set; }
        [StringLength(5)]
        public string SessionClientId { get; set; }
        [StringLength(255)]
        public string SessionRollDefendentName { get; set; }
        [StringLength(1)]
        public string AgainstInsurance { get; set; }
        [StringLength(1)]
        public string ReOpenEnforcement { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? FinanceFileClosureDate { get; set; }
        [StringLength(1)]
        public string FinalOnvoiceOnHold { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? SuggestedDate { get; set; }

        [StringLength(1)]
        public string TransportationFee { get; set; }
        [StringLength(1)]
        public string JudRecRedStamp { get; set; }
        [StringLength(1)]
        public string ClientReply { get; set; }
        [StringLength(1)]
        public string CourtFollow { get; set; }
        [StringLength(10)]
        public string ReasonCode { get; set; }
        [StringLength(3)]
        public string FileAllocation { get; set; }
        [Display(Name = "Litigation File Closure Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? LitigationFileClosureDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? FirstEmailDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? CommissioningDate { get; set; }
        [StringLength(1)]
        public string TransportationSource { get; set; }
        [StringLength(4000)]
        public string CourtFollowRequirement { get; set; }
        [StringLength(1)]
        public string StopEnfRequest { get; set; }
        [StringLength(10)]
        public string OfficeFileStatus { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? UpdateBoxDate { get; set; }
        public int? UpdateBoxBy { get; set; }
        public decimal? CorporateFee { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string CorporateWorkDetail { get; set; }
        [StringLength(1)]
        public string Translation { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ActionDate { get; set; }
        [StringLength(5)]
        public string EnforcementAdmin { get; set; }
        [StringLength(4000)]
        public string SessionNote_Remark { get; set; }
        [StringLength(5)]
        public string CourtFollow_LawyerId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? StoreDate { get; set; }
        public string StoreNotes { get; set; }
        [StringLength(1)]
        public string FinalClosureApprovalStatus { get; set; }
        [StringLength(30)]
        public string FinalClosureInitiatedBy { get; set; }
        [StringLength(30)]
        public string FinalClosureApproveddBy { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? AccountAuditDate { get; set; }
        [StringLength(30)]
        public string AccountAuditBy { get; set; }
        [StringLength(30)]
        public string ClosureArchieveddBy { get; set; }

        public ICollection<CourtCasesDetail> DetailId { get; set; }
        public ICollection<CourtCasesEnforcement> EnforcementId { get; set; }
        public ICollection<CaseInvoice> InvoiceId { get; set; }
        public ICollection<CourtCaseStatusDetail> StatusDetailId { get; set; }
        public ICollection<PayVoucher> Voucher_No { get; set; }
        public ICollection<CaseRegistration> CaseRegistrationId { get; set; }
        public ICollection<SessionsRoll> SessionRollId { get; set; }
        public ICollection<DecisionTranslation> TranslationId { get; set; }
        public ICollection<ClosurePartialDetail> PartDetailId { get; set; }
        public ICollection<DefendentContact> DefendentContactId { get; set; }

        public CourtCases()
        {
            DetailId = new HashSet<CourtCasesDetail>();
            InvoiceId = new HashSet<CaseInvoice>();
            StatusDetailId = new HashSet<CourtCaseStatusDetail>();
            Voucher_No = new HashSet<PayVoucher>();
            CaseSubject = "0";
            OmaniExp = "0";
            ClientCaseType = "0";
            CaseStatus = "1";
            LaborConflicPlace = "0";
            PAPCPlace = "0";
            PublicPlace = "0";
            PoliceStation = "0";
            ODBBankBranch = "0";
            ODBLoanCode = "0";
            LoanManager = "0";
            ParentCourtId = "0";
        }
        #region NotMapped Names

        [NotMapped]
        public string ClientName { get; set; }
        [NotMapped]
        public string DefClientName { get; set; }
        [NotMapped]
        public string AgainstName { get; set; }
        [NotMapped]
        public string ReceiveLevelName { get; set; }
        [NotMapped]
        public string CaseTypeName { get; set; }
        [NotMapped]
        public string CaseLevelName { get; set; }
        [NotMapped]
        public string ODBLoanName { get; set; }
        [NotMapped]
        public string ODBBankBranchName { get; set; }
        [NotMapped]
        public string CaseStatusName { get; set; }
        [NotMapped]
        public string PoliceStationName { get; set; }
        [NotMapped]
        public string PublicPlaceName { get; set; }
        [NotMapped]
        public string PAPCPlaceName { get; set; }
        [NotMapped]
        public string LaborConflicPlaceName { get; set; }

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "STATUS DATE")]
        public DateTime CaseStatusDate { get; set; }

        [NotMapped]
        public string ClientCaseTypeName { get; set; }
        [NotMapped]
        public string ParentCourtName { get; set; }
        [NotMapped]
        public string CaseLocationName { get; set; }

        [NotMapped]
        public string ReasonName { get; set; }
        [NotMapped]
        public string LoanManagerName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [NotMapped]
        public string CaseSubjectName { get; set; }

        [NotMapped]
        public string ClientClassificationName { get; set; }

        #endregion

        #region NotMapped List

        [NotMapped]
        public ICollection<MasterSetups> ListClientName { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListDefClient { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListAgainst { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListReceiveLevel { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListCaseType { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListCaseLevel { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListODBLoan { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListODBBankBranch { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListCaseStatus { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListPoliceStation { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListPublicPlace { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListPAPCPlace { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListLaborConflicPlace { get; set; }

        #endregion
    }

    public class ToBeRegisterVM
    {
        public int CaseId { get; set; }

        [Display(Name = "OFFICE FILE NO")]
        public string OfficeFileNo { get; set; }

        [Display(Name = "CLASSIFICATION OF CLIENT")] //Dropdown Client Master
        public string ClientClassificationCode { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "RECEIVED DATE")]
        public DateTime? ReceptionDate { get; set; }

        [Display(Name = "RECEIVE LEVEL")]
        public string ReceiveLevelCode { get; set; } //Dropdown 

        [Display(Name = "CASE TYPE")]
        public string CaseTypeCode { get; set; } //Dropdown 

        [Display(Name = "AGAINST")]
        public string AgainstCode { get; set; } //Dropdown 

        [Display(Name = "CLIENT NAME")] //Dropdown Client Master
        public string ClientCode { get; set; }

        [Display(Name = "DEFENDANT NAME")]
        public string Defendant { get; set; }

        [Display(Name = "CURRENT CASE LEVEL")]
        public string CaseLevelCode { get; set; } //Dropdown 

        [Display(Name = "LOAN TYPE")]
        public string ClientCaseType { get; set; }

        public string AccountContractNo { get; set; }
        public string ClientFileNo { get; set; }

        //[DisplayFormat(DataFormatString = "{0:0,0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "CLAIM AMOUNT")]
        public string ClaimAmount { get; set; }

        [Display(Name = "ID/CIVIL No. ")]
        public string IdRegistrationNo { get; set; }
        [Display(Name = "NATIONALITY")]
        public string OmaniExp { get; set; }

        [Display(Name = "CR / REGISTRATION No.")]
        public string CRRegistrationNo { get; set; }

        [Display(Name = "CASE SUBJECT")]
        public string CaseSubject { get; set; }  //Dropdown 

        [Display(Name = "SUBJECT")]
        public string Subject { get; set; }

        [Display(Name = "ODB BANK BRANCH")]
        public string ODBBankBranch { get; set; } //Dropdown 

        [Display(Name = "LOAN MANAGER")]
        public string LoanManager { get; set; }  //Dropdown 

        [Display(Name = "SPECIAL INSTRUCTIONS")]
        public string SpecialInstructions { get; set; }

        [Display(Name = "Y&S NOTE")]
        public string YandSNotes { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CurrentHearingDate { get; set; }
        public string CourtDecision { get; set; }
        public string SessionRemarks { get; set; }
        public string Requirements { get; set; }
        public string SessionClientId { get; set; }
        public string SessionRollClientName { get; set; }
        public string SessionRollDefendentName { get; set; }

        #region BEFORE COURT
        [Display(Name = "POLICE NO")]
        public string PoliceNo { get; set; }

        [Display(Name = "POLICE STATION")]
        public string PoliceStation { get; set; }//Dropdown 

        [Display(Name = "PUBLIC PROSECUTION NO")]
        public string PublicProsecutionNo { get; set; }

        [Display(Name = "PUBLIC PROSECUTION PLACE")]
        public string PublicPlace { get; set; }//Dropdown 

        [Display(Name = "PAPC NO")]
        public string PAPCNo { get; set; }

        [Display(Name = "PAPC PLACE")]
        public string PAPCPlace { get; set; }//Dropdown 

        [Display(Name = "LABOR CONFLIC NO")]
        public string LaborConflicNo { get; set; }

        [Display(Name = "LABOR CONFLIC PLACE")]
        public string LaborConflicPlace { get; set; }//Dropdown
        [Display(Name = "RECONCILIATION NO.")]
        public string ReconciliationNo { get; set; }
        [Display(Name = "RECONCILIATION DEPT")]
        public string ReconciliationDep { get; set; }

        #endregion

        #region NEW CASES [Tobe Register]
        public int CaseRegistrationId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "RECEIVED DATE")]
        public DateTime? ReceptionDateDisp { get; set; }

        [Display(Name = "ACTION DATE")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ActionDate { get; set; }
        public string AdminFile { get; set; }

        [Display(Name = "URGENT CASES")]
        public int? UrgentCaseDays { get; set; }

        [Display(Name = "REGISTERED")]
        public bool CourtDetailRegistered { get; set; }
        public string OfficeProcedure { get; set; } //NOTES
        public string MainRemarks { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FormPrintLastDate { get; set; }
        public string FormPrintWorkRequired { get; set; }
        [Display(Name = "FILE STATUS حالة الملف")]
        public string FileStatus { get; set; }
        public string OfficeFileStatus { get; set; }
        [Display(Name = "ENFORCEMENT DISPUTE")]
        public string EnforcementDispute { get; set; }
        public string DisputeLevel { get; set; }
        public string DisputeType { get; set; }
        #endregion

        #region PRIMARY APPEAL SUPREME ENFORCEMENT

        public int DetailId { get; set; }

        [Display(Name = "COURT")]
        public string Courtid { get; set; } //Dropdown Court

        [Display(Name = "COURT REGISTRATION NO")]
        public string CourtRefNo { get; set; }

        [Display(Name = "COURT")]
        public string CourtLocationid { get; set; } //Dropdown Location

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "REGISTRATION DATE")]
        public DateTime? RegistrationDate { get; set; }

        [Display(Name = "Apeal By Who")]
        public string ApealByWho { get; set; }
        public string CaseLevelName { get; set; }
        public string RealEstateYesNo { get; set; }
        public string RealEstateDetail { get; set; }
        public string ClaimSummary { get; set; }
        public string GovernorateId { get; set; }
        public string EnforcementAdmin { get; set; }
        [Display(Name = "CURRENT ENFORCEMENT LEVEL مرحلة التنفيذ الحالية")]
        public string EnforcementlevelId { get; set; } //Dropdown EnforcementLevel 265
        public string AgainstInsurance { get; set; }

        #endregion

        #region ENFORCEMENT SPECIFIC
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "ACTION DATE")]
        public DateTime? EnforcementActionDate { get; set; }
        [Display(Name = "CLIENT REPLY رد الموكل")]
        public string ClientReply { get; set; }
        [Display(Name = "COURT FOLLOW مراجعة المحكمة")]
        public string CourtFollow { get; set; }
        public string LawyerId { get; set; }
        [Display(Name = "ANNOUNCEMENT TYPE طريقة الإعلان")]
        public string AnnouncementTypeId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "ANNOUNCEMENT COMPLETED AFTER يكتمل الإعلان بعد")]
        public DateTime? AnnouncementCompleteDt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "SUBMISSION DATE تاريخ ارسال الطلب للمحكمة")]
        public DateTime? SubmissionDt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "APPROVAL DATE تاريخ موافقة القاضي على الطلب")]
        public DateTime? ApprovalDt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "INQUIRY DATE تاريخ صدور مخاطبة الجهات")]
        public DateTime? InquiryDt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "MOH RESULT DATE تاريخ وصول رد الإسكان")]
        public DateTime? MOHResultDt { get; set; }
        [Display(Name = "MOH RESULT مضمون رد الإسكان")]
        public string MOHResult { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "ROP RESULT DATE تاريخ وصول رد المرور")]
        public DateTime? ROPResultDt { get; set; }
        [Display(Name = "ROP RESULT مضمون رد المرور")]
        public string ROPResult { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "BANK'S RESULT DATE تاريخ وصول رد البنوك")]
        public DateTime? BankResultDt { get; set; }
        [Display(Name = "BANK RESULT مضمون رد البنوك")]
        public string BankResult { get; set; }
        [Display(Name = "ARREST ORDER NO رقم أمر الحبس")]
        public string ArrestOrderNo { get; set; }
        [Display(Name = "ARREST ORDER ISSUE DATE تاريخ صدوره")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ArrestOrderIssueDate { get; set; }
        [Display(Name = "POLICE STATION مركز الشرطة")]
        public string PoliceStationid { get; set; }
        [Display(Name = "ARRESTED تم القبض عليه")]
        public string Arrested { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "ARRESTED DATE تاريخ القبض عليه")]
        public DateTime? ArrestedDate { get; set; }
        [Display(Name = "AUCTION PROCESS إجراءات البيع")]
        public string AuctionProcess { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "DATE تاريخ بدء هذا الإجراء")]
        public DateTime? JUDAuctionDate { get; set; }
        [Display(Name = "VALUE قيمة العقار")]
        public decimal? JUDAuctionValue { get; set; }
        [Display(Name = "AUCTION REPEAT تكرار المزاد")]
        public string JUDAuctionRepeat { get; set; }
        [Display(Name = "JUDGE'S DECISION قرار القاضي بشأن طلب الوقف")]
        public string JUDDecisionId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SuspensionStartDate { get; set; }
        [Display(Name = "SUSPENSION PERIOD مدة الوقف بالأيام")]
        public int? SuspensionPeriod { get; set; } // In Months
        [Display(Name = "CAUSE OF SUSPENSION سبب الوقف")]
        public string SuspensionCauseId { get; set; } //Dropdown CauseOfSuspension 272
        [Display(Name = "CAUSE OF RECOVERY سبب الاسترداد")]
        public string CauseOfRecoveryId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "DATE OF INSTRUCTION تاريخ إبلاغ المحامي")]
        public DateTime? DateOfInstruction { get; set; }
        [Display(Name = "PRIMARY OBJECTION NO.")]
        public string PrimaryObjectionNo { get; set; }
        [Display(Name = "PRIMARY OBJECTION COURT")]
        public string PrimaryObjectionCourt { get; set; }
        [Display(Name = "APEAL OBJECTION NO.")]
        public string ApealObjectionNo { get; set; }
        [Display(Name = "APEAL OBJECTION COURT")]
        public string ApealObjectionCourt { get; set; }
        [Display(Name = "SUPREME OBJECTION NO.")]
        public string SupremeObjectionNo { get; set; }
        [Display(Name = "SUPREME OBJECTION COURT")]
        public string SupremeObjectionCourt { get; set; }
        [Display(Name = "PRIMARY PLAINT NO.")]
        public string PrimaryPlaintNo { get; set; }
        [Display(Name = "PRIMARY PLAINT COURT")]
        public string PrimaryPlaintCourt { get; set; }
        [Display(Name = "APEAL PLAINT NO.")]
        public string ApealPlaintNo { get; set; }
        [Display(Name = "APEAL PLAINT COURT")]
        public string ApealPlaintCourt { get; set; }
        [Display(Name = "SUPREME PLAINT NO.")]
        public string SupremePlaintNo { get; set; }
        [Display(Name = "SUPREME PLAINT COURT")]
        public string SupremePlaintCourt { get; set; }
        [Display(Name = "DISPUTE LEVEL مرحلة منازعة التنفيذ")]
        public string CurrentDisputeLevelandType { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MoneyTrRequestDate { get; set; }
        public string ReOpenEnforcement { get; set; } //DROP DOWN
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DEF_DateOfContact { get; set; }
        public string DEF_MobileNo { get; set; }
        public string DEF_Corresponding { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "FIRST EMAIL DATE تاريخ الايميل الأول")]
        public DateTime? FirstEmailDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = " COMMISSIONING DATE تاريخ التكليف")]
        public DateTime? CommissioningDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NextHearingDate { get; set; }
        [Display(Name = "SOURCE مصدر التحديث")]
        public string TransportationSource { get; set; }
        public string TransportationFee { get; set; }
        public string DEF_MobileNo2 { get; set; }
        public string DEF_MobileNo3 { get; set; }
        public string DEF_MobileNo4 { get; set; }
        public string DEF_MobileNo5 { get; set; }
        public string DEF_CallerName { get; set; }
        public string DEF_LawyerId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DEF_VisitDate { get; set; }
        [Display(Name = "CONTACT TYPE طريقة التواصل")]
        public string DEF_ContactType { get; set; }

        public string CourtApproval { get; set; }
        public string ArrestLevel { get; set; }
        #endregion

        #region CLOSING

        [Display(Name = "REASON")]
        public string ReasonCode { get; set; } //Dropdown Case Status
        public string ClosureLevel { get; set; } //Dropdown ClosureLevel 500
        public string FileAllocation { get; set; } //Dropdown FileAllocation 501

        [Display(Name = "Litigation File Closure Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LitigationFileClosureDate { get; set; }
        public string FinalClosureApprovalStatus { get; set; }
        public string FinalClosureInitiatedBy { get; set; }
        public string FinalClosureApproveddBy { get; set; }
        public string AccountAuditBy { get; set; }
        public string ClosureArchieveddBy { get; set; }
        public DateTime? AccountAuditDate { get; set; }
        public DateTime? StoreDate { get; set; }

        [Display(Name = "CLOSURE NOTE ملاحظات الغلق")]
        public string ClosingNotes { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "NOTES DATE تاريخ الملاحظة")]
        public DateTime? ClosingNotesDate { get; set; }

        #endregion
        public string ClientName { get; set; }
        public string COURT { get; set; }
        public string ClientClassName { get; set; }
        public string CaseStatusName { get; set; }
        public string PartialViewName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ContactDateFrom { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ContactDateTo { get; set; }
        public string CourtFollowRequirement { get; set; }
        public string UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdateBoxDate { get; set; }
        public int? UpdateBoxBy { get; set; }
        public string UpdateBoxByName { get; set; }
        public string CurrentCaseLevel { get; set; }
        public string CaseType { get; set; }
        public string CountLocationName { get; set; }
        public bool ShowFollowup { get; set; }
        public bool ShowSuspend { get; set; }
        public string Update_Follow { get; set; }
        public string Update_Suspend { get; set; }
        public string FollowerId { get; set; }
        public string SuspendedFollowerId { get; set; }

        public DateTime? LastDate { get; set; }
        public string WorkRequired { get; set; }
        public string SessionNotes { get; set; }
        public DateTime? SuspendedLastDate { get; set; }
        public string SuspendedWorkRequired { get; set; }
        public string SuspendedSessionNotes { get; set; }
        public string SessionNote_Remark { get; set; }
        public string Session_LawyerId { get; set; }
        public string CourtFollow_LawyerId { get; set; }
        public string UpdatePV_Type { get; set; }
        public string SavePV_Data { get; set; }
        public int SessionRollId { get; set; }
        #region MONEY TRANSFER
        public int DefendentTransferId { get; set; }
        public string DataFor { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TransferDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        public decimal? Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MoneyTrCompleteDate { get; set; }
        public string MoneyWith { get; set; }

        #endregion
        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "CORPORATE FEE")]
        public decimal? CorporateFee { get; set; }
        [Display(Name = "DETAILS")]
        public string CorporateWorkDetail { get; set; }
        public string Translation { get; set; }
        public PayVoucherVM PVDetail { get; set; }
        public FileClosureEnteryVM FileClosureDetail { get; set; }
        public ToBeRegisterVM()
        {
            DetailId = 0;
            ApealByWho = "0";
            EnforcementlevelId = "0";
            CaseType = "0";
            LawyerId = "0";
            GovernorateId = "0";
            AgainstInsurance = "0";
            ActionDate = DateTime.UtcNow.AddHours(4);
        }

    }
    public class BeforeCourtVM
    {
        public int CaseId { get; set; }

        [Display(Name = "POLICE NO")]
        public string PoliceNo { get; set; }

        [Display(Name = "POLICE STATION")]
        public string PoliceStation { get; set; }//Dropdown 

        [Display(Name = "PUBLIC PROSECUTION NO")]
        public string PublicProsecutionNo { get; set; }

        [Display(Name = "PUBLIC PROSECUTION PLACE")]
        public string PublicPlace { get; set; }//Dropdown 

        [Display(Name = "PAPC NO")]
        public string PAPCNo { get; set; }

        [Display(Name = "PAPC PLACE")]
        public string PAPCPlace { get; set; }//Dropdown 

        [Display(Name = "LABOR CONFLIC NO")]
        public string LaborConflicNo { get; set; }

        [Display(Name = "LABOR CONFLIC PLACE")]
        public string LaborConflicPlace { get; set; }//Dropdown 
        [Display(Name = "RECONCILIATION NO.")]
        public string ReconciliationNo { get; set; }
        [Display(Name = "RECONCILIATION DEPT")]
        public string ReconciliationDep { get; set; }

        public string CaseLevelCode { get; set; } //Dropdown 

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Closure Date")]
        public DateTime? ClosureDate { get; set; }

        [Display(Name = "Closed By")]
        public string ClosedbyStaff { get; set; }

        //[Required(ErrorMessage = "This is Required")]
        [Display(Name = "Next Case Level")]
        public string NextCaseLevel { get; set; } //Dropdown 

        //[Required(ErrorMessage = "This is Required")]
        [Display(Name = "Next Case Level Code")]
        public string NextCaseLevelCode { get; set; }

        public int SessionRollId { get; set; }

        #region SESSION ROLL VM - UPDATE

        //[Required(ErrorMessage = "This is Required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CurrentHearingDate { get; set; }
        //[Required(ErrorMessage = "This is Required")]
        public string CourtDecision { get; set; }
        public string SessionRemarks { get; set; }
        public string Requirements { get; set; }

        #endregion

        #region SESSION ROLL VM - FOLLOW

        //[Required(ErrorMessage = "This is Required")]
        public string SessionClientId { get; set; }
        //[Required(ErrorMessage = "This is Required")]
        public bool ShowFollowup { get; set; }
        public bool ShowSuspend { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastDate { get; set; }
        public string WorkRequired { get; set; }
        public string SessionNotes { get; set; }
        public string FollowerId { get; set; }
        public string SuspendedWorkRequired { get; set; }
        public string SuspendedSessionNotes { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SuspendedLastDate { get; set; }
        public string SuspendedFollowerId { get; set; }
        public string SessionNote_Remark { get; set; }
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
        #endregion
        public string SessionRollDefendentName { get; set; }
        public string Defendant { get; set; }
        public string OfficeFileNo { get; set; }
        public string SessionRollClientName { get; set; }
        public string ClientName { get; set; }

    }
    public class FileClosureEnteryVM
    {
        public int CaseId { get; set; }
        public string OfficeFileNo { get; set; }

        [Display(Name = "CASE STATUS")]
        public string StatusCode { get; set; } //Dropdown Case Status

        [Display(Name = "CLOSURE DATE تاريخ الغلق")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StatusDate { get; set; }

        [Display(Name = "REASONE سبب الغلق")]
        public string ReasonCode { get; set; } //Dropdown Case Status

        [Display(Name = "CURRENT CASE LEVEL")]
        public string CurrentCaseLevel { get; set; } //Dropdown CurrentCaseLevel 499

        [Display(Name = "CLOSURE LEVEL")]
        public string ClosureLevel { get; set; } //Dropdown ClosureLevel 500

        [Display(Name = "ARCHIVE")]
        public string FileAllocation { get; set; } //Dropdown FileAllocation 501

        [Display(Name = "LITIGATION FILE CLOSURE DATE")]
        [RestrictFutureDate(ErrorMessage = "FUTURE DATE NOT ALLOWED FOR FINAL CLOSURE DATE")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LitigationFileClosureDate { get; set; }
        public int? TempMonth { get; set; }

        [Display(Name = "CLOSURE NOTE ملاحظات الغلق")]
        public string ClosingNotes { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "NOTES DATE تاريخ الملاحظة")]
        public DateTime? ClosingNotesDate { get; set; }
        public string ReOpenEnforcement { get; set; } //DROP DOWN

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "FINANCE FILE CLOSURE DATE")]
        public DateTime? FinanceFileClosureDate { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "PASS كلمة المرور")]
        public string Password { get; set; }
        public int PartDetailId { get; set; }
        [Display(Name = "CLOUSER PART DATE تاريخ الغلق الجزئي")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ClosurePartDate { get; set; }
        [Display(Name = "REASON-PARTIAL CLOSURE سبب الغلق الجزئي")]
        public string FileTypeClosure { get; set; } //Dropdown FILE TYPE CLOSURE
        [Display(Name = "PART NO. الجزء المغلق")]
        public string PartNo { get; set; } //Dropdown FILE TYPE CLOSURE
        [DataType(DataType.Password)]
        [Display(Name = "PASS كلمة المرور")]
        public string ApprovalPassword { get; set; }
        [Display(Name = "APPROVE موافقة على الغلق")]
        public string ClosureApprovalStatus { get; set; } // "0" PLEASE SELECT[INITIATED], "1" YES, "2" NO
        public string ClosureInitiatedBy { get; set; } 
        public string ClosureApproveddBy { get; set; }
        [Display(Name = "STORE DATE تاريخ حفظ الملف")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StoreDate { get; set; }
        [Display(Name = "NOTES ملاحظات")]
        public string StoreNotes { get; set; }
        public string FileClosureCategory { get; set; }
        [Display(Name = "PASS كلمة المرور")]
        public string AccountPassword { get; set; }
        [Display(Name = "PASS كلمة المرور")]
        public string StorePassword { get; set; }
        [Display(Name = "ACC. AUDIT تدقيق قسم المحاسبة")]
        public string AccountAudit { get; set; }
        [Display(Name = "DATE")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AccountAuditDate { get; set; }
        [Display(Name = "ARCHIVED أرشفة الملف")]
        public string FileArchieved { get; set; }
        public string ClosureArchieveddBy { get; set; }
        public int TotalPartialClosure { get; set; }
        #region BASIC INFO
        public string ClientClassificationCode { get; set; } // FOR CONDITION PURPOSE not Entry
        public string ClientCode { get; set; } // FOR CONDITION PURPOSE not Entry

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "RECEIVED DATE")]
        public DateTime? ReceptionDate { get; set; } // FOR CONDITION PURPOSE not Entry

        [Display(Name = "RECEIVE LEVEL")]
        public string ReceiveLevelCode { get; set; } //Dropdown 

        [Display(Name = "CASE TYPE")]
        public string CaseTypeCode { get; set; } //Dropdown 

        [Display(Name = "AGAINST")]
        public string AgainstCode { get; set; } //Dropdown 

        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "CLAIM AMOUNT")]
        public decimal? ClaimAmount { get; set; }

        [Display(Name = "NATIONALITY")]
        public string OmaniExp { get; set; }

        [Display(Name = "ODB LOAN TYPE")]
        public string ClientCaseType { get; set; }

        public string AccountContractNo { get; set; }
        public string ClientFileNo { get; set; }

        [Display(Name = "ODB BANK BRANCH")]
        public string ODBBankBranch { get; set; } //Dropdown 

        [Display(Name = "LOAN MANAGER")]
        public string LoanManager { get; set; }  //Dropdown 
        public string GovernorateId { get; set; }
        public string ClientName { get; set; }
        public string Defendant { get; set; }
        public string btnSave { get; set; }
        public string FinalOnvoiceOnHold { get; set; }
        public DateTime? SuggestedDate { get; set; }
        public string JudRecRedStamp { get; set; }
        #endregion
        public FileClosureEnteryVM()
        {
            PartDetailId = 0;
            ClosureApprovalStatus = "0";
            PartNo = "1";
        }
    }
    public class FinanceVM
    {
        public int CaseId { get; set; }

        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "CLAIM AMOUNT")]
        public decimal? ClaimAmount { get; set; }

        [Display(Name = "FINANCE NOTES")]
        public string SpecialInstructions { get; set; }

        [Display(Name = "DATE OF LAST INVOICE")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfLastInvoice { get; set; }

        [Display(Name = "NOTES")]
        public string ClosingNotes { get; set; }
        public string TransportationFee { get; set; }
        public string Translation { get; set; }
    }
    public class CourtStatusVM
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Status Date")]
        public DateTime CaseStatusDate { get; set; }
        public string CaseStatus { get; set; }
        public string CaseStatusName { get; set; }
    }
    public class CaseDefaulterVM
    {
        public string OfficeFileNo { get; set; }
        public string AccountContractNo { get; set; }
        public string ClientFileNo { get; set; }
        public string Defendant { get; set; }
        public string CaseStatusName { get; set; }
    }
}