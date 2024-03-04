namespace YandS.DAL
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RepCaseParameterForm
    {
        public RepCaseParameterForm()
        {
            this.ReportFormat = "EXCEL"; //"PDF","WORD" (implement Radio button)
            this.ClaimAmount = 0;
            this.ClaimAmountFrom = 0;
            this.PartialViewName = "_litigation";
            this.Location = "A";
            this.ClientCode = "0";
            this.AgainstCode = "0";
            this.InvoiceStatus = "0";
            this.CaseStatus = "0";
            this.ParentCourtId = "0";
            this.GovernorateId = "0";
            this.AgainstInsurance = "0";
            this.ODBBankBranch = "0";
            this.ClientCaseType = "0";
            this.OmaniExp = "0";
            this.EnforcementlevelId = "0";
            this.ReOpenEnforcement = "0";
            this.CourtLocationid = "0";
            this.ReasonCode = "0";
        }

        [Display(Name = "Y & S BRANCH")]
        public string Location { get; set; }

        [Display(Name = "CLIENT NAME")]
        public string ClientCode { get; set; }

        [Display(Name = "AGAINST")]
        public string AgainstCode { get; set; }

        [Display(Name = "INVOICE STATUS")]
        public string InvoiceStatus { get; set; }

        [Display(Name = "CLAIM AMOUNT")]
        public double? ClaimAmount { get; set; }

        [Display(Name = "CLAIM AMOUNT FROM")]
        public decimal? ClaimAmountFrom { get; set; }

        [Display(Name = "CLAIM AMOUNT TO")]
        public decimal? ClaimAmountTo { get; set; }

        [Display(Name = "CASE TYPE")]
        public string CaseTypeCode { get; set; }

        [Display(Name = "CASE LEVEL")]
        public string CaseLevelCode { get; set; }

        [Display(Name = "CASE STATUS")]
        public string CaseStatus { get; set; }

        [Display(Name = "PARENT COURT")]
        public string ParentCourtId { get; set; }
        [Display(Name = "REPORT FORMAT")]
        public string ReportFormat { get; set; }

        [Display(Name = "RECEPTION DATE FROM")]
        public DateTime? ReceptionDateFrom { get; set; }

        [Display(Name = "RECEPTION DATE TO")]
        public DateTime? ReceptionDateTo { get; set; }

        [Display(Name = "REGISTRATION DATE FROM")]
        public DateTime? RegistrationDateFrom { get; set; }

        [Display(Name = "REGISTRATION DATE TO")]
        public DateTime? RegistrationDateTo { get; set; }

        [Display(Name = "JUDGMENT DATE FROM")]
        public DateTime? JudgmentDateFrom { get; set; }

        [Display(Name = "JUDGMENT DATE TO")]
        public DateTime? JudgmentDateTo { get; set; }

        public string VoucherType { get; set; }
        [Display(Name = "PAY TO")]
        public string Payment_To { get; set; }
        public string PartialViewName { get; set; }

        public string ApealByWho { get; set; }
        public string GovernorateId { get; set; }
        public string AgainstInsurance { get; set; }
        [Display(Name = "ODB BANK BRANCH")]
        public string ODBBankBranch { get; set; } //Dropdown 
        [Display(Name = "ODB LOAN TYPE")]
        public string ClientCaseType { get; set; }
        [Display(Name = "LOAN MANAGER")]
        public string LoanManager { get; set; }  //Dropdown 

        [Display(Name = "NATIONALITY")]
        public string OmaniExp { get; set; }
        [Display(Name = "CURRENT ENFORCEMENT LEVEL مرحلة التنفيذ الحالية")]
        public string EnforcementlevelId { get; set; } //Dropdown EnforcementLevel 265
        public string ReOpenEnforcement { get; set; }
        public string ClickedButtonName { get; set; }
        [Display(Name = "COURT")]
        public string CourtLocationid { get; set; }
        [Display(Name = "DESCRIPTION")]
        public string FeeTypeId { get; set; }
        [Display(Name = "REASON")]
        public string ReasonCode { get; set; } //Dropdown Case Status

    }
}