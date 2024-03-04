namespace YandS.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CourtCasesDetail : Base
    {
        [Key]
        public int DetailId { get; set; }
        public int CaseId { get; set; }
        public CourtCases CourtCases { get; set; }

        [Display(Name = "Court")]
        [StringLength(3)]
        public string Courtid { get; set; } //Dropdown Court

        [Display(Name = "Court Registration No")]
        [StringLength(100)]
        public string CourtRefNo { get; set; }

        [Display(Name = "Court Location")]
        [StringLength(5)]
        public string CourtLocationid { get; set; } //Dropdown Location

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Registration Date")]
        [Column(TypeName = "datetime2")]
        public DateTime? RegistrationDate { get; set; }

        [Display(Name = "Court Department")]
        [StringLength(10)]
        public string CourtDepartment { get; set; } //Dropdown 

        [Display(Name = "Case Level")]
        [StringLength(2)]
        public string CaseLevelCode { get; set; } //Dropdown 

        [Display(Name = "Court Status")]
        [StringLength(5)]
        public string CourtStatus { get; set; }
        [Display(Name = "Apeal By Who")]
        [StringLength(2)]
        public string ApealByWho { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Judgement Date")]
        public DateTime? JudgementDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Judgement Receiving Date")]
        [Column(TypeName = "datetime2")]
        public DateTime? JudgementReceivingDate { get; set; }

        [Display(Name = "Judgement Details")]
        public string JudgementDetails { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RestrictFutureDate(ErrorMessage = "Future date not allowed for Final Closure Date")]
        [Display(Name = "Closure Date")]
        [Column(TypeName = "datetime2")]
        public DateTime? ClosureDate { get; set; }

        [Display(Name = "Closed By")]
        [StringLength(50)]
        public string ClosedbyStaff { get; set; }
        [StringLength(50)]
        public string NextCaseLevel { get; set; } //Dropdown 
        [StringLength(100)]
        public string NextCaseLevelCode { get; set; }
        [StringLength(3)]
        public string DepartmentType { get; set; } //INVESTMENT YES/NO
        public ICollection<CourtCasesFollowup> FollowupId { get; set; }

        public CourtCasesDetail()
        {
            FollowupId = new HashSet<CourtCasesFollowup>();
        }

        #region NotMapped Names

        [NotMapped]
        public string CourtName { get; set; }
        [NotMapped]
        public string CourtLocationName { get; set; }
        [NotMapped]
        public string CourtDepartmentName { get; set; }
        [NotMapped]
        public string CaseLevelName { get; set; }

        #endregion
    }

    #region View Modal

    public class CourtCasesDetailVM
    {
        public int DetailId { get; set; }
        public int CaseId { get; set; }

        [Display(Name = "COURT")]
        public string Courtid { get; set; } //Dropdown Court

        [Required(ErrorMessage = "Court Registration Number is Required")]
        [Display(Name = "COURT REGISTRATION NO")]
        public string CourtRefNo { get; set; }

        [Required(ErrorMessage = "Court Location is Required")]
        [Display(Name = "COURT")]
        public string CourtLocationid { get; set; } //Dropdown Location

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Court Registration Date is Required")]
        [RestrictFutureDate(ErrorMessage = "Future Date not allowed for Registration Date")]
        [Display(Name = "REGISTRATION DATE")]
        public DateTime? RegistrationDate { get; set; }

        [Display(Name = "Court Department")]
        public string CourtDepartment { get; set; } //Dropdown 

        [Display(Name = "CASE LEVEL")]
        public string CaseLevelCode { get; set; } //Dropdown 

        [Display(Name = "Court Status")]
        public string CourtStatus { get; set; }

        [Required(ErrorMessage = "Please Select Appeal by Who")]
        //[Required(ErrorMessage = "This is Required")]
        [Display(Name = "Apeal By Who")]
        public string ApealByWho { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RestrictFutureDate(ErrorMessage = "Future Date not allowed for Judgement Date")]
        [DateGreaterThan(otherPropertyName = "RegistrationDate", ErrorMessage = "Judgement Date must be greater than Registration Date")]
        [Display(Name = "Judgement Date")]
        public DateTime? JudgementDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RestrictFutureDate(ErrorMessage = "Future Date not allowed for Judgement Receiving Date")]
        [DateGreaterThan(otherPropertyName = "JudgementDate", ErrorMessage = "Judgement Receiving Date must be greater than Judgement Date")]
        [Display(Name = "Judgement Receiving Date")]
        public DateTime? JudgementReceivingDate { get; set; }

        [Display(Name = "Judgement Details")]
        public string JudgementDetails { get; set; }

        public string CourtName { get; set; }
        public string CourtLocationName { get; set; }
        public string CourtDepartmentName { get; set; }
        public string CaseLevelName { get; set; }
        [Display(Name = "Y & S Updates")]
        public string YandSNotesUpdate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RestrictFutureDate(ErrorMessage = "Future date not allowed for Final Closure Date")]
        [Display(Name = "Closure Date")]
        public DateTime? ClosureDate { get; set; }

        [Display(Name = "Closed By")]
        public string ClosedbyStaff { get; set; }

        //[Required(ErrorMessage = "This is Required")]
        [Display(Name = "Next Case Level")]
        public string NextCaseLevel { get; set; } //Dropdown 

        //[Required(ErrorMessage = "This is Required")]
        [Display(Name = "Next Case Level Code")]
        public string NextCaseLevelCode { get; set; }
        public string RealEstateYesNo { get; set; }
        public string RealEstateDetail { get; set; }
        public string ClaimSummary { get; set; }
        [Display(Name = "INVESTMENT تبسيط الإجراءات")]
        public string DepartmentType { get; set; } //INVESTMENT YES/NO
        
        #region SESSION ROLL VM

        public int SessionRollId { get; set; }

        #region SESSION ROLL VM - UPDATE

        //[Required(ErrorMessage = "This is Required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CurrentHearingDate { get; set; }
        //[Required(ErrorMessage = "This is Required")]
        public string CourtDecision { get; set; }
        public string SessionRemarks { get; set; }
        public string Requirements { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "FIRST EMAIL DATE تاريخ الايميل الأول")]
        public DateTime? FirstEmailDate { get; set; }
        [Display(Name = "CLIENT REPLY رد الموكل")]
        public string ClientReply { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NextHearingDate { get; set; }
        [Display(Name = "SOURCE المصدر")]
        public string TransportationSource { get; set; }
        public string TransportationFee { get; set; }
        #endregion

        #region SESSION ROLL VM - FOLLOW

        //[Required(ErrorMessage = "This is Required")]
        public string SessionClientId { get; set; }
        //[Required(ErrorMessage = "This is Required")]
        public string ClientName { get; set; }
        public string Defendant { get; set; }
        public string OfficeFileNo { get; set; }
        public string SessionRollClientName { get; set; }
        public string SessionRollDefendentName { get; set; }
        public bool ShowFollowup { get; set; }
        public bool ShowSuspend { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastDate { get; set; }
        public string WorkRequired { get; set; }
        public string SessionNotes { get; set; }
        public string FollowerId { get; set; }
        public string SuspendedWorkRequired { get; set; }
        public string SuspendedSessionNotes { get; set; }
        public string UpdatedOn { get; set; }

        #endregion


        #endregion
        public CourtCasesDetailVM()
        {
            this.NextCaseLevel = "";
            this.SessionClientId = "0";
            this.FollowerId = "0";
        }
    }

    #endregion
}