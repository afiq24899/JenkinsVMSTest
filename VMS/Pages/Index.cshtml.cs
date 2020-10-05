using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lingkail.VMS.Auth.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore; //Use EF Core
using Microsoft.AspNetCore.Authorization;
using Lingkail.VMS.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Runtime.InteropServices;
//----------------------------------------------------------
using Microsoft.Extensions.Configuration;

using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using System.Linq;
using Lingkail.VMS.Data;

namespace Lingkail.VMS.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public string MapboxAccessToken { get; }

        public IList<Board> Boards { get; set; }

        public IList<History> Histories { get; set; }
        public IList<History> HistoriesBackSlash { get; set; }//this keep track of just the sourcefoldername
        public List<History> HistoryLatest { get; set; } //this keep only the latest history of a particular board


        public IWebHostEnvironment _webHostEnvironment { get; set; }

        private readonly Lingkail.VMS.Data.SenaVMSContext _context;
        public IndexModel(Lingkail.VMS.Data.SenaVMSContext context, IConfiguration configuration,
            UserManager<VmsUser> userManager,
            SignInManager<VmsUser> signInManager,
            IEmailSender emailSender,
            IWebHostEnvironment webHostEnvironment)
        {            
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            MapboxAccessToken = configuration["Mapbox:AccessToken"];

            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        /*        public IList<Board> Boards { get; set; }

                public async Task OnGetAsync()
                {
                    Boards = await _context.Boards
                        .Include(b => b.Location)
                        .AsNoTracking()
                        .ToListAsync();
                }*/
        // ================= Login =================================
        private readonly UserManager<VmsUser> _userManager;
        private readonly SignInManager<VmsUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public bool IsTwoFactorEnabled { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        //public void onGet()
        //{
        //    ViewData["boardName"] = "test";
        //}

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                //if first time login or nobody login, will redirect to login page
                //return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                return RedirectToPage("/login");         
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Email = email
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            IsTwoFactorEnabled = user.TwoFactorEnabled;

            //retrieve boards. include necessary tables
            IQueryable<Board> boardsIQ = from b in _context.Boards
                               .Include(b => b.Zone)
                               .Include(b => b.Location)
                               .Include(b => b.Display) 
                                   .ThenInclude(d => d.MessageAssignments)
                                         select b;
            boardsIQ = boardsIQ.OrderBy(b => b.Name); //Sort Name in ascending order

            Boards = await boardsIQ.AsNoTracking().ToListAsync();
            //When an IQueryable is created or modified, no query is sent to the database. 
            //The IQueryable code results in a single query that's not executed until the LAST statement.

            //var results = collection
            //   .Where(item => item.PID == 0 || item.PID == 1)
            //   .GroupBy(item => item.Name)
            //   .Select(g => g.OrderBy(item => item.PID).First());

            var queryLatestHistory = _context.Histories.GroupBy(name => name.H_Name)
                .Select(group =>
                        new
                        {
                            //H_Name = group.Key,
                            H_NowDateTime = group.OrderByDescending(x => x.H_NowDateTime)
                        })
                 .OrderBy(group => group.H_NowDateTime.First().Object);


            //history - added AF
            IQueryable<History> historiesIQ = from h in _context.Histories
                                              //.GroupBy(b=>b.H_Name)
                                              .OrderByDescending(b => b.H_NowDateTime) //Sort by last edited                                            
                                              select h;
            //historiesIQ = historiesIQ.OrderByDescending(b => b.H_NowDateTime); //Sort by last edited

            //should create function to return h based on selected boardId in ReturnLatestHistoryPath

            Histories = await historiesIQ.AsNoTracking().ToListAsync();
            HistoriesBackSlash = await historiesIQ.AsNoTracking().ToListAsync();

            for(int i=0;i< HistoriesBackSlash.Count;i++)
            {
                string sourcePath = HistoriesBackSlash[i].Object; //if can replaced this with jsonmarkers of history.object
                string sourceFolderName = sourcePath.Split('\\').Last();
                HistoriesBackSlash[i].Object = sourceFolderName;

            }

            //create a List to store only latest history per board
            HistoryLatest = new List<History>();
            HistoryLatest.Clear();//clear all elements for list, this list will use to store only one latest record per board

            if (Histories.Count > 0)
            {
                //store the first element
                History firstElement = new History();
                //firstElement.Object = Histories[0].Object.Split('\\').Last();
                firstElement.Object = Histories[0].Object;
                firstElement.H_Address = Histories[0].H_Address;
                firstElement.H_Name = Histories[0].H_Name;
                firstElement.H_NowDateTime = Histories[0].H_NowDateTime;
                HistoryLatest.Add(firstElement);

                for (int i = 1; i < Histories.Count; i++)
                {
                    bool uniqueFlag = true;
                    for (int i2 = 0; i2 < HistoryLatest.Count; i2++)
                    {
                        if (Histories[i].H_Name == HistoryLatest[i2].H_Name)
                        {
                            uniqueFlag = false; //this board has been added previously
                        }
                    }
                    //if board is unique we can add it
                    if (uniqueFlag)
                    {
                        History a = new History();
                        //a.Object = Histories[i].Object.Split('\\').Last();
                        a.Object = Histories[i].Object;
                        a.H_Address = Histories[i].H_Address;
                        a.H_Name = Histories[i].H_Name;
                        a.H_NowDateTime = Histories[i].H_NowDateTime;
                        HistoryLatest.Add(a);
                    }
                }
            }

            //HistoryLatest = new List<History>(from rec in Histories
            //                                  //where rec.H_Name == "V001"
            //                                  select new History
            //                                  {
            //                                      H_Name = rec.H_Name,
            //                                      H_NowDateTime = rec.H_NowDateTime,
            //                                      //Object = rec.Object //this will cause map not able to show
            //                                      Object = rec.Object.Split('\\').Last()
            //                                  });

            return Page();
        }

        public class myHistory
        {
            public string BoardId { get; set; }
            public DateTime Date { get; set; }
            public string folder { get; set; }
        }

        public  IActionResult OnGetVMS()
        {
            {

                FeatureCollection featureCollection = new FeatureCollection();

                //-----Hardcoded name, long, lat for now --------
                for (int i=0; i<2; i++) { 

                string name = "Board Name";
                double longitude = 101.6986777;
                double latitude = 3.170816062;

                featureCollection.Features.Add(new Feature(
                    new Point(new Position(longitude,latitude)),
                    new Dictionary<string, object>
                    {
                    {"name", name}
                    }));

                }

                JsonResult myjasonresult = new JsonResult(featureCollection);
                return myjasonresult;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }

                // In this application, the email and the username are the same, so when we update the email we need to update the user name.
                //TODO: what if setting the email succeeds but the username fails? keep track of this issue -> https://github.com/aspnet/AspNetCore/issues/6613
                var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.Email);
                if (!setUserNameResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting name for user with ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }

        //public string getImagefilepath(string historySourceFolder,string fileName)
        //{
        //    bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        //    bool isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        //    string uploadFolderPath;
        //    string fullfilePath;

        //    if (isWindows)
        //    {
        //        // Change file upload path to wwwroot -> uploads -> <board ID> 
        //        //uploadFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/History", historySourceFolder);
        //        fullfilePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads\\History", historySourceFolder, fileName);

        //        //Create the path if it does not exist
        //        //if (!Directory.Exists(uploadFolderPath)) Directory.CreateDirectory(uploadFolderPath);
        //    }
        //    else if (isLinux)
        //    {
        //        //##PATH FOR LINUX##
        //        uploadFolderPath = "";
        //        fullfilePath = "";
        //    }
        //    else
        //    {
        //        fullfilePath = "";
        //        throw new PlatformNotSupportedException("Not supported OS platform!!");                
        //    }

        //    return fullfilePath;
        //}

    }
}
