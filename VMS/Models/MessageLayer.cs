using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Classses: EditorMessage, EditorMessageType, MessageAssignment
/// </summary>
namespace Lingkail.VMS.Models
{
    //1 EditorMessage = 1 EditorMessageType, * Displays(MessageAssignment)
    public class EditorMessage
    {
        //Properties - - - - - - - - - - - - - - - - - 
        public int ID { get; set; } //primary key
        public string Message { get; set; } //e.g. message1, message2, ...
        public string ImageFileName { get; set; } //e.g. message1, message2, ... (w/o ext)

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; } //?

        public int? Timer { get; set; }  //1s by default (in seconds)
        public string CodePlus { get; set; } //cluster of information on client inputs
        public int ThisBoard { get; set; } //e.g. 0,1,... 0 = multiple boards 
        public int? EditorMessageTypeID { get; set; } //foreign key


        //Navigation Properties - - - - - - - - - - - - - - - - - 
        public EditorMessageType EditorMessageType { get; set; }
        public ICollection<MessageAssignment> MessageAssignments { get; set; }
    }

    //1 EditorMessageType = * EditorMessages, * EditorMessages (MessageAssignment)
    //Note: duplicate
    public class EditorMessageType
    {
        //Properties - - - - - - - - - - - - - - - - - 
        public int ID { get; set; } //primary key
        public string Type { get; set; }    //e.g. Parking, Upload, ...


        //Navigation Properties - - - - - - - - - - - - - - - - - 
        public ICollection<EditorMessage> EditorMessages { get; set; }
        public ICollection<MessageAssignment> MessageAssignments { get; set; }
    }

    #region Many-To-Many Join Table with Payload
    /*A many-to-many join table with payload - NOT a pure join table(PJT).
    EFCore does not support implicit join tables for many-to-many relationships*/
    public class MessageAssignment
    {
        //Properties - - - - - - - - - - - - - - - - - 
        //Composite PK configurated with fluent API - modelBuilder
        public int DisplayBoardID { get; set; } //composite primary key
        public int EditorMessageID { get; set; } //composite primary key
        public string ImageFileName { get; set; } //<copy from EditorMessage>

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; } //<copy from EditorMessage>

        public int? Timer { get; set; } //<copy from EditorMessage>
        public string CodePlus { get; set; } //<copy from EditorMessage>
        public int? EditorMessageTypeID { get; set; } //<copy from EditorMessage>


        //Navigation Properties - - - - - - - - - - - - - - - - - 
        public EditorMessageType EditorMessageType { get; set; }
        public Display Display { get; set; }
        public EditorMessage EditorMessage { get; set; }
    }
    #endregion

}
