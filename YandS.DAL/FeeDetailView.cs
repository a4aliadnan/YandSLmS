namespace YandS.DAL
{
    public class FeeDetailView
    {
        public string InvClassification { get; set; }
        public string CaseLevel { get; set; }
        public string Descriptions { get; set; }
        public string Details { get; set; }
        public string Numbers { get; set; }
        public decimal Amounts { get; set; }
        public decimal? VATPct { get; set; }
        public decimal? VATAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}