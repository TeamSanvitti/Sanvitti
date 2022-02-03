using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class ForecastLog
    {
        public string ForecastStatus { get; set; }
        public DateTime ForecastDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Comments { get; set; }
        public string UserName { get; set; }
        public string ForecastSource { get; set; }
        
        
    }
}