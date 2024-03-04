namespace YandS.DAL
{

    public class ClosureFormVM
    {
        public string DateOfClosure { get; set; }
        public string ClosedBy { get; set; }
        public string CourtLevel { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string TypeOfFees { get; set; }
        public string InvoiceStatus { get; set; }
        public string NextCaseLevel { get; set; }
        public string NextCaseLevelCode { get; set; }
        public string OfficeFileNo { get; set; }
        public string Defendant { get; set; }
        public string ClientName { get; set; }
    }
    public class ClosureFormView
    {
        public string OfficeFileNo { get; set; }
        public string Defendant { get; set; }
        public string ClosurePartDate { get; set; }
        public string FileTypeClosure { get; set; }
        public string ClosingNotes { get; set; }
        public string ClosureLevel { get; set; }
        public string ClosureDate { get; set; }
        public string ClosureReason { get; set; }
        public string DataFor { get; set; }
    }
}