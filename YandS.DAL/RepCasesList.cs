namespace YandS.DAL
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RepCasesList
    {
        public int CaseId { get; set; }

        [Display(Name = "Office File No")]
        public string OfficeFileNo { get; set; }

        [Display(Name = "Client Code")] 
        public string ClientCode { get; set; }

        [Display(Name = "Client Name")]
        public string ClientName { get; set; }

        [Display(Name = "Defendant Name")]
        public string Defendant { get; set; }

        [Display(Name = "Against")]
        public string AgainstCode { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Received Date")]
        public DateTime? ReceptionDate { get; set; }

        [Display(Name = "Receive Level")]
        public string ReceiveLevelCode { get; set; } //Dropdown 

        [Display(Name = "A/C Contract No")]
        public string AccountContractNo { get; set; }

        [Display(Name = "Client File No")]
        public string ClientFileNo { get; set; }

        [Display(Name = "Claim Amount")]
        public decimal? ClaimAmount { get; set; }

        [Display(Name = "Amount")]
        public decimal? PrivateFee { get; set; }

        [Display(Name = "Case Type")]
        public string CaseTypeCode { get; set; }

        [Display(Name = "Case Level")]
        public string CaseLevelCode { get; set; } 
        [Display(Name = "ID / Registration No.")]
        public string IdRegistrationNo { get; set; }  
        [Display(Name = "Parent Court")]
        public string ParentCourtId { get; set; } 

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Legal Notice Date")]
        public DateTime? LegalNoticeDate { get; set; }

        [Display(Name = "ODB LOAN")]
        public string ODBLoanCode { get; set; } 

        [Display(Name = "ODB Bank Branch")]
        public string ODBBankBranch { get; set; }

        [Display(Name = "POLICE NO")]
        public string PoliceNo { get; set; }

        [Display(Name = "POLICE STATION")]
        public string PoliceStation { get; set; }

        [Display(Name = "PUBLIC PROSECUTION NO")]
        public string PublicProsecutionNo { get; set; }

        [Display(Name = "PUBLIC PROSECUTION PLACE")]
        public string PublicPlace { get; set; }

        [Display(Name = "PAPC NO")]
        public string PAPCNo { get; set; }

        [Display(Name = "PAPC PLACE")]
        public string PAPCPlace { get; set; }

        [Display(Name = "LABOR CONFLIC NO")]
        public string LaborConflicNo { get; set; }

        [Display(Name = "LABOR CONFLIC PLACE")]
        public string LaborConflicPlace { get; set; }

        [Display(Name = "Y&S Note")]
        public string YandSNotes { get; set; }

        [Display(Name = "Case Status")]
        public string CaseStatus { get; set; }

        [Display(Name = "Special Instructions")]
        public string SpecialInstructions { get; set; }

        [Display(Name = "Client Case Type")]
        public string ClientCaseType { get; set; }
        public string DefClientName { get; set; }
        public string AgainstName { get; set; }

        public string ReceiveLevelName { get; set; }

        public string CaseTypeName { get; set; }

        public string CaseLevelName { get; set; }

        public string ODBLoanName { get; set; }

        public string ODBBankBranchName { get; set; }

        public string CaseStatusName { get; set; }

        public string PoliceStationName { get; set; }

        public string PublicPlaceName { get; set; }

        public string PAPCPlaceName { get; set; }

        public string LaborConflicPlaceName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Status Date")]
        public DateTime CaseStatusDate { get; set; }

        public string ClientCaseTypeName { get; set; }

        public string ParentCourtName { get; set; }
    }
}