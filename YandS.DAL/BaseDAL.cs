using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandS.DAL
{
    public class BaseDAL
    {
        public string ConnectionString
        {
            get
            {
                string strConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
                return strConnectionString;
            }
        }
    }
}
