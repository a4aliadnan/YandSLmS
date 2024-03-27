namespace YandS.DAL
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CourtCaseListForIndex
    {
        public int CaseId { get; set; }

        [Display(Name = "Office File No")]
        public string OfficeFileNo { get; set; }

        [Display(Name = "Client Name")]
        public string ClientName { get; set; }

        [Display(Name = "Defendant Name")]
        public string DefClientName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Received Date")]
        public DateTime? ReceptionDate { get; set; }

        [Display(Name = "A/C Contract No")]
        public string AccountContractNo { get; set; }

        [Display(Name = "Client File No")]
        public string ClientFileNo { get; set; }
        public string CaseTypeName { get; set; }
        public string CaseLevelName { get; set; }
        public string CaseStatusName { get; set; }
        public string AgainstName { get; set; }
        public string CaseLevelCode { get; set; }
        public string CaseStatus { get; set; }
        public int ToBeRegisterDays { get; set; }
        public string CourtRefNo { get; set; }
        public string COURT { get; set; }
        public string FileStatus { get; set; }
        public string FileStatusName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ActionDate { get; set; }
        public string Notes { get; set; }
        public int TotalRecords { get; set; }
        public int UserNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CurrentHearingDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NextHearingDate { get; set; }
        public string CourtDecision { get; set; }
        public string CourtFollowRequirement { get; set; }
        public DateTime? ClosureDate { get; set; }
        public string CurrentEnforcementLevel { get; set; }
        public int DaysCounter { get; set; }
        public int AnotherDaysCounter { get; set; }
        public string AnnouncementType { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AnnouncementCompleteDt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SubmissionDt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ApprovalDt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? InquiryDt { get; set; }
        public string ArrestOrderNo { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ArrestOrderIssueDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ArrestedDate { get; set; }
        public string PoliceStationName { get; set; }
        public string AuctionProcess { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? JUDAuctionDate { get; set; }
        public decimal? JUDAuctionValue { get; set; }
        public string JUDAuctionRepeat { get; set; }
        public int? SuspensionPeriod { get; set; }
        public int? DaysRemaining { get; set; }
        public string SuspensionCause { get; set; }
        public string JUDDecision { get; set; }
        public string CauseOfRecovery { get; set; }
        public string LawyerName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfInstruction { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MoneyTrRequestDate { get; set; }
        public string Requirements { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FirstEmailDate { get; set; }
        public DateTime? CommissioningDate { get; set; }
        public int? PausedDays { get; set; }
        public string Mst_Value { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TransferDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MoneyTrCompleteDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        [Display(Name = "AMOUNT")]
        public decimal Amount { get; set; }
        public int DefendentTransferId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DEF_DateOfContact { get; set; }
        public string DEF_CallerName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DEF_VisitDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SuspensionEndDate { get; set; }
        public string GovernorateId { get; set; }
        public string GovernorateName { get; set; }
        public string Subject { get; set; }
        public string CorporateWorkDetail { get; set; }
        public string SessionRollDefendentName { get; set; }
        public string SessionRollClientName { get; set; }
        public decimal CorporateFee { get; set; }
        public string MoneyWithName { get; set; }
        public string CourtApproval { get; set; }
        public string ArrestLevel { get; set; }
    }

    public class CourtCaseDTView
    {
        public System.Collections.Generic.List<CourtCaseListForIndex> data { get; set; }
        public int MCTRecords { get; set; }
        public int SLLRecords { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }

    }
}