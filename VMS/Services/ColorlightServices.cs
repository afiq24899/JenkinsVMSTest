using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using RestSharp;
using System.IO;
using Microsoft.Extensions.Logging;
using Lingkail.VMS.Data;
using Lingkail.VMS.Models;
using Lingkail.VMS.Models.StaticFolders;
using Newtonsoft.Json;
using System.Linq;


namespace Lingkail.VMS.Services
{
    public class ColorlightServices
    {
        private readonly IWebHostEnvironment _webEnv;
        private readonly SenaVMSContext _context;
        private ILogger<ColorlightServices> logger;

        //Class Constructor with Parameters
        public ColorlightServices(
            IWebHostEnvironment webHostEnvironment,
            SenaVMSContext context,
            ILogger<ColorlightServices> logger
            ) 
        {
            _webEnv = webHostEnvironment;
            _context = context;
            this.logger = logger;
        }

        //Properties
        private Board ThisBoard { get; set; }
        private RestClient Client { get; set; }
        private RestRequest Request { get; set; }
        private IRestResponse Response { get; set; }
        private int NumericStatusCode { get; set; }

        //Methods
        public async Task GetAllPowerStatus()
        {
            var boards = await _context.Boards
                .Include(b => b.Display)
                .ToListAsync();

            foreach (var entity in boards)
            {
                if (entity.Display.C4_IP != "sURL") //To be removed once all boards are installed.
                {
                    //Colorlight API - powerstatus
                    //0-- Device is sleeping 1-- Device is awake
                    var client = new RestClient("http://" + entity.Display.C4_IP + "/api/powerstatus.json");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    request.AlwaysMultipartFormData = true;
                   // request.Timeout = 250; //ms
                    IRestResponse response = client.Execute(request);

                    if (response.Content == "{\n \"powerstatus\": 1\n}")
                    {
                        var displaytoUpdate = await _context.Displays.Where(b => b.BoardID == entity.ID).FirstOrDefaultAsync();
                        displaytoUpdate.IsOnline = true;
                        _context.Update(displaytoUpdate);
                        _context.SaveChanges();

                    }
                    else
                    {
                        var displaytoUpdate = await _context.Displays.Where(b => b.BoardID == entity.ID).FirstOrDefaultAsync();
                        displaytoUpdate.IsOnline = false;
                        _context.Update(displaytoUpdate);
                        _context.SaveChanges();
                    }
                }
            }
        }
        public async Task<bool> SendProgramAsync(int boardId, bool isMultiple)
        {
            ThisBoard = await _context.Boards
                .Include(b => b.Display)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == boardId);

            string c4_IPAddress = ThisBoard.Display.C4_IP;
            string vsnFilename = "board" + boardId.ToString() + ".vsn";

            Client = new RestClient("http://" + c4_IPAddress + "/api/program/" + vsnFilename);
            Client.Timeout = -1;

            Request = new RestRequest(Method.POST);
            Request.AddHeader("Content-Type", "multipart/form-data;bounda6ry=;");

            //Manage post directory and the program (+files) to be sent
            string postDirectory; 
            string defaultDirectory = Directory.GetCurrentDirectory();
            this.logger.LogDebug("Current Directory: " + defaultDirectory);
            if (!isMultiple) { postDirectory = UploadsFolder.GetUploadsSubPath(UploadsFolder.TempFolderSingle); }
            else { postDirectory = UploadsFolder.GetUploadsSubPath(UploadsFolder.TempFolderMultiple); }
            Directory.SetCurrentDirectory(postDirectory);
            this.logger.LogDebug("New Directory: " + postDirectory);
            List<string> filenameList = GetAllFilenames(postDirectory);

            int key = 0;
            foreach (var file in filenameList)
            {
                Request.AddFile("f" + key.ToString(), file);
                key++;
            }

