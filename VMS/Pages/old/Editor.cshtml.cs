using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; //Use EF Core
using Lingkail.VMS.Models;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using RestSharp;//http API
using Lingkail.VMS.Auth.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace Lingkail.VMS.Pages.VMS
{
    [Authorize]
    public class EditorModel : PageModel
    {
        private readonly UserManager<VmsUser> _userManager;
        private readonly SignInManager<VmsUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly Lingkail.VMS.Data.SenaVMSContext _context;
        public IWebHostEnvironment WebHostEnvironment { get; set; }
        private bool isWindows;
        private bool isLinux;
        ILogger<EditorModel> logger;

        /*In ASP.NET Core, the physical paths to both the content root 
         * and the web root directories can be retrieved via the IWebHostEnvironment 
         * service. Here's an example of a model that uses constructor 
         * dependency injection to get an IWebHostEnvironment*/
        public EditorModel(
            UserManager<VmsUser> userManager,
            SignInManager<VmsUser> signInManager,
            IEmailSender emailSender,
            Lingkail.VMS.Data.SenaVMSContext context,
            IWebHostEnvironment webHostEnvironment,
            ILogger<EditorModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
            WebHostEnvironment = webHostEnvironment;//look at explanation above constructor 
            this.logger = logger;
        }

        //Upon successful editing and saving, message will pop up to notify- 'please check history'
        [TempData]
        public string Message { get; set; }


        [BindProperty]
        public Board Board { get; set; }
        [BindProperty]
        public Location Location { get; set; }
        [BindProperty]
        public Display Display { get; set; }
        [BindProperty]
        public History History { get; set; }
        [BindProperty]
        public EditorMessage EditorMessage { get; set; }



        /*Parking Template
         1. Board   2. Parking Info*/
        public List<DBKL_PGIS> ParkingInfos { get; set; }
        public string[] sname;
        public string[] bay;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ParkingInfos = _context.ParkingInfos
                .AsNoTracking()
                .ToList();
                
            //Get the values(parking bay) respective to the name
                sname = new string[15];
                bay = new string[15];
                int i = 0;

                foreach (var item in ParkingInfos)
                {
                    sname[i] = item.sname;
                    bay[i] = item.parking;
                    i++;
                }
            //End----------------------

            if (id == null)
            {
                return NotFound();
            }

            Board = await _context.Boards
                .Include(b => b.Display)
                .Include(b => b.Location)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Board == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Board = await _context.Boards
                .Include(b => b.Display)
                .Include(b => b.Location)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            var displayToUpdate = await _context.Displays.FindAsync(id);

            var user = await _userManager.GetUserAsync(User);
            var userName = await _userManager.GetUserNameAsync(user);

            //Try updating with posted values
            if (await TryUpdateModelAsync<Display>(
                displayToUpdate,
                "display",   // Prefix for form value. (Bind with Display)
                s => s.Screenshot_URL)) 
            {
                await _context.SaveChangesAsync();
            }

            //Save in History
            _context.Histories.Add( //Add
                new History
                {
                    H_Name = Board.Name,
                    H_Address = Board.Location.Address,
                    H_NowDateTime = DateTime.Now,
                    H_User = userName
                });
            await _context.SaveChangesAsync();
            Message = "Successfully Edited. Please check history.";

            return RedirectToPage("/VMS/Table");
        }

        /*
         this section is for testing to do API call via colourlight 1.6.3 to manage 
         VMS board via C4
         */
        /// <summary>
        /// this can only be run on development pc with both backend and frontend on same pc
        /// when in actual deployment ajax call is necessary to communicate across thread
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public async Task<IActionResult> OnPostTestAddVSNAsync(int? id)
        //{

        //    Board = await _context.Boards
        //        .Include(b => b.Display)
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(m => m.ID == id);

        //    string c4_ip = Board.Display.C4_IP;
        //    string vsnFilename = "board" + id.ToString() + ".vsn";//the name of vsn file to be uploaded

        //    //replace with your own file path, below use an image in wwwroot for example

        //    string mypath = "empty";//initialize

        //    //var client = new RestClient("http://" + c4_ip + "/api/program/" + vsnFilename);
        //    var client = new RestClient("http://10.20.3.200/api/program/board1.vsn");
        //    client.Timeout = -1;

        //    isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        //    isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        //    var request = new RestRequest(Method.POST);
        //    //request.AddHeader("Content-Type", "multipart/form-data");
        //    //request.AddHeader("Content-Type", "multipart/form-data; boundary=--------------------------419717524527763218296724");

        //    string subfolder = "";

        //    if (isWindows)
        //    {
        //        subfolder = "uploads\\";

        //    }

        //    else if (isLinux)
        //    {
        //        subfolder = "uploads/";
        //    }

        //    else
        //    {
        //        throw new PlatformNotSupportedException("Not supported OS platform!!");
        //    }

        //    //var request = new RestRequest(Method.POST);
        //    request.AddHeader("Content-Type", "multipart/form-data;boundary=;");
        //    request.AddFile("f0", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/board1.vsn");
        //    request.AddFile("f1", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/message1.jpg");
        //    request.AddFile("f2", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/message2.jpg");
        //    request.AddFile("f3", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/message3.jpg");
        //    request.AddFile("f4", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/message4.jpg");
        //    request.AddFile("f5", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/message5.jpg");
        //    IRestResponse response = client.Execute(request);

        //    return RedirectToPage("/VMS/Table");
        //}

        /// <summary>
        /// this can only be run on development pc with both backend and frontend on same pc
        /// when in actual deployment ajax call is necessary to communicate across thread
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostTestAddVSNAsync(int? id)
        {
            //ref: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-3.1

            Board = await _context.Boards
                .Include(b => b.Display)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            string c4_ip = Board.Display.C4_IP;
            string vsnFilename = "board" + id.ToString() + ".vsn";//the name of vsn file to be uploaded

            RestClient client = new RestClient("http://" + c4_ip + "/api/program/" + vsnFilename);
            client.Timeout = -1;

            isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            
            //string subfolder = "";
            string target = @"C:\Users\SenaTraffic\source\repos\LKL\VMS\wwwroot\uploads\1";
            //string testTarget = "";

            if (isWindows)
            {
                //subfolder = "uploads\\";
                //testTarget = @"C:\Users\SenaTraffic\source\repos\LKL\VMS\wwwroot\uploads\1";
                target = string.Concat(WebHostEnvironment.WebRootPath+"\\uploads\\", id.ToString());

            }

            else if (isLinux)
            {
                //subfolder = "uploads/";
                target = string.Concat(WebHostEnvironment.WebRootPath + "/uploads/", id.ToString());
            }

            else
            {
                throw new PlatformNotSupportedException("Not supported OS platform!!");
            }

            
            RestRequest request = new RestRequest(Method.POST);
            string currentpath = Directory.GetCurrentDirectory();
            this.logger.LogDebug("OnPostTestAddVSNAsync current path: " + currentpath);
            Directory.SetCurrentDirectory(target);
            this.logger.LogDebug("directory is set to " + target);
            request.AddHeader("Content-Type", "multipart/form-data;bounda6ry=;");
            request.AddFile("f0", "board1.vsn");
            request.AddFile("f1", "video1.mp4");
            request.AddFile("f2", "message2.jpg");
            request.AddFile("f3", "eta_car.png");
            request.AddFile("f4", "message4.jpg");
            request.AddFile("f5", "accident_1.png");
            request.AddFile("f6", "accident_2.png");


            //request.AddHeader("Content-Type", "multipart/form-data;boundary=;");
            //mypath = Path.Combine(WebHostEnvironment.WebRootPath, subfolder, id.ToString(), vsnFilename);//need escpae character double slash
            //request.AddFile("f0", mypath);
            //mypath = Path.Combine(WebHostEnvironment.WebRootPath, subfolder, id.ToString(), "message1.jpg");
            //request.AddFile("f1", mypath);
            //mypath = Path.Combine(WebHostEnvironment.WebRootPath, subfolder, id.ToString(), "message2.jpg");
            //request.AddFile("f2", mypath);
            //mypath = Path.Combine(WebHostEnvironment.WebRootPath, subfolder, id.ToString(), "message3.jpg");
            //request.AddFile("f3", mypath);
            //mypath = Path.Combine(WebHostEnvironment.WebRootPath, subfolder, id.ToString(), "message4.jpg");
            //request.AddFile("f4", mypath);
            //mypath = Path.Combine(WebHostEnvironment.WebRootPath, subfolder, id.ToString(), "message5.jpg");
            //request.AddFile("f5", mypath);

            //request.AddHeader("Content-Type", "multipart/form-data;bounda6ry=;");
            //request.AddFile("f0", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/board1.vsn");
            //request.AddFile("f1", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/message1.jpg");
            //request.AddFile("f2", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/message2.jpg");
            //request.AddFile("f3", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/message3.jpg");
            //request.AddFile("f4", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/message4.jpg");
            //request.AddFile("f5", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/message5.jpg");

            IRestResponse response = client.Execute(request);
            System.Net.HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;
          //  Assert.AreEqual(400, StatusCode, “Status code is not 200”);
            string statusDescription = response.StatusDescription;
            string theexception=response.ErrorMessage;
            string theresponse= response.Content;
            string protocolVersion = response.ProtocolVersion.ToString();

            Directory.SetCurrentDirectory(currentpath);
            string latestcurrentpath = Directory.GetCurrentDirectory();
            this.logger.LogDebug("directory is reset to " + latestcurrentpath);
            return RedirectToPage("/VMS/Table");
        }

        public void createRequest()
        {
            var client = new RestClient("http://10.20.3.200/api/program/board1.vsn");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data;boundary=;");
            request.AddFile("f0", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/board1.vsn");
            request.AddFile("f1", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/message1.jpg");
            request.AddFile("f2", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/message2.jpg");
            request.AddFile("f3", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/message3.jpg");
            request.AddFile("f4", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/message4.jpg");
            request.AddFile("f5", "/C:/Users/SenaTraffic/source/repos/LKL/VMS/wwwroot/uploads/1/message5.jpg");
            IRestResponse response = client.Execute(request);
        }

        public async Task<IActionResult> OnPostTestDeleteVSNAsync(int? id)
        {
            Board = await _context.Boards
                .Include(b => b.Display)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            var c4_ip = Board.Display.C4_IP;
            string vsnFilename = "board1.vsn";//the name of vsn file to be deleted

            var client = new RestClient("http://" + c4_ip + "/api/vsns/sources/lan/vsns/" + vsnFilename);
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Content-Type", "text/plain");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            return RedirectToPage("/VMS/Table");
        }

        public async Task<IActionResult> OnPostScreenshotAsync(int? id)
        {
            Board = await _context.Boards
                .Include(b => b.Display)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            var c4_ip = Board.Display.C4_IP;

            var client = new RestClient("http://" + c4_ip + "/api/screenshot");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            return Page();
        }

    }
}

