using System.IO;

namespace Lingkail.VMS.Models.StaticFolders
{
    //Static folder in \wwwroot\uploads
    public static class UploadsFolder
    {
        //Folder for posting VSN (and files) via Colorlight API
        public static string TempFolderSingle = "TempFolderSingle";
        public static string TempFolderMultiple = "TempFolderMultiple";
        //Folder for saving files uploaded by client via Editor UI
        public static string TempFolderClient = "TempFolderClient";
        //History folder
        public static string History = "History";
        //CombinedHistory folder
        public static string CombineHistory= "CombineHistory";

        public static string GetUploadsSubPath(string folderName)
        {
            string uploadsPath;
            string uploadsSubfolderPath;

            uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsPath)) Directory.CreateDirectory(uploadsPath);

            uploadsSubfolderPath = Path.Combine(uploadsPath, folderName);
            if (!Directory.Exists(uploadsSubfolderPath)) Directory.CreateDirectory(uploadsSubfolderPath);

            return uploadsSubfolderPath;
        }
    }
}
