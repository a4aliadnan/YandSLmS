namespace YandS.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [Table("HR_Employee_s")]
    public class Employees : Base
    {
        [Key]
        public int EmployeeId { get; set; }
        //Reference for Regular Expression for Name Restruction
        //https://citriusjohn.wordpress.com/2013/09/13/asp-net-mvc-restrict-username-field/
        //For Remarked one stakeoverflow Question
        //https://stackoverflow.com/questions/42802167/mvc-regex-to-not-allow-spaces-and-special-characters

        [Required]
        [Display(Name = "Employee Number")]
        //[RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""\s]+$")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers Allowed without space")]
        [StringLength(10)]
        public string EmployeeNumber { get; set; } // ApplicationUser => UserName

        [Required]
        [Display(Name = "Employee Name")]
        [StringLength(200)]
        public string FullName { get; set; }

        [StringLength(200)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        [Column(TypeName = "datetime2")]
        public DateTime DOB { get; set; }

        //[Range(typeof(DateTime), "1/2/2004", "3/4/2004",ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [Required]
        [CheckDateRange]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Appointment")]
        [Column(TypeName = "datetime2")]
        public DateTime DOA { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Retirement")]
        [Column(TypeName = "datetime2")]
        public Nullable<DateTime> DOR { get; set; }

        public string Gender { get; set; }
        [Display(Name = "Employee Location")]
        [StringLength(5)]
        public string LocationCode { get; set; }
        [StringLength(3)]
        [Required]
        public string Nationality { get; set; }
        [StringLength(3)]
        public string Designation { get; set; }
        [StringLength(3)]
        public string Department { get; set; }

        [Display(Name = "Maximum Leave Allowed")]
        [Required]
        public int LeaveAllowed { get; set; }
        [Display(Name = "Parmanent Address")]
        public string PAddress { get; set; }
        [Display(Name = "Current Address")]
        public string CAddress { get; set; }
        [Display(Name = "Contact Numbers")]
        [StringLength(20)]
        public string ContactNumber { get; set; }
        [Display(Name = "Basic Salary")]
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        [Required]
        public decimal BasicSalary { get; set; }
        [Display(Name = "Total Allowance")]
        [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
        [Required]
        public decimal Allowance { get; set; }

        [StringLength(3)]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }
        [StringLength(100)]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }
        [StringLength(3)]
        [Display(Name = "Message Language")]
        public string MessageLang { get; set; }

        public ICollection<EmpDoc> DocId { get; set; }

        [NotMapped]
        public string GenderName { get; set; }
        [NotMapped]
        public string LocationName { get; set; }
        [NotMapped]
        public string NationalityName { get; set; }
        [NotMapped]
        public string DesignationName { get; set; }
        [NotMapped]
        public string DepartmentName { get; set; }
        [NotMapped]
        public string CreatedByName { get; set; }
        [NotMapped]
        public string UpdatedByName { get; set; }
        [NotMapped]
        public string EmployeeBankName { get; set; }

        [NotMapped]
        public ICollection<MasterSetups> ListNationality { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListDesignation { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListDepartment { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListGender { get; set; }
        [NotMapped]
        public ICollection<MasterSetups> ListLocationCode { get; set; }

    }
}