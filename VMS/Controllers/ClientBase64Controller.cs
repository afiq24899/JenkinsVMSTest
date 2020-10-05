using System;
using System.IO;
using Lingkail.VMS.Models.StaticFolders;
using Lingkail.VMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Lingkail.VMS.Controllers
{
    [Route("clientBase64")]
    public class ClientBase64Controller : Controller
    {
        public IWebHostEnvironment _webHostEnvironment { get; set; }
        private FileManagement _fileManagement;

        public ClientBase64Controller(
            IWebHostEnvironment webHostEnvironment,
            FileManagement fileManagement
            )
        {
            _webHostEnvironment = webHostEnvironment;
            _fileManagement = fileManagement;
        }

        public class PostModel
        {
            public string Message1 { get; set; }
            public string Message2 { get; set; }
            public string Message3 { get; set; }
            public string Message4 { get; set; }
            public string Message5 { get; set; }
            public int BoardId { get; set; }
        }

        [AllowAnonymous]
        [HttpPost("convertBase64")]
        public void ImageDataBase64([FromBody] PostModel clientInput)
        {
            string[] imgDataArray = {
                clientInput.Message1,
                clientInput.Message2,
                clientInput.Message3,
                clientInput.Message4,
                clientInput.Message5
            };

            const string fileExtension = ".jpg";  //Image file type 
            int thisBoard = clientInput.BoardId;

            string folderPath;
            if (thisBoard != 0) //Single id
            {
                folderPath = UploadsFolder.GetUploadsSubPath(UploadsFolder.TempFolderSingle);
                if (Directory.Exists(folderPath)) Directory.Delete(folderPath, true); //delete old folder
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath); //create new folder

                var sourcePathToCopy = UploadsFolder.GetUploadsSubPath(thisBoard.ToString());//board folder
                if (Directory.Exists(sourcePathToCopy)) _fileManagement.CopyFiles(sourcePathToCopy, folderPath); //make a copy from board folder to temp folder
                Directory.Delete(sourcePathToCopy, true); //delete board folder (to create a new one from scratch)
            }
            else //0 = Multiple ids
            {
                folderPath = UploadsFolder.GetUploadsSubPath(UploadsFolder.TempFolderMultiple);
                if (Directory.Exists(folderPath)) Directory.Delete(folderPath, true); //delete old folder
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath); //create new folder

                var sourcePathToCopy = UploadsFolder.GetUploadsSubPath(thisBoard.ToString());
                if (Directory.Exists(sourcePathToCopy)) _fileManagement.CopyFiles(sourcePathToCopy, folderPath);
            }

            //Save messages 1,2,3,4,5 - convert client base64 back to image, save in server
            int count = 1;
            foreach (var item in imgDataArray)
            {
                string filename = "message" + count.ToString() + fileExtension; //Image filename to be saved
                string absolutePath = Path.Combine(folderPath, filename);

                byte[] imageBytes = Convert.FromBase64String(item);
                System.IO.File.WriteAllBytes(absolutePath, imageBytes);

                count++;
            }

            _fileManagement.CopyClientUploadedFiles(folderPath);
            _fileManagement.CopyParkingLogos(folderPath);

            string clientTempPath = UploadsFolder.GetUploadsSubPath(UploadsFolder.TempFolderClient);
            if (Directory.Exists(clientTempPath)) Directory.Delete(clientTempPath, true); //delete TempFolderClient, subfolders, files
        }
    }
}