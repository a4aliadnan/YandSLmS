namespace YandS.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CLIENT_ACCESS")]
    public class ClientAccess
    {
        [Key]
        public int ClientId { get; set; }
        [StringLength(5)]
        public string ClientCode { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        [StringLength(15)]
        public string PassWord { get; set; }
        public bool Inactive { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime LastModified { get; set; }

        public ClientAccess()
        {
            LastModified = DateTime.UtcNow.AddHours(4);
            Inactive = false;
        }
    }

    public class ClientAccessVM
    {
        public int ClientId { get; set; }
        public string ClientCode { get; set; }
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }
        [Display(Name = "Login ID")]
        public string UserName { get; set; }
        [Display(Name = "Name")]
        public string DisplayName { get; set; }
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string ConfirmPassword { get; set; }
        public bool Inactive { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModified { get; set; }
        public int UserType { get; set; }
        public string Code { get; set; }
        public ClientAccessVM()
        {
            ClientId = 0;
            LastModified = DateTime.UtcNow.AddHours(4);
            Inactive = false;
            UserType = 1;
        }
    }
}