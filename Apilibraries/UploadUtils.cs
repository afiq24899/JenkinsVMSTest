using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Lingkail.Apilibraries
{
    public class UploadUtils
    {
        private static readonly string[] Extensions = { ".jpeg", ".jpg", ".png", "mp4", "rar", "zip", "txt", "xlsx", "xlsm", "xlsb", "xltx", "xltm", "xls", "xlt", "xls", "xml", "pdf", "xlam", "csv" };
        private static readonly string[] ExtensionsImage = { ".jpeg", ".jpg", ".png" };
        private const long FileSize = 20000000;
        private const string UploadFolder = "wwwroot/uploads";
        private const string DefaultExtension = "jpg";

        //In ASP.NET Core, the physical paths to both the web root and the content root directories can be retrieved by 
        //injecting and querying the IHostEnvironment service
        //reference: https://stackoverflow.com/questions/56299355/how-to-combine-path-with-webrootpath
        private readonly IHostEnvironment _hostingEnvironment;
        private static UploadUtils _itself;
        private static readonly object _lock = new object();

        private UploadUtils(IHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public static UploadUtils Instance(IHostEnvironment hostingEnvironment)
        {
            if (_itself == null)
            {
                lock (_lock)
                {
                    if (_itself == null)
                    {
                        _itself = new UploadUtils(hostingEnvironment);
                    }
                }
            }

            return _itself;
        }

        internal object Save(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public string Save(string base64)
        {
            try
            {
                lock (_lock)
                {
                    var fileName = "";
                    var boardID = "";
                    string[] boardArray = null;

                    if (base64[0] == '1')
                    {
                        fileName = $"message1.{DefaultExtension}";
                        base64 = base64.Remove(0, 1);
                        boardID = base64.Split('-')[0];
                        if (boardID.Contains(","))
                        {
                            boardArray = boardID.Split(',');
                        }
                        //var newfilename = base64.Split('-')[0];
                        //fileName = $"{newfilename}_message1.{DefaultExtension}";
                        base64 = base64.Split('-')[1];
                    }
                    else if (base64[0] == '2')
                    {
                        fileName = $"message2.{DefaultExtension}";
                        base64 = base64.Remove(0, 1);
                        boardID = base64.Split('-')[0];
                        if (boardID.Contains(","))
                        {
                            boardArray = boardID.Split(',');
                        }
                        base64 = base64.Split('-')[1];
                    }
                    else if (base64[0] == '3')
                    {
                        fileName = $"message3.{DefaultExtension}";
                        base64 = base64.Remove(0, 1);
                        boardID = base64.Split('-')[0];
                        if (boardID.Contains(","))
                        {
                            boardArray = boardID.Split(',');
                        }
                        base64 = base64.Split('-')[1];
                    }
                    else if (base64[0] == '4')
                    {
                        fileName = $"message4.{DefaultExtension}";
                        base64 = base64.Remove(0, 1);
                        boardID = base64.Split('-')[0];
                        if (boardID.Contains(","))
                        {
                            boardArray = boardID.Split(',');
                        }
                        base64 = base64.Split('-')[1];
                    }
                    else if (base64[0] == '5')
                    {
                        fileName = $"message5.{DefaultExtension}";
                        base64 = base64.Remove(0, 1);
                        boardID = base64.Split('-')[0];
                        if (boardID.Contains(","))
                        {
                            boardArray = boardID.Split(',');
                        }
                        base64 = base64.Split('-')[1];
                    }
                    else
                    {
                        fileName = "noname";
                    }

                    var uploadFolder = "";
                    var subFolderPath = "";
                    var fileAbsolutePath = "";
                    var relativePath = "";
                    byte[] imageBytes;

                    //Folder path name 
                    uploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, UploadFolder);
                    //Create the path (directory) with the folder path name
                    if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);


                    if (boardArray != null) //If multiple Ids present 
                    {
                        foreach (var item in boardArray)
                        {
                            //Sub folder path name
                            subFolderPath = Path.Combine(uploadFolder, item);
                            //Create the path (directory) with the sub folder path name
                            if (!Directory.Exists(subFolderPath)) Directory.CreateDirectory(subFolderPath);
                            //File (i.e. image) path name - absolute path
                            fileAbsolutePath = Path.Combine(subFolderPath, fileName);

                            imageBytes = Convert.FromBase64String(base64);
                            File.WriteAllBytes(fileAbsolutePath, imageBytes);
                        }
                    }
                    else
                    {
                        //Sub folder path name
                        subFolderPath = Path.Combine(uploadFolder, boardID);
                        //Create the path (directory) with the sub folder path name
                        if (!Directory.Exists(subFolderPath)) Directory.CreateDirectory(subFolderPath);
                        //File (i.e. image) path name - absolute path
                        fileAbsolutePath = Path.Combine(subFolderPath, fileName);

                        imageBytes = Convert.FromBase64String(base64);
                        File.WriteAllBytes(fileAbsolutePath, imageBytes);
                    }
                                                         
                    //File (i.e. image) path name - relative (wwwroot) path
                    relativePath = $"{UploadFolder}/{subFolderPath}/{fileName}";


                    return relativePath;
                } 
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
