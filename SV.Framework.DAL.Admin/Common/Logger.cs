using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.DAL.Admin
{
    public class Logger
    {
        public static bool LogMessage(Exception ex, object o)
        {
            bool ret = false;

            if (ex != null && o != null)
            {
                ret = true;
                //string exString = ($"This is a exception in class {o.GetType().ToString()} :\n {ex.ToString()}");
                // log the message to DB or file by calling writer method.
            }

            return ret;
        }

    }
}
