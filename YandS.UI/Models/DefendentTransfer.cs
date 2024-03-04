namespace YandS.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DefendentTransfer")]
    public class DefendentTransfer
    {
        [Key]
        public int DefendentTransferId { get; set; }
        public int CaseId { get; set; }
        [StringLength(2)]
        public string CaseLevelCode { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime TransferDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        [Display(Name = "AMOUNT")]
        public decimal Amount { get; set; }

    }
}