namespace YandS.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CourtCasesFollowup : Base
    {
        [Key]
        public int FollowupId { get; set; }
        public int DetailId { get; set; }
        public CourtCasesDetail CourtCasesDetail { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hearing Date")]
        [RestrictFutureDate(ErrorMessage = "Future Date not allowed for Hearing Date")]
        [Column(TypeName = "datetime2")]
        public DateTime HearingDate { get; set; }

        [Display(Name = "Hearing Remarks")]
        public string HearingRemarks { get; set; } //Dropdown 

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Next Hearing Date")]
        [DateGreaterThan(otherPropertyName = "HearingDate", ErrorMessage = "Next Hearing Date must be greater than Hearing Date")]
        [Column(TypeName = "datetime2")]
        public DateTime? NextHearingDate { get; set; }

        public CourtCasesFollowup()
        {
            HearingDate = DateTime.Now;
        }

    }
}