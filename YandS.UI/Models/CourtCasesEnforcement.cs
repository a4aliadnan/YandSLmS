namespace YandS.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CourtCasesEnforcement : Base
    {
        [Key]
        public int EnforcementId { get; set; }
        public int CaseId { get; set; }
        public CourtCases CourtCases { get; set; }

        [Display(Name = "ENFORCEMENT NO")]
        [StringLength(100)]
        public string EnforcementNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RestrictFutureDate(ErrorMessage = "Future Date not allowed for Received Date")]
        [Display(Name = "REGISTRATION DATE")]
        public DateTime? RegistrationDate { get; set; }

        [Display(Name = "Court Name")]
        [StringLength(2)]
        public string Courtid { get; set; } //Dropdown Court

        [Display(Name = "COURT")]
        [StringLength(5)]
        public string CourtLocationid { get; set; } //Dropdown Location


        [Display(Name = "CURRENT ENFORCEMENT LEVEL مرحلة التنفيذ الحالية")]
        [StringLength(10)]
        public string EnforcementlevelId { get; set; } //Dropdown EnforcementLevel 265

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RestrictFutureDate(ErrorMessage = "Future Date not allowed for Received Date")]
        [Display(Name = "SUSPENSION START DATE")]
        public DateTime? SuspensionStartDate { get; set; }

        [Display(Name = "SUSPENSION PERIOD")]
        public int? SuspensionPeriod { get; set; } // In Months

        [Display(Name = "CAUSE OF SUSPENSION")]
        [StringLength(3)]
        public string SuspensionCauseId { get; set; } //Dropdown CauseOfSuspension 272
        
        [Display(Name = "arrest order issued ?")]
        public bool ArrestOrderIssued { get; set; }
        [Display(Name = "arrest order status")]
        public string ArrestOrderStatusId { get; set; } //Dropdown ArrestOrderStatus 272

        [Display(Name = "ARREST ORDER NO")]
        [StringLength(100)]
        public string ArrestOrderNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RestrictFutureDate(ErrorMessage = "Future Date not allowed for Received Date")]
        [Display(Name = "ARREST ORDER ISSUE DATE")]
        public DateTime? ArrestOrderIssueDate { get; set; }

        [Display(Name = "POLICE STATION")]
        [StringLength(5)]
        public string PoliceStationid { get; set; } //Dropdown Location

        [Display(Name = "Objection Detail")]
        public string ObjectionDetail { get; set; }

        [Display(Name = "Objection Type")]
        public string ObjectionType { get; set; }

        [Display(Name = "PRIMARY OBJECTION NO.")]
        [StringLength(100)]
        public string PrimaryObjectionNo { get; set; }

        [Display(Name = "PRIMARY OBJECTION COURT")]
        [StringLength(5)]
        public string PrimaryObjectionCourt { get; set; }

        [Display(Name = "Primary Objection Court Location")]
        [StringLength(5)]
        public string PrimaryObjectionCourtLocationid { get; set; } //Dropdown Location

        [Display(Name = "APEAL OBJECTION NO.")]
        [StringLength(100)]
        public string ApealObjectionNo { get; set; }

        [Display(Name = "APEAL OBJECTION COURT")]
        [StringLength(5)]
        public string ApealObjectionCourt { get; set; }

        [Display(Name = "Apeal Objection Court Location")]
        [StringLength(5)]
        public string ApealObjectionCourtLocationid { get; set; } //Dropdown Location

        [Display(Name = "Plaint Detail")]
        public string PlaintDetail { get; set; }

        [Display(Name = "Plaint Type")]
        public string PlaintType { get; set; }

        [Display(Name = "PRIMARY PLAINT NO.")]
        [StringLength(100)]
        public string PrimaryPlaintNo { get; set; }

        [Display(Name = "PRIMARY PLAINT COURT")]
        [StringLength(5)]
        public string PrimaryPlaintCourt { get; set; }

        [Display(Name = "Primary Plaint Court Location")]
        [StringLength(5)]
        public string PrimaryPlaintCourtLocationid { get; set; } //Dropdown Location

        [Display(Name = "APEAL PLAINT NO.")]
        [StringLength(100)]
        public string ApealPlaintNo { get; set; }

        [Display(Name = "APEAL PLAINT COURT")]
        [StringLength(5)]
        public string ApealPlaintCourt { get; set; }
        [Display(Name = "Apeal Plaint Court Location")]
        [StringLength(5)]
        public string ApealPlaintCourtLocationid { get; set; } //Dropdown Location
        [StringLength(5)]
        public string EnforcementInfoInvoice { get; set; } //Radio 
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RestrictFutureDate(ErrorMessage = "Future date not allowed for Final Closure Date")]
        [Display(Name = "Closure Date")]
        [Column(TypeName = "datetime2")]
        public DateTime? ClosureDate { get; set; }

        [Display(Name = "Closed By")]
        [StringLength(50)]
        public string ClosedbyStaff { get; set; }

        [Display(Name = "ENFORCEMENT BY")]
        [StringLength(5)]
        public string EnforcementBy { get; set; }

        [Display(Name = "NAME")]
        [StringLength(255)]
        public string ArrestName { get; set; }

        [Display(Name = "ID NO.")]
        [StringLength(100)]
        public string ArrestIDNo { get; set; }

        [Display(Name = "SUPREME OBJECTION NO.")]
        [StringLength(100)]
        public string SupremeObjectionNo { get; set; }

        [Display(Name = "SUPREME OBJECTION COURT")]
        [StringLength(5)]
        public string SupremeObjectionCourt { get; set; }

        [Display(Name = "SUPREME PLAINT NO.")]
        [StringLength(100)]
        public string SupremePlaintNo { get; set; }

        [Display(Name = "SUPREME PLAINT COURT")]
        [StringLength(5)]
        public string SupremePlaintCourt { get; set; }
        [Display(Name = "ENFORCEMENT BY")]
        [StringLength(5)]
        public string EnforcementAdmin { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? ActionDate { get; set; }
        [StringLength(2)]
        public string AnnouncementTypeId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? AnnouncementCompleteDt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? SubmissionDt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? ApprovalDt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? InquiryDt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? MOHResultDt { get; set; }
        [StringLength(2)]
        public string MOHResult { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? ROPResultDt { get; set; }
        [StringLength(2)]
        public string ROPResult { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? BankResultDt { get; set; }
        [StringLength(2)]
        public string BankResult { get; set; }
        [StringLength(1)]
        public string Arrested { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? ArrestedDate { get; set; }
        [StringLength(3)]
        public string AuctionProcess { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? JUDAuctionDate { get; set; }
        public decimal? JUDAuctionValue { get; set; }
        [StringLength(3)]
        public string JUDAuctionRepeat { get; set; }
        [StringLength(2)]
        public string JUDDecisionId { get; set; }
        [StringLength(2)]
        public string CauseOfRecoveryId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? DateOfInstruction { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? MoneyTrRequestDate { get; set; }
        [StringLength(5)]
        public string LawyerId { get; set; }

        /* التواصل مع المنفذ ضدهم  CONTACT  DEFENDANT*/
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? DEF_DateOfContact { get; set; }
        [StringLength(4000)]
        public string DEF_MobileNo { get; set; }
        [StringLength(4000)]
        public string DEF_Corresponding { get; set; }

        [StringLength(20)]
        public string DEF_MobileNo2 { get; set; }

        [StringLength(20)]
        public string DEF_MobileNo3 { get; set; }

        [StringLength(20)]
        public string DEF_MobileNo4 { get; set; }

        [StringLength(20)]
        public string DEF_MobileNo5 { get; set; }

        [StringLength(5)]
        public string DEF_CallerName { get; set; }
        [StringLength(500)]
        public string DEF_LawyerId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? DEF_VisitDate { get; set; }
        [StringLength(2)]
        public string CourtDepartment { get; set; } //Dropdown 
        [StringLength(2)]
        public string CurrentDisputeLevelandType { get; set; } //Dropdown 
        [StringLength(1)]
        public string CourtApproval { get; set; } //Dropdown Location
        public CourtCasesEnforcement()
        {
            this.Courtid = "4";
            this.ArrestOrderStatusId = "0";
            this.PrimaryObjectionCourtLocationid = "0";
            this.ApealObjectionCourtLocationid = "0";
            this.PrimaryPlaintCourtLocationid = "0";
            this.ApealPlaintCourtLocationid = "0";
            this.NextCaseLevel = "";
            this.SessionClientId = "0";
            this.FollowerId = "0";
            this.ArrestOrderIssued =false;
        }
        #region NotMapped Names

        [NotMapped]
        public string CourtName { get; set; }
        [NotMapped]
        public string CourtLocationName { get; set; }
        [NotMapped]
        public string EnforcementlevelName { get; set; }
        [NotMapped]
        public string ArrestOrderStatusName { get; set; }
        [NotMapped]
        public string PoliceStationName { get; set; }
        [NotMapped]
        public string ApealPlaintCourtName { get; set; }
        [NotMapped]
        public string PrimaryPlaintCourtName { get; set; }
        [NotMapped]
        public string PrimaryObjectionCourtName { get; set; }
        [NotMapped]
        public string ApealObjectionCourtName { get; set; }
        [NotMapped]
        [Display(Name = "Y & S Updates")]
        public string YandSNotesUpdate { get; set; }
        [NotMapped]
        public string CaseLevelCode { get; set; }

        #region SESSION ROLL VM

        [NotMapped]
        public int SessionRollId { get; set; }

        #region SESSION ROLL VM - UPDATE

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CurrentHearingDate { get; set; }
        [NotMapped]
        public string CourtDecision { get; set; }
        [NotMapped]
        public string SessionRemarks { get; set; }
        [NotMapped]
        public string Requirements { get; set; }

        #endregion

        #region SESSION ROLL VM - FOLLOW

        [NotMapped]
        public string SessionClientId { get; set; }
        [NotMapped]
        public string SessionRollDefendentName { get; set; }
        [NotMapped]
        public bool ShowFollowup { get; set; }
        [NotMapped]
        public bool ShowSuspend { get; set; }
        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastDate { get; set; }
        [NotMapped]
        public string WorkRequired { get; set; }
        [NotMapped]
        public string SessionNotes { get; set; }
        [NotMapped]
        public string FollowerId { get; set; }
        [NotMapped]
        public string SuspendedWorkRequired { get; set; }
        [NotMapped]
        public string SuspendedSessionNotes { get; set; }

        #endregion

        #region SESSION ROLL VM - CLOSURE

        [NotMapped]
        public string NextCaseLevel { get; set; }
        [NotMapped]
        public string NextCaseLevelCode { get; set; }

        #endregion

        #endregion

        #endregion
    }
}