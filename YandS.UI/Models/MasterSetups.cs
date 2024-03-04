namespace YandS.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;
    using System.Web.Mvc;

    [Table("MASTER_S")]
    public class MasterSetups : Base
    {
        [Key]
        public int MstId { get; set; }
        public int? MstParentId { get; set; }
        [Display(Name = "Description")]
        [Required]
        [StringLength(255)]
        public string Mst_Desc { get; set; }
        [Display(Name = "Value")]
        [Required]
        [StringLength(10)]
        public string Mst_Value { get; set; }
        public int? DisplaySeq { get; set; }

        [Display(Name = "Active Status")]
        [SqlDefaultValue(DefaultValue = "true")]
        public bool Active_Flag { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }

        public MasterSetups() {
            this.Active_Flag = true;
        }

    }
}