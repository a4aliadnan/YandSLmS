using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YandS.UI.Models
{
    public class CaseInvoiceFeeCalculation
    {
        [Key]
        public int FeeCaclId { get; set; }
        public int InvoiceId { get; set; }
        public CaseInvoice CaseInvoice { get; set; }

        [Display(Name = "CLAIM AMOUNT %")]
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal ClaimAmountPct { get; set; }

        [Display(Name = "FEE AMOUNT %")]
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal FeeAmountPct { get; set; }
        [Display(Name = " ")]
        public bool ShowInInvoice { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        [Display(Name = "CLAIM AMOUNT")]
        public decimal? ClaimAmount { get; set; }

        #region NotMapped Names

        [NotMapped]
        [Display(Name = "Calculated Claim Amount")]
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        public decimal ClaimAmountCalculated { get; set; }

        [NotMapped]
        [Display(Name = "Calculated Fee Amount")]
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal FeeAmountCalculated { get; set; }

        #endregion

    }
}