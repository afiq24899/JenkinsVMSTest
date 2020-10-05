using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lingkail.VMS.Models;
using Lingkail.VMS.Data;
using Microsoft.EntityFrameworkCore;
using Lingkail.VMS.Models.StaticFolders;

namespace Lingkail.VMS.Pages.EditorV2.IFrameElement
{
    public class UploadModel : PageModel
    {
        public IWebHostEnvironment _webHostEnvironment { get; set; }
        private readonly SenaVMSContext _context;

        public UploadModel(
            IWebHostEnvironment webHostEnvironment,
            SenaVMSContext context
            )
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        [BindProperty] public Board Board { get; set; }
        [BindProperty] public IFormFile imgFile { get; set; }
        [BindProperty] public IFormFile vidFile { get; set; }

        [TempData] public string alertMessage { get; set; }
        public bool ShowMessage => !string.IsNullOrEmpty(alertMessage);

        public async Task OnGetAsync(int? id)
        {
            alertMessage = null;

            Board = await _context.Boards
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id); //Get board id from parent page (index) to this child page 
                                                       //Using string query from iframe - <iframe src="/EditorV2/Upload?id=x"></iframe>
        }

        public IActionResult OnPost()
        {
            if (imgFile != null && vidFile == null) {
                // Extract file name from whatever was posted by browser
                var imgfileName = Path.GetFileName(imgFile.FileName); //e.g. img.png
                UploadFile(imgfileName, imgFile);
                alertMessage = "Successfully Uploaded!";
            }
            if (vidFile != null && imgFile == null) {
                // Extract file name from whatever was posted by browser
                var vidfileName = System.IO.Path.GetFileName(vidFile.FileName); //e.g. vid.mp4
                UploadFile(vidfileName, vidFile);
                alertMessage = "Successfully Uploaded!";
            }
            return Page();
        }

        public void UploadFile(string fileName, IFormFile file)
        {
            string uploadFolderPath;
            string filePath;

            uploadFolderPath = UploadsFolder.GetUploadsSubPath(UploadsFolder.TempFolderClient);
            filePath = Path.Combine(uploadFolderPath, fileName);

            // If file with same name exists delete it
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }

            // Create new local file and copy contents of uploaded file
            using (var localFile = System.IO.File.OpenWrite(filePath))
            using (var uploadedFile = file.OpenReadStream())
            {
                uploadedFile.CopyTo(localFile);
            }
        }

    }
}