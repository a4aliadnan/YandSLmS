namespace YandS.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [Table("PayVoucher")]
    public class PayVoucher : Base
    {
        [Key]
        public int Voucher_No { get; set; }

        [Required]
        [Display(Name = "VOUCHER DATE")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime Voucher_Date { get; set; } // ApplicationUser => UserName

        [Required]
        [Display(Name = "PAYMENT TYPE")]
        [StringLength(2)]
        public string Payment_Type { get; set; }

        [Required]
        [Display(Name = "PAY FROM")]
        public int Debit_Account { get; set; }

        [Display(Name = "PAY TYPE")]
        [StringLength(6)]
        public string Payment_Head { get; set; }
        [StringLength(6)]
        public string PaymentHeadDetail { get; set; }
        [StringLength(500)]
        public string Payment_Head_Remarks { get; set; }


        [Display(Name = "PAY TO")]
        [StringLength(10)]
        public string Payment_To { get; set; }
        [StringLength(10)]
        public string PaymentToBenificry { get; set; }

        [Display(Name = "PAY TO")]
        public int Credit_Account { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        [Display(Name = "Amount المبلغ")]
        public decimal Amount { get; set; }
        public string Remarks { get; set; }

        [Required]
        [Display(Name = "PAYMENT MODE")]
        [StringLength(2)]
        public string Payment_Mode { get; set; }

        [Display(Name = "CHEQUE NUMBER / TRANSFER")]
        public string Cheque_Number { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CHEQUE / TRANSFER DATE")]
        [Column(TypeName = "datetime2")]
        public DateTime? Cheque_Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "RECEIVED ON")]
        [Column(TypeName = "datetime2")]
        public DateTime? Received_on { get; set; }

        [Required]
        [StringLength(2)]
        public string Status { get; set; }

        [Required]
        [Display(Name = "VOUCHER TYPE")]
        [StringLength(1)]
        public string VoucherType { get; set; }

        [Display(Name = "PV NUMBER")]
        public string PV_No { get; set; }// Format : Muscat-0001-2020 (Genrate after approval)
        public int? AuthorizeBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? AuthorizeDate { get; set; }
        [StringLength(2)]
        public string VoucherStatus { get; set; }
        [StringLength(2)]
        public string CourtType { get; set; }
        [Display(Name = "COURT LOCATION")]
        [StringLength(3)]
        public string LocationCode { get; set; }

        [Display(Name = "TRANSACTION TYPE")]
        [StringLength(3)]
        [SelectZeroVal(ErrorMessage = "Please Select Trans Type")]
        public string TransTypeCode { get; set; }

        [Display(Name = "TRANSACTION REASON")]
        [StringLength(3)]
        public string TransReasonCode { get; set; }

        public int? CaseId { get; set; }
        public CourtCases CourtCases { get; set; }

        [Display(Name = "Case Invoices")]
        public string CaseInvoices { get; set; }
        [Display(Name = "BANK TRANSFER AUTHORITY")]
        public string BankTransferStaff { get; set; } //Dropdown

        [Display(Name = "REFERENCE NO.")]
        [StringLength(1000)]
        public string BillNumber { get; set; }

        [Display(Name = "CHQ DATE")]
        [Column(TypeName = "datetime2")]
        public DateTime? FutureChequeDate { get; set; }

        [Display(Name = "CHEQUE NO.")]
        [StringLength(255)]
        public string PDCRefNo { get; set; }

        [Display(Name = "NOTIFICATON")]
        [StringLength(500)]
        public string SpecialNotification { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        [Display(Name = "Vat الضريبة")]
        public decimal? VatAmount { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? InvoiceProcessDate { get; set; }
        [StringLength(5)]
        public string PVLocation { get; set; }
        public bool IsDeleted { get; set; }
        #region Not Mapped Names

        [NotMapped]
        public string CreatedByLoginId { get; set; }

        [NotMapped]
        public string AuthorizeByLoginId { get; set; }

        [NotMapped]
        public string PaymentHeadName { get; set; }
        [NotMapped]
        public string PaymentToName { get; set; }
        [NotMapped]
        public string PaymentTypeName { get; set; }
        [NotMapped]
        public string DebitAccountName { get; set; }
        [NotMapped]
        public string CreditAccountName { get; set; }
        [NotMapped]
        public string PaymentModeName { get; set; }
        [NotMapped]
        public string StatusName { get; set; }
        [NotMapped]
        public string VoucherStatusName { get; set; }
        [NotMapped]
        public string VoucherTypeName { get; set; }
        [NotMapped]
        public string CourtTypeName { get; set; }
        [NotMapped]
        public string LocationName { get; set; }
        [NotMapped]
        public string CreatedByName { get; set; }
        [NotMapped]
        public string AuthorizeByName { get; set; }
        [NotMapped]
        public string Beneficiary { get; set; }
        [NotMapped]
        public string TransTypeName { get; set; }
        [NotMapped]
        public string TransReasonName { get; set; }

        [NotMapped]
        [Display(Name = "Defendant Name")]
        public string Defendant { get; set; }

        [NotMapped]
        [Display(Name = "Office File No")]
        public string OfficeFileNo { get; set; }

        [NotMapped]
        [Display(Name = "Office File No")]
        public string OfficeFileNoSessionRollPV { get; set; }

        [NotMapped]
        [Display(Name = "A/C Contract No")]
        public string AccountContractNo { get; set; }

        [NotMapped]
        [Display(Name = "Client File No")]
        public string ClientFileNo { get; set; }

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "Claim Amount")]
        public decimal? ClaimAmount { get; set; }

        [NotMapped]
        public string ClientName { get; set; }

        [NotMapped]
        public string ReceptionDate { get; set; }
        [NotMapped]
        public string RegistrationLevel { get; set; }

        [NotMapped]
        public string EnforcementNo { get; set; }

        [NotMapped]
        public string EnforcementCourt { get; set; }

        [NotMapped]
        public string ReturnApprove { get; set; }
        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total amounts مبلغ الفاتورة")]
        public decimal? TotalAmount { get; set; }

        [NotMapped]
        public string PayToMstValue { get; set; }
        [NotMapped]
        public string PayToMstDesc { get; set; }
        [NotMapped]
        public string PayToBankName { get; set; }
        [NotMapped]
        public string PayToAccountNumber { get; set; }
        [NotMapped]
        public string PayToEmail { get; set; }
        [NotMapped]
        public string PayToContactNo { get; set; }
        [NotMapped]
        [Display(Name = "BANK البنك")]
        public string PayToBankNameDisp { get; set; }
        [NotMapped]
        [Display(Name = "ACC الحساب")]
        public string PayToAccountNumberDisp { get; set; }

        #endregion

        #region Not Mapped ICollectionLists

        [NotMapped]
        public ICollection<MasterSetups> ListPaymentType { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListPaymentHead { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListPayTo { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListPaymentMode { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListLinkBankAccount { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListLinkBankAccountCR { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListStatus { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListVoucherStatus { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListVoucherType { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListCourtType { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListLocation { get; set; }

        #endregion
    }
}