            Response = Client.Execute(Request);
            /*string content = response.Content;
            string contentType = response.ContentType;
            IList<Parameter> headers = response.Headers;
            string errorMessage = response.ErrorMessage;
            Exception errorException = response.ErrorException;*/
            NumericStatusCode = (int)Response.StatusCode;

            //Reset default directory after request execution
            Directory.SetCurrentDirectory(defaultDirectory);
            this.logger.LogDebug("Directory is reset to " + Directory.GetCurrentDirectory());

            if (Response.IsSuccessful || Response.ResponseStatus.ToString() == "Completed")
            {
                if (NumericStatusCode == 200) { return true; }
                else { return false; }
            }
            else 
            {
                return false;
            }
        }
        public async Task<string> GetActiveProgramAsync(int boardId)
        {
            ThisBoard = await _context.Boards
                .Include(b => b.Display)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == boardId);

            string c4_IPAddress = ThisBoard.Display.C4_IP;

            Client = new RestClient("http://" + c4_IPAddress + "/api/vsns.json");
            Client.Timeout = -1;
            Request = new RestRequest(Method.GET);
            Response = Client.Execute(Request);
            NumericStatusCode = (int)Response.StatusCode;

            if (Response.IsSuccessful || Response.ResponseStatus.ToString() == "Completed")
            {
                if (NumericStatusCode == 200) 
                {
                    DeviceModel.Root deserializedObj = JsonConvert.DeserializeObject<DeviceModel.Root>(Response.Content);
                    string activevsn = deserializedObj.playing.name; //if no program playing "", else <vsn_name>.vsn
                    return activevsn;
                }
                else { return "Error"; }
                
            }
            else { return "Error"; }
        }
        public async Task DeleteProgramAsync(int boardId)
        {
            ThisBoard = await _context.Boards
                .Include(b => b.Display)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == boardId);

            string c4_IPAddress = ThisBoard.Display.C4_IP;

            Client = new RestClient("http://" + c4_IPAddress
                + "/api/vsns/sources/lan/vsns/board" + boardId.ToString() + ".vsn");
            Client.Timeout = -1;
            Request = new RestRequest(Method.DELETE);
            Response = Client.Execute(Request);
        }
        private async Task StorageManagement(int boardId) //Future Use
        {
            ThisBoard = await _context.Boards
                .Include(b => b.Display)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == boardId);

            string c4_IPAddress = ThisBoard.Display.C4_IP;

            //** Clean internet program's cache **
            /*var client = new RestClient("http://192.168.1.4/api/clrresunused");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);*/

            //** Clean program assets cache **
            /*var client = new RestClient("http://192.168.1.4/api/clrcache");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);*/

            //** Clean lan program's unused files **
            /*var client = new RestClient("http://192.168.1.4/api/clrfiles");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);*/

            //Clean all programs
            Client = new RestClient("http://" + c4_IPAddress + "/api/clrprgms");
            Client.Timeout = -1;
            Request = new RestRequest(Method.DELETE);
            Response = Client.Execute(Request);
        }
        private List<string> GetAllFilenames(string sourcePath)
        {
            List<string> filenameList = new List<string>();

            string[] sourceFileCollection = Directory.GetFiles(sourcePath);
            // Copy the files and overwrite destination files if they already exist.
            foreach (string sourceFile in sourceFileCollection)
            {
                // Use static Path methods to extract only the file name from the path.
                string fileName = Path.GetFileName(sourceFile);
                filenameList.Add(fileName);
            }
            return filenameList;
        }
    }

    public class DeviceModel 
    {
        public class Content2
        {
            public string md5 { get; set; }
            public string name { get; set; }
            public string publishedmd5 { get; set; }
            public int size { get; set; }

        }
        public class Content
        {
            public List<Content2> content { get; set; }
            public string type { get; set; }
            public int? ressize { get; set; }
            public int? unused { get; set; }

        }
        public class Playing
        {
            public string name { get; set; }
            public string type { get; set; }

        }
        public class Root
        {
            public List<Content> contents { get; set; }
            public Playing playing { get; set; }

        }
    }
}
