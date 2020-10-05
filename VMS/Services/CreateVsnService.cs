using Lingkail.VMS.Data;
using Lingkail.VMS.Models;
using Lingkail.VMS.Models.StaticFolders;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Lingkail.VMS.Services
{
    public class CreateVsnService
    {
        private readonly IWebHostEnvironment _webEnv;
        private readonly SenaVMSContext _context;

        public CreateVsnService(
            SenaVMSContext context,
            IWebHostEnvironment webHostEnvironment
            )
        {
            _context = context;
            _webEnv = webHostEnvironment;
        }


        //Create .vsn (Json) file 
        public string createVSN(int boardId, bool IsMultiple)
        {
            string myPath = "empty";//initialize

            try
            {
                if (!IsMultiple) { myPath = UploadsFolder.GetUploadsSubPath(UploadsFolder.TempFolderSingle); }
                else { myPath = UploadsFolder.GetUploadsSubPath(UploadsFolder.TempFolderMultiple); }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error saving to file: {e.Message}");
                Debug.WriteLine($"Error saving to file: {e.Message}");
                Debug.WriteLine($"Stack trace: {e.StackTrace}");
            }
            //create the jason object
            RootObject boardVsn = new RootObject();
            Programs myPrograms = new Programs();

            Information boardInformation = new Information();
            boardInformation.Width = "768";
            boardInformation.Height = "128";

            //random Id

            //Id randID = new Id();
            //randID.ToString();

            Program myProgram = new Program();
            myProgram.Information = boardInformation;
            myProgram.Id = DateTime.Now;




            //loop through all index and create page
            //int pageCount = 5;//number of board. in the future this will be dynamically created from editor, now fix to 5
            //int index = 0;

            myProgram.Pages = new List<Page>();

            //for (index = 0; index < pageCount; index++)
            //{
            //    //to detect from database what type of page is to be created for current index
            //    myProgram.Pages.Add(constructPage(pageType.Picture, index, id));
            //}

            //loading from database
            //public TrafficInfo mytrafficinfo { get; set; }
            TrafficInfo mytrafficinfo = _context.TrafficInfos
               .FirstOrDefault(m => m.PointA == "VZ001");  //Gets the 'location' for that specific boardId

            //to detect from database what type of page is to be created for current index
            //now manually create 5 pages
            //myProgram.Pages.Add(constructPage(pageType.Picture, 0, id));


            List<EditorMessage> thisBoardMessageList = new List<EditorMessage>();
            var allEditorMessages = _context.EditorMessages.ToList();

            foreach (var item in allEditorMessages)
            {
                if (item.CodePlus != null)
                {
                    thisBoardMessageList.Add(item);
                }
            }

            foreach (var messageObj in thisBoardMessageList)
            {
                pageType thisPageType = new pageType();
                switch (messageObj.EditorMessageTypeID)
                {
                    case 1: //Parking
                        thisPageType = pageType.ParkingInfo;
                        break;
                    case 2: //Upload
                        if (messageObj.CodePlus.Contains("IMAGE")) { thisPageType = pageType.Picture; break; }
                        if (messageObj.CodePlus.Contains("VIDEO")) { thisPageType = pageType.Video; break; }
                        else { throw new System.InvalidOperationException(""); }
                    case 3: //Travel Time
                        thisPageType = pageType.TravelTime;
                        break;
                    case 4: //Custom (For now, as a whole picture) 
                        thisPageType = pageType.Picture;
                        break;
                    case 5: //Weather
                        thisPageType = pageType.Weather;
                        break;
                    default: //Error
                        throw new System.InvalidOperationException("");
                }
                myProgram.Pages.Add(constructPage(messageObj, thisPageType, messageObj.ID, boardId)); //Don't use messageObj.ThisBoard (multiple will give Board 0)
            }

            myPrograms.Program = myProgram;
            boardVsn.Programs = myPrograms;

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize<RootObject>(boardVsn, options);
            System.IO.File.WriteAllText(Path.Combine(myPath, "board" + boardId.ToString() + ".vsn"), jsonString);

            return "board" + boardId.ToString() + ".vsn";
        }

        protected Page constructPage(EditorMessage messageObj, pageType pagetype, int msgIndex, int boardId)
        {
            int? timer_ms_int = messageObj.Timer * 1000; //database store in s, vsn need timer in ms
            string timer_ms = timer_ms_int.ToString();

            Page myPage = new Page();       //initialize otherwise it complains               

            switch (pagetype)
            {
                case pageType.Picture:
                    Regions myRegions = new Regions();
                    myRegions = constructRegion_picture(messageObj, myRegions, msgIndex, boardId);
                    myPage = new Page();
                    myPage.AppointDuration = timer_ms;
                    myPage.LoopType = "0";
                    myPage.Regions = new List<Regions>();
                    myPage.Regions.Add(myRegions);
                    myPage.Name = "Message " + msgIndex.ToString() + "-" + pagetype.ToString();
                    break;
                case pageType.Video:
                    myPage = new Page();
                    myPage.AppointDuration = timer_ms;
                    myPage.LoopType = "1";
                    myPage.Regions = new List<Regions>();
                    myPage = constructRegion_video(messageObj, myPage, msgIndex, boardId);
                    myPage.Name = "Message " + msgIndex.ToString() + "-" + pagetype.ToString();
                    break;
                case pageType.Accident:
                    myPage = new Page();
                    myPage.AppointDuration = timer_ms;
                    myPage.LoopType = "0";
                    myPage.Regions = new List<Regions>();
                    myPage = constructRegion_accident(messageObj, myPage, msgIndex, boardId);
                    myPage.Name = "Message " + msgIndex.ToString() + "-" + pagetype.ToString();
                    break;
                case pageType.TravelTime:
                    myPage = new Page();
                    myPage.AppointDuration = timer_ms;
                    myPage.LoopType = "0";
                    myPage.Regions = new List<Regions>();
                    //passing myPage and myRegions to function constructRegion_traveltime
                    //which will create all the regions to be added to myPage and return myPage
                    myPage = constructRegion_traveltime(messageObj, myPage, msgIndex, boardId);
                    myPage.Name = "Message " + msgIndex.ToString() + "-" + pagetype.ToString();
                    break;
                case pageType.ParkingInfo:
                    myPage = new Page();
                    myPage.AppointDuration = timer_ms;
                    myPage.LoopType = "0";
                    myPage.Regions = new List<Regions>();
                    myPage = constructRegion_parking(messageObj, myPage, msgIndex, boardId);
                    myPage.Name = "Message " + msgIndex.ToString() + "-" + pagetype.ToString();
                    break;
                case pageType.Weather:
                    myPage = new Page();
                    myPage.AppointDuration = timer_ms;
                    myPage.LoopType = "0";
                    myPage.Regions = new List<Regions>();
                    myPage = constructRegion_weather(messageObj, myPage, msgIndex, boardId);
                    myPage.Name = "Message " + msgIndex.ToString() + "-" + pagetype.ToString();
                    break;
                default:
                    //error
                    throw new System.InvalidOperationException("invalid pageType specifier");
            }

            return myPage;
        }

        #region constructRegion_<pageType> functions
        private Regions constructRegion_picture(EditorMessage messageObj, Regions myRegions, int index, int id)
        {
            string[] codePlus = messageObj.CodePlus.Split(',');
            string uploadedFileName = codePlus[1];

            //construct vsn file
            Rect myRect = new Rect();
            myRect.X = "0";
            myRect.Y = "0";
            myRect.Width = "768";
            myRect.Height = "128";
            myRect.BorderWidth = "0";
            myRect.BorderColor = "0xFFFFFF00";

            Item myItems = new Item();
            myItems.Type = "2";
            myItems.FileSource = new FileSource();
            myItems.FileSource.IsRelative = "1";
            string messageFilePath = "";
            messageFilePath = "./board" + id.ToString() + ".files/" + uploadedFileName;
            myItems.FileSource.FilePath = messageFilePath;

            myRegions.type = "1";
            myRegions.Name = "File 1";
            myRegions.Layer = 1;
            myRegions.Rect = myRect;
            myRegions.Items = new List<Item>();
            myRegions.Items.Add(myItems);

            return myRegions;
        }
        private Page constructRegion_video(EditorMessage messageObj, Page myPage, int index, int id)
        {
            string[] codePlus = messageObj.CodePlus.Split(',');
            string uploadedFileName = codePlus[1];

            //construct vsn file
            Rect myRect = new Rect();
            myRect.X = "0";
            myRect.Y = "0";
            myRect.Width = "768";
            myRect.Height = "128";
            myRect.BorderWidth = "0";
            myRect.BorderColor = "0xFFFFFF00";

            Item myItems = new Item();
            myItems.Type = "3";
            myItems.FileSource = new FileSource();
            myItems.FileSource.IsRelative = "1";
            string messageFilePath = "";
            messageFilePath = "./board" + id.ToString() + ".files/" + uploadedFileName;
            myItems.FileSource.FilePath = messageFilePath;

            Regions myRegions = new Regions();
            myRegions.type = "1";
            myRegions.Name = "File 1";
            myRegions.Layer = 1;
            myRegions.Rect = myRect;
            myRegions.Items = new List<Item>();
            myRegions.Items.Add(myItems);
            myPage.Regions.Add(myRegions);

            return myPage;
        }
        private Page constructRegion_accident(EditorMessage messageObj, Page myPage, int index, int id)
        {
            //construct vsn file
            string messageFilePath = "";
            Rect myRect;
            Item myItem;

            #region region 0
            myRect = new Rect();
            myRect.X = "00";
            myRect.Y = "0";
            myRect.Width = "256";
            myRect.Height = "128";
            myItem = new Item();
            myItem.Type = "2";
            myItem.FileSource = new FileSource();
            myItem.FileSource.IsRelative = "1";
            messageFilePath = "./board" + id.ToString() + ".files/" + "<ImageFilename+ext>"; // e.g. ETA.png , logo from David
            myItem.FileSource.FilePath = messageFilePath;

            Regions myRegions = new Regions();
            myRegions.Items = new List<Item>();
            myRegions.Items.Add(myItem);
            myRegions.Rect = myRect;
            myRegions.type = "1";
            myRegions.Layer = 1;
            myRegions.Name = "file 1";

            myPage.Regions.Add(myRegions);
            #endregion

            #region region 1
            Rect myRect1 = new Rect();//replace the previous myRect
            myRect1.X = "256";
            myRect1.Y = "0";
            myRect1.Width = "256";
            myRect1.Height = "128";

            Item myItem1 = new Item();
            myItem1.Type = "5";
            myItem1.Text = "<Some incident texts>";     // Taking 'Text' from Incident Table in database
            myItem1.TextColor = "#00FF00";
            myItem1.IsScroll = "0";

            Regions myRegions1 = new Regions();
            myRegions1.Items = new List<Item>();
            myRegions1.Rect = myRect1;
            myRegions1.type = "1";
            myRegions1.Layer = 2;
            myRegions1.Items.Add(myItem1);

            myPage.Regions.Add(myRegions1);
            #endregion

            #region region 3
            myRect = new Rect();
            myRect.X = "512";
            myRect.Y = "0";
            myRect.Width = "256";
            myRect.Height = "128";
            myItem = new Item();
            myItem.Type = "2";
            myItem.FileSource = new FileSource();
            myItem.FileSource.IsRelative = "1";
            messageFilePath = "";
            messageFilePath = "./board" + id.ToString() + ".files/" + "<ImageFilename+ext>"; // from database, image pushed by Jahari
            myItem.FileSource.FilePath = messageFilePath;

            Regions myRegions2 = new Regions();
            myRegions2.Items = new List<Item>();
            myRegions2.Items.Add(myItem);
            myRegions2.Rect = myRect;
            myRegions2.type = "1";
            myRegions2.Layer = 3;
            myRegions2.Name = "file 1";

            myPage.Regions.Add(myRegions2);
            #endregion
            return myPage;

        }
        private Page constructRegion_parking(EditorMessage messageObj, Page myPage, int index, int id)
        {
            string[] codePlus = messageObj.CodePlus.Split(',');
            string[] canvasMallid_S = { codePlus[1], codePlus[3], codePlus[5], codePlus[7] };
            int[] canvasMallId = Array.ConvertAll(canvasMallid_S, int.Parse); ;
            int TLHS_LogoId = canvasMallId[0];  //Top Left Hand Side
            var TLHS_Entity = _context.ParkingInfos.Find(TLHS_LogoId);

            int BLHS_LogoId = canvasMallId[1];  //Bottom Left Hand Side
            var BLHS_Entity = _context.ParkingInfos.Find(BLHS_LogoId);

            int TRHS_LogoId = canvasMallId[2];  //Top Right Hand Side
            var TRHS_Entity = _context.ParkingInfos.Find(TRHS_LogoId);

            int BRHS_LogoId = canvasMallId[3];  //Bottom Right Hand Side
            var BRHS_Entity = _context.ParkingInfos.Find(BRHS_LogoId);
            //---------------------------------------------------------

            //there are in total 8 regions
            string messageFilePath = "";
            Rect myRect;
            Item myItem;


            #region region 1
            myRect = new Rect();
            myRect.X = "0";           // top left 
            myRect.Y = "0";
            myRect.Width = "192";
            myRect.Height = "64";
            myRect.BorderColor = "0xFF000000";
            myRect.BorderWidth = "2";
            myItem = new Item();
            myItem.Type = "2";
            myItem.FileSource = new FileSource();
            myItem.FileSource.IsRelative = "1";
            messageFilePath = "./board" + id.ToString() + ".files/" + TLHS_Entity.ImageFileName + ".png";
            myItem.FileSource.FilePath = messageFilePath;

            Regions myRegions1 = new Regions();
            myRegions1.Items = new List<Item>();
            myRegions1.Items.Add(myItem);
            myRegions1.Rect = myRect;
            myRegions1.type = "1";
            myRegions1.Layer = 1;
            myRegions1.Name = "Logo 1";

            myPage.Regions.Add(myRegions1);

            #endregion

            #region region 2
            Rect myRect1 = new Rect();//replace the previous myRect
            myRect1.X = "192";       // top 2nd left 
            myRect1.Y = "0";
            myRect1.Width = "192";
            myRect1.Height = "64";
            myRect1.BorderColor = "0xFF000000";
            myRect1.BorderWidth = "2";

            Item myItem1 = new Item();
            myItem1.Type = "4";
            myItem1.Text = TLHS_Entity.parking;
            myItem1.TextColor = "#00FF00";
            myItem1.IsScroll = "0";
            LogFont myLogFont1 = new LogFont();
            myLogFont1.lfHeight = "53";
            myLogFont1.lfWeight = "400";
            myLogFont1.lfCharSet = "1";
            myLogFont1.lfQuality = "3";
            myLogFont1.lfPitchAndFamily = "32";
            myLogFont1.lfFaceName = "Arial";
            myItem1.LogFont = myLogFont1;

            Regions myRegions2 = new Regions();
            myRegions2.Items = new List<Item>();
            myRegions2.Items.Add(myItem1);
            myRegions2.Rect = myRect1;
            myRegions2.type = "1";
            myRegions2.Layer = 1;
            myRegions2.Name = "Info 1";

            myPage.Regions.Add(myRegions2);

            #endregion

            #region region 3
            myRect = new Rect();
            myRect.X = "0";         // bottom left 
            myRect.Y = "64";
            myRect.Width = "192";
            myRect.Height = "64";
            myRect.BorderColor = "0xFF000000";
            myRect.BorderWidth = "2";
            myItem = new Item();
            myItem.Type = "2";
            myItem.FileSource = new FileSource();
            myItem.FileSource.IsRelative = "1";
            messageFilePath = "./board" + id.ToString() + ".files/" + BLHS_Entity.ImageFileName + ".png";
            myItem.FileSource.FilePath = messageFilePath;

            Regions myRegions3 = new Regions();
            myRegions3.Items = new List<Item>();
            myRegions3.Items.Add(myItem);
            myRegions3.Rect = myRect;
            myRegions3.type = "1";
            myRegions3.Layer = 1;
            myRegions3.Name = "Logo 2";

            myPage.Regions.Add(myRegions3);

            #endregion

            #region region 4
            Rect myRect2 = new Rect();//replace the previous myRect
            myRect2.X = "192";      // bottom 2nd left
            myRect2.Y = "64";
            myRect2.Width = "192";
            myRect2.Height = "64";
            myRect2.BorderColor = "0xFF000000";
            myRect2.BorderWidth = "2";

            Item myItem2 = new Item();
            myItem2.Type = "4";
            myItem2.Text = BLHS_Entity.parking;
            myItem2.TextColor = "#00FF00";
            myItem2.IsScroll = "0";
            LogFont myLogFont2 = new LogFont();
            myLogFont2.lfHeight = "53";
            myLogFont2.lfWeight = "400";
            myLogFont2.lfCharSet = "1";
            myLogFont2.lfQuality = "3";
            myLogFont2.lfPitchAndFamily = "32";
            myLogFont2.lfFaceName = "Arial";
            myItem2.LogFont = myLogFont2;

            Regions myRegions4 = new Regions();
            myRegions4.Items = new List<Item>();
            myRegions4.Items.Add(myItem2);
            myRegions4.Rect = myRect2;
            myRegions4.type = "1";
            myRegions4.Layer = 1;
            myRegions4.Name = "Info 2";

            myPage.Regions.Add(myRegions4);

            #endregion

            #region region 5
            myRect = new Rect();
            myRect.X = "384";        // top right 
            myRect.Y = "0";
            myRect.Width = "192";
            myRect.Height = "64";
            myRect.BorderColor = "0xFF000000";
            myRect.BorderWidth = "2";
            myItem = new Item();
            myItem.Type = "2";
            myItem.FileSource = new FileSource();
            myItem.FileSource.IsRelative = "1";
            messageFilePath = "./board" + id.ToString() + ".files/" + TRHS_Entity.ImageFileName + ".png";
            myItem.FileSource.FilePath = messageFilePath;

            Regions myRegions5 = new Regions();
            myRegions5.Items = new List<Item>();
            myRegions5.Items.Add(myItem);
            myRegions5.Rect = myRect;
            myRegions5.type = "1";
            myRegions5.Layer = 1;
            myRegions5.Name = "Logo 3";

            myPage.Regions.Add(myRegions5);

            #endregion

            #region region 6
            Rect myRect3 = new Rect();//replace the previous myRect
            myRect3.X = "576";        // top 4th right 
            myRect3.Y = "0";
            myRect3.Width = "192";
            myRect3.Height = "64";
            myRect3.BorderColor = "0xFF000000";
            myRect3.BorderWidth = "2";

            Item myItem3 = new Item();
            myItem3.Type = "4";
            myItem3.Text = TRHS_Entity.parking;
            myItem3.TextColor = "#00FF00";
            myItem3.IsScroll = "0";
            LogFont myLogFont3 = new LogFont();
            myLogFont3.lfHeight = "53";
            myLogFont3.lfWeight = "400";
            myLogFont3.lfCharSet = "1";
            myLogFont3.lfQuality = "3";
            myLogFont3.lfPitchAndFamily = "32";
            myLogFont3.lfFaceName = "Arial";
            myItem3.LogFont = myLogFont3;

            Regions myRegions6 = new Regions();
            myRegions6.Items = new List<Item>();
            myRegions6.Items.Add(myItem3);
            myRegions6.Rect = myRect3;
            myRegions6.type = "1";
            myRegions6.Layer = 1;
            myRegions6.Name = "Info 3";

            myPage.Regions.Add(myRegions6);

            #endregion

            #region region 7
            myRect = new Rect();
            myRect.X = "384";      // bottom right 
            myRect.Y = "64";
            myRect.Width = "192";
            myRect.Height = "64";
            myRect.BorderColor = "0xFF000000";
            myRect.BorderWidth = "2";
            myItem = new Item();
            myItem.Type = "2";
            myItem.FileSource = new FileSource();
            myItem.FileSource.IsRelative = "1";
            messageFilePath = "./board" + id.ToString() + ".files/" + BRHS_Entity.ImageFileName + ".png";
            myItem.FileSource.FilePath = messageFilePath;

            Regions myRegions7 = new Regions();
            myRegions7.Items = new List<Item>();
            myRegions7.Items.Add(myItem);
            myRegions7.Rect = myRect;
            myRegions7.type = "1";
            myRegions7.Layer = 1;
            myRegions7.Name = "Logo 4";

            myPage.Regions.Add(myRegions7);

            #endregion

            #region region 8
            Rect myRect4 = new Rect();//replace the previous myRect
            myRect4.X = "576";           // bottom 4th right 
            myRect4.Y = "64";
            myRect4.Width = "192";
            myRect4.Height = "64";
            myRect4.BorderColor = "0xFF000000";
            myRect4.BorderWidth = "2";

            Item myItem4 = new Item();
            myItem4.Type = "4";
            myItem4.Text = BRHS_Entity.parking;
            myItem4.TextColor = "#00FF00";
            myItem4.IsScroll = "0";
            LogFont myLogFont4 = new LogFont();
            myLogFont4.lfHeight = "53";
            myLogFont4.lfWeight = "400";
            myLogFont4.lfCharSet = "1";
            myLogFont4.lfQuality = "3";
            myLogFont4.lfPitchAndFamily = "32";
            myLogFont4.lfFaceName = "Arial";
            myItem4.LogFont = myLogFont4;

            Regions myRegions8 = new Regions();
            myRegions8.Items = new List<Item>();
            myRegions8.Items.Add(myItem4);
            myRegions8.Rect = myRect4;
            myRegions8.type = "1";
            myRegions8.Layer = 1;
            myRegions8.Name = "Info 4";

            myPage.Regions.Add(myRegions8);

            #endregion

            return myPage;

        }
        private Page constructRegion_traveltime(EditorMessage messageObj, Page myPage, int index, int id)
        {
            //to do: load data from database with real parameters. now hard coded
            /*string etaTimesSquare = traveltimeService.retrieveTravelTime(id, "1", "VZ001", "TIMES SQUARE");   //hardcoded info
            string etaMidValley = traveltimeService.retrieveTravelTime(id, "1", "VZ001", "MID VALLEY");*/

            //---------------------------------------------------------
            string[] codePlus = messageObj.CodePlus.Split(',');
            string[] selected_name = codePlus;

            var etaDest1 = _context.TravelTimeInfos_B1s
                .Where(t => t.name == selected_name[0])
                .Select(t => t.eta)
                .FirstOrDefault();
            var etaDest2 = _context.TravelTimeInfos_B1s
                .Where(t => t.name == selected_name[1])
                .Select(t => t.eta)
                .FirstOrDefault();


            //there are in total 7 regions
            string messageFilePath = "";
            Rect myRect;
            Item myItem;

            #region region 0
            myRect = new Rect();
            myRect.X = "100";
            myRect.Y = "40";
            myRect.Width = "100";
            myRect.Height = "50";
            myItem = new Item();
            myItem.Type = "2";
            myItem.FileSource = new FileSource();
            myItem.FileSource.IsRelative = "1";
            messageFilePath = "";
            messageFilePath = "./board" + id.ToString() + ".files/ETA.png";    // Fixed image (ETA logo) 
            // The above may cause some issues because this file must be available in the folder with vsn 
            myItem.FileSource.FilePath = messageFilePath;

            Regions myRegions = new Regions();
            myRegions.Items = new List<Item>();
            myRegions.Items.Add(myItem);
            myRegions.Rect = myRect;
            myRegions.type = "1";
            myRegions.Layer = 1;
            myRegions.Name = "File 1";

            myPage.Regions.Add(myRegions);

            #endregion

            #region region 1
            Rect myRect1 = new Rect();//replace the previous myRect
            myRect1.X = "250";
            myRect1.Y = "32";
            myRect1.Width = "250";
            myRect1.Height = "32";

            Item myItem1 = new Item();
            myItem1.Type = "5";
            myItem1.Text = selected_name[0];
            myItem1.TextColor = "#FF6347";
            myItem1.IsScroll = "0";

            Regions myRegions1 = new Regions();
            myRegions1.Items = new List<Item>();
            myRegions1.Rect = myRect1;
            myRegions1.type = "1";
            myRegions1.Layer = 3;
            myRegions1.Items.Add(myItem1);

            myPage.Regions.Add(myRegions1);
            #endregion

            #region region 2
            Rect myRect2 = new Rect();//replace the previous myRect
            myRect2.X = "250";
            myRect2.Y = "70";
            myRect2.Width = "250";
            myRect2.Height = "32";

            Item myItem2 = new Item();
            myItem2.Type = "5";
            myItem2.Text = selected_name[1];
            myItem2.TextColor = "#FF6347";
            myItem2.IsScroll = "0";

            Regions myRegions2 = new Regions();
            myRegions2.Items = new List<Item>();
            myRegions2.Rect = myRect2;
            myRegions2.type = "1";
            myRegions2.Layer = 3;
            myRegions2.Items.Add(myItem2);

            myPage.Regions.Add(myRegions2);
            #endregion

            #region region 3
            Rect myRect3 = new Rect();//replace the previous myRect
            myRect3.X = "400";
            myRect3.Y = "70";
            myRect3.Width = "250";
            myRect3.Height = "32";

            Item myItem3 = new Item();
            myItem3.Type = "5";
            myItem3.Text = "ETA";      // Hardcoded text (not from database)
            myItem3.TextColor = "#FF6347";
            myItem3.IsScroll = "0";

            Regions myRegions3 = new Regions();
            myRegions3.Items = new List<Item>();
            myRegions3.Rect = myRect3;
            myRegions3.type = "1";
            myRegions3.Layer = 3;
            myRegions3.Items.Add(myItem3);

            myPage.Regions.Add(myRegions3);
            #endregion

            #region region 4
            Rect myRect4 = new Rect();//replace the previous myRect
            myRect4.X = "400";
            myRect4.Y = "32";
            myRect4.Width = "250";
            myRect4.Height = "32";

            Item myItem4 = new Item();
            myItem4.Type = "5";
            myItem4.Text = "ETA";
            myItem4.TextColor = "#FF6347";
            myItem4.IsScroll = "0";

            Regions myRegions4 = new Regions();
            myRegions4.Items = new List<Item>();
            myRegions4.Rect = myRect4;
            myRegions4.type = "1";
            myRegions4.Layer = 3;
            myRegions4.Items.Add(myItem4);

            myPage.Regions.Add(myRegions4);
            #endregion

            #region region 5
            Rect myRect5 = new Rect();//replace the previous myRect5
            myRect5.X = "640";
            myRect5.Y = "32";
            myRect5.Width = "250";
            myRect5.Height = "32";

            Item myItem5 = new Item();
            myItem5.Type = "5";
            myItem5.Text = etaDest1;
            myItem5.TextColor = "#FF6347";
            myItem5.IsScroll = "0";

            Regions myRegions5 = new Regions();
            myRegions5.Items = new List<Item>();
            myRegions5.Rect = myRect5;
            myRegions5.type = "1";
            myRegions5.Layer = 3;
            myRegions5.Items.Add(myItem5);

            myPage.Regions.Add(myRegions5);
            #endregion

            #region region 6
            Rect myRect6 = new Rect();//replace the previous myRect
            myRect6.X = "640";
            myRect6.Y = "70";
            myRect6.Width = "250";
            myRect6.Height = "32";

            Item myItem6 = new Item();
            myItem6.Type = "5";
            myItem6.Text = etaDest2;
            myItem6.TextColor = "#FF6347";
            myItem6.IsScroll = "0";

            Regions myRegions6 = new Regions();
            myRegions6.Items = new List<Item>();
            myRegions6.Rect = myRect6;
            myRegions6.type = "1";
            myRegions6.Layer = 3;
            myRegions6.Items.Add(myItem6);

            myPage.Regions.Add(myRegions6);
            #endregion

            return myPage;
        }


        private Page constructRegion_weather(EditorMessage messageObj, Page myPage, int index, int id)
        {

            string[] codePlus = messageObj.CodePlus.Split(',');
            string[] selected_sname = codePlus;

            var morningcondition = _context.WeatherForecast
                .Where(t => t.Location == selected_sname[0])
                .Select(t => t.MorningCondition)
                .FirstOrDefault();

            var afternooncondition = _context.WeatherForecast
                .Where(t => t.Location == selected_sname[0])
                .Select(t => t.AfternoonCondition)
                .FirstOrDefault();

            var nightcondition = _context.WeatherForecast
                .Where(t => t.Location == selected_sname[0])
                .Select(t => t.NightCondition)
                .FirstOrDefault();

            //there are in total 7 regions
            //string messageFilePath = "";
            Rect myRect;
            Item myItem;

            #region region 0
            myRect = new Rect();
            myRect.X = "6";
            myRect.Y = "35";
            myRect.Width = "147";
            myRect.Height = "48";

            myItem = new Item();
            myItem.Type = "5";
            myItem.Text = selected_sname[0]; //location selected
            myItem.TextColor = "#FFFFFFFF";
            myItem.IsScroll = "0";
            LogFont myLogFont0 = new LogFont();
            myLogFont0.lfHeight = "33";
            myLogFont0.lfWeight = "700";
            myLogFont0.lfCharSet = "1";
            myLogFont0.lfQuality = "3";
            myLogFont0.lfPitchAndFamily = "32";
            myLogFont0.lfFaceName = "Arial";
            myItem.LogFont = myLogFont0;
            //myItem.Type = "2";
            //myItem.FileSource = new FileSource();
            //myItem.FileSource.IsRelative = "1";

            //messageFilePath = "";
            //messageFilePath = "./board" + id.ToString() + ".files/weather.jpg";    // Fixed image (ETA logo) 
            // The above may cause some issues because this file must be available in the folder with vsn 
            //myItem.FileSource.FilePath = messageFilePath;

            Regions myRegions = new Regions();
            myRegions.Items = new List<Item>();
            myRegions.Items.Add(myItem);
            myRegions.Rect = myRect;
            myRegions.type = "1";
            myRegions.Layer = 1;
            myRegions.Name = "File 1";

            myPage.Regions.Add(myRegions);

            #endregion

            #region region 1
            Rect myRect1 = new Rect();//replace the previous myRect
            myRect1.X = "558";
            myRect1.Y = "62";
            myRect1.Width = "175";
            myRect1.Height = "48";


            Item myItem1 = new Item();
            myItem1.Type = "5";
            myItem1.Text = nightcondition; //rain, no rain, thunderstorm
            myItem1.TextColor = "#FFFFFFFF";
            myItem1.IsScroll = "0";

            LogFont myLogFont1 = new LogFont();
            myLogFont1.lfHeight = "27";
            myLogFont1.lfWeight = "700";
            myLogFont1.lfCharSet = "1";
            myLogFont1.lfQuality = "3";
            myLogFont1.lfPitchAndFamily = "32";
            myLogFont1.lfFaceName = "Arial";
            myItem1.LogFont = myLogFont1;

            Regions myRegions1 = new Regions();
            myRegions1.Items = new List<Item>();
            myRegions1.Rect = myRect1;
            myRegions1.type = "1";
            myRegions1.Layer = 3;
            myRegions1.Items.Add(myItem1);

            myPage.Regions.Add(myRegions1);
            #endregion

            #region region 2
            Rect myRect2 = new Rect();//replace the previous myRect
            myRect2.X = "360";
            myRect2.Y = "64";
            myRect2.Width = "175";
            myRect2.Height = "48";

            Item myItem2 = new Item();
            myItem2.Type = "5";
            myItem2.Text = afternooncondition; //rain, no rain, thunderstorm
            myItem2.TextColor = "#FFFFFFFF";
            myItem2.IsScroll = "0";
            LogFont myLogFont2 = new LogFont();
            myLogFont2.lfHeight = "27";
            myLogFont2.lfWeight = "700";
            myLogFont2.lfCharSet = "1";
            myLogFont2.lfQuality = "3";
            myLogFont2.lfPitchAndFamily = "32";
            myLogFont2.lfFaceName = "Arial";
            myItem2.LogFont = myLogFont2;

            Regions myRegions2 = new Regions();
            myRegions2.Items = new List<Item>();
            myRegions2.Rect = myRect2;
            myRegions2.type = "1";
            myRegions2.Layer = 3;
            myRegions2.Items.Add(myItem2);

            myPage.Regions.Add(myRegions2);
            #endregion

            #region region 3
            Rect myRect3 = new Rect();//replace the previous myRect
            myRect3.X = "161";
            myRect3.Y = "63";
            myRect3.Width = "175";
            myRect3.Height = "48";



            Item myItem3 = new Item();
            myItem3.Type = "5";
            myItem3.Text = morningcondition; //rain, no rain, thunderstorm
            myItem3.TextColor = "#FFFFFFFF";
            myItem3.IsScroll = "0";
            LogFont myLogFont3 = new LogFont();
            myLogFont3.lfHeight = "27";
            myLogFont3.lfWeight = "700";
            myLogFont3.lfCharSet = "1";
            myLogFont3.lfQuality = "3";
            myLogFont3.lfPitchAndFamily = "32";
            myLogFont3.lfFaceName = "Arial";
            myItem3.LogFont = myLogFont3;

            Regions myRegions3 = new Regions();
            myRegions3.Items = new List<Item>();
            myRegions3.Rect = myRect3;
            myRegions3.type = "1";
            myRegions3.Layer = 3;
            myRegions3.Items.Add(myItem3);

            myPage.Regions.Add(myRegions3);
            #endregion

            #region region 4 
            Rect myRect4 = new Rect();//replace the previous myRect 
            myRect4.X = "558";
            myRect4.Y = "14";
            myRect4.Width = "175";
            myRect4.Height = "48";


            Item myItem4 = new Item();
            myItem4.Type = "5";
            myItem4.Text = "Night";
            myItem4.TextColor = "#FF00FF00";
            myItem4.IsScroll = "0";
            LogFont myLogFont4 = new LogFont();
            myLogFont4.lfHeight = "33";
            myLogFont4.lfWeight = "700";
            myLogFont4.lfCharSet = "1";
            myLogFont4.lfQuality = "3";
            myLogFont4.lfPitchAndFamily = "32";
            myLogFont4.lfFaceName = "Arial";
            myItem4.LogFont = myLogFont4;

            Regions myRegions4 = new Regions();
            myRegions4.Items = new List<Item>();
            myRegions4.Rect = myRect4;
            myRegions4.type = "1";
            myRegions4.Layer = 3;
            myRegions4.Items.Add(myItem4);

            myPage.Regions.Add(myRegions4);
            #endregion

            #region region 5
            Rect myRect5 = new Rect();//replace the previous myRect5 (Noon)
            myRect5.X = "360";
            myRect5.Y = "16";
            myRect5.Width = "175";
            myRect5.Height = "48";


            Item myItem5 = new Item();
            myItem5.Type = "5";
            myItem5.Text = "Afternoon";
            myItem5.TextColor = "#FF00FF00";
            myItem5.IsScroll = "0";
            LogFont myLogFont5 = new LogFont();
            myLogFont5.lfHeight = "33";
            myLogFont5.lfWeight = "700";
            myLogFont5.lfCharSet = "1";
            myLogFont5.lfQuality = "3";
            myLogFont5.lfPitchAndFamily = "32";
            myLogFont5.lfFaceName = "Arial";
            myItem5.LogFont = myLogFont5;

            Regions myRegions5 = new Regions();
            myRegions5.Items = new List<Item>();
            myRegions5.Rect = myRect5;
            myRegions5.type = "1";
            myRegions5.Layer = 3;
            myRegions5.Items.Add(myItem5);

            myPage.Regions.Add(myRegions5);
            #endregion

            #region region 6
            Rect myRect6 = new Rect();//replace the previous myRect (Morning)
            myRect6.X = "161";
            myRect6.Y = "15";
            myRect6.Width = "175";
            myRect6.Height = "48";


            Item myItem6 = new Item();
            myItem6.Type = "5";
            myItem6.Text = "Morning";
            myItem6.TextColor = "#FF00FF00";
            myItem6.IsScroll = "0";
            LogFont myLogFont6 = new LogFont();
            myLogFont6.lfHeight = "33";
            myLogFont6.lfWeight = "700";
            myLogFont6.lfCharSet = "1";
            myLogFont6.lfQuality = "3";
            myLogFont6.lfPitchAndFamily = "32";
            myLogFont6.lfFaceName = "Arial";
            myItem6.LogFont = myLogFont6;

            Regions myRegions6 = new Regions();
            myRegions6.Items = new List<Item>();
            myRegions6.Rect = myRect6;
            myRegions6.type = "1";
            myRegions6.Layer = 3;
            myRegions6.Items.Add(myItem6);

            myPage.Regions.Add(myRegions6);
            #endregion

            #region region 7
            Rect myRect7 = new Rect();//replace the previous myRect (Morning)
            myRect7.X = "0";
            myRect7.Y = "0";
            myRect7.Width = "213";
            myRect7.Height = "28";


            Item myItem7 = new Item();
            myItem7.Type = "5";
            myItem7.Text = "Today Weather Forecast";
            myItem7.TextColor = "#F1C40F";
            myItem7.IsScroll = "0";
            LogFont myLogFont7 = new LogFont();
            myLogFont7.lfHeight = "16";
            myLogFont7.lfWeight = "700";
            myLogFont7.lfCharSet = "1";
            myLogFont7.lfQuality = "3";
            myLogFont7.lfPitchAndFamily = "32";
            myLogFont7.lfFaceName = "Lucida Sans";
            myItem7.LogFont = myLogFont7;

            Regions myRegions7 = new Regions();
            myRegions7.Items = new List<Item>();
            myRegions7.Rect = myRect7;
            myRegions7.type = "1";
            myRegions7.Layer = 3;
            myRegions7.Items.Add(myItem7);

            myPage.Regions.Add(myRegions7);
            #endregion

            return myPage;
        }
        #endregion

        /// <summary>
        /// Construct page based on type of page edited in editor
        /// 6 types of event from AliBaba:
        /// 1. accident
        /// 2. congestion
        /// 3. illegal_stop
        /// 4. person_on_lane
        /// 5. bad_weather
        /// 6. retrograde
        ///additional events for us
        /// 7. flood_detection
        /// 8. travel_time
        /// 9. parking_info
        /// </summary>
        protected enum pageType
        {
            //the six type of events
            Picture,
            Video,
            Accident,
            Congestion,
            IllegalStop,
            PersonOnLane,
            Bad_weather,
            Retrograde,
            Flood,
            TravelTime,
            ParkingInfo,
            Weather
        }
    }

    public class Utf8ReaderFromFile
    {
        private readonly byte[] s_nameUtf8 = System.Text.Encoding.UTF8.GetBytes("name");
        private ReadOnlySpan<byte> Utf8Bom => new byte[] { 0xEF, 0xBB, 0xBF };
        public void PerformRead(string fileName)
        {

            ReadOnlySpan<byte> jsonReadOnlySpan = File.ReadAllBytes(fileName);

            // Read past the UTF-8 BOM bytes if a BOM exists.
            if (jsonReadOnlySpan.StartsWith(Utf8Bom))
            {
                jsonReadOnlySpan = jsonReadOnlySpan.Slice(Utf8Bom.Length);
            }

            // Or read as UTF-16 and transcode to UTF-8 to convert to a ReadOnlySpan<byte>
            //string fileName = "Universities.json";
            //string jsonString = File.ReadAllText(fileName);
            //ReadOnlySpan<byte> jsonReadOnlySpan = Encoding.UTF8.GetBytes(jsonString);


            int count = 0;
            int total = 0;

            var reader = new Utf8JsonReader(jsonReadOnlySpan);

            while (reader.Read())
            {
                JsonTokenType tokenType = reader.TokenType;

                switch (tokenType)
                {
                    case JsonTokenType.StartObject:
                        total++;
                        break;
                    case JsonTokenType.PropertyName:
                        if (reader.ValueTextEquals(s_nameUtf8))
                        {
                            // Assume valid JSON, known schema
                            reader.Read();
                            if (reader.GetString().EndsWith("Item"))
                            {
                                count++;
                            }
                        }
                        break;
                }
            }
            Console.WriteLine($"{count} out of {total} have names that end with 'Item'");
        }
    }

    //public class VsnContents
    //{
    //    public Dictionary<string, Informations> Information { get; set; }
    //}

    //public class Informations
    //{
    //    public int Width { get; set; }
    //    public int Height { get; set; }
    //}

    //public class WeatherForecastWithPOCOs
    //{
    //    public DateTimeOffset Date { get; set; }
    //    public int TemperatureCelsius { get; set; }
    //    public string Summary { get; set; }
    //    public string SummaryField;
    //    public IList<DateTimeOffset> DatesAvailable { get; set; }
    //    public Dictionary<string, HighLowTemps> TemperatureRanges { get; set; }
    //    public string[] SummaryWords { get; set; }
    //}

    //public class HighLowTemps
    //{
    //    public int High { get; set; }
    //    public int Low { get; set; }
    //}

    //public class CommunicationMessage : Dictionary<string, object>
    //{
    //    //this "hack" exposes the "Page" as a List
    //    public List<CommunicationMessage> Pages
    //    {
    //        get
    //        {
    //            return (List<CommunicationMessage>)this["Page"];
    //        }
    //        set
    //        {
    //            this["Page"] = value;
    //        }
    //    }

    //    public CommunicationMessage()
    //    {
    //        this["Page"] = new List<CommunicationMessage>();
    //    }
    //}

    #region Class for VMS
    //this is generated from http://json2csharp.com/#
    //public class Id
    //{ 
    //public DateTime IdRand { get; set; }
    //}

    public class Information
    {
        public string Width { get; set; }
        public string Height { get; set; }
    }

    public class Rect
    {
        public string X { get; set; }
        public string Y { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string BorderWidth { get; set; }
        public string BorderColor { get; set; }
    }

    public class FileSource
    {
        public string IsRelative { get; set; }
        public string FilePath { get; set; }
    }

    public class Item
    {
        public string Type { get; set; }
        public FileSource FileSource { get; set; }
        public string TextColor { get; set; }
        public string Text { get; set; }
        public string IsScroll { get; set; }
        public string CentralAlign { get; set; }
        public string BgColor { get; set; }
        public string fontSize { get; set; }
        public LogFont LogFont { get; set; }
    }


    public class LogFont
    {
        public string lfHeight { get; set; }
        public string lfWeight { get; set; }
        public string lfQuality { get; set; }
        public string lfCharSet { get; set; }
        public string lfPitchAndFamily { get; set; }
        public string lfFaceName { get; set; }

    }

    public class Regions
    {
        public string Name { get; set; }
        public string type { get; set; }
        public int Layer { get; set; }
        public Rect Rect { get; set; }
        public List<Item> Items { get; set; }
    }

    //public class Regions
    //{        
    //    public List<Region> Region { get; set; }
    //}

    public class Page
    {
        public string Name { get; set; }
        public string AppointDuration { get; set; }
        public string LoopType { get; set; }
        public List<Regions> Regions { get; set; }
    }

    public class Program
    {
        public DateTime Id { get; set; }
        public Information Information { get; set; }
        public List<Page> Pages { get; set; }
    }

    public class Programs
    {
        public Program Program { get; set; }
    }

    public class RootObject
    {
        public Programs Programs { get; set; }
    }
    #endregion

}
