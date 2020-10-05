using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Lingkail.VMS.Data;
using Lingkail.VMS.Models.StaticFolders;
using System.IO;
using System;

//Note temporary to test eta, weather and parking

namespace Lingkail.VMS.Services
{
    public class DynamicDataService
    {
        private IWebHostEnvironment _hostingEnvironment;
        public DatabaseAPIServices.ForTravelTime traveltimeService;
        public DatabaseAPIServices.ForParking parkingService;
        public DatabaseAPIServices.ForVidBroadcast videoService;
        public DatabaseAPIServices.ForWeather weatherService;
        private readonly SenaVMSContext _context;

        private ColorlightServices _colorlightServices;
        private CreateVsnService _createVsnService;
        private FileManagement _fileManagement;


        public DynamicDataService(
            IWebHostEnvironment environment,
            DatabaseAPIServices.ForTravelTime databaseTravelTimeService,
            DatabaseAPIServices.ForParking databaseParkingService,
            DatabaseAPIServices.ForVidBroadcast databaseVideoService,
            DatabaseAPIServices.ForWeather databaseWeatherService,
            ColorlightServices colorlightServices,
            CreateVsnService createVsnService,
            SenaVMSContext context,
            FileManagement fileManagement)
        {
            _hostingEnvironment = environment;
            traveltimeService = databaseTravelTimeService;
            parkingService = databaseParkingService;
            videoService = databaseVideoService;
            weatherService = databaseWeatherService;
            _colorlightServices = colorlightServices;
            _createVsnService = createVsnService;
            _context = context;
            _fileManagement = fileManagement;
        }

        public async Task AutoUpdateRandDynamicData(int id)
        {
            //generate random eta
            Random rndETA1 = new Random();
            int etaint1 = rndETA1.Next(1, 60);  // creates a number between 1 and 60
            string eta1 = etaint1.ToString() + "min";
            Random rndETA2 = new Random();
            int etaint2 = rndETA2.Next(1, 60);  // creates a number between 1 and 60
            string eta2 = etaint2.ToString() + "min";

            //call traveltimeService to update the values into postgresql database 
            traveltimeService.updateTravelTime(id, "1", "mvalley", "Mid Valley",
                DateTime.Now.ToString(), eta1);
            traveltimeService.updateTravelTime(id, "1", "tsquare", "Times Square",
                DateTime.Now.ToString(), eta2);

            //call parkingService to update the values into postgresql database 
            //--------

            //call weatherService to update the values into postgresql database 
            /*Random rand1 = new Random();
            int diceNum1 = rand1.Next(0, 2);
            Random rand2 = new Random();
            int diceNum2 = rand2.Next(0, 2);
            Random rand3 = new Random();
            int diceNum3 = rand3.Next(0, 2);
            string[] weatherStatus = new string[] { "rain", "no rain", "thunderstorm" };

            string morning = weatherStatus[diceNum1];
            string afternoon = weatherStatus[diceNum2];
            string night = weatherStatus[diceNum3];

            weatherService.updateWeather(id, "Selayang", morning, afternoon,
                night, DateTime.Now.ToString());*/


            //delete tempfoldersingle, recreate tempfilersingle, copy from board folder
            var folderPath = UploadsFolder.GetUploadsSubPath(UploadsFolder.TempFolderSingle);
            if (Directory.Exists(folderPath)) Directory.Delete(folderPath, true); //delete old folder
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath); //create new folder

            var sourcePathToCopy = UploadsFolder.GetUploadsSubPath(id.ToString());
            if (Directory.Exists(sourcePathToCopy)) _fileManagement.CopyFiles(sourcePathToCopy, folderPath); //make a copy from board folder

            //call createvsn to re-create the vsn file
            _createVsnService.createVSN(id, false);
            //update physical board
            await _colorlightServices.SendProgramAsync(id, false);
        }
        public async Task AutoUpdateDynamicData(int id)
        {
            //delete tempfoldersingle, recreate tempfilersingle, copy from board folder
            var folderPath = UploadsFolder.GetUploadsSubPath(UploadsFolder.TempFolderSingle);
            if (Directory.Exists(folderPath)) Directory.Delete(folderPath, true); //delete old folder
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath); //create new folder

            var sourcePathToCopy = UploadsFolder.GetUploadsSubPath(id.ToString());
            if (Directory.Exists(sourcePathToCopy)) _fileManagement.CopyFiles(sourcePathToCopy, folderPath); //make a copy from board folder

            //call createvsn to re-create the vsn file
            _createVsnService.createVSN(id, false);
            //update physical board
            await _colorlightServices.SendProgramAsync(id, false);
        }
    }
}