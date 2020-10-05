using System;
using System.ComponentModel.DataAnnotations;

namespace Lingkail.VMS.Models
{
    public class History
    {
        public int ID { get; set; }     //Primary Key (Non-Nullable)


        [Display(Name = "VMS")]
        public string H_Name { get; set; }


        [Display(Name = "Address")]
        public string H_Address { get; set; }

        [Display(Name = "User")]
        public string H_User { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss tt}")]
        [Display(Name = "Last Edited")]
        public DateTime H_NowDateTime { get; set; }

        [Display(Name = "Folder Path")]
        public string Object { get; set; }         //Stores bulk information 
    }
}
