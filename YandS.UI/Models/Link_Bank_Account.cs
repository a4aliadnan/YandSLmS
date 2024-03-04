namespace YandS.UI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Link_Bank_Account
    {
        [Key]
        public int LinkId { get; set; }
        public int BankId { get; set; }
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
    }
}