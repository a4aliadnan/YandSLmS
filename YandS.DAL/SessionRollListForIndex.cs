namespace YandS.DAL
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SessionRollListForIndex
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
        public string CaseTypeName { get; set; }
        public string LawyerId { get; set; }
        public string LawyerName { get; set; }
        public string SessionRemarks { get; set; }
        public string CurrentHearingDate { get; set; }
        public string CourtDecision { get; set; }
        public string NextHearingDate { get; set; }
        public string CaseDismisses { get; set; }
        public string Requirements { get; set; }
        public string CourtRegistration { get; set; }
        public string BeforeDate { get; set; }
        public string FollowerId { get; set; }
        public string FollowerName { get; set; }
        public string FinalJDDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public string PrimaryJudgementsDate { get; set; }
        public string PrimaryJudgements { get; set; }
        public string PrimaryIsFavorable { get; set; }
        public string PrimaryJDReceiveDate { get; set; }
        public decimal? PrimaryFinalJDAmount { get; set; }
        public string PrimaryFinalJudgedInterests { get; set; }
        public string PrimaryAllJudgements { get; set; }
        public string AppealJudgementsDate { get; set; }
        public string AppealJudgements { get; set; }
        public string AppealIsFavorable { get; set; }
        public string AppealJDReceiveDate { get; set; }
        public decimal? AppealFinalJDAmount { get; set; }
        public string AppealFinalJudgedInterests { get; set; }
        public string AppealAllJudgements { get; set; }
        public string EnforcementJudgementsDate { get; set; }
        public string EnforcementJudgements { get; set; }
        public string EnforcementIsFavorable { get; set; }
        public string EnforcementJDReceiveDate { get; set; }
        public string LastDate { get; set; }
        public string WorkRequired { get; set; }
        public string SessionNotes { get; set; }
        public string CurrentCaseLevel { get; set; }
        public string ClaimSummary { get; set; }
        public string AccountContractNo { get; set; }
        public string ClientFileNo { get; set; }
        public string CreatedByName { get; set; }
        public string UpdatedByName { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string LocationCode { get; set; }
        public int CreatedDaysPassed { get; set; }
        public int UpdatedDaysPassed { get; set; }
        public int TotalRecords { get; set; }
        public int DaysCounterFollow { get; set; }
        public int DaysCounterTobeUpdate { get; set; }
        public int FJDCounter { get; set; }
        public string OfficeFileNo { get; set; }
        public string OfficeFN { get; set; }
        public string FollowNotes { get; set; }
        public string DisplaySentence { get; set; }
        public string IsNoFavorbleDecision { get; set; }
        public string CourtRefNo { get; set; }
        public string SupremeJudgementsDate { get; set; }
        public string DifferentPanelNotes { get; set; }
        public string SuspendedWorkRequired { get; set; }
        public string SuspendedSessionNotes { get; set; }

        public string SuspendedLastDate { get; set; }
        public string SuspendedFollowerId { get; set; }
        public string SuspendedFollowerName { get; set; }
        public string SessionOnHold { get; set; }
        public string SessionOnHoldDesc { get; set; }
        public string SessionOnHoldUntill { get; set; }
        public string SessionFileStatus { get; set; }
        public string SessionFileStatusDesc { get; set; }
        public string AgainstName { get; set; }

    }
}