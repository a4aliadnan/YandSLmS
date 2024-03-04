namespace YandS.DAL
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class InvoiceNotificationDTView
    {
        public int CaseId { get; set; } 
        public string OfficeFN { get; set; } //Y&S NO
        public int DaysCounter { get; set; } //Y&S NO
        public string COURT { get; set; }
        public string ClientName { get; set; }
        public string Defendant { get; set; }
        public string PaymentFor { get; set; }
        public string PaymentTo { get; set; }
        public double Amount { get; set; }
        public double VatAmount { get; set; }
        public double TotalAmount { get; set; }
        public string AccountContractNo { get; set; }
        public string CodeNumber { get; set; }
        public string RegistrationDate { get; set; }
        public string ReasonDesc { get; set; }
        public string UIDENT { get; set; }
        public string UpdateDate { get; set; }
        public int DaysCounterUpdateDate { get; set; }
        public int TotalRecords { get; set; }
        public string CurrentCaseLevel { get; set; }
        public string SugestedDate { get; set; }
        public string ClosingNotes { get; set; }
        public string CourtDecision { get; set; }
        public string TransSource { get; set; }
    }
    public class PayVoucherDTView
    {
        public System.Collections.Generic.List<PayVoucherData> data { get; set; }
        public string MuscatTTL { get; set; }
        public string SalalahTTL { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }

    }
    public class PayVoucherData : PVListForIndex
    {
        public int DaysCounter { get; set; }
    }
    public class PVListForIndex
    {
        public int Voucher_No { get; set; }

        [Display(Name = "PV Number")]
        public string PV_No { get; set; }

        [Display(Name = "Voucher Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}", ApplyFormatInEditMode = true)]
        public string Voucher_Date { get; set; }


        [Display(Name = "Voucher Type")]
        public string VoucherTypeName { get; set; }

        [Display(Name = "Authorize Status")]
        public string VoucherStatusName { get; set; }
        public string VoucherStatus { get; set; }

        [Display(Name = "Amount")]
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        [Display(Name = "Vat Amount")]
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal VatAmount { get; set; }
        [Display(Name = "Total Amount")]
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Status")]
        public string StatusName { get; set; }
        public string Status { get; set; }

        [Display(Name = "Invoice Numbers")]
        public string CaseInvoices { get; set; }

        [Display(Name = "Payment Mode")]
        public string PaymentModeName { get; set; }
        [Display(Name = "Payment Mode")]
        public string Payment_Mode { get; set; }
        public string LocationCode { get; set; }
        public string Payment_Head { get; set; }
        public string PaymentHeadName { get; set; }
        public string Payment_To { get; set; }
        public string PaymentToName { get; set; }

        [Display(Name = "W/D Date")]
        public string W_D_Date { get; set; }
        public string FutureChequeDate { get; set; }
        public string TransReasonName { get; set; }
        public string TransReasonCode { get; set; }
        public string Remarks { get; set; }
        public string BillNumber { get; set; }
        public string W_D_BANK { get; set; }
        public string OfficeFileNo { get; set; } //Y&S NO
        public string ClientName { get; set; }
        public string Defendant { get; set; }
        public string CaseLevel { get; set; }
        public string REF_PAID { get; set; }
        public string RejectReason { get; set; }
        public int DaysLeft { get; set; }
        public string PDCRefNo { get; set; }
        public string SpecialNotification { get; set; }
        public string PaymentHeadDetail { get; set; }
        public string PaymentHeadDetailName { get; set; }
        public string PaymentToBenificry { get; set; }
        public string PaymentToBenificryName { get; set; }
        public int TotalRecords { get; set; }
        public string CourtRefNo { get; set; }
        public string PayToBankName { get; set; }
        public string PayToAccountNumber { get; set; }
    }
}