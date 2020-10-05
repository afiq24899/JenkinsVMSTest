using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingkail.VMS.Models
{
    //This is the properties inside Weather Forecast table
    public class WeatherForecast
    {
        public int ID { set; get; }
        public string Location { set; get; }
        public string MorningCondition { set; get; }
        public string AfternoonCondition { set; get; }
        public string NightCondition { set; get; }
        public string Date { set; get; }
    }
}
