namespace YandS.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ApplicationError
    {
        [Key]
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

        public ApplicationError()
        {
            this.CreatedOn = DateTime.Now;
    
        }
    }
}