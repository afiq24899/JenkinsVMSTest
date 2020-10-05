using Lingkail.VMS.Data;
using Lingkail.VMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Lingkail.VMS.Models.Services;

namespace Lingkail.VMS.Services
{
    public class DatabaseAPIServices
    {
        private class Test { } // test nested class (private)
        public class ForIncident
        {
            private readonly SenaVMSContext _context;
            private readonly IHttpContextAccessor _httpContext;

            private DotnetHubInvoker _dotnetHubInvoker;

            public ForIncident(
                SenaVMSContext context,
                IHttpContextAccessor httpContextAccessor,
                DotnetHubInvoker dotnetHubInvoker) 
            { 
                _context = context;
                _httpContext = httpContextAccessor;
                _dotnetHubInvoker = dotnetHubInvoker;
            }

            public async Task<bool> HandleNewIncident(int incidenttypeid, string image, 
                string text, bool isfullwebpath, int boardid, string boardname)
            {

                HttpRequest requestHttp = _httpContext.HttpContext.Request;
                string thisHost = string.Format("{0}://{1}", requestHttp.Scheme, requestHttp.Host);

                /*  string strHostName = Dns.GetHostName();
                if (!isfullwebpath) image = "http://" + strHostName + ":5000/" + "sharingfolder/" + image;*/

                //Check image - full web path or just image name+ext
                if (!isfullwebpath) image = thisHost + "/sharingfolder/" + image;

                //Save new incident in the database
                Incident NewIncident = new Incident
                {
                    IncidentTypeID = incidenttypeid,
                    DateTimeReceived = DateTime.Now,
                    EventImage = image,
                    Text = text,
                    IsFullWebPath = isfullwebpath,
                    ThisBoardId = boardid,
                    ThisBoardName = boardname
                };

                _context.Add(NewIncident);
                await _context.SaveChangesAsync();

                //check if input boardid and the name matches 
                /*            var thisboard = await _context.Boards.FindAsync(boardid);
                            var isMatching = thisboard.Name.Contains(boardname);
                            if (thisboard == null || !isMatching) { return false; }
                            else 
                            { */

                //Temporary to test Alibaba incident - Accident Event-------------
                //Update alert icon to (true) and broadcast to all 
                var displayToUpdate = _context.Displays.Find(boardid);
                displayToUpdate.HasAlert = true;

                displayToUpdate.AlibabaAccidentImage = image;
                displayToUpdate.AlibabaText = text;
                displayToUpdate.AlibabaCreationDate = NewIncident.DateTimeReceived;

                _context.Update(displayToUpdate);
                await _context.SaveChangesAsync();

                await _dotnetHubInvoker.IncidentAlert(boardid, boardname);
                //Temporary to test Alibaba incident - Accident Event-------------

                return true;
                // }
            }


            private string GetIPAddress()
            {
                string strHostName = Dns.GetHostName();
                IPAddress[] iPAddresses = Dns.GetHostAddresses(strHostName);
                List<IPAddress> iP4_Addresses = new List<IPAddress>();
                List<IPAddress> iP6_Addresses = new List<IPAddress>();

                foreach (IPAddress this_iP in iPAddresses)
                {
                    if (this_iP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) //ip4
                    {
                        iP4_Addresses.Add(this_iP);
                        Debug.WriteLine("IP4: " + this_iP);
                    }
                    if (this_iP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6) //ip6
                    {
                        iP6_Addresses.Add(this_iP);
                        Debug.WriteLine("IP6: " + this_iP);
                    }
                }

                return iPAddresses.ToString();
            }
        }
    
        //To refactor >>
        public class ForParking
        {
            private readonly SenaVMSContext _context;
            public DBKL_PGIS ParkingInfos { get; set; }
            public Board Board { get; set; }

            public ForParking (SenaVMSContext context)
            {
                _context = context;
            }

            public string retrieveImageFileName(int boardID, int MallID, string Bname, string sname)
            {
                ParkingInfos = _context.ParkingInfos
                    .FirstOrDefault(m => m.sname == sname);
                return ParkingInfos.ImageFileName;
            }

