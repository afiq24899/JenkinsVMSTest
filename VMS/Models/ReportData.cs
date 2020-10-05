using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lingkail.VMS.Models
{
    public class ReportData
    {
        public int ID { get; set; }
        public string Year { get; set; }
        public string Months { get; set; }
        public string Days { get; set; }
        public string Boards { get; set; }
        public string UpTotal { get; set; }
        public string DownTotal { get; set; }
        public string StartDate { get; set; }
        public string Location { get; set; }
        public string Remark { get; set; }
    }
}
