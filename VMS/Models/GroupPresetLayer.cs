using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Classses: GroupPreset, Camera, CameraGroupingAssignment, BoardGroupingAssignment
/// </summary>
namespace Lingkail.VMS.Models
{
    //1 GroupPreset = * Boards(BoardGroupingAssignment), * Cameras(CameraGroupingAssignment)
    public class GroupPreset
    {
        //Properties - - - - - - - - - - - - - - - - - 
        public int ID { get; set; }
        [Display(Name = "GroupName")] public string Name { get; set; } //e.g. preset#1

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss tt}")]
        public DateTime DateTimeCreated { get; set; }

        //Navigation Properties - - - - - - - - - - - - - - - - - 
        public ICollection<BoardGroupingAssignment> BoardGroupingAssignments { get; set; }
        public ICollection<CameraGroupingAssignment> CameraGroupingAssignments { get; set; }
    }

    //1 Camera = * GroupPreset(CameraGroupingAssignment)
    public class Camera
    {
        //Properties - - - - - - - - - - - - - - - - - 
        public int ID { get; set; }
        [Display(Name = "CameraName")] public string Name { get; set; } //e.g. CN00A


        //Navigation Properties - - - - - - - - - - - - - - - - - 
        public ICollection<CameraGroupingAssignment> CameraGroupingAssignments { get; set; }
    }

    #region Many-To-Many Pure-Join-Table (PJT)
    /*A many-to-many pure join table(PJT).
    EFCore does not support implicit join tables for many-to-many relationships*/
    public class CameraGroupingAssignment
    {
        //Properties - - - - - - - - - - - - - - - - - 
        //Composite PK configurated with fluent API - modelBuilder
        public int GroupPresetID { get; set; } //composite primary key
        public int CameraID { get; set; } //composite primary key


        //Navigation Properties - - - - - - - - - - - - - - - - - 
        public GroupPreset GroupPreset { get; set; }
        public Camera Camera { get; set; }
    }

    public class BoardGroupingAssignment
    {
        //Properties - - - - - - - - - - - - - - - - - 
        //Composite PK configurated with fluent API - modelBuilder
        public int GroupPresetID { get; set; } //composite primary key
        public int BoardID { get; set; } //composite primary key


        //Navigation Properties - - - - - - - - - - - - - - - - - 
        public GroupPreset GroupPreset { get; set; }
        public Board Board { get; set; }
    }
    #endregion
}
