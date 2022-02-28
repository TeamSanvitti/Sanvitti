using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Common
{
    public class BaseCreateInstance
    {
        public static T CreateInstance<T>()
        {
            T retObj = default;

            if (retObj == null)
            {
                retObj = Activator.CreateInstance<T>();
            }

            return retObj;
        }
    }
}
