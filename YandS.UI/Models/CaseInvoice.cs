using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YandS.UI.Models
{
    public class CaseInvoice : Base
    {
        [Key]
        public int InvoiceId { get; set; }

        [Display(Name = "INVOICE DATE")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? InvoiceDate { get; set; }

        [Display(Name = "INVOICE NUMBER")]
        public string InvoiceNumber { get; set; }

        public int CaseId { get; set; }
        public CourtCases CourtCases { get; set; }

        [Required]
        [Display(Name = "CASE LEVEL")]
        [StringLength(2)]
        public string CourtType { get; set; } //DropDown Court List, 
                                              //Case Level to Generate Invoice
        [Display(Name = "INVOICE STATUS")]
        public string InvoiceStatus { get; set; } // From Master Table {Paid, Un-Paid, Cancel}

        public string TransferType { get; set; }

        [Display(Name = "TRANSFER/CHEQUE NUMBER")]
        public string TransferNumber { get; set; }

        [Display(Name = "PAID DATE")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RestrictFutureDate(ErrorMessage = "FUTURE DATE NOT ALLOWED FOR PAID DATE")]
        [Column(TypeName = "datetime2")]
        public DateTime? TransferDate { get; set; }

        [Display(Name = "TRANSFER INVOICE AMOUNT TO")]
        public int Credit_Account { get; set; }
        public ICollection<CaseInvoiceFee> FeeId { get; set; }
        public ICollection<CaseInvoiceFeeCalculation> FeeCaclId { get; set; }
        public string CourtRefNo { get; set; }
        public string CourtLocation { get; set; }
        [Display(Name = "SHOW ON INVOICE")]
        public bool ShowInInvoice { get; set; }

        [Display(Name = "LAST INVOICE ?")]
        public bool IsLastInvoice { get; set; }

        [Display(Name = "EXPECTED FEES")]
        [Required(ErrorMessage = "EXPECTED FEES IS REQUIRED")]
        public string ExpectedFees { get; set; }
        public string BeforeCourtStage { get; set; }
        [Display(Name = "ENFORCEMENT STAGE")]
        public string EnforcementStage { get; set; }
        [Display(Name = "ENFORCEMENT STAGE NO")]
        public string EnforcementStageNo { get; set; }
        [Display(Name = "COUNSULTING FEE TYPE")]
        public string CounsultingFeeType { get; set; }
        [DisplayFormat(DataFormatString = "{0:###0.0#}", ApplyFormatInEditMode = true)]
        [Display(Name = "NO. OF HOURS")]
        public decimal? HourlyNoOfHours { get; set; }

        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "RATE")]
        public decimal? HourlyRate { get; set; }
        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "AMOUNT")]
        public decimal? FixedAmount { get; set; }
        [Display(Name = "MONTH")]
        [DisplayFormat(DataFormatString = "{0:###0.0#}", ApplyFormatInEditMode = true)]
        public decimal? RetainershipMonth { get; set; }


        public CaseInvoice()
        {
            FeeId = new HashSet<CaseInvoiceFee>();
            FeeCaclId = new HashSet<CaseInvoiceFeeCalculation>();
            InvoiceStatus = "1";
            InvoiceDate = DateTime.Now;
        }
       
        #region NotMapped Names

        [NotMapped]
        public string InvoiceStatusName { get; set; }

        [NotMapped]
        [Display(Name = "DEFENDANT NAME")]
        public string Defendant { get; set; }

        [NotMapped]
        [Display(Name = "FILE NO")]
        public string OfficeFileNo { get; set; }

        [NotMapped]
        [Display(Name = "A/C CONTRACT NO")]
        public string AccountContractNo { get; set; }

        [NotMapped]
        [Display(Name = "CLIENT FILE NO")]
        public string ClientFileNo { get; set; }

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "CLAIM AMOUNT")]
        public decimal? ClaimAmount { get; set; }

        [NotMapped]
        public string CourtName { get; set; }
        [NotMapped]
        [Display(Name = "PLEASE TRANSFER THE BILL AMOUNT TO OUR A/C")]
        public string CreditAccountName { get; set; }
        [NotMapped]
        public string ClientName { get; set; }
        [NotMapped]
        public string ClientCaseTypeName { get; set; }
        [NotMapped]
        public string CreatedByBranchName { get; set; }
        [NotMapped]
        public string buttonName { get; set; }

        [NotMapped]
        public string CaseRegisterDate { get; set; }

        [NotMapped]
        public string CourtNumber { get; set; }

        [NotMapped]
        public string CourtLocationName { get; set; }

        [NotMapped]
        [Display(Name = "SUBJECT")]
        public string Subject { get; set; }
        [NotMapped]
        public string ClientCaseType { get; set; }

        [NotMapped]
        public string CaseTypeName { get; set; }

        [NotMapped]
        public string ClientClassificationCode { get; set; }
        [NotMapped]
        public string ClientClassificationName { get; set; }

        [NotMapped]
        public string ODBBranchName { get; set; }
        [NotMapped]
        public string CaseSubjectName { get; set; }

        [NotMapped]
        public string CaseAgainst { get; set; }
        [NotMapped]
        public string CaseAgainstCode { get; set; }

        [NotMapped]
        public string EnforcementStageName { get; set; }




        #endregion

    }
    public class CaseInvoiceVM
    {
        public int InvoiceId { get; set; }

        [Display(Name = "INVOICE DATE")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? InvoiceDate { get; set; }

        public string InvoiceNumber { get; set; }

        public int CaseId { get; set; }

        [Display(Name = "CURRENT CASE LEVEL")]
        public string CourtType { get; set; } //DropDown Case Level to Generate Invoice

        [Display(Name = "INVOICE STATUS")]
        public string InvoiceStatus { get; set; } // From Master Table {Paid, Un-Paid, Cancel}

        public string TransferType { get; set; }

        [Display(Name = "TRANSFER/CHEQUE NUMBER")]
        public string TransferNumber { get; set; }

        [Display(Name = "TRANSFER/CHEQUE DATE")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RestrictFutureDate(ErrorMessage = "Future Date not allowed for Transfer/Cheque Date")]
        public DateTime? TransferDate { get; set; }

        [Display(Name = "PLEASE TRANSFER THE BILL AMOUNT TO OUR A/C")]
        public int Credit_Account { get; set; }

        public string CourtRefNo { get; set; }

        [Display(Name = "SHOW ON INVOICE")]
        public bool ShowInInvoice { get; set; }
        public string InvoiceStatusName { get; set; }

        [Display(Name = "DEFENDANT NAME")]
        public string Defendant { get; set; }

        [Display(Name = "FILE NO")]
        public string OfficeFileNo { get; set; }

        [Display(Name = "A/C CONTRACT NO")]
        public string AccountContractNo { get; set; }

        [Display(Name = "CLIENT FILE NO")]
        public string ClientFileNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "CLAIM AMOUNT")]
        public decimal? ClaimAmount { get; set; }

        [Display(Name = "CLIENT FILE NO")]
        public string CourtName { get; set; }

        [Display(Name = "PLEASE TRANSFER THE BILL AMOUNT TO OUR A/C")]
        public string CreditAccountName { get; set; }

        public string ClientName { get; set; }
        public string ClientCaseTypeName { get; set; }
        public string CreatedByBranchName { get; set; }
        public string buttonName { get; set; }
        public string CaseRegisterDate { get; set; }

        public string CourtLocationName { get; set; }
        public string EnforcementStage { get; set; }
        public string EnforcementStageNo { get; set; }
        public string BeforeCourtStage { get; set; }
        public string BeforeCourtStageNo { get; set; }
        public string BeforeCourtStageLocation { get; set; }
        public string Subject { get; set; }
        public string CounsultingFeeType { get; set; }
        public double HourlyNoOfHours { get; set; }
        public double HourlyRate { get; set; }
        public double FixedAmount { get; set; }
        public double RetainershipMonth { get; set; }


    }
}