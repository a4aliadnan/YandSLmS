using System;

namespace YandS.UI.Models
{
    public class UserVM
    {
        public int EmployeeId { get; set; }

        public string EmployeeNumber { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime DOB { get; set; }
        public DateTime DOA { get; set; }

        public DateTime? DOR { get; set; }

        public string LocationCode { get; set; }
        public string Nationality { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public int LeaveAllowed { get; set; }
        public string PAddress { get; set; }
        public string CAddress { get; set; }
        public string ContactNumber { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowance { get; set; }
        public string GenderName { get; set; }
        public string LocationName { get; set; }
        public string NationalityName { get; set; }
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string CreatedByName { get; set; }
        public string UpdatedByName { get; set; }
        public int UserType { get; set; }
        public string ClientName { get; set; }
        public string ClientCode { get; set; }
        public UserVM()
        {
            UserType = 0;
        }
    }
}