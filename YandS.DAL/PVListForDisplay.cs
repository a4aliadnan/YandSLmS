namespace YandS.DAL
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PVListForDisplay : PVListForIndex
    {
        [Display(Name = "Payment Type")]
        public string Payment_Type { get; set; }

        [Display(Name = "Pay From")]
        public int Debit_Account { get; set; }

        [Display(Name = "Pay Type")]
        public string Payment_Head { get; set; }

        public string Payment_Head_Remarks { get; set; }

        [Display(Name = "Pay To")]
        public string Payment_To { get; set; }

        [Display(Name = "Pay To")]
        public int Credit_Account { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "Cheque Number / Transfer")]
        public string Cheque_Number { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Cheque / Transfer Date")]
        public DateTime? Cheque_Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Received on")]
        public DateTime? Received_on { get; set; }

        [Display(Name = "Voucher Type")]
        public string VoucherType { get; set; }

        public int? AuthorizeBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AuthorizeDate { get; set; }

        [Display(Name = "Transaction Type")]
        public string TransTypeCode { get; set; }

        [Display(Name = "Transaction Reason")]
        public string TransReasonCode { get; set; }


        [Display(Name = "Bank Transfer Authority")]
        public string BankTransferStaff { get; set; } //Dropdown



        public string PaymentHeadName { get; set; }

        public string PaymentToName { get; set; }

        public string PaymentTypeName { get; set; }

        public string DebitAccountName { get; set; }

        public string CreditAccountName { get; set; }

        public string CreatedByName { get; set; }

        public string AuthorizeByName { get; set; }

        public string TransTypeName { get; set; }

        public string TransReasonName { get; set; }


        [Display(Name = "Defendant Name")]
        public string Defendant { get; set; }


        [Display(Name = "Office File No")]
        public string OfficeFileNo { get; set; }


        [Display(Name = "A/C Contract No")]
        public string AccountContractNo { get; set; }


        [Display(Name = "Client File No")]
        public string ClientFileNo { get; set; }


        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "Claim Amount")]
        public decimal? ClaimAmount { get; set; }


        public string ClientName { get; set; }


    }
}