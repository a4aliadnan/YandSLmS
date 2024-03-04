namespace YandS.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CourtCaseStatusDetail
    {
        [Key]
        public int StatusDetailId { get; set; }
        public int CaseId { get; set; }
        public CourtCases CourtCases { get; set; }

        [Display(Name = "Case Status")]
        public string StatusCode { get; set; } //Dropdown Case Status

        [Display(Name = "Status Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? StatusDate { get; set; }

        [Display(Name = "Reason")]
        public string ReasonCode { get; set; } //Dropdown Case Status
        public string CurrentCaseLevel{ get; set; } //Dropdown CurrentCaseLevel 499
        public string ClosureLevel{ get; set; } //Dropdown ClosureLevel 500
        public string FileAllocation{ get; set; } //Dropdown FileAllocation 501

        [Display(Name = "Litigation File Closure Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime? LitigationFileClosureDate { get; set; }

        public int? TempMonth { get; set; }

        [Display(Name = "Created By")]
        public int? CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created Date Time")]
        [Column(TypeName = "datetime2")]
        public DateTime? CreatedOn { get; set; }
        #region NotMapped Names

        [NotMapped]
        public string StatusName { get; set; }
        [NotMapped]
        public string ReasonName { get; set; }
        [NotMapped]
        public string CaseLevelName { get; set; }
        [NotMapped]
        public string CurrentCaseLevelName { get; set; }
        [NotMapped]
        public string ClosureLevelName { get; set; }
        [NotMapped]
        public string FileAllocationName { get; set; }

        #endregion
    }
}