namespace YandS.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DefendentContact")]
    public class DefendentContact : Base
    {
        [Key]
        public int DefendentContactId { get; set; }
        public int CaseId { get; set; }
        public CourtCases CourtCases { get; set; }
        [StringLength(2)]
        public string CaseLevelCode { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? DEF_DateOfContact { get; set; }

        [StringLength(4000)]
        public string DEF_MobileNo { get; set; }
        [StringLength(4000)]
        public string DEF_Corresponding { get; set; }
        [StringLength(5)]
        public string DEF_CallerName { get; set; }
        [StringLength(500)]
        public string DEF_LawyerId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? DEF_VisitDate { get; set; }
        [StringLength(2)]
        public string DEF_ContactType { get; set; }
    }
}