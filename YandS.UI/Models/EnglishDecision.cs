namespace YandS.UI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public class EnglishDecision
    {
        [Key]
        public int DecisionId { get; set; }
        public int CaseId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? CurrentHearingDate { get; set; }
        public string CourtDecision { get; set; }

    }
}