using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class FulfillmentForecast
    {
        //private string forecastNumber;
        //private DateTime forecastDate;
        //private string forecastNumber;

        public string ForecastNumber { get; set; }
        public DateTime ForecastDate { get; set; }
        public string Comments { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public int ForecastID { get; set; }
        public int CompanyID { get; set; }
        
        public List<ForecastLineItem> ForecastLineItems { get; set; }



    }
}