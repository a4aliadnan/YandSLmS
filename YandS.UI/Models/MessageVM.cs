namespace YandS.UI.Models
{
    public class MessageVM
    {
        public int id { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }
        public string IsShowForm { get; set; }
        public string CaseId { get; set; }

        public MessageVM()
        {
            id = 0;
        }
    }
}