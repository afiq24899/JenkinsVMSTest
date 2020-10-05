using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Classses: Board, Zone, Location, Display, Player
/// </summary>
namespace Lingkail.VMS.Models
{
    //1 Board = 1 Zone, 1 Location, 1 Display, * GroupPresets(BoardGroupingAssignment)
    public class Board
    {
        //Properties - - - - - - - - - - - - - - - - - 
        public int ID { get; set; } //primary key   
        [Display(Name = "VMS")] public string Name { get; set; } //e.g. v001
        public bool IsAtSite { get; set; } //has location on the map(true), testboard or non-existing(false)
        public int ZoneID { get; set; } //foreign key


        //Navigation Properties - - - - - - - - - - - - - - - - - 
        public Zone Zone { get; set; } 
        public Location Location { get; set; }
        public Display Display { get; set; }
        public ICollection<BoardGroupingAssignment> BoardGroupingAssignments { get; set; }
    }

    //1 Zone = * Boards
    public class Zone
    {
        //Properties - - - - - - - - - - - - - - - - - 
        public int ID { get; set; } //primary key
        [Display(Name = "ZoneName")] public string Name { get; set; } //e.g. zone1    
        public string Description { get; set; } //e.g. first20


        //Navigation Properties - - - - - - - - - - - - - - - - - 
        public ICollection<Board> Boards { get; set; }
    }

    //0..1 Location = 1 Board 
    public class Location
    {
        public Location() {
        //create empty constructor 
        //see: https://stackoverflow.com/questions/55838188/system-invalidoperationexception-no-suitable-constructor-found-for-entity-type
        /*issue - No suitable constructor found for entity type 'Location'. 
        The following constructors had parameters that could not be bound to properties 
        of the entity type: cannot bind 'board' in 'Location(Board board)'.*/
        }

        //Properties - - - - - - - - - - - - - - - - - 
        [Key] public int BoardID { get; set; } //primary key 
        public string Address { get; set; } //e.g. JalanX
        public double Latitude { get; set; } //e.g. 3.171541667
        public double Longitude { get; set; } //e.g. 101.6988056    

        //Navigation Properties - - - - - - - - - - - - - - - - - 
        public Board Board { get; set; }

    }

    //0..1 Display = 1 Board, 1 Player, * EditorMessages(MessageAssignments)
    public class Display
    {
        private string sURL;

        //Properties - - - - - - - - - - - - - - - - -
        [Key] public int BoardID { get; set; } //primary key
        public bool IsOnline { get; set; } 
        public bool HasAlert { get; set; } //incoming 3rd party info
        public bool HasUniqueDisplay { get; set; } //displaying 3rd party info 
        public string CCTV { get; set; } //e.g. rtsp://admin:...
        public DateTime InstallationDate { get; set; } //initial installation date of C4
        public int OperationalStatus { get; set; } //1-installed and operational 2-not yet installed 3-under maintainence
        public string PTZ { get; set; } //e.g. http://admin:... or port

        //To be moved to another class in the future - e.g. player (for different player provider)
        public string C4_IP { get; set; } //e.g. 192.168.43.65
        public string Screenshot_URL
        {
            get { return sURL; }
            set { if (IsOnline) sURL = "http://" + C4_IP + "/api/screenshot"; else sURL = "OFFLINE/SLEEP"; }
        }  //e.g. http://.../api/screenshot
        //To be moved to another class in the future - e.g. player

        //Temporary to test Alibaba incident - Accident Event
        public string AlibabaAccidentImage { get; set; }
        public string AlibabaText { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss tt}")]
        public DateTime AlibabaCreationDate { get; set; }
        //Temporary to test Alibaba incident - Accident Event


        //Navigation Properties - - - - - - - - - - - - - - - - -
        public Board Board { get; set; }
        public ICollection<MessageAssignment> MessageAssignments { get; set; }

        //  public Player Player { get; set; }

    }

    //1 Player = * Displays
/*    public class Player {
        public int ID { get; set; }
        public string Provider { get; set; } //e.g. ColorLight


        *//*incomplete
         move the c4 and screenshot property to this class/ new class
        note to update db context to 
        create table on database*//*

        public ICollection<Display> Displays { get; set; }
    }*/

}
