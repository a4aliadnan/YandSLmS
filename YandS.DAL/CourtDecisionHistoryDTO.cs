namespace YandS.DAL
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class CourtDecisionHistoryDTO
    {
        public int Id { get; set; }
        public int SessionRollId { get; set; }
        public int CaseId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CurrentHearingDate { get; set; }
        public string CourtDecision { get; set; }
        public int Userid { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedByDate { get; set; }
        public string UpdatedByName { get; set; }
        public string UpdatedByDate { get; set; }
        public string DataFor { get; set; }
        public int TotalRecords { get; set; }
        public string TransportationSource { get; set; }
    }
    public class InvoiceCheckingDTO
    {
        public int CaseId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? InvoiceDate { get; set; }
        public int FeeTypeId { get; set; }
        public decimal FeeAmount { get; set; }

    }
}