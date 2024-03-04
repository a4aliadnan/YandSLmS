namespace YandS.DAL
{
    using System;

    public class CaseInvoiceDetail
    {
        public int CaseId { get; set; }

        public int InvoiceId { get; set; }

        public string InvoiceNumber { get; set; }

        public string InvoiceDate { get; set; }

        public string CaseLevelName { get; set; }
        public string InvClassificationName { get; set; }

        public string FeeTypeName { get; set; }

        public string FeeAmount { get; set; }


    }
}