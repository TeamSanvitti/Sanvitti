using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SV.Framework.Inventory
{
    public static class CommonUtil
    {
        public static T Get<T>(this DataRow source, string columnName)
        {
            if (source == null)
                throw new ArgumentNullException("Data is null");

            if (columnName == null)
                throw new ArgumentNullException(columnName);

            if (source.IsNull(columnName))
            {
                T defaultValue = default(T);
                // if (defaultValue == null)
                return defaultValue;
            }

            // throws if the column is null and T is a non-nullable value type

            return (T)Convert.ChangeType(source[columnName], typeof(T));
        }

        public static T Get<T>(this object sourceValue)
        {
            if (sourceValue == null)
                throw new ArgumentNullException("Data is null");

            if (sourceValue.Equals(DBNull.Value))
            {
                T defaultValue = default(T);
                // if (defaultValue == null)
                return defaultValue;
            }

            // throws if the column is null and T is a non-nullable value type

            return (T)Convert.ChangeType(sourceValue, typeof(T));
        }

        public static string SerializeObject<T>(T obj)
        {
            StringWriter xmlstringVal = null;
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            try
            {
                xmlstringVal = new StringWriter();
                xmlSettings.OmitXmlDeclaration = true;
                XmlSerializer xs = new XmlSerializer(typeof(T));
                XmlWriter xmlWriter = XmlWriter.Create(xmlstringVal, xmlSettings);
                xs.Serialize(xmlWriter, obj, ns);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                xmlSettings = null;
                xmlstringVal.Dispose();
            }

            return xmlstringVal.ToString().Trim();
        }

        public static T Def<T>(this SqlDataReader r, int ord)
        {
            var t = r.GetSqlValue(ord);
            if (t == DBNull.Value) 
                return default(T);
            else
                return (T)t;
        }

        public static T Val<T>(this SqlDataReader r, int ord) where T : struct
        {
            var t = r.GetSqlValue(ord);
            if (t == DBNull.Value) 
                return default(T);
            else
                return (T)t;
        }

        public static T Ref<T>(this SqlDataReader r, int ord) where T : class
        {
            var t = r.GetSqlValue(ord);
            if (t == DBNull.Value) 
                return null;
            else
                return  (T)t;
        }
    
    }

}
