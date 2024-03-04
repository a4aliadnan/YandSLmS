using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YandS.UI.Models
{
    public class CaseInvoiceFee
    {
        [Key]
        public int FeeId { get; set; }
        public int InvoiceId { get; set; }
        public CaseInvoice CaseInvoice { get; set; }

        [Display(Name = "DESCRIPTION")]
        public string FeeTypeId { get; set; }
        [Display(Name = "CASE LEVEL")]
        public string CaseLevel { get; set; }
        [Display(Name = "INVOICE CLASSIFICATION")]
        public string InvClassification { get; set; }

        [Range(1, 9999999, ErrorMessage = "The value must be greater than 0")]
        [Display(Name = "Amount")]
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal FeeAmount { get; set; }

        [Display(Name = "NUMBER")]
        public string FeeTypeDetail { get; set; }

        [Display(Name = "PV Number")]
        public string PV_No { get; set; }

        [Display(Name = "DETAILS")]
        public string FeeTypeCascadeDetail { get; set; }

        [Display(Name = "VAT %")]
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal? VATPct { get; set; }

        [NotMapped]
        [Display(Name = "VAT Amount")]
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal? VATAmount { get; set; }

        [NotMapped]
        [Display(Name = "Total Amount")]
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal? TotalAmount { get; set; }

    }
}