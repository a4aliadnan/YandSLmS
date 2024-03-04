using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YandS.UI.Models
{
    public class ErrorVM
    {
        public int ErrorId { get; set; }
        public string ErrorMessage { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string StackTrace { get; set; }
        public string EmployeeNumber { get; set; }
        public string Url { get; set; }
        public string IPAddress { get; set; }
        public string PCName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}