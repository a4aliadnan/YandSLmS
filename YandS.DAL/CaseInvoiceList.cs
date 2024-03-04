namespace YandS.DAL
{
    using System;

    public class CaseInvoiceList
    {
        public int CaseId { get; set; }

        public string OfficeFileNo { get; set; }

        public string ClientCode { get; set; }

        public string ClientName { get; set; }

        public string Defendant { get; set; }

        public string AgainstCode { get; set; }

        public string AccountContractNo { get; set; }

        public string ClientFileNo { get; set; }

        public decimal ClaimAmount { get; set; }

        public decimal InvoiceAmount { get; set; }

        public string AgainstName { get; set; }

        public int InvoiceId { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime? InvoiceDate { get; set; }
        public DateTime? TransferDate { get; set; }

        public string CourtType { get; set; }

        public string InvoiceStatus { get; set; }
        public string InvoiceStatusName { get; set; }

        public string CourtName { get; set; }

        public string CourtLevelDisp { get; set; }

        public int Credit_Account { get; set; }

        public string CreditAccountName { get; set; }
        public int TotalRecords { get; set; }

    }
}