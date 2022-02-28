using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SV.Framework.Models.Common
{
    public class clsGeneral
    {
        public static object getColumnData(DataRow dr, string colName, object defVal, bool bThrow)
        {
            object retVal = defVal;

            if (dr.Table.Columns.Contains(colName) == false && bThrow)
                throw new Exception(colName + " column does not exists");
            else if (dr.Table.Columns.Contains(colName) == false && bThrow == false)
            {
            }
            else
            {
                object obj = dr[colName];
                try
                {
                    // If column does not exist, it will throw exception and return default value if throw is false
                    retVal = (((obj == null) || (obj != null && (obj.GetType() == typeof(System.DBNull)))) ? defVal : dr[colName]);
                }
                catch (Exception)
                {
                    if (bThrow) { throw; }
                }
            }

            return retVal;
        }
        
    }
}
