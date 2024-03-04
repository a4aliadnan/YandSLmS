namespace YandS.UI.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("PERMISSIONS")]
    public class PERMISSION
    {
        [Key]
        public int PermissionId { get; set; }

        [Required]
        [StringLength(50)]
        public string PermissionDescription { get; set; }

        public virtual List<ApplicationRole> ROLES { get; set; }
    }
}