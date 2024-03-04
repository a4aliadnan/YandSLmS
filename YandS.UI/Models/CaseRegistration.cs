namespace YandS.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CaseRegistration : Base
    {
        [Key]
        public int CaseRegistrationId { get; set; }
        public int CaseId { get; set; }
        public CourtCases CourtCases { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ActionDate { get; set; }
        [StringLength(3)]
        public string ActionLevel { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? JudgementDate { get; set; }
        public int? UrgentCaseDays { get; set; }
        public string EnforcementDispute { get; set; }
        [StringLength(3)]
        public string CourtRegistration { get; set; }
        [StringLength(10)]
        public string FileStatus { get; set; }
        [StringLength(255)]
        public string FileStatusRemarks { get; set; }
        public string CourtMessage { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? SendDate { get; set; }
        public string OmanPostNo { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? FirstReminderDate { get; set; }
        public string ReminderNo { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? CourtRequestDate { get; set; }
        public string OfficeProcedure { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? PaymentDate { get; set; }
        [StringLength(255)]
        public string AssignedTo { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? AssignedDate { get; set; }
        public bool CourtDetailRegistered { get; set; }
        public string AdminFile { get; set; }
        [StringLength(3)]
        public string DepartmentType { get; set; } //INVESTMENT YES/NO
        public int? Voucher_No { get; set; }

        public string FormPrintDefendant { get; set; }
        public DateTime? FormPrintLastDate { get; set; }
        public string FormPrintWorkRequired { get; set; }
        public bool IsDeleted { get; set; }
        [StringLength(3)]
        public string RealEstateYesNo { get; set; }
        public string RealEstateDetail { get; set; }
        public string ClaimSummary { get; set; }
        public decimal? CourtFeeAmount { get; set; }
        [StringLength(100)]
        public string ElectronicNo { get; set; }

        //CourtCasesDetail Column JudgementDate will be use as NextHearingDate
        //CourtCasesDetail Column JudgementDetails will be use as NextHearingNotes

        //[Column(TypeName = "datetime2")]
        //public DateTime? NextHearingDate { get; set; }
        public string Remarks { get; set; }
        [StringLength(1)]
        public string OnHoldDone { get; set; }
        [StringLength(5)]
        public string ConsultantId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? StopRegEmailDate { get; set; }
        [StringLength(10)]
        public string StopRegUserName { get; set; }
        [StringLength(5)]
        public string LawyerId { get; set; }
        public string CourtLocationid { get; set; } //Dropdown Location
        [Column(TypeName = "datetime2")]
        public DateTime? RegistrationDate { get; set; }
        [StringLength(1)]
        public string DisputeLevel { get; set; }
        [StringLength(1)]
        public string DisputeType { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? DisputrRegisterDate { get; set; }
        public int? SessionRollId { get; set; }
        public CaseRegistration()
        {
            CaseRegistrationId = 0;
            ActionLevel = "1";
            FileStatus = "OFS-1";
            EnforcementDispute = "0";
            CourtRegistration = "0";
            DepartmentType = "0";
            CourtLocationid = "0";
            DisputeLevel = "0";
            DisputeType = "0";
            AdminFile = "";
            CourtDetailRegistered = false;
            IsDeleted = false;
        }
    }
}