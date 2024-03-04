namespace YandS.DAL
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class DefendentTransferDTO
    {
        public int DefendentTransferId { get; set; }
        public int CaseId { get; set; }
        public string CaseLevelCode { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MoneyTrRequestDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TransferDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MoneyTrCompleteDate { get; set; }
        public string MoneyWith { get; set; }
        public int Userid { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedByDate { get; set; }
        public string UpdatedByName { get; set; }
        public string UpdatedByDate { get; set; }
        public string DataFor { get; set; }
        public int TotalRecords { get; set; }

    }
}