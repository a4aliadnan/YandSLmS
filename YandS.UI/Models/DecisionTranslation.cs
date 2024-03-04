namespace YandS.UI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DecisionTranslation : Base
    {
        [Key]
        public int TranslationId { get; set; }
        public int CaseId { get; set; }
        public CourtCases CourtCases { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? CurrentHearingDate { get; set; }
        public string CourtDecision { get; set; }
        public string CourtDecisionTranslated { get; set; }
        public bool TranslationDone { get; set; }

        public DecisionTranslation()
        {
            TranslationDone = false;
        }
    }
    public class DecisionTranslationVM
    {
        public int TranslationId { get; set; }
        public int CaseId { get; set; }
        public DateTime? CurrentHearingDate { get; set; }
        public string CourtDecision { get; set; }
        public string CourtDecisionTranslated { get; set; }
        public bool TranslationDone { get; set; }
    }
}