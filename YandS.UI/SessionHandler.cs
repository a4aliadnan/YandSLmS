using System.Web;
using YandS.UI.Models;

namespace YandS.UI
{
    public static class SessionHandler
    {
        public static UserVM User
        {
            get
            {
                return ((UserVM)HttpContext.Current.Session["User"]);
            }
        }

        public static ErrorVM errorVM
        {
            get
            {
                return ((ErrorVM)HttpContext.Current.Session["Err"]);
            }
        }
    }
}