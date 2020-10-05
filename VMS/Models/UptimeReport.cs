using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lingkail.VMS.Models
{
    public class UptimeReport
    {
        //Properties------------------------------------------------ 
        public int ID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string Remark { get; set; }
         
    }
}
