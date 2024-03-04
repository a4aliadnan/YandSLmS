namespace YandS.UI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ClosurePartialDetail : Base
    {
        [Key]
        public int PartDetailId { get; set; }
        public int CaseId { get; set; }
        public CourtCases CourtCases { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ClosurePartDate { get; set; }
        [StringLength(1)]
        public string FileTypeClosure { get; set; }
        [StringLength(1)]
        public string PartNo { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ClosingNotesDate { get; set; }
        public string ClosingNotes { get; set; }
        [StringLength(1)]
        public string ClosureApprovalStatus { get; set; }
        [StringLength(30)]
        public string ClosureInitiatedBy { get; set; }
        [StringLength(30)]
        public string ClosureApproveddBy { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? StoreDate { get; set; }
        public string StoreNotes { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? AccountAuditDate { get; set; }
        [StringLength(30)]
        public string ClosureArchieveddBy { get; set; }
        [StringLength(3)]
        public string FileAllocation { get; set; }
    }
}