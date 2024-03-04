using System;
using System.ComponentModel.DataAnnotations;

namespace YandS.UI.Models
{
    public class CaseRegistrationVM : BeforeCourtVM
    {
        public int CaseRegistrationId { get; set; }

        [Display(Name = "ACTION DATE")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ActionDate { get; set; }

        [Display(Name = "ACTION LEVEL")]
        public string ActionLevel { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? JudgementDate { get; set; }

        [Display(Name = "URGENT CASES")]
        public int? UrgentCaseDays { get; set; }
        [Display(Name = "ENFORCEMENT DISPUTE")]
        public string EnforcementDispute { get; set; }
        [Display(Name = "COURT REGISTRATION")]
        public string CourtRegistration { get; set; }
        [Display(Name = "FILE STATUS")]
        public string FileStatus { get; set; }
        public string FileStatusRemarks { get; set; }
        public string ReceptionDate { get; set; }

        public string CreatedByName { get; set; }
        public string UpdatedByName { get; set; }
        [Display(Name = "COURT REQUEST")]
        public string CourtMessage { get; set; } //COURT REQUEST

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SendDate { get; set; }
        public string OmanPostNo { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FirstReminderDate { get; set; }
        public string ReminderNo { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CourtRequestDate { get; set; }
        public string OfficeProcedure { get; set; } //NOTES
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PaymentDate { get; set; }
        public string AssignedTo { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AssignedDate { get; set; }
        [Display(Name = "REGISTERED")]
        public bool CourtDetailRegistered { get; set; }
        public string AdminFile { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NextHearingDate { get; set; }
        public string NextHearingNotes { get; set; }
        public string CourtReg_RegNo { get; set; }
        public string CourtReg_RegCourt { get; set; }
        [Display(Name = "REGISTRATION DATE")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CourtReg_RegDate { get; set; }
        public string CourtReg_Regby { get; set; }

        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "CLAIM AMOUNT")]
        public decimal? CourtReg_ClaimAmount { get; set; }
        [Display(Name = "INVESTMENT تبسيط الإجراءات")]
        public string DepartmentType { get; set; }
        [Display(Name = "REMARK")]
        public string MainRemarks { get; set; }

        #region FORM PRINTING
        public string FormPrintJugDate { get; set; }
        public string FormPrintDefendant { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FormPrintLastDate { get; set; }
        public string FormPrintWorkRequired { get; set; }
        public string OnHoldReasonDDL { get; set; }
        #endregion

        #region PAYMENT VOUCHER VARIABLES

        public int? Voucher_No { get; set; }
        [Display(Name = "PAYMENT TYPE")]
        public string Payment_Type { get; set; }
        [Display(Name = "PAYMENT MODE")]
        public string Payment_Mode { get; set; } //3 BANK TRANSFER
        [Display(Name = "VOUCHER TYPE")]
        public string VoucherType { get; set; } //1 REFUNDABLE
        [Display(Name = "Office File No")]
        public string OfficeFileNoSessionRollPV { get; set; }
        public string CourtType { get; set; } // CASE LEVEL
        public string LocationCode { get; set; }
        public string Status { get; set; }
        [Display(Name = "PAY TYPE")]
        public string Payment_Head { get; set; } // PAY FOR 
        public string Remarks { get; set; } // REMARKS
        public string PV_No { get; set; }// Format : Muscat-0001-2020 (Genrate after approval)
        public string VoucherStatus { get; set; }
        public int Credit_Account { get; set; }
        public string TransTypeCode { get; set; }
        public string TransReasonCode { get; set; }
        public string CaseInvoices { get; set; }
        public string SpecialNotification { get; set; }
        public string PDCRefNo { get; set; }
        public DateTime? FutureChequeDate { get; set; }
        [Display(Name = "REFERENCE NO. رقم الدعوى")]
        public string BillNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        [Display(Name = "Amount المبلغ")]
        public decimal Amount { get; set; }
        [Display(Name = "PAY TO يحول المبلغ إلى")]
        public string Payment_To { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        [Display(Name = "Vat الضريبة")]
        public decimal? VatAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total amounts مبلغ الفاتورة")]
        public decimal? TotalAmount { get; set; }
        [Display(Name = "PAY FROM")]
        public int Debit_Account { get; set; }
        [Display(Name = "CHEQUE NUMBER / TRANSFER")]
        public string Cheque_Number { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CHEQUE / TRANSFER DATE")]
        public DateTime? Cheque_Date { get; set; }
        public string PaymentHeadDetail { get; set; }
        public string PaymentToBenificry { get; set; }
        public string PayToMstValue { get; set; }
        public string PayToMstDesc { get; set; }
        public string PayToBankName { get; set; }
        public string PayToAccountNumber { get; set; }
        public string PayToEmail { get; set; }
        public string PayToContactNo { get; set; }
        [Display(Name = "BANK البنك")]
        public string PayToBankNameDisp { get; set; }
        [Display(Name = "ACC الحساب")]
        public string PayToAccountNumberDisp { get; set; }
        public string PayToMessageLang { get; set; }
        #endregion

        #region CASE MANAGEMENT ONLINE SUBMISSION PRIMARY 


        [Display(Name = "CASE TYPE")]
        public string CaseTypeCode { get; set; } //Dropdown 
        [Display(Name = "AGAINST")]
        public string AgainstCode { get; set; } //Dropdown 
        [Display(Name = "CLIENT CASE TYPE")]
        public string ClientCaseType { get; set; }
        [Display(Name = "CASE SUBJECT")]
        public string CaseSubject { get; set; }  //Dropdown 
        [Display(Name = "ID/CIVIL  NO. ")]
        public string IdRegistrationNo { get; set; }
        [Display(Name = "CR / REGISTRATION NO.")]
        public string CRRegistrationNo { get; set; }
        [Display(Name = "NATIONALITY")]
        public string OmaniExp { get; set; }

        #endregion

        #region ENFORCEMENT VARIABLES
        [Display(Name = "PRIMARY OBJECTION NO.")]
        public string PrimaryObjectionNo { get; set; }

        [Display(Name = "PRIMARY OBJECTION COURT")]
        public string PrimaryObjectionCourt { get; set; }
        [Display(Name = "APEAL OBJECTION NO.")]
        public string ApealObjectionNo { get; set; }

        [Display(Name = "APEAL OBJECTION COURT")]
        public string ApealObjectionCourt { get; set; }


        [Display(Name = "PRIMARY PLAINT NO.")]
        public string PrimaryPlaintNo { get; set; }

        [Display(Name = "PRIMARY PLAINT COURT")]
        public string PrimaryPlaintCourt { get; set; }
        [Display(Name = "APEAL PLAINT NO.")]
        public string ApealPlaintNo { get; set; }

        [Display(Name = "APEAL PLAINT COURT")]
        public string ApealPlaintCourt { get; set; }
        [Display(Name = "SUPREME OBJECTION NO.")]
        public string SupremeObjectionNo { get; set; }

        [Display(Name = "SUPREME OBJECTION COURT")]
        public string SupremeObjectionCourt { get; set; }

        [Display(Name = "SUPREME PLAINT NO.")]
        public string SupremePlaintNo { get; set; }

        [Display(Name = "SUPREME PLAINT COURT")]
        public string SupremePlaintCourt { get; set; }
        #endregion

        #region PRIMARY / APPEAL / SUPREME VARIABLES FOR REGISTER
        public int DetailId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "REGISTRATION DATE")]
        public DateTime? RegistrationDate { get; set; }
        public string CourtRefNo { get; set; }
        [Display(Name = "COURT")]
        public string CourtLocationid { get; set; } //Dropdown Location
        public string CourtLocationName { get; set; } //Dropdown Location
        public string ENFCourtLocationName { get; set; } //Dropdown Location
        public string ENFCourtRefNo { get; set; }

        [Display(Name = "Apeal By Who")]
        public string ApealByWho { get; set; }
        [Display(Name = "Court Department")]
        public string CourtDepartment { get; set; } //Dropdown 

        #endregion

        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        public decimal? CourtFeeAmount { get; set; }
        [Display(Name = "REAL ESTATE العقارات")]
        public string RealEstateYesNo { get; set; }
        [Display(Name = "REAL ESTATE DETAIL")]
        public string RealEstateDetail { get; set; }
        public string ClaimSummary { get; set; }
        [Display(Name = "ELECTRONIC NO.")]
        public string ElectronicNo { get; set; }
        [Display(Name = "SOURCE مصدر التحديث")]
        public string TransportationSource { get; set; }
        public string TransportationFee { get; set; }
        [Display(Name = "CLIENT REPLY رد الموكل")]
        public string ClientReply { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "FIRST EMAIL DATE تاريخ الايميل الأول")]
        public DateTime? FirstEmailDate { get; set; }
        [Display(Name = "DONE استكمال")]
        public string OnHoldDone { get; set; }
        [Display(Name = "ADVISOR المستشار")]
        public string ConsultantId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "EMAIL DATE تاريخ وصول الايميل")]
        public DateTime? StopRegEmailDate { get; set; }
        [Display(Name = "CODE")]
        public string StopRegUserName { get; set; }
        [Display(Name = "STOP ENFC REQUEST طلب وقف التنفيذ")]
        public string StopEnfRequest { get; set; }
        public string AccountContractNo { get; set; }
        public string ClientFileNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:###0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "CLAIM AMOUNT")]
        public decimal? ClaimAmount { get; set; }
        public string GovernorateId { get; set; }
        [Display(Name = "COURT FOLLOW مراجعة المحكمة")]
        public string CourtFollow { get; set; }
        public string CourtFollow_LawyerId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = " COMMISSIONING DATE تاريخ التكليف")]
        public DateTime? CommissioningDate { get; set; }
        public string LawyerId { get; set; }

        public string PartialViewName { get; set; }
        public string redirectTo { get; set; }
        public string DisputeLevel { get; set; }
        public string DisputeLevelName { get; set; }
        public string DisputeType { get; set; }
        public string DisputeTypeName { get; set; }
        public string CourtFollowRequirement { get; set; }
        public DateTime? DisputrRegisterDate { get; set; }
        public bool IsDeleted { get; set; }
        public string IsShowWithLawyer { get; set; }

        #region SESSION DETAIL
        public string UpdatePV_Type { get; set; }
        public string CurrentCaseLevel { get; set; }
        public string CountLocationName { get; set; }
        public string Update_Follow { get; set; }
        public string Update_Suspend { get; set; }
        public string Update_CourtFollow { get; set; }
        public string Update_CourtTransfer { get; set; }
        public string Update_Addreass { get; set; }
        public string Update_PV { get; set; }
        [Display(Name = "ANNOUNCEMENT TYPE طريقة الإعلان")]
        public string AnnouncementTypeId { get; set; }

        public string Session_CaseType { get; set; }
        public string Session_LawyerId { get; set; }
        public string Session_FileStatusName { get; set; }
        public string UpdatedOn { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? JudgementsDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? JDReceiveDate { get; set; }
        public string IsFavorable { get; set; }
        public string JDCaseLevelCode { get; set; }
        public string CurrentTableName { get; set; }
        public int JDSessionId { get; set; }
        public string Judgement { get; set; }
        #endregion

        public string UpdatedBy { get; set; }
        public string IsForDisputeShow { get; set; }
        public string WhatsAppMessage { get; set; }
        public string retMessageWHM_Email { get; set; }


        public CaseRegistrationVM()
        {
            CaseRegistrationId = 0;
            Amount = 0;
            JDSessionId = 0;
        }
    }
}