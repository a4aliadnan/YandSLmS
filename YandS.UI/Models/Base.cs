using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace YandS.UI.Models
{
    public class Base
    {
        public virtual ApplicationUser Created { get; set; }

        [Display(Name = "Created By")]
        public int CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created Date Time")]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        public virtual ApplicationUser Modified { get; set; }

        [Display(Name = "Modified By")]
        public int? UpdatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Modified Date Time")]
        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedOn { get; set; }
    }
}