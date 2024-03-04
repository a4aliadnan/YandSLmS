using System;
using System.ComponentModel.DataAnnotations;

namespace YandS.UI.Models
{
    public class PayVoucherVM
    {
        public int Voucher_No { get; set; }

        [Display(Name = "VOUCHER DATE")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Voucher_Date { get; set; } // ApplicationUser => UserName

        [Display(Name = "PAYMENT TYPE")]
        public string Payment_Type { get; set; }

        [Display(Name = "PAY FROM")]
        public int Debit_Account { get; set; }

        [Display(Name = "PAY TYPE")]
        public string Payment_Head { get; set; }
        public string PaymentHeadDetail { get; set; }

        public string Payment_Head_Remarks { get; set; }


        [Display(Name = "PAY TO")]
        public string Payment_To { get; set; }

        [Display(Name = "PAY TO")]
        public int Credit_Account { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        [Display(Name = "Amount المبلغ")]
        public decimal? Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        [Display(Name = "Vat الضريبة")]
        public decimal? VatAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total amounts مبلغ الفاتورة")]
        public decimal? TotalAmount { get; set; }
        public string Remarks { get; set; }

        [Display(Name = "PAYMENT MODE")]
        public string Payment_Mode { get; set; }

        [Display(Name = "CHEQUE NUMBER / TRANSFER")]
        public string Cheque_Number { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CHEQUE / TRANSFER DATE")]
        public DateTime? Cheque_Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "RECEIVED ON")]
        public DateTime? Received_on { get; set; }

        public string Status { get; set; }

        [Display(Name = "VOUCHER TYPE")]
        public string VoucherType { get; set; }

        [Display(Name = "PV NUMBER")]
        public string PV_No { get; set; }// Format : Muscat-0001-2020 (Genrate after approval)
        public int? AuthorizeBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AuthorizeDate { get; set; }
        [StringLength(2)]
        public string VoucherStatus { get; set; }
        [StringLength(2)]
        public string CourtType { get; set; }
        [Display(Name = "COURT LOCATION")]
        public string LocationCode { get; set; }

        [Display(Name = "TRANSACTION TYPE")]
        public string TransTypeCode { get; set; }

        [Display(Name = "TRANSACTION REASON")]
        public string TransReasonCode { get; set; }

        public int? CaseId { get; set; }

        [Display(Name = "Case Invoices")]
        public string CaseInvoices { get; set; }
        [Display(Name = "BANK TRANSFER AUTHORITY")]
        public string BankTransferStaff { get; set; } //Dropdown

        [Display(Name = "REFERENCE NO.")]
        public string BillNumber { get; set; }

        [Display(Name = "CHQ DATE")]
        public DateTime? FutureChequeDate { get; set; }

        [Display(Name = "PDC REFERENCE NO.")]
        public string PDCRefNo { get; set; }

        [Display(Name = "NOTIFICATON")]
        public string SpecialNotification { get; set; }
        public string PayToMstValue { get; set; }
        public string PaymentToBenificry { get; set; }
        public string PayToMstDesc { get; set; }
        public string PayToBankName { get; set; }
        public string PayToAccountNumber { get; set; }
        public string PayToEmail { get; set; }
        public string PayToContactNo { get; set; }
        [Display(Name = "BANK البنك")]
        public string PayToBankNameDisp { get; set; }
        [Display(Name = "ACC الحساب")]
        public string PayToAccountNumberDisp { get; set; }
        public string PayToMessageLang { get; set; }
        #region Not Mapped Names

        public string CreatedByLoginId { get; set; }
        public string AuthorizeByLoginId { get; set; }
        public string PaymentHeadName { get; set; }
        public string PaymentToName { get; set; }
        public string PaymentTypeName { get; set; }
        public string DebitAccountName { get; set; }
        public string CreditAccountName { get; set; }
        public string PaymentModeName { get; set; }
        public string StatusName { get; set; }
        public string VoucherStatusName { get; set; }
        public string VoucherTypeName { get; set; }
        public string CourtTypeName { get; set; }
        public string LocationName { get; set; }
        public string CreatedByName { get; set; }
        public string AuthorizeByName { get; set; }
        public string Beneficiary { get; set; }
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
        public string ReceptionDate { get; set; }
        public string RegistrationLevel { get; set; }
        public string EnforcementNo { get; set; }
        public string EnforcementCourt { get; set; }

        #endregion

        public PayVoucherVM()
        {
        }
    }
}