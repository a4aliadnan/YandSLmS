namespace YandS.UI.Models
{
    public enum MASTER_S : ushort
    {
        CourtType = 1,
        Banks = 2,
        AccoutTitle = 3,
        Location = 4,
        AccountNumber = 5,
        Gender = 6,
        PaymentHeads = 7,
        PaymentMode = 8,
        PaymentType = 9,
        ChequeStatus = 10,
        CourtLocation = 11,
        CaseAgainst = 12,
        ReceiveLevel = 13,
        CaseType = 14,
        CaseLevel = 15,
        ODBLoan = 16,
        CourtDepartment = 17,
        ODBBankBranch = 18,
        Nationality = 196,
        Department = 197,
        Designation = 206,
        PayTo = 214,
        DocumentType = 218,
        VoucherType = 224,
        VoucherStatus = 228,
        FeesType = 232,
        Branch = 236,
        Client = 241,
        InvoiceStatus = 247,
        CaseStatus = 252,
        EnforcementLevel = 265,
        CauseOfSuspension = 272,
        ArrestOrderStatus = 280,
        ClientCaseType = 285,
        FeeClassification = 289,
        ParentCourt = 298,
        ApealByWho = 391,
        FileCloseReason = 395,
        PVTransType = 401,
        PVTransReason = 402,
        LoanManager = 428,
        OmaniExp = 460,
        CurrentCaseLevel = 499,
        ClosureLevel = 500,
        FileAllocation = 501,
        ClientClassification = 523,
        CaseSubject = 532,
        FeeTypeCascadeDetail = 567,
        BeforeCourtStage = 568,
        EnforcementStage = 569,
        CounsultingFeeType = 570,
        ActionLevel = 785,
        EnforcementDispute = 786,
        FileStatus = 788,
        OnHoldReason = 821,
        DepartmentType = 822,
        SessionLevel = 858,
        SessionCaseType = 859,
        SessionLawyers = 860,
        SessionFollower = 861,
        SessionClients = 913,
        SessionFileStatus = 1091,
        SessionOnHold = 1092,
        ReconciliationDeptt = 1152,
        Governorate = 1153,
        EnfcAdmin = 1154,
        AnnouncementType = 1288,
        InquiryResult = 1289,
        AuctionProcess = 1290,
        JudicialDecision = 1291,
        CauseOfRecovery = 1292,
        TransportationSource = 1385,
        CallerName = 1408,
        DisputeLevel = 1450,
        DisputeType = 1451,
        JudgementLevel = 1465,
        OfficeFileStatus = 1573,
        DisputeLevelandTypes = 1632,
        FileTypeClosure = 1690,
        MoneyWith = 1727,
        PAYTONEW = 1898,
        ArrestLevel = 2021,
        DEFContactType = 2033
    }
    public enum Courts : ushort
    {
        PrimaryCourt = 1,
        ApealCourt = 2,
        SupremeCourt = 3,
        EnforcementCourt = 4,
        
    }

    public class OfficeFileStatus
    {
        private OfficeFileStatus(string value) { Value = value; }

        public string Value { get; private set; }

        public static OfficeFileStatus PleaseSelect { get { return new OfficeFileStatus("0"); } }
        public static OfficeFileStatus Transfer { get { return new OfficeFileStatus("OFS-1"); } }
        public static OfficeFileStatus LegalNotice { get { return new OfficeFileStatus("OFS-2"); } }
        public static OfficeFileStatus LegalNoticeSub { get { return new OfficeFileStatus("OFS-67"); } }
        public static OfficeFileStatus WritingSubmission { get { return new OfficeFileStatus("OFS-3"); } }
        public static OfficeFileStatus SubmissionApproval { get { return new OfficeFileStatus("OFS-4"); } }
        public static OfficeFileStatus ApprovalForAppeal { get { return new OfficeFileStatus("OFS-10"); } }
        public static OfficeFileStatus ApprovalForSupreme { get { return new OfficeFileStatus("OFS-11"); } }
        public static OfficeFileStatus CompletingDocs { get { return new OfficeFileStatus("OFS-13"); } }
        public static OfficeFileStatus ReceiptOfFees { get { return new OfficeFileStatus("OFS-14"); } }
        public static OfficeFileStatus Translation { get { return new OfficeFileStatus("OFS-15"); } }
        public static OfficeFileStatus Scanned { get { return new OfficeFileStatus("OFS-5"); } }
        public static OfficeFileStatus OnlineRegTBR { get { return new OfficeFileStatus("OFS-6"); } }
        public static OfficeFileStatus CourtMsg { get { return new OfficeFileStatus("OFS-7"); } }
        public static OfficeFileStatus ForPayment { get { return new OfficeFileStatus("OFS-8"); } }
        public static OfficeFileStatus WithLawyer { get { return new OfficeFileStatus("OFS-12"); } }
        public static OfficeFileStatus Registered { get { return new OfficeFileStatus("OFS-9"); } }
        public static OfficeFileStatus RunningCase { get { return new OfficeFileStatus("OFS-16"); } }
        public static OfficeFileStatus JudgIssued { get { return new OfficeFileStatus("OFS-17"); } }
        public static OfficeFileStatus ToKnowSessionDate { get { return new OfficeFileStatus("OFS-18"); } }
        public static OfficeFileStatus RequestMeetJudge { get { return new OfficeFileStatus("OFS-19"); } }
        public static OfficeFileStatus Dispute { get { return new OfficeFileStatus("OFS-20"); } }
        public static OfficeFileStatus AuctionSession { get { return new OfficeFileStatus("OFS-21"); } }
        public static OfficeFileStatus OnlineRegENF { get { return new OfficeFileStatus("OFS-22"); } }
        public static OfficeFileStatus Announcement { get { return new OfficeFileStatus("OFS-23"); } }
        public static OfficeFileStatus ContactingAuthorities { get { return new OfficeFileStatus("OFS-24"); } }
        public static OfficeFileStatus JudicalSale { get { return new OfficeFileStatus("OFS-25"); } }
        public static OfficeFileStatus ArrestApplication { get { return new OfficeFileStatus("OFS-26"); } }
        public static OfficeFileStatus ArrestOrder { get { return new OfficeFileStatus("OFS-27"); } }
        public static OfficeFileStatus Suspendfd { get { return new OfficeFileStatus("OFS-28"); } }
        public static OfficeFileStatus RecoveryRedStamp_Close { get { return new OfficeFileStatus("OFS-29"); } }
        public static OfficeFileStatus RecoveryRedStamp_Re_Open { get { return new OfficeFileStatus("OFS-30"); } }
        public static OfficeFileStatus MeetJudge { get { return new OfficeFileStatus("OFS-31"); } }
        public static OfficeFileStatus AssigningJudge { get { return new OfficeFileStatus("OFS-32"); } }
        public static OfficeFileStatus JudgStamp { get { return new OfficeFileStatus("OFS-33"); } }
        public static OfficeFileStatus CorrectingJudg { get { return new OfficeFileStatus("OFS-34"); } }
        public static OfficeFileStatus PeriodOfAppeal { get { return new OfficeFileStatus("OFS-35"); } }
        public static OfficeFileStatus RedStamp { get { return new OfficeFileStatus("OFS-36"); } }
        public static OfficeFileStatus EnfcApproval { get { return new OfficeFileStatus("OFS-37"); } }
        public static OfficeFileStatus Refundable3_4 { get { return new OfficeFileStatus("OFS-38"); } }
        public static OfficeFileStatus FileReview { get { return new OfficeFileStatus("OFS-39"); } }
        public static OfficeFileStatus OPP { get { return new OfficeFileStatus("OFS-40"); } }
        public static OfficeFileStatus ROP { get { return new OfficeFileStatus("OFS-41"); } }
        public static OfficeFileStatus MOL { get { return new OfficeFileStatus("OFS-42"); } }
        public static OfficeFileStatus PACP { get { return new OfficeFileStatus("OFS-43"); } }
        public static OfficeFileStatus MOCI { get { return new OfficeFileStatus("OFS-44"); } }
        public static OfficeFileStatus EstablishingCompanies { get { return new OfficeFileStatus("OFS-45"); } }
        public static OfficeFileStatus Arbition { get { return new OfficeFileStatus("OFS-46"); } }
        public static OfficeFileStatus AttendAssociation { get { return new OfficeFileStatus("OFS-47"); } }
        public static OfficeFileStatus WrittingContracts { get { return new OfficeFileStatus("OFS-48"); } }
        public static OfficeFileStatus ReviewingContracts { get { return new OfficeFileStatus("OFS-49"); } }
        public static OfficeFileStatus LegalServices { get { return new OfficeFileStatus("OFS-50"); } }
        public static OfficeFileStatus Closing_Settelment { get { return new OfficeFileStatus("OFS-51"); } }
        public static OfficeFileStatus Closing_Payments { get { return new OfficeFileStatus("OFS-52"); } }
        public static OfficeFileStatus Closing_AgainstClient { get { return new OfficeFileStatus("OFS-53"); } }
        public static OfficeFileStatus Closing_Consultants { get { return new OfficeFileStatus("OFS-54"); } }
        public static OfficeFileStatus Closing_Others { get { return new OfficeFileStatus("OFS-55"); } }
        public static OfficeFileStatus BlueStamp { get { return new OfficeFileStatus("OFS-56"); } }
        public static OfficeFileStatus DifferentPanel { get { return new OfficeFileStatus("OFS-57"); } }
        public static OfficeFileStatus Closing_ReSchedule { get { return new OfficeFileStatus("OFS-58"); } }
        public static OfficeFileStatus Disability { get { return new OfficeFileStatus("OFS-59"); } }
        public static OfficeFileStatus BeforeRegister { get { return new OfficeFileStatus("OFS-60"); } }
        public static OfficeFileStatus Liquidation { get { return new OfficeFileStatus("OFS-61"); } }
        public static OfficeFileStatus TransferFile { get { return new OfficeFileStatus("OFS-62"); } }
        public static OfficeFileStatus MissingDocuments { get { return new OfficeFileStatus("OFS-63"); } }
        public static OfficeFileStatus DefendentDeath { get { return new OfficeFileStatus("OFS-64"); } }
        public static OfficeFileStatus InitiateClosingFile { get { return new OfficeFileStatus("OFS-65"); } }
        public static OfficeFileStatus CriminalFile { get { return new OfficeFileStatus("OFS-66"); } }
        public static OfficeFileStatus ArrestAnnouncement { get { return new OfficeFileStatus("OFS-68"); } }

        public override string ToString()
        {
            return Value;
        }
    }
}