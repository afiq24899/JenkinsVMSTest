using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lingkail.Apilibraries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Lingkail.VMS.Data;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using Lingkail.VMS.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lingkail.VMS.Controllers
{
    [Route("api/upload")]
    public class ValuesController : Controller
    { 
        private readonly SenaVMSContext _context;
        private readonly IHostEnvironment _hostingEnvironment;
        public IWebHostEnvironment _webHostEnvironment { get; set; }

        private FileManagement _fileManagement;

        public ValuesController(
            IHostEnvironment hostingEnvironment, 
            IWebHostEnvironment webHostEnvironment,
            SenaVMSContext context,
            FileManagement fileManagement
            )
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _webHostEnvironment = webHostEnvironment;
            _fileManagement = fileManagement;
        }

        public class PostStringModel
        {
            public string Value { get; set; } /*BACH UploadWithBase64 route-base64*/
            public int BoardId { get; set; }
            public string remark_board { get; set; }
            public string BoardName { get; set; }
        }

        #region BACH UploadWithBase64 route-base64
        [AllowAnonymous] 
        [HttpPost("base64")]
        //When 'save canvas' is clicked ...
        public IActionResult UploadWithBase64([FromBody] PostStringModel model)
        {
            //Go to UploadUtils Instance() in UploadUtils.cs 
            var imageUtils = UploadUtils.Instance(_hostingEnvironment); //Create hosting env with the rootpath,etc.
            //Go to Save() method in UploadUtils.cs 
            var relativePath = imageUtils.Save(model.Value); //Relative path (wwwroot) of the image is returned

            return Ok(new
            {
                relativePath
            });
        }
        #endregion

        [AllowAnonymous]
        [HttpPost("remarkupdate")]
        //When 'save canvas' is clicked ...
        public async Task UpdateRemarkBoard([FromBody] PostStringModel model)
        {
            var year = DateTime.Now.ToString("yyyy");
            var month = DateTime.Now.ToString("MM");
            var day = DateTime.Now.ToString("dd");
            var date_fullform = DateTime.Now.ToString("dd-MM-yyy");
            foreach (var item in _context.ReportData.Where(x => x.Boards == model.BoardName && 
                                                            x.Year == year &&
                                                            x.Months == month &&
                                                            x.Days == day).ToListAsync().Result)
            {
                item.Remark = model.remark_board;
                await _context.SaveChangesAsync();
            }
            foreach (var item in _context.UptimeReports.Where(x => x.Name == model.BoardName &&
                                                           x.Date == date_fullform).ToListAsync().Result)
            {
                item.Remark = model.remark_board;
                await _context.SaveChangesAsync();
            }

        }

        [HttpPost("")]
        [Route("cctv")]
        public ActionResult<string> Get([FromBody] PostStringModel myData)
        {
            using (var process = new Process())
            {
                //var external_exe = @"C:\CCTV_VMS\CCTV.exe";
                var external_exe = @"..\CCTV\bin\Release\netcoreapp3.1\CCTV.exe";
                //Debug.WriteLine(Directory.GetCurrentDirectory());
                process.StartInfo.FileName = external_exe;
                process.StartInfo.Arguments = $"{myData.BoardId}";
                
                //Debug.WriteLine(processExists);
                //check if this board id contans cctv, determine from database
                //temporary, only BoardId==1 contains CCTV
                if(myData.BoardId==77)
                {
                    process.StartInfo.Arguments = String.Concat(myData.BoardId, " /0");
                }
                else
                {
                    process.StartInfo.Arguments = String.Concat(myData.BoardId, " /1");
                    //start the windows form 
                }
                var processExists = Process.GetProcesses().Any(p => p.ProcessName.Contains("CCTV"));
                if (processExists == false)
                {
                    Debug.WriteLine("RUNNING CCTV FORM");
                    process.Start();
                }
                else
                {
                    //if there is prior cctv open, it will open another new window
                    process.Start();
                }
                //Console.WriteLine("starting");
            }
            return "value";
        }
    }
}
