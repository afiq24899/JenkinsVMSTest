using System;
using System.ComponentModel.DataAnnotations;

namespace Lingkail.VMS.Models
{
    public class DBKL_PGIS
    {
        [Key]
        public int MallID { get; set; }
        public string Board { get; set; }
        public int Phase { get; set; }        //I.e. name of a mall 
        public string Bname { get; set; }    // board name
        public string sname { get; set; }         //Available space
        public string name { get; set; }         //Available space
        public string parking { get; set; }         //Available space
        public DateTime? NowDateTime { get; set; }  //Capture the current date and time for incoming information 

        public string ImageFileName { get; set; } //Logos
    }

    public class TravelTimeInfos_B1s
    {
        [Key]
        public int id { get; set; }
        public string eventType { get; set; }
        public string sname { get; set; }         //name of board
        public string name { get; set; }         //name of destination
        public string datetime { get; set; }  //Capture the current date and time for incoming information 
        public string eta { get; set; }         //eta
    }

    public class TrafficInfo
    {
        [Key]                                       //Coexist with a Info Provider
        public int InfoProviderID { get; set; }     //Primary key (Non-Nullable)
        public string Board { get; set; }
        public string Event { get; set; }           //6 types of event in jpeg (i.e. accident, weather, ...) 
        public string TravelTime { get; set; }      //Travel time from PointA to PointB in text 
        public string PointA { get; set; }          //Staring point shown in board display        
        public string PointB { get; set; }          //Destination point shown in board display  
        public DateTime? NowDateTime { get; set; }  //Capture the current date and time for incoming information 

    }

    public class Services 
    { 

        public class TravelTimeUpdateRequest
        {

            public int id { set; get; } //destination id
            public string eventType { set; get; }//all the event types supported
            public string sname { set; get; } // the name of VMS board
            public string name { set; get; } //destination
            public string datetime { set; get; }
            public string eta { set; get; }

        }

        public class ParkingUpdateRequest
        {
            public int id { set; get; }
            public int MallID { get; set; }
            public string Bname { set; get; } // the name of VMS board
            public string sname { get; set; }    // mall name/logo
            public string parking { get; set; }    // parking slot 
            public string ImageFileName { get; set; }
        }

        public class AccidentUpdateRequest
        {
            public int id { set; get; }
            public string EventClass { set; get; }//all the event types supported
            public string Bname { set; get; } // the name of VMS board
            public string EventImage { set; get; } // currently will be fixed to img that David draw (accident_sign.png)
            public string ExtraImage { set; get; }   // images that will be pushed by their side will be taken into EventImage2
            public string Text { set; get; }   // Text that will pushed by their side
            
            public bool IsFullPath { get; set; }

            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss tt}")]
            public DateTime DateTimePushed { get; set; }

        }



        public class Video
        {
            [Key]
            public int MessageID { get; set; }
            public string VidType { get; set; }
            public string BoardID { get; set; }
            public string Bname { get; set; }
            public string Message { get; set; }
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
            public DateTime NowDateTime { get; set; }

        }

        public class VideoUpdateRequest
        {
            public int id { set; get; }
            public string VidType { set; get; }//all the video types supported
            public string BoardID { set; get; } // the ID of VMS board
            public string Bname { set; get; } // Board name
            public string Message { set; get; }   // Message video will pushed by Jahari's side

        }

        public class WeatherRequest
        {
            public int wid { set; get; }
            public string wmorning { set; get; }
            public string wafternoon { set; get; }
            public string wnight { set; get; }
            public string wdate { set; get; }
        }
    }
}
