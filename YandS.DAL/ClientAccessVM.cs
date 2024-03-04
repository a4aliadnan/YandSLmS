using System;
using System.ComponentModel.DataAnnotations;

namespace YandS.DAL
{
    public class ClientAccessVM
    {
        public int ClientId { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string PassWord { get; set; }
        public bool Inactive { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModified { get; set; }
        public int TotalRecords { get; set; }
        public ClientAccessVM()
        {
            LastModified = DateTime.Now;
            Inactive = false;
        }
    }
}
