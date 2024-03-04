namespace YandS.UI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SessionsRoll : Base
    {
        [Key]
        public int SessionRollId { get; set; }
        public int CaseId { get; set; }
        public CourtCases CourtCases { get; set; }

        [StringLength(2)]
        public string SessionLevel { get; set; }
        [StringLength(5)]
        public string CountLocationId { get; set; }
        [StringLength(2)]
        public string CaseType { get; set; }
        [StringLength(5)]
        public string LawyerId { get; set; }
                
        [Column(TypeName = "datetime2")]
        public DateTime? BeforeDate { get; set; }
        [StringLength(5)]
        public string FollowerId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PrimaryJudgementsDate { get; set; }
        public string PrimaryJudgements { get; set; }
        [StringLength(1)]
        public string PrimaryIsFavorable { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? PrimaryJDReceiveDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal? PrimaryFinalJDAmount { get; set; }
        [StringLength(255)]
        public string PrimaryFinalJudgedInterests { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? AppealJudgementsDate { get; set; }
        public string AppealJudgements { get; set; }
        [StringLength(1)]
        public string AppealIsFavorable { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? AppealJDReceiveDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal? AppealFinalJDAmount { get; set; }

        [StringLength(255)]
        public string AppealFinalJudgedInterests { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? EnforcementJudgementsDate { get; set; }
        public string EnforcementJudgements { get; set; }
        [StringLength(1)]
        public string EnforcementIsFavorable { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? EnforcementJDReceiveDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LastDate { get; set; }
        [StringLength(500)]
        public string WorkRequired { get; set; }
        [StringLength(500)]
        public string SessionNotes { get; set; }
        [StringLength(500)]
        public string SuspendedWorkRequired { get; set; }
        [StringLength(500)]
        public string SuspendedSessionNotes { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedOn { get; set; }
        public int? DeletedBy { get; set; }
        public string FollowNotes { get; set; }

        [StringLength(1)]
        public string PrimaryCrtInRegInProgress{ get; set; }
        [StringLength(1)]
        public string AppealCrtInRegInProgress{ get; set; }
        [StringLength(1)]
        public string EnforcementCrtInRegInProgress{ get; set; }
        public bool ShowFollowup{ get; set; }
        public bool ShowSuspend{ get; set; }
        [StringLength(1)]
        public string DifferentPanelYesSet { get; set; }
        public string DifferentPanelNotes { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? SupremeJudgementsDate { get; set; }
        public string SupremeJudgements { get; set; }
        [StringLength(1)]
        public string SupremeIsFavorable { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? SupremeJDReceiveDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal? SupremeFinalJDAmount { get; set; }

        [StringLength(255)]
        public string SupremeFinalJudgedInterests { get; set; }

        //Feb Sprint New Column
        [Column(TypeName = "datetime2")]
        public DateTime? SuspendedLastDate { get; set; }
        [StringLength(5)]
        public string SuspendedFollowerId { get; set; }
        [StringLength(2)]
        public string SessionOnHold { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? SessionOnHoldUntill { get; set; }
        [StringLength(10)]
        public string SessionFileStatus { get; set; }
        public bool FinalJudgement { get; set; }
        public int? CaseRegistrationId { get; set; }
        [StringLength(5)]
        public string CourtFollow_LawyerId { get; set; }
        [StringLength(4000)]
        public string SessionNote_Remark { get; set; }
        [StringLength(1)]
        public string JudgementLevel { get; set; }

        public SessionsRoll()
        {
            CaseType = "0";
            LawyerId = "0";
            SessionLevel = "0";
            CountLocationId = "0";
            FollowerId = "0";
            SuspendedFollowerId = "0";
            SessionFileStatus = "1";
            SessionOnHold = "0";
            JudgementLevel = "0";
        }
    }
}