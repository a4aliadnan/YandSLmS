using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YandS.UI.Models.ViewModels
{
    public class Error
    {
        public Error(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }
        public string Message { get; set; }
    }
}