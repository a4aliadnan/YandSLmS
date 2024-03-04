namespace YandS.DAL
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CaseRegistrationListForIndex
    {
        public int CaseRegistrationId { get; set; }
        public int CaseId { get; set; }

        public int DaysCounter { get; set; }

        [Display(Name = "FILE NO")]
        public string OfficeFileNo { get; set; }

        public string ClientCode { get; set; }

        [Display(Name = "CLIENT")]
        public string ClientName { get; set; }

        [Display(Name = "DEFENDANT")]
        public string Defendant { get; set; }

        public string ActionDate { get; set; }

        [Display(Name = "ACTION LEVEL")]
        public string ActionLevel { get; set; }
        public string ActionLevelName { get; set; }

        public string JudgementDate { get; set; }
        public string ReceptionDate { get; set; }

        public int? UrgentCaseDays { get; set; }
        public string EnforcementDispute { get; set; }
        [Display(Name = "ENFORCEMENT DISPUTE")]
        public string EnforcementDisputeName { get; set; }
        public string CourtRegistration { get; set; }
        [Display(Name = "COURT")]
        public string CourtRegistrationName { get; set; }
        public string FileStatus { get; set; }
        [Display(Name = "FILE STATUS")]
        public string FileStatusName { get; set; }
        public string ReasonOfStopRegistration { get; set; }
        public string OnHoldReason { get; set; }
        public string ElectronicNo { get; set; }
        public string CourtFee { get; set; }
        public string LawyerDetail { get; set; }
        public string CourtMessage { get; set; }
        public string CreatedByName { get; set; }
        public string UpdatedByName { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public int CreatedDaysPassed { get; set; }
        public int UpdatedDaysPassed { get; set; }
        public int TotalRecords { get; set; }

        public string SendDate { get; set; }
        public int SentCounter { get; set; }
        public string OmanPostNo { get; set; }
        public string FirstReminderDate { get; set; }
        public int ReminderCounter { get; set; }
        public int ActionCounter { get; set; }
        public int ActionCounter1 { get; set; }
        public int CourtRequestCounter { get; set; }
        public int PaymentCounter { get; set; }
        public int AssignedCounter { get; set; }
        public string ReminderNo { get; set; }
        public string CourtRequestDate { get; set; }
        public string OfficeProcedure { get; set; }
        public string OfficeProcedure1 { get; set; }
        public string PaymentDate { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedDate { get; set; }
        public string CourtDetailRegistered { get; set; }
        public string AdminFile { get; set; }
        public string NextHearingDate { get; set; }
        public string NextHearingNotes { get; set; }
        public string EnforcementNo { get; set; }
        public string DepartmentTypeName { get; set; }
        public string DepartmentType { get; set; }
        public int? Voucher_No { get; set; }
        public int IsUrgentCase { get; set; }
        public int IsMainRemarks { get; set; }

    }
}