using Lingkail.VMS.Models.StaticFolders;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Lingkail.VMS.Services
{
    public class FileManagement
    {
        public IWebHostEnvironment _webHostEnvironment { get; set; }

        public FileManagement(
            IWebHostEnvironment webHostEnvironment
            )
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void CopyFromTempFolderSingle(int? boardId)
        {
            var sourcePath = UploadsFolder.GetUploadsSubPath(UploadsFolder.TempFolderSingle);

            var destPath = UploadsFolder.GetUploadsSubPath(boardId.ToString());
            if (!Directory.Exists(destPath)) Directory.CreateDirectory(destPath);

            CopyFiles(sourcePath, destPath);
        }
        public void CopyFromTempFolderMultiple(int? boardId)
        {
            var sourcePath = UploadsFolder.GetUploadsSubPath(UploadsFolder.TempFolderMultiple);

            var destPath = UploadsFolder.GetUploadsSubPath(boardId.ToString());
            if (!Directory.Exists(destPath)) Directory.CreateDirectory(destPath);

            CopyFiles(sourcePath, destPath);

            //Delete unrelated vsn
            DirectoryInfo thisDirectory = new DirectoryInfo(destPath);
            FileInfo[] vsnFiles = thisDirectory.GetFiles("board*.vsn"); //Files start with 'board'
            foreach (var vsn in vsnFiles)
            {
                string vsnToKeep = "board" + boardId.ToString() + ".vsn";
                if (vsn.Name != vsnToKeep)
                {
                    vsn.Delete();
                }
            }
        }
        public void CopyParkingLogos(string destPath)
        {
            var sourcePath = Path.Combine(_webHostEnvironment.WebRootPath, "ParkingLogo"); ;
            CopyFiles(sourcePath, destPath);
        }
        public void CopyClientUploadedFiles(string destPath)
        {
            var sourcePath = UploadsFolder.GetUploadsSubPath(UploadsFolder.TempFolderClient);
            if (!Directory.Exists(sourcePath)) Directory.CreateDirectory(sourcePath);
            CopyFiles(sourcePath, destPath);
        }
        public void CopyFiles(string sourcePath, string destPath)
        {
            if (!Directory.Exists(destPath)) Directory.CreateDirectory(destPath);

            string[] sourceFileCollection = Directory.GetFiles(sourcePath);

            // Copy the files and overwrite destination files if they already exist.
            foreach (string sourceFile in sourceFileCollection)
            {
                // Use static Path methods to extract only the file name from the path.
                string fileName = Path.GetFileName(sourceFile);
                string destFile = Path.Combine(destPath, fileName);
                File.Copy(sourceFile, destFile, true);
            }
        }
    }
}