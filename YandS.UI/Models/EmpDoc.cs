namespace YandS.UI.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EmpDoc
    {
        [Key]
        public int DocId { get; set; }
        [Display(Name = "Document Type")]
        [Required]
        public string DocTypeId { get; set; }
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }
        public int EmployeId { get; set; }
        public Employees Employees { get; set; }
        [NotMapped]
        public string DocTypeName { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListDocType { get; set; }

    }
}