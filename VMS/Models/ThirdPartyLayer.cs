using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Classses: 
/// </summary>
namespace Lingkail.VMS.Models
{
    public class InfoType
    {
        //Properties - - - - - - - - - - - - - - - - - 
        public int ID { get; set; } //primary key 
        public string Name { get; set; } //e.g. incident, advertisement, festival, ... 
        public string Object { get; set; } //some info
        
    }


    public class Incident
    {
        private string incidentimg;

        //Properties - - - - - - - - - - - - - - - - - 
        public int ID { get; set; } //primary key
        public int IncidentTypeID { get; set; } //foreign key
        public bool IsFullWebPath { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss tt}")]
        public DateTime DateTimeReceived { get; set; }
        public string EventImage { get; set; }
        public string Text { get; set; }
        public int ThisBoardId { get; set; } //e.g. 1,2,3, ...
        public string ThisBoardName { get; set; } //e.g. V001 


        //Navigation Properties - - - - - - - - - - - - - - - - -
        public IncidentType IncidentType { get; set; }
    }

    public class IncidentType
    {
        //Properties - - - - - - - - - - - - - - - - - 
        public int ID { get; set; }
        public string Name { get; set; }


        //Navigation Properties - - - - - - - - - - - - - - - - - 
        public ICollection<Incident> Incidents { get; set; }
    }
}
