using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SV.Framework.Models.Common
{
    public static class CsvConverter
    {
        /*
            List<person> persons = new List<person>();
            persons.Add(new person() { name = "jag", address = "1" });
            persons.Add(new person() { name = "jag2", address = "1" });
            string string2CSV = persons.ToCSV();
            
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=SqlExport.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(string2CSV);
            Response.Flush();
            Response.End();
         * */
        public static string ToCSV<T>(this IEnumerable<T> list)
        {
            var type = typeof(T);
            var props = type.GetProperties();

            //Setup expression constants
            var param = Expression.Parameter(type, "x");
            var doublequote = Expression.Constant("\"");
            var doublequoteescape = Expression.Constant("\"\"");
            var comma = Expression.Constant(",");

            //Convert all properties to strings, escape and enclose in double quotes
            var propq = (from prop in props
                         let tostringcall = Expression.Call(Expression.Property(param, prop), prop.ReflectedType.GetMethod("ToString", new Type[0]))
                         let replacecall = Expression.Call(tostringcall, typeof(string).GetMethod("Replace", new Type[] { typeof(String), typeof(String) }), doublequote, doublequoteescape)
                         select Expression.Call(typeof(string).GetMethod("Concat", new Type[] { typeof(String), typeof(String), typeof(String) }), doublequote, replacecall, doublequote)
                         ).ToArray();

            var concatLine = propq[0];
            for (int i = 1; i < propq.Length; i++)
                concatLine = Expression.Call(typeof(string).GetMethod("Concat", new Type[] { typeof(String), typeof(String), typeof(String) }), concatLine, comma, propq[i]);

            var method = Expression.Lambda<Func<T, String>>(concatLine, param).Compile();

            var header = String.Join(",", props.Select(p => p.Name).ToArray());

            return header + Environment.NewLine + String.Join(Environment.NewLine, list.Select(method).ToArray());
        }
        public static string ToTXT<T>(this IEnumerable<T> list)
        {
            var type = typeof(T);
            var props = type.GetProperties();

            //Setup expression constants
            var param = Expression.Parameter(type, "x");
            var doublequote = Expression.Constant("\"");
            var doublequoteescape = Expression.Constant("\"\"");
            var comma = Expression.Constant(",");

            //Convert all properties to strings, escape and enclose in double quotes
            var propq = (from prop in props
                         let tostringcall = Expression.Call(Expression.Property(param, prop), prop.ReflectedType.GetMethod("ToString", new Type[0]))
                         let replacecall = Expression.Call(tostringcall, typeof(string).GetMethod("Replace", new Type[] { typeof(String), typeof(String) }), doublequote, doublequoteescape)
                         select Expression.Call(typeof(string).GetMethod("Concat", new Type[] { typeof(String), typeof(String), typeof(String) }), doublequote, replacecall, doublequote)
                         ).ToArray();

            var concatLine = propq[0];
            for (int i = 1; i < propq.Length; i++)
                concatLine = Expression.Call(typeof(string).GetMethod("Concat", new Type[] { typeof(String), typeof(String), typeof(String) }), concatLine, comma, propq[i]);

            var method = Expression.Lambda<Func<T, String>>(concatLine, param).Compile();

            //var header = String.Join(",", props.Select(p => p.Name).ToArray());

            return String.Join(Environment.NewLine, list.Select(method).ToArray());
        }
    }
}