            public string retrieveParking(int boardID, int MallID, string Bname, string sname)
            {
                ParkingInfos = _context.ParkingInfos
                    .FirstOrDefault(m => m.sname == sname);
                return ParkingInfos.parking;
            }

            public void updateImageFileName(int boardID, int MallID, string Bname, string sname, string ImageFileName)
            {
                ParkingInfos = _context.ParkingInfos
                   .FirstOrDefault(m => m.sname == sname);  //Gets the 'mall name' for that specific boardId


                //Save changes in the database 
                ParkingInfos.ImageFileName = ImageFileName;
                _context.Update(ParkingInfos);
                _context.SaveChanges();
            }

            public void updateParking(int boardID, int MallID, string Bname, string sname, string parking)
            {
                ParkingInfos = _context.ParkingInfos
                   .FirstOrDefault(m => m.MallID == MallID);  //Gets the 'mall name' for that specific boardId

                //Save changes in the database 
                ParkingInfos.parking = parking;
                _context.Update(ParkingInfos);
                _context.SaveChanges();
            }

        }
        public class ForTravelTime
        {
            public IWebHostEnvironment WebHostEnvironment { get; }
            private readonly SenaVMSContext _context;
            public TrafficInfo mytrafficinfo { get; set; }
            public Board Board { get; set; }

            public ForTravelTime (IWebHostEnvironment webHostEnvironment,
                SenaVMSContext context)
            {
                WebHostEnvironment = webHostEnvironment;
                _context = context;
            }

            public string retrieveTravelTime(int boardID, string eventType, string sname, string name)
            {
                mytrafficinfo = _context.TrafficInfos
                    .FirstOrDefault(m => m.PointB == name);
                return mytrafficinfo.TravelTime;
            }

            public void updateTravelTime(int destId, string eventType, string sname, string name,
                    string datetimePushed, string eta)
            {
                var mytrafficinfo = _context.TravelTimeInfos_B1s
                   //.Include(b => b.TravelTime)
                   //.AsNoTracking()
                   .FirstOrDefault(m => m.id == destId); //Times Square, Mid Valley

                //overwrite the address too
                //locations.First(x => x.BoardID == boardID).Address = address;

                //Save changes in the database
                mytrafficinfo.eta = eta;
                mytrafficinfo.datetime = datetimePushed;

                _context.Update(mytrafficinfo);
                _context.SaveChanges();
            }

        }
        public class ForVidBroadcast
        {
            public IWebHostEnvironment WebHostEnvironment { get; }
            private readonly SenaVMSContext _context;
            public Video Video { get; set; }
            public Board Board { get; set; }

            public ForVidBroadcast (IWebHostEnvironment webHostEnvironment,
               SenaVMSContext context)
            {
                WebHostEnvironment = webHostEnvironment;
                _context = context;
            }

            public void updateMessage(int MessageID, string VidType, string BoardID, string Bname, string Message)
            {
                Video = _context.Videos
                   .FirstOrDefault(m => m.MessageID == MessageID);  //Gets the 'MessageID' for that specific boardId


                //Save changes in the database 
                Video.Message = Message;
                _context.Update(Video);
                _context.SaveChanges();
            }

        }
        public class ForWeather
        {
            public IWebHostEnvironment WebHostEnvironment { get; }
            private readonly SenaVMSContext _context;
            public WeatherForecast myweatherinfo { get; set; }
            public Board Board { get; set; }

            public ForWeather(IWebHostEnvironment webHostEnvironment,
                SenaVMSContext context)
            {
                WebHostEnvironment = webHostEnvironment;
                _context = context;
            }

            public void updateWeather(int wid, string wmorning, string wafternoon,
                    string wnight, string wdate)
            {
                var myweatherinfo = _context.WeatherForecast
                   .FirstOrDefault(m => m.ID == wid); //Selayang, Kuala Lumpur..

                //Save changes in the database (update data get by endpoint to database)
                myweatherinfo.MorningCondition = wmorning;
                myweatherinfo.AfternoonCondition = wafternoon;
                myweatherinfo.NightCondition = wnight;
                myweatherinfo.Date = wdate;

                _context.Update(myweatherinfo);
                _context.SaveChanges();
            }

        }
    }
}